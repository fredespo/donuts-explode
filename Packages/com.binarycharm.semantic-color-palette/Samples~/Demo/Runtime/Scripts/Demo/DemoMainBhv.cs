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

using System.Collections.Generic;

using BinaryCharm.SemanticColorPalette;
using BinaryCharm.SemanticColorPalette.RuntimeManagement;

namespace BinaryCharm.Samples.SemanticColorPalette.Demo {

    public class DemoMainBhv : MonoBehaviour {

        [SerializeField] private SCP_RuntimeManagerUI m_rRuntimeManagerUI;
        [SerializeField] private Button m_rShowRuntimeManagerBtn;
        [SerializeField] private Image m_rRuntimeManagerBtnHighlight;
        [SerializeField] protected Transform m_rProvidersFatherTr;

        #region MonoBehaviour -------------------------------------------------

        private void Awake() {
            s_rInstance = this;
            m_rShowRuntimeManagerBtn.onClick.AddListener(() => m_rRuntimeManagerUI.Show());
        }

        private void Update() {
            m_rShowRuntimeManagerBtn.interactable = !m_rRuntimeManagerUI.IsShown();
            m_rRuntimeManagerBtnHighlight.enabled = !m_rRuntimeManagerUI.IsShown() && s_bAskedToHighlightRuntimeManagerBtn;
        }

        #endregion ------------------------------------------------------------


        #region Quick & Dirty centralized demo panels management --------------

        private static DemoMainBhv s_rInstance;
        private static bool s_bAskedToHighlightRuntimeManagerBtn = false;

        private const string sLINK_TARGET_RuntimeManagerUI = "RuntimeManagerUI";

        public static void handleLinkHovering(string sUrl) {
            s_bAskedToHighlightRuntimeManagerBtn =
                !string.IsNullOrEmpty(sUrl) && (sUrl == sLINK_TARGET_RuntimeManagerUI);
        }

        public static void handleLinkClicked(string sUrl) {
            if (sUrl == sLINK_TARGET_RuntimeManagerUI) {
                s_rInstance.m_rRuntimeManagerUI.Show();
            }
            else {
                Application.OpenURL(sUrl);
            }
        }

        public static void adjustProviders(List<SCP_PaletteProvider> rNeededProviders) {
            foreach (Transform rTr in s_rInstance.m_rProvidersFatherTr) {
                SCP_PaletteProvider rPP = rTr.GetComponent<SCP_PaletteProvider>();
                rPP.enabled = rNeededProviders.Contains(rPP);
            }
        }

        #endregion ------------------------------------------------------------
    }

}
