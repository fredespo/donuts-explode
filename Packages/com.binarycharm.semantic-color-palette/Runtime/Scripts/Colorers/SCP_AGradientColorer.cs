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

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Abstract base class for colorers controlling a @UnityEngine.Gradient
    /// property.
    /// </summary>
    public abstract class SCP_AGradientColorer : SCP_AColorerBase, SCP_IColorer<SCP_ColorId[]> { 

        [SerializeField] internal SCP_ColorId[] m_gradientColors = new SCP_ColorId[0];

        internal abstract Gradient getCurrGradient();
        internal abstract void setCurrGradient(Gradient rGradient);


        #region SCP_IColorer --------------------------------------------------

        /// <inheritdoc/>
        public SCP_ColorId[] GetColorIds() {
            return m_gradientColors;
        }

        /// <inheritdoc/>
        public void SetColorIds(in SCP_ColorId[] colorIds) {
            if (colorIds.Length != getCurrGradient().colorKeys.Length) {
                throw new System.Exception(
                    "The length of the colorIds array must match " +
                    "the number of GradientColorKeys in the current gradient");
            }
            m_gradientColors = colorIds;
            refresh();
        }

        #endregion ------------------------------------------------------------


        #region SCP_AColorerBase ----------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, SCP_IGradientAccessor rGradientAccessor, in SCP_ColorId[] colorIds) {
            Gradient rCurrGradient = rGradientAccessor.GetCurrGradient();
            Gradient rNewGradient = new Gradient();

            int iNumColorKeys = colorIds.Length;
            if (iNumColorKeys != rCurrGradient.colorKeys.Length) return; // colorsIds not properly set

            GradientColorKey[] rNewColorKeys = new GradientColorKey[iNumColorKeys];
            for (int i = 0; i < iNumColorKeys; ++i) {
                SCP_ColorId colorId = colorIds[i];
                Color color = colorId != SCP_ColorId.DO_NOT_APPLY ?
                    rPalette.GetColor(colorId) :
                    rCurrGradient.colorKeys[i].color;
                float fKeyTime = rCurrGradient.colorKeys[i].time;
                rNewColorKeys[i] = new GradientColorKey(color, fKeyTime);
            }

            rNewGradient.SetKeys(rNewColorKeys, rCurrGradient.alphaKeys);
            rGradientAccessor.SetCurrGradient(rNewGradient);
        }

        #endregion ------------------------------------------------------------

    }

}
