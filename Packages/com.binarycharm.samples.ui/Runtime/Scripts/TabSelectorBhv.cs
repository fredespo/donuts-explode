/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2020 Binary Charm - All Rights Reserved
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

namespace BinaryCharm.Samples.UI
{
    public class TabSelectorBhv : MonoBehaviour
    {
        #pragma warning disable 649
        [System.Serializable]
        struct BtnAndPanel
        {
            public string m_sLabel;
            public TabButtonBhv m_rTabBtn;
            public GameObject m_rPanelGO;
        }

        [SerializeField]
        private BtnAndPanel[] m_rTabs;

        [SerializeField]
        private Transform m_rBack;

        [SerializeField]
        private Transform m_rFront;
        #pragma warning restore 0649

        void Start() {
            foreach (BtnAndPanel rTab in m_rTabs) {
                rTab.m_rTabBtn.set(rTab.m_sLabel, () => {
                    setCurrTab(rTab.m_rPanelGO);
                });
            }
            setCurrTab(m_rTabs[0].m_rPanelGO);
        }

        private void setCurrTab(GameObject rGO) {
            //foreach (BtnAndPanel rTab in m_rTabs) {
            //    bool bActivePanel = rTab.m_rPanelGO == rGO;
            //    rTab.m_rTabBtn.gameObject.transform.SetParent(
            //        bActivePanel ? m_rFront : m_rBack, false);
            //    rTab.m_rPanelGO.SetActive(bActivePanel);
            //}

            // deactivate first, then activate:
            BtnAndPanel toActivate = m_rTabs[0];
            foreach (BtnAndPanel rTab in m_rTabs) {
                if (rTab.m_rPanelGO == rGO) {
                    toActivate = rTab;
                    continue;
                }
                rTab.m_rTabBtn.gameObject.transform.SetParent(
                    m_rBack, false);
                rTab.m_rPanelGO.SetActive(false);
            }
            toActivate.m_rTabBtn.gameObject.transform.SetParent(m_rFront, false);
            toActivate.m_rPanelGO.SetActive(true);
        }
    }
}