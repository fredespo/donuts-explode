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
using UnityEngine.EventSystems;

namespace BinaryCharm.UI {

    public class DraggableBhv
            : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        private bool m_bIsMouseDown = false;
        private Vector3 m_vStartMousePosition;
        private Vector3 m_vStartPosition;

        public void OnPointerDown(PointerEventData dt) {
            m_bIsMouseDown = true;

            m_vStartPosition = GetComponent<Transform>().position;
            m_vStartMousePosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData dt) {
            m_bIsMouseDown = false;
        }

        void Update() {
            if (m_bIsMouseDown) {
                Vector3 vCurrPos = Input.mousePosition;
                Vector3 vPosDelta = vCurrPos - m_vStartMousePosition;
                Vector3 vNextPos = m_vStartPosition + vPosDelta;
                GetComponent<Transform>().position = vNextPos;
            }
        }

    }

}