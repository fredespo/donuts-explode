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

using System;

using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;

namespace BinaryCharm.Common.TMPro {

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMProLinksInteractionBhv : MonoBehaviour, 
            IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {

        public event Action<int> LinkHoveringStarted;
        public event Action<int> LinkHoveringEnded;
        public event Action<string> LinkClicked;

        private TextMeshProUGUI m_rText;
        private Camera m_rCamera;

        private int m_iCurrentlyHoveredLinkIndex = -1;
        private int m_iCurrentlyClickedLinkIndex = -1;

        public bool IsHoveringLink() {
            return m_iCurrentlyHoveredLinkIndex != -1;
        }

        public int GetHoveredLinkIndex() {
            return m_iCurrentlyHoveredLinkIndex;
        }

        public string GetHoveredLinkTarget() {
            return getLinkTarget(m_iCurrentlyHoveredLinkIndex);
        }

        public bool IsClickingLink() {
            return m_iCurrentlyClickedLinkIndex != -1;
        }

        public int GetClickedLinkIndex() {
            return m_iCurrentlyClickedLinkIndex;
        }

        protected virtual void Awake() {
            m_rText = GetComponent<TextMeshProUGUI>();
            Canvas rCanvas = GetComponentInParent<Canvas>();

            // we need a camera reference to check for mouse intersections calling
            // TMP_TextUtilities.IsIntersectingRectTransform
            m_rCamera = rCanvas.renderMode == RenderMode.ScreenSpaceOverlay ?
                null : rCanvas.worldCamera;
        }

        protected virtual void OnLinkHoveringStarted(int iLinkId) {
            if (LinkHoveringStarted != null) LinkHoveringStarted(iLinkId);
        }

        protected virtual void OnLinkHoveringEnded(int iLinkId) {
            if (LinkHoveringEnded != null) LinkHoveringEnded(iLinkId);
        }

        protected virtual void OnLinkClicked(string sUrl) {
            if (LinkClicked != null) LinkClicked(sUrl);
        }

        private string getLinkTarget(int iLinkIndex) {
            TMP_LinkInfo linkInfo = m_rText.textInfo.linkInfo[iLinkIndex];
            string sUrl = linkInfo.GetLinkID();
            return sUrl;
        }

        void LateUpdate() {

            int iCurrentlyHoveredLinkIndex = TMP_TextUtilities.FindIntersectingLink(m_rText, Input.mousePosition, m_rCamera);
            // iCurrentlyHoveredLinkIndex == -1 when not hovering a link

            if (m_iCurrentlyHoveredLinkIndex != -1 && iCurrentlyHoveredLinkIndex != m_iCurrentlyHoveredLinkIndex) {
                OnLinkHoveringEnded(m_iCurrentlyHoveredLinkIndex);
                m_iCurrentlyHoveredLinkIndex = -1;
            }

            if (iCurrentlyHoveredLinkIndex != -1 && iCurrentlyHoveredLinkIndex != m_iCurrentlyHoveredLinkIndex) {
                m_iCurrentlyHoveredLinkIndex = iCurrentlyHoveredLinkIndex;
                OnLinkHoveringStarted(m_iCurrentlyHoveredLinkIndex);
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            int iLinkIndex = TMP_TextUtilities.FindIntersectingLink(m_rText, Input.mousePosition, m_rCamera);
            if (iLinkIndex == -1) return;

            string sUrl = getLinkTarget(iLinkIndex);
            OnLinkClicked(sUrl);
        }

        public void OnPointerDown(PointerEventData eventData) {
            m_iCurrentlyClickedLinkIndex = TMP_TextUtilities.FindIntersectingLink(m_rText, Input.mousePosition, m_rCamera);
        }

        public void OnPointerUp(PointerEventData eventData) {
            m_iCurrentlyClickedLinkIndex = -1;
        }

    }

}

#endif
