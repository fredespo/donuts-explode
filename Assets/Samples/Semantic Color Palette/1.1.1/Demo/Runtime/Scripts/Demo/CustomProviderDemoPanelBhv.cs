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
using UnityEngine.UI;

using BinaryCharm.Samples.UI;
using BinaryCharm.Samples.SemanticColorPalette.CustomProviders;

namespace BinaryCharm.Samples.SemanticColorPalette.Demo {

    public class CustomProviderDemoPanelBhv : DemoPanelBhv {

        [SerializeField] private Button m_rSwitchBtn;
        [SerializeField] private VarDisplayBhv m_rFadeDurationSlider;
        [SerializeField] private FadingPaletteProvider m_rFadingPaletteProvider;

        void Start() {

            m_rFadeDurationSlider.setup("fade duration", false, 0, 3f, 1f, (float f) => {
                m_rFadingPaletteProvider.SetFadeDuration(f);
            });

            m_rSwitchBtn.onClick.AddListener(() => {
                int iCurrPalette = m_rFadingPaletteProvider.GetActivePaletteIndex();
                m_rFadingPaletteProvider.SetActivePaletteIndex(1 - iCurrPalette);
            });

        }

        private void Update() {
            m_rSwitchBtn.interactable = !m_rFadingPaletteProvider.IsFading();
        }

    }

}
