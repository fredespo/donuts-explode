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

#if UNITY_EDITOR
using UnityEditor;
#endif

using BinaryCharm.SemanticColorPalette;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomProviders {

    [ExecuteAlways]
    public class FadingPaletteProvider : SCP_PaletteProvider {

        [SerializeField] internal float m_fFadeDuration = 1f;

        private int m_iPrevPalette;
        private float m_fPaletteChangeTimestamp;
        private SCP_Palette m_rInterpolationPalette;

        private enum eState {
            non_fading,
            fading
        };
        private eState m_state;

        private SCP_Palette getInterpolationPalette() {
            if (m_rInterpolationPalette == null) {
                m_rInterpolationPalette = SCP_Palette.CreateVariant("dyn_pal_interpolation", base.GetPalette());
            }
            return m_rInterpolationPalette;
            //return m_rInterpolationPalette ??= 
            //    SCP_Palette.CreateVariant("dyn_pal_interpolation", base.GetPalette());
        }

        #region MonoBehaviour -------------------------------------------------

        private void Awake() {
            m_iPrevPalette = GetActivePaletteIndex();
            m_state = eState.non_fading;
        }

        private new void Update() {
            base.Update();

            if (m_state == eState.non_fading) {
                if (m_iPrevPalette != GetActivePaletteIndex() && m_fFadeDuration > 0f) {
                    m_state = eState.fading;
                    m_fPaletteChangeTimestamp = Time.realtimeSinceStartup;
                }
            }
            if (m_state == eState.non_fading) return;

            float fElapsed = Time.realtimeSinceStartup - m_fPaletteChangeTimestamp;
            float fInterpolationLevel = fElapsed / m_fFadeDuration;

            SCP_Palette rPrevPalette = GetPaletteByIndex(m_iPrevPalette);
            SCP_Palette rCurrPalette = GetPaletteByIndex(GetActivePaletteIndex());
            SCP_Palette rInterpPalette = getInterpolationPalette();

            if (rPrevPalette == null || rCurrPalette == null) return;

            int iNumElems = rCurrPalette.GetNumElems();
            for (int i = 0; i < iNumElems; ++i) {
                SCP_ColorId colorId = rPrevPalette.GetColorIdByIndex(i);
                Color srcColor = rPrevPalette.GetColor(colorId);
                Color dstColor = rCurrPalette.GetColor(colorId);
                Color c = Color.Lerp(srcColor, dstColor, fInterpolationLevel);
                rInterpPalette.SetColor(colorId, c);
            }

            if (fInterpolationLevel > 1f) {
                m_iPrevPalette = GetActivePaletteIndex();
                m_state = eState.non_fading;
            }

            m_stateTracker.SetChangedIfDependencyChanged(
                rInterpPalette.GetStateTracker()
            );
        }

#if UNITY_EDITOR
        private void updateInEditMode() {
            if (Application.isPlaying) return;
            EditorApplication.QueuePlayerLoopUpdate();
        }
#endif

        protected new void OnEnable() {
            base.OnEnable();
#if UNITY_EDITOR
            EditorApplication.update += updateInEditMode;
#endif
        }

        protected new void OnDisable() {
            base.OnDisable();
#if UNITY_EDITOR
            EditorApplication.update -= updateInEditMode;
#endif
        }

        protected new void OnValidate() {
            base.OnValidate();
            m_fFadeDuration = Mathf.Clamp(m_fFadeDuration, 0f, float.MaxValue);
        }

        #endregion ------------------------------------------------------------


        #region Public API ----------------------------------------------------

        public void SetFadeDuration(float f) {
            m_fFadeDuration = f;
        }

        public bool IsFading() {
            return m_state == eState.fading;
        }

        public override SCP_Palette GetPalette() {
            return m_state == eState.non_fading ?
                base.GetPalette() :
                getInterpolationPalette();
        }

        #endregion ------------------------------------------------------------

    }

}
