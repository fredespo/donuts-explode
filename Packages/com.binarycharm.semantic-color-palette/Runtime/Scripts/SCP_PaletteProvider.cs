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

using UnityEngine;

using BinaryCharm.Common.StateManagement;
using BinaryCharm.SemanticColorPalette.RuntimeManagement;

namespace BinaryCharm.SemanticColorPalette {

    /// <summary>
    /// One of the core components of the system, acting as broker between
    /// palettes and colorers.
    /// Every colorer gets linked to a
    /// @BinaryCharm.SemanticColorPalette.SCP_PaletteProvider, that handles a 
    /// list of one or more 
    /// @BinaryCharm.SemanticColorPalette.SCP_Palette instances.
    /// Colorers monitor their provider for changes, and the provider themselves
    /// monitor their palettes. So, when a palette color changes, the change
    /// gets propagated to all the providers holding that palette, and then
    /// to all colorers linked to those providers.
    /// 
    /// For convenience, providers have an "active" palette that is the one 
    /// that colorers use by default. Changing active palette also refreshes
    /// all the linked colorers.
    /// </summary>
    [DefaultExecutionOrder(1)]
    [ExecuteAlways]
    public class SCP_PaletteProvider : MonoBehaviour {

        [SerializeField] internal List<SCP_Palette> m_rPalettes = new List<SCP_Palette>() {};
        [SerializeField] internal int m_iActivePaletteIndex = 0;

        private int m_iPrevActivePaletteIndex = -1;
        private int m_iPrevNumPalettes = 0;
        private SCP_Palette m_rPrevActivePalette = null;

        #region State Tracking ------------------------------------------------

        /// <summary>
        /// State tracking struct. Potentially needs to be accessed in custom
        /// providers inheriting from this class.
        /// </summary>
        protected MutableStateTracker m_stateTracker;

        /// <summary>
        /// Returns a reference to the state tracking struct.
        /// </summary>
        /// 
        /// <remarks>
        /// Should never be used in everyday application code.
        /// </remarks>
        /// 
        /// <returns>A reference to the state tracking struct.</returns>
        public ref MutableStateTracker GetStateTracker() {
            return ref m_stateTracker;
        }

        #endregion ------------------------------------------------------------

        #region MonoBehaviour -------------------------------------------------

        /// <summary>
        /// If any of the handled palette has been modified, or if the active
        /// palette selection has been changed, then also the provider is 
        /// considered modified (and this will prompt the linked colorers to
        /// update).
        /// </summary>
        protected void Update() {
#if UNITY_EDITOR
            if (!isConfigurationValid()) {
                if (Application.isPlaying) {
                    Debug.LogError("Palette Provider with invalid configuration!", gameObject);
                }
                return;
            }
#endif

            bool bMustPropagateChange = false;
            if (m_iPrevActivePaletteIndex != m_iActivePaletteIndex) {
                m_iPrevActivePaletteIndex = m_iActivePaletteIndex;
                bMustPropagateChange = true;
            }
            else {
                foreach (SCP_Palette rP in m_rPalettes) {
                    if (rP == null) continue;
                    if (m_stateTracker.IsDependencyChanged(rP.GetStateTracker())) {
                        bMustPropagateChange = true;
                        break;
                    }
                }
            }
            if (m_rPrevActivePalette != m_rPalettes[m_iActivePaletteIndex]) {
                m_rPrevActivePalette = m_rPalettes[m_iActivePaletteIndex];
                bMustPropagateChange = true;
            }
            if (m_iPrevNumPalettes != m_rPalettes.Count) {
                m_iPrevNumPalettes = m_rPalettes.Count;
                bMustPropagateChange = true;
            }
            if (bMustPropagateChange) {
                m_stateTracker.SetChanged();
            }
        }

        protected void OnEnable() {
            SCP_RuntimeManager.addProvider(this);
            SCP_RuntimeManager.enableProvider(this);
        }

        protected void OnDisable() {
            SCP_RuntimeManager.disableProvider(this);
        }

        protected void OnDestroy() {
            SCP_RuntimeManager.remProvider(this);
        }

        protected void OnValidate() {
            // reserved for future usage that won't break child classes
        }

        #endregion ------------------------------------------------------------


        #region Public API ----------------------------------------------------

        /// <summary>
        /// Fetches the number of palettes currently handled by the provider.
        /// </summary>
        /// 
        /// <returns>The number of palettes currently handled.</returns>
        public int GetNumPalettes() {
            return m_rPalettes.Count;
        }

        /// <summary>
        /// Changes the active palette index. It is user resposability to pass
        /// a valid <paramref name="iPaletteIndex"/> (between 0 and 
        /// [GetNumPalettes()](BinaryCharm.SemanticColorPalette.SCP_PaletteProvider.html#BinaryCharm_SemanticColorPalette_SCP_PaletteProvider_GetNumPalettes) - 1)
        /// </summary>
        /// 
        /// <param name="iPaletteIndex">The value to set as active palette
        ///   index.</param>
        public void SetActivePaletteIndex(int iPaletteIndex) {
            m_iActivePaletteIndex = iPaletteIndex;
        }

        /// <summary>
        /// Fetches the active palette index.
        /// </summary>
        /// 
        /// <returns>The active palette index.</returns>
        public int GetActivePaletteIndex() {
            return m_iActivePaletteIndex;
        }

        /// <summary>
        /// Fetches the currently active palette.
        /// </summary>
        /// 
        /// <returns>The currently active palette.</returns>
        public virtual SCP_Palette GetPalette() {
            if (m_iActivePaletteIndex < 0 || m_iActivePaletteIndex >= m_rPalettes.Count) return null;
            return m_rPalettes[m_iActivePaletteIndex];
        }

        /// <summary>
        /// Fetches the palette specified by <paramref name="iPaletteIndex"/>
        /// </summary>
        /// 
        /// <param name="iPaletteIndex">The zero-based index of the palette to
        ///   fetch.</param>
        /// <returns>The requested palette, or null if an out of bounds 
        ///   <paramref name="iPaletteIndex"/> is provided.</returns>
        public SCP_Palette GetPaletteByIndex(int iPaletteIndex) {
            if (iPaletteIndex < 0 || iPaletteIndex >= m_rPalettes.Count) return null;
            return m_rPalettes[iPaletteIndex];
        }

        /// <summary>
        /// Adds a palette to the set of palettes managed by the provider.
        /// If such set is not empty (that is, the PaletteProvider already 
        /// handles one or more palettes), the new palette must be compatible
        /// with the previously added others, or an Exception is thrown and
        /// the palette doesn't get added.
        /// 
        /// Two palettes A and B are compatible when
        /// - A is a main palette and B is a variant of A
        /// - A and B are both variants of the same main palette
        /// </summary>
        /// 
        /// <param name="rPalette">The Palette to be added.</param>
        public void AddPalette(SCP_Palette rPalette) {
            if (m_rPalettes.Count > 0) {
                SCP_Palette rMain = m_rPalettes[0].GetMainPalette();
                bool bSameMainPalette = rPalette.GetMainPalette() == rMain;
                if (!bSameMainPalette) {
                    throw new Exception("AddPalette: palette " + rPalette.GetName() + " is not a variant of " + rMain.Name);
                }
            }
            m_rPalettes.Add(rPalette);
            m_stateTracker.SetChanged();
        }

        /// <summary>
        /// Removes <paramref name="rPalette"/> from the list of handled 
        /// palettes. If <paramref name="rPalette"/> is not in such list,
        /// it throws an exception.
        /// </summary>
        /// 
        /// <remarks>
        /// Removing a palette from the list might invalidate the configuration
        /// of any colorers referring to this palette provider.
        /// </remarks>
        /// 
        /// <param name="rPalette">Reference to the palette to remove.</param>
        public void RemPalette(SCP_Palette rPalette) {
            bool bRemoved = m_rPalettes.Remove(rPalette);
            if (!bRemoved) {
                throw new Exception("RemPalette: palette " + rPalette.GetName() + " is not handled by the provider");
            }
            m_stateTracker.SetChanged();
        }

        /// <summary>
        /// Removes the palette at index <paramref name="iPaletteIndex"/> from
        /// the set of handled palettes. If <paramref name="iPaletteIndex"/> is
        /// not valid (that is: not between 0 and 
        /// [GetNumPalettes()](BinaryCharm.SemanticColorPalette.SCP_PaletteProvider.html#BinaryCharm_SemanticColorPalette_SCP_PaletteProvider_GetNumPalettes) - 1),
        /// throws an exception.
        /// </summary>
        /// 
        /// <remarks>
        /// Removing a palette from the list might invalidate the configuration
        /// of any colorers referring to this palette provider.
        /// </remarks>
        /// 
        /// <param name="iPaletteIndex">Index of the palette to remove.</param>
        public void RemPaletteByIndex(int iPaletteIndex) {
            if (iPaletteIndex < 0 || iPaletteIndex >= m_rPalettes.Count) {
                throw new Exception("RemPaletteByIndex: iPaletteIndex out of range, must be between 0 and GetNumPalettes()-1");
            }
            m_rPalettes.RemoveAt(iPaletteIndex);
            m_stateTracker.SetChanged();
        }

        /// <summary>
        /// Retrieves the current position of <paramref name="rPalette"/> in 
        /// the list of palettes handled by the provider. Note that it's an 
        /// index and not an identifier, and it might change when removing
        /// other palettes.
        /// </summary>
        /// 
        /// <param name="rPalette">The palette to look for.</param>
        /// <returns>The index of <paramref name="rPalette"/> in the list of
        ///   palettes handled by the provider (throws an Exception if the
        ///   passed palette is not in the list).</returns>
        public int GetPaletteIndex(SCP_Palette rPalette) {
            int iPaletteIndex = m_rPalettes.IndexOf(rPalette);
            if (iPaletteIndex == -1) {
                throw new Exception("GetPaletteIndex: palette " + rPalette.GetName() + " is not handled by the provider");
            }
            return iPaletteIndex;
        }

        #endregion ------------------------------------------------------------

        internal bool isConfigurationValid() {
            if (m_rPalettes.Count == 0) return false;
            SCP_Palette rMainPalette = null;
            for (int i = 0; i < m_rPalettes.Count; ++i) {
                SCP_Palette rPalette = m_rPalettes[i];
                if (rPalette == null) return false;

                if (rMainPalette == null) rMainPalette = rPalette.GetMainPalette();
                if (rPalette.GetMainPalette() != rMainPalette) return false;
            }
            return true;
        }

    }

}
