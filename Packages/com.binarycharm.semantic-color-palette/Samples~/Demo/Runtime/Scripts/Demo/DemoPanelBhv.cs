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

using System.Collections.Generic;

using BinaryCharm.Common.TMPro;
using BinaryCharm.SemanticColorPalette;

namespace BinaryCharm.Samples.SemanticColorPalette.Demo {

    public class DemoPanelBhv : MonoBehaviour {

        [SerializeField] protected List<SCP_PaletteProvider> m_rNeededProviders = new List<SCP_PaletteProvider>();
        [SerializeField] private TMProLinksInteractionBhv m_rLinksInteraction;


        protected void Awake() {
            if (m_rLinksInteraction == null) return;
            m_rLinksInteraction.LinkClicked += DemoMainBhv.handleLinkClicked;
        }

        private void Update() {
            if (m_rLinksInteraction.IsHoveringLink()) {
                string sTarget = m_rLinksInteraction.GetHoveredLinkTarget();
                DemoMainBhv.handleLinkHovering(sTarget);
            } else {
                DemoMainBhv.handleLinkHovering(null);
            }
        }

        private void OnEnable() {
            DemoMainBhv.adjustProviders(m_rNeededProviders);
        }

    }

}
