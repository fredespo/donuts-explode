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

#if TEXTMESHPRO_PRESENT 

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using BinaryCharm.Common.Extensions;

namespace BinaryCharm.UI {

    public class MinimizableWindowBhv : MonoBehaviour {

        [SerializeField] private Button m_rMinimizeBtn;
        [SerializeField] private Button m_rMaximizeBtn;
        [SerializeField] private Button m_rCloseBtn;

        [SerializeField] private RectTransform m_rMainRT;
        [SerializeField] private RectTransform m_rWindowBarRT;
        [SerializeField] private RectTransform m_rWindowContentRT;

        [SerializeField] private TMP_Text m_rWindowTitle;

        public enum eWinState {
            maximized,
            minimized
        };

        private eWinState m_winState;
        private float m_fMinimizedHeight;
        private float m_fMaximizedHeight;

        private bool m_bWinVisibility;

        private void Awake() {
            float fBorderHeight = Mathf.Abs(m_rWindowBarRT.anchoredPosition.y);

            float fBarHeight = m_rWindowBarRT.rect.height;
            float fContentHeight = m_rWindowContentRT.rect.height;

            m_fMinimizedHeight = fBarHeight + (2 * fBorderHeight);
            m_fMaximizedHeight = m_fMinimizedHeight + fContentHeight + fBorderHeight;

            m_rMinimizeBtn.onClick.AddListener(() => setWinState(eWinState.minimized));
            m_rMaximizeBtn.onClick.AddListener(() => setWinState(eWinState.maximized));
            m_rCloseBtn.onClick.AddListener(() => setWinVisible(false));

            setWinState(eWinState.maximized);
            setWinVisible(false);
        }

        public void setWinTitle(string sTitle) {
            m_rWindowTitle.text = sTitle;
        }

        public void setWinVisible(bool bVisible) {
            m_bWinVisibility = bVisible;
            m_rMainRT.gameObject.SetActive(bVisible);
        }

        public bool isWinVisible() {
            return m_bWinVisibility;
        }

        public void setWinState(eWinState state) {
            m_winState = state;
            
            bool bIsContentShown = m_winState == eWinState.maximized;
            float fWinHeight = bIsContentShown ? m_fMaximizedHeight : m_fMinimizedHeight;

            m_rMainRT.sizeDelta = m_rMainRT.sizeDelta.withY(fWinHeight);
            m_rWindowContentRT.gameObject.SetActive(bIsContentShown);
            m_rMinimizeBtn.gameObject.SetActive(bIsContentShown);
            m_rMaximizeBtn.gameObject.SetActive(!bIsContentShown);
        }

        public eWinState getWinState() {
            return m_winState;
        }

    }

}

#endif