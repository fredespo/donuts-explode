/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2022 Binary Charm - All Rights Reserved
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@                        @@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@                           @@@@@@@@@@@@@@@@@@
             @@@@@@@@@   @@@@@@@@@@@  @@@@@        @@@@@@@@@@@@@@@
             @@@@@@@@@@@  @@@@@@@@@  @@@@@@@@@@       (@@@@@@@@@@@
             @@@@@@@@@@@@  @@@@@@@@& @@@@@@@@@@ @@@@     @@@@@@@@@
             @@@@@@@@@@@@@ @@@@@@@@@@ *@@@@@@@ @@@@@@@@@*   @@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*/

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using BinaryCharm.Common.StateManagement;

namespace BinaryCharm.SemanticColorPalette {

    /// <summary>
    /// Class representing a color palette suitable for usage in the system.
    /// A @BinaryCharm.SemanticColorPalette.SCP_Palette is a named set of color
    /// definitions that can be created at runtime through code and exist only
    /// in memory, but in most cases it will be created through the Unity 
    /// inspector and stored as a asset file by the Unity serialization system.
    /// </summary>
    [CreateAssetMenu(fileName = "pal_new", menuName = "Semantic Color Palette/Palette", order = 109)]
    [Serializable]
    public class SCP_Palette : ScriptableObject, SCP_IPaletteCore {

        /// <summary>
        /// A readable name for recognizing the palette. Names don't need to be
        /// unique and, for istances created in the Unity Editor, are 
        /// derived from the asset filename.
        /// </summary>
        [SerializeField] internal string Name;
        [SerializeField] internal Element[] Elements = new Element[] { };
        [SerializeField] internal eType Type;
        [SerializeField] internal SCP_Palette MainPalette;


        #region Type Definitions ----------------------------------------------

        /// <summary>
        /// Distinguishes main palettes from palette variants.
        /// </summary>
        public enum eType {
            /// <summary>
            /// Identifies a main palette, of which many variants can be defined
            /// </summary>
            main,
            /// <summary>
            /// identifies a palette which is a variant of a main palette,
            /// assigning different colors to the color identifiers defined by
            /// the main palette.
            /// </summary>
            variant
        };


        [Serializable]
        internal class Element {

            public int Id;
            public string Name;
            public Color Color;

            public Element(int id, string name, Color color) {
                Id = id;
                Name = name;
                Color = color;
            }

        }

        #endregion ------------------------------------------------------------


        #region State Tracking ------------------------------------------------

        private MutableStateTracker m_stateTracker;

        /// <summary>
        /// Returns a reference to the state tracking struct.
        /// </summary>
        /// 
        /// <remarks>
        /// Should never be used in normal application code.
        /// Only public to allow monitoring by custom extensions of 
        /// @BinaryCharm.SemanticColorPalette.SCP_PaletteProvider
        /// with no performance penalties.
        /// </remarks>
        /// 
        /// <returns>A reference to the state tracking struct.</returns>
        public ref MutableStateTracker GetStateTracker() {
            return ref m_stateTracker;
        }

        #endregion ------------------------------------------------------------


        #region ScriptableObject ----------------------------------------------

        private void OnValidate() {
            rebuildCache();
        }

        #endregion ------------------------------------------------------------


        #region Factory Methods -----------------------------------------------

        /// <summary>
        /// Empty palette creation, only useful in the Unity Editor, when
        /// defining a palette through the inspector.
        /// </summary>
        /// <returns>An empty palette instance.</returns>
        internal static SCP_Palette CreateEmpty() {
            SCP_Palette rPalette = ScriptableObject.CreateInstance<SCP_Palette>();
            rPalette.Type = eType.main;
            rPalette.rebuildCache();
            return rPalette;
        }

        /// <summary>
        /// Factory method for building a 
        /// @BinaryCharm.SemanticColorPalette.SCP_Palette providing a string to 
        /// use as palette name and, most importantly, a 
        /// Dictionary&lt;string, Color&gt; holding the
        /// (colorName, colorValue) pairs that will characterize the created
        /// palette.
        /// The use of a Dictionary guarantees that the color names are
        /// unique, which makes them suitable to be used as readable identifiers.
        /// Additionally, each color name will be associated to a
        /// @BinaryCharm.SemanticColorPalette.SCP_ColorId that will need to be 
        /// retrieved by calling 
        /// [GetColorIdByName](xref:BinaryCharm.SemanticColorPalette.SCP_Palette#BinaryCharm_SemanticColorPalette_SCP_Palette_GetColorIdByName_System_String_)
        /// on the instantiated palette, and should be the preferred way to
        /// access a color definition.
        /// </summary>
        /// 
        /// <remarks>
        /// Calling this method is the proper way to create new "main" palettes
        /// through code.
        /// The number of color entries and the color names cannot be altered
        /// after creation, while it is possible to change the color values
        /// (by calling
        /// [SetColor](BinaryCharm.SemanticColorPalette.SCP_Palette.html#BinaryCharm_SemanticColorPalette_SCP_Palette_SetColor_BinaryCharm_SemanticColorPalette_SCP_ColorId_Color_)
        /// ).
        /// 
        /// </remarks>
        /// <param name="sName">The name of the palette to be created.</param>
        /// <param name="rColorDefs">The (colorName, colorValue) pairs that 
        ///   will define the palette entries</param>
        /// <returns>A palette instance built according to the parameters.</returns>
        public static SCP_Palette CreateMain(string sName, Dictionary<string, Color> rColorDefs) {
            SCP_Palette rPalette = ScriptableObject.CreateInstance<SCP_Palette>();
            rPalette.Type = eType.main;

            if (string.IsNullOrEmpty(sName)) {
                throw new Exception("You must specify a palette name.");
            }
            if (rColorDefs == null || rColorDefs.Count == 0) {
                throw new Exception("You must specify some name -> color entries.");
            }

            rPalette.Name = sName;
            rPalette.Elements = new Element[rColorDefs.Count()];

            int i = 0;
            foreach (var kv in rColorDefs) {
                rPalette.Elements[i] = new Element(i, kv.Key, kv.Value);
                ++i;
            }

            rPalette.rebuildCache();
            return rPalette;
        }

        /// <summary>
        /// Factory method to create a palette variant of 
        /// <paramref name="rMainPalette"/> named <paramref name="sName"/>.
        /// The instantiated palette will be a copy of the main palette, to be
        /// subsequently modified by calling
        /// [SetColor](BinaryCharm.SemanticColorPalette.SCP_Palette.html#BinaryCharm_SemanticColorPalette_SCP_Palette_SetColor_BinaryCharm_SemanticColorPalette_SCP_ColorId_Color_)
        /// (for a single entry accessed by color id) or the utility method 
        /// [SCP_Utils.UpdateColorDefs](BinaryCharm.SemanticColorPalette.Utils.SCP_Utils.html#BinaryCharm_SemanticColorPalette_Utils_SCP_Utils_UpdateColorDefs_BinaryCharm_SemanticColorPalette_SCP_Palette_Dictionary_System_String_Color__)
        /// (for multiple entries accessed by color name).
        /// </summary>
        /// 
        /// <param name="sName">The name of the palette variant to be created.</param>
        /// <param name="rMainPalette">The "main" palette of which the newly
        ///   created palette will be variant.</param>
        /// <returns>A palette variant instance initialized as a copy of 
        ///   <paramref name="rMainPalette"/></returns>
        public static SCP_Palette CreateVariant(string sName, SCP_Palette rMainPalette) {
            SCP_Palette rPalette = Instantiate(rMainPalette);
            rPalette.MainPalette = rMainPalette;
            rPalette.Name = sName;
            rPalette.Type = eType.variant;
            rPalette.rebuildCache();
            return rPalette;
        }

        /// <summary>
        /// Factory method building a Palette from a JSON string using the
        /// default Unity deserialization of ScriptableObject data.
        /// </summary>
        /// 
        /// <remarks>
        /// Warning: there is no integrity check on the input string, it is user
        /// responsibility to pass valid data.
        /// In most common scenarios where a palette needs to be 
        /// stored/retrieved through an external JSON file, consider using the
        /// utility methods
        /// [SCP_Utils.SavePalette](BinaryCharm.SemanticColorPalette.Utils.SCP_Utils.html#BinaryCharm_SemanticColorPalette_Utils_SCP_Utils_SavePalette_BinaryCharm_SemanticColorPalette_SCP_Palette_System_String_)
        /// and
        /// [SCP_Utils.LoadPalette](BinaryCharm.SemanticColorPalette.Utils.SCP_Utils.html#BinaryCharm_SemanticColorPalette_Utils_SCP_Utils_LoadPalette_System_String_)
        /// .
        /// </remarks>
        /// 
        /// <param name="sJsonData">A JSON string representing a palette 
        /// instance.</param>
        /// <returns>A palette instance built deserializing the 
        /// <paramref name="sJsonData"/> input.</returns>
        public static SCP_Palette CreateByJsonData(string sJsonData) {
            SCP_Palette rPalette = ScriptableObject.CreateInstance<SCP_Palette>();
            JsonUtility.FromJsonOverwrite(sJsonData, rPalette);
            rPalette.rebuildCache();
            return rPalette;
        }

        #endregion ------------------------------------------------------------


        #region Public API ----------------------------------------------------

        /// <summary>
        /// Retrieves a reference to the "main" palette of the instance.
        /// If the palette is not a palette variant, returns the palette
        /// itself.
        /// </summary>
        /// <returns>The "main" palette (if called on a palette variant)
        /// or the palette itself (if called on a main palette)</returns>
        public SCP_Palette GetMainPalette() {
            SCP_Palette rMainPalette = this;
            while (rMainPalette.Type == eType.variant && rMainPalette.MainPalette != null) {
                rMainPalette = rMainPalette.MainPalette;
            }
            return rMainPalette;
        }

        /// <summary>
        /// Fetches the name of the palette. For palettes defined through the
        /// Unity Editor, it will match the asset filename.
        /// </summary>
        /// <returns>The mnemonic name of the palette instance.</returns>

        public string GetName() {
            return Name;
        }

        /// <summary>
        /// Fetches the number of color entries in the palette.
        /// </summary>
        /// <returns>The number of color entries in the palette.</returns>

        public int GetNumElems() {
            return Elements == null ? 0 : Elements.Length;
        }

        // these should not be needed and might break stuff
        // let's comment them out for now...
        //
        //public void SetAsVariantOf(SCP_Palette rMainPalette) {
        //    Type = eType.variant;
        //    MainPalette = rMainPalette;
        //}
        //
        //public void SetAsMain() {
        //    Type = eType.main;
        //    MainPalette = null;
        //}

        #endregion ------------------------------------------------------------


        #region Index-based Data Access (read only) ---------------------------

        /// <summary>
        /// Fetches the color name of the i-th palette element.
        /// </summary>
        /// 
        /// <remarks>
        /// <paramref name="iIndex"/> is expected to a be a valid value, 
        /// between 0 and 
        /// [GetNumElems()](BinaryCharm.SemanticColorPalette.SCP_Palette.html#BinaryCharm_SemanticColorPalette_SCP_Palette_GetNumElems) - 1.
        /// </remarks>
        /// 
        /// <param name="iIndex">index of the element name to retrieve</param>
        /// <returns>The color name of the i-th palette element.</returns>
        public string GetColorNameByIndex(int iIndex) {
            return Elements[iIndex].Name;
        }

        /// <summary>
        /// Fetches the color id of the i-th palette element.
        /// </summary>
        /// 
        /// <remarks>
        /// <paramref name="iIndex"/> is expected to a be a valid value, 
        /// between 0 and 
        /// [GetNumElems()](BinaryCharm.SemanticColorPalette.SCP_Palette.html#BinaryCharm_SemanticColorPalette_SCP_Palette_GetNumElems) - 1.
        /// </remarks>
        /// 
        /// <param name="iIndex">index of the element id to retrieve.</param>
        /// <returns>The color id of the i-th palette element.</returns>
        public SCP_ColorId GetColorIdByIndex(int iIndex) {
            return Elements[iIndex].Id + 1;
        }

        /// <summary>
        /// Fetches the color value of the i-th palette element.
        /// </summary>
        /// 
        /// <remarks>
        /// <paramref name="iIndex"/> is expected to a be a valid value, 
        /// between 0 and 
        /// [GetNumElems()](BinaryCharm.SemanticColorPalette.SCP_Palette.html#BinaryCharm_SemanticColorPalette_SCP_Palette_GetNumElems) - 1.
        /// </remarks>
        /// 
        /// <param name="iIndex">index of the element color to retrieve.</param>
        /// <returns>The color value of the i-th palette element.</returns>
        public Color GetColorByIndex(int iIndex) {
            return Elements[iIndex].Color;
        }

        #endregion ------------------------------------------------------------


        #region Fast Id-based Data Access -------------------------------------

        private PaletteRuntimeCache m_rRuntimePalette;

        /// <summary>
        /// Builds a representation of the palette suited to fast access by the
        /// colorers. Only needed when a palette is created or when it is 
        /// modified through the inspector (entries added/removed, string 
        /// identifiers changed).
        /// </summary>
        private void rebuildCache() {
            m_rRuntimePalette = new PaletteRuntimeCache(this);// new SCP_RuntimePalette(this);
            m_stateTracker.SetChanged();
        }

        private PaletteRuntimeCache getCache() {
            if (m_rRuntimePalette == null) {
                m_rRuntimePalette = new PaletteRuntimeCache(this);
            }
            return m_rRuntimePalette;
            //return m_rRuntimePalette ??= new PaletteRuntimeCache(this);
        }

        /// <summary>
        /// Retrieves the color id associated to a color name, useful especially
        /// when using the system through scripting (to retrieve color ids after
        /// creating a new palette instance).
        /// </summary>
        /// 
        /// <param name="sName">Color name of which we want the associated color id.</param>
        /// <returns>The color id associated to <paramref name="sName"/></returns>
        public SCP_ColorId GetColorIdByName(string sName) {
            return getCache().getColorIdByName(sName);
        }

        /// <summary>
        /// Retrieves the color value associated to a color id, caring about
        /// performances. It's the method used by almost all 
        /// colorers to retrieve colors.
        /// </summary>
        /// 
        /// <param name="colorId">Identifier of the color to retrieve.</param>
        /// <returns>The color associated to <paramref name="colorId"/></returns>
        public Color GetColor(SCP_ColorId colorId) {
            return getCache().getColor(colorId);
        }

        /// <summary>
        /// Sets the color value associated to a color id, caring about 
        /// performances.
        /// </summary>
        /// 
        /// <param name="colorId">The identifier of the color value we want 
        ///   to change.</param>
        /// <param name="color">The color value we want to set.</param>
        public void SetColor(SCP_ColorId colorId, Color color) {
            PaletteRuntimeCache rCache = getCache();
            int iIndex = rCache.getColorIndexByColorId(colorId);
            Elements[iIndex].Color = color;
            rCache.setColor(colorId, color);

            m_stateTracker.SetChanged();
        }

        #endregion ------------------------------------------------------------


        #region Internal Use Utils --------------------------------------------

        internal int getMaxId() {
            int iMax = -1; // int.MinValue;
            foreach (Element rPE in Elements) {
                int iColorId = rPE.Id;
                if (rPE.Id > iMax) {
                    iMax = iColorId;
                }
            }
            return iMax;
        }

        #endregion ------------------------------------------------------------


        #region PaletteRuntimeCache private utility class ---------------------

        private class PaletteRuntimeCache {
            private readonly Dictionary<string, SCP_ColorId> m_rNameToIdMap;
            private readonly int[] m_rIdToIndexMap;
            private Color[] m_rIdToColorMap;

            public PaletteRuntimeCache(SCP_Palette rPalette) {
                int iNumElems = rPalette.GetNumElems();
                m_rNameToIdMap = new Dictionary<string, SCP_ColorId>(iNumElems);
                
                int iMaxId = rPalette.getMaxId() + 2;
                m_rIdToColorMap = new Color[iMaxId];
                m_rIdToIndexMap = new int[iMaxId];

                for (int iIndex = 0; iIndex < iNumElems; ++iIndex) {
                    Element rPE = rPalette.Elements[iIndex];
                    SCP_ColorId colorId = rPE.Id + 1;

                    m_rNameToIdMap[rPE.Name] = colorId;
                    m_rIdToColorMap[colorId] = rPE.Color;
                    m_rIdToIndexMap[colorId] = iIndex;
                }
            }

            public void setColor(SCP_ColorId colorId, Color color) {
                if (colorId < 0 || colorId > m_rIdToColorMap.Length - 1) throw new Exception("invalid colorId");
                m_rIdToColorMap[colorId] = color;
            }

            public Color getColor(SCP_ColorId colorId) {
                if (colorId < 0 || colorId > m_rIdToColorMap.Length - 1) return Color.magenta;
                return m_rIdToColorMap[colorId];
            }

            public SCP_ColorId getColorIdByName(string sColorName) {
                SCP_ColorId colorId;
                if (m_rNameToIdMap.TryGetValue(sColorName, out colorId)) {
                    return colorId;
                }
                return SCP_ColorId.INVALID;
            }

            public int getColorIndexByColorId(SCP_ColorId colorId) {
                if (colorId < 0 || colorId > m_rIdToIndexMap.Length -1) return 0;
                return m_rIdToIndexMap[colorId];
            }
        }

        #endregion ------------------------------------------------------------

    }

}
