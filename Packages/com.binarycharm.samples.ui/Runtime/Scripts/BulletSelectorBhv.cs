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

using System;

using UnityEngine;
using UnityEngine.UI;

namespace BinaryCharm.Samples.UI {

    [RequireComponent(typeof(ToggleGroup))]
    public class BulletSelectorBhv : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField]
        private GameObject m_rBulletPrefab;
        #pragma warning restore 649

        #pragma warning disable 67
        public event Action<int> onBulletSelected;
        #pragma warning restore 67

        private Toggle[] m_rToggles;

        public void setup(int iNumBullets, 
                Action<int> onBulletSelectedAction) {

            ToggleGroup rToggleGroup = GetComponent<ToggleGroup>();
            m_rToggles = new Toggle[iNumBullets];

            for (int i = 0; i < iNumBullets; ++i) {
                GameObject rBullet = Instantiate<GameObject>(m_rBulletPrefab);
                rBullet.transform.SetParent(transform, false);
                Toggle rToggle = rBullet.GetComponentInChildren<Toggle>();
                rToggle.group = rToggleGroup;

                int iBulletId = i;
                rToggle.onValueChanged.AddListener((bool b) => {
                    if (onBulletSelectedAction != null) {
                        onBulletSelectedAction(iBulletId);
                    }
                });

                m_rToggles[i] = rToggle;
            }

            rToggleGroup.SetAllTogglesOff();
        }

        public void selectBullet(int iId) {
            m_rToggles[iId].isOn = true;
        }
    }
}