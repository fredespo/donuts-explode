using UnityEngine;

using BinaryCharm.Common.StateManagement;
using BinaryCharm.SemanticColorPalette.RuntimeManagement;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Abstract class implementing the core colorer logic. All colorers
    /// must inherit (directly or indirectly) from this class.
    /// 
    /// Excluding very particular circumstances (for example: see
    /// @BinaryCharm.SemanticColorPalette.Colorers.TMPro.SCP_TMP_RichTextColorer
    /// ), a custom, user-defined colorer should not inherit directly from this
    /// class, but should instead extend one of its children, accordings to this
    /// selection logic:
    /// - @BinaryCharm.SemanticColorPalette.Colorers.SCP_AColorer: there's a 
    ///       single color to apply
    /// - @BinaryCharm.SemanticColorPalette.Colorers.SCP_AColorer`1: there's 
    ///       multiple colors to apply, and their identifiers are wrapped in a
    ///       struct of type `T` 
    /// - @BinaryCharm.SemanticColorPalette.Colorers.SCP_AMaterialColorer: 
    ///       there's some colors to apply to a Material, and their identifiers 
    ///       are hold in a SCP_MaterialColorDef array automatically generated 
    ///       according to the shader characteristics
    /// - @BinaryCharm.SemanticColorPalette.Colorers.SCP_AMaterialColorer`1:
    ///       there's some colors to apply to a @UnityEngine.Material of a 
    ///       specific type (with specific shader color properties), and their
    ///       identifiers are wrapped in a struct of type `T`
    /// - @BinaryCharm.SemanticColorPalette.Colorers.SCP_AGradientColorer: 
    ///       there's a @UnityEngine.Gradient property that we want to
    ///       control through palette color identifiers
    /// </summary>
    [DefaultExecutionOrder(2)]
    [ExecuteAlways]
    public abstract class SCP_AColorerBase : MonoBehaviour {

        [SerializeField] internal SCP_PaletteProvider m_rPaletteProvider;
        [SerializeField] internal bool m_bIsActivePaletteIndexOverridden = false;
        [SerializeField] internal int m_iActivePaletteIndexOverride;

        private MutableStateTracker m_stateTracker;

        #region MonoBehaviour -------------------------------------------------

        /// <summary>
        /// If the configured palette provider state has been updated, the 
        /// colorer updates consequently (applying provided colors).
        /// </summary>
        private void Update() {
#if UNITY_EDITOR
            if (!isConfigurationValid()) {
                if (Application.isPlaying) {
                    Debug.LogError("Colorer with no valid Palette Provider configured!", gameObject);
                }
                return;
            }
#endif

            if (m_stateTracker.IsDependencyChanged(m_rPaletteProvider.GetStateTracker())) {
                refresh();
            }
        }

        private void OnValidate() {
            refresh();
        }

        private void OnEnable() {
            SCP_RuntimeManager.addColorer(this);
            refresh();
        }

        private void OnDestroy() {
            SCP_RuntimeManager.remColorer(this);
        }

        #endregion ------------------------------------------------------------

        #region Public API ----------------------------------------------------

        /// <summary>
        /// Sets the palette provider, which will act as source for the palette
        /// colors applied by the colorer.
        /// If the optional <paramref name="iActivePaletteIndexOverride"/> 
        /// parameter is used, the colorer will always use the palette at the 
        /// specified index. Otherwise, it will use the "active" palette of the
        /// palette provider (you can change the active palette through the
        /// palette provider public API).
        /// </summary>
        /// 
        /// <param name="rPaletteProvider">A reference to a palette provider.</param>
        /// <param name="iActivePaletteIndexOverride">An optional index that 
        /// overrides the "active" palette setting of the provider and forces
        /// to use a specific palette.</param>
        public void SetPaletteProvider(SCP_PaletteProvider rPaletteProvider, int? iActivePaletteIndexOverride = null) {
            m_rPaletteProvider = rPaletteProvider;
            m_bIsActivePaletteIndexOverridden = iActivePaletteIndexOverride.HasValue;
            if (m_bIsActivePaletteIndexOverridden) {
                m_iActivePaletteIndexOverride = iActivePaletteIndexOverride.Value;
            }
            refresh();
        }

        #endregion ------------------------------------------------------------

        /// <summary>
        /// Indicates a state update, fetches the appropriate palette from the
        /// configured provider, and applies such palette colors, effectively
        /// updating the colors of the element handled by the colorer.
        /// </summary>
        protected void refresh() {
            m_stateTracker.SetChanged();

            if (m_rPaletteProvider == null) return;

            SCP_IPaletteCore rPalette = m_bIsActivePaletteIndexOverridden ?
                m_rPaletteProvider.GetPaletteByIndex(m_iActivePaletteIndexOverride) :
                m_rPaletteProvider.GetPalette();
            
            if (rPalette == null) return;
            
            applyPalette(rPalette);
        }

        /// <summary>
        /// Updates the color(s) of element handled by the colorer according to
        /// <paramref name="rPalette"/>.
        /// </summary>
        /// 
        /// <param name="rPalette">The palette that will act as colors source.
        /// </param>
        protected abstract void applyPalette(SCP_IPaletteCore rPalette);

        internal bool isConfigurationValid() {
            if (m_rPaletteProvider == null) return false;
            return m_rPaletteProvider.isConfigurationValid();
        }

    }

}
