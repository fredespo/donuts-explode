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

using System;

using TMPro;

using UnityEngine;

using BinaryCharm.Common.Extensions;
using BinaryCharm.Common.TMPro;
using BinaryCharm.SemanticColorPalette;
using BinaryCharm.SemanticColorPalette.Colorers;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomColorers {

    [Serializable]
    public struct TMProLinksColorsDef {
        public SCP_ColorId linkColor;
        public SCP_ColorId hoveringColor;
        public SCP_ColorId clickingColor;
    }

    [RequireComponent(typeof(TMProLinksInteractionBhv), typeof(TextMeshProUGUI))]
    public class TMProLinksColorer : SCP_AColorer<TMProLinksColorsDef> {

        private int m_iPrevHoveredLinkIndex = -1;
        private int m_iPrevClickedLinkIndex = -1;

        private void LateUpdate() {

            TMProLinksInteractionBhv rLinkProcessingBhv = GetComponent<TMProLinksInteractionBhv>();

            int iClickedLinkIndex = rLinkProcessingBhv.GetClickedLinkIndex();
            int iHoveredLinkIndex = rLinkProcessingBhv.GetHoveredLinkIndex();

            if (iClickedLinkIndex != m_iPrevClickedLinkIndex || iHoveredLinkIndex != m_iPrevHoveredLinkIndex) {
                m_iPrevClickedLinkIndex = iClickedLinkIndex;
                m_iPrevHoveredLinkIndex = iHoveredLinkIndex;
                refresh();
            }

        }

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TMProLinksColorsDef colorIds = GetColorIds();

            bool bMustApplyClickingColor = colorIds.clickingColor != SCP_ColorId.DO_NOT_APPLY;
            bool bMustApplyHoveringColor = colorIds.hoveringColor != SCP_ColorId.DO_NOT_APPLY;
            bool bMustApplyLinkColor = colorIds.linkColor != SCP_ColorId.DO_NOT_APPLY;
            
            Color linkColor = bMustApplyClickingColor ? 
                rPalette.GetColor(colorIds.linkColor) : Color.magenta;
            Color clickingColor = bMustApplyClickingColor ?
                rPalette.GetColor(colorIds.clickingColor) : Color.magenta;
            Color hoveringColor = bMustApplyHoveringColor ?
                rPalette.GetColor(colorIds.hoveringColor) : Color.magenta;

            TMProLinksInteractionBhv rLinkProcessingBhv = GetComponent<TMProLinksInteractionBhv>();
            int iHoveredLinkIndex = rLinkProcessingBhv.GetHoveredLinkIndex();
            int iClickedLinkIndex = rLinkProcessingBhv.GetClickedLinkIndex();

            // applyPalette might happen before OnEnabled
            TextMeshProUGUI rText = GetComponent<TextMeshProUGUI>(); 

            // prevent infinite recursion
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);

            rText.ForceMeshUpdate();
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);

            int iNumLinks = rText.textInfo == null ? 0 : rText.textInfo.linkCount;

            for (int i = 0; i < iNumLinks; ++i) {
                Color c;
                bool bApplyColor = true;

                if (i == iClickedLinkIndex && bMustApplyClickingColor) {
                    c = clickingColor;
                } else if (i == iHoveredLinkIndex && bMustApplyHoveringColor) {
                    c = hoveringColor;
                } else if (bMustApplyLinkColor) {
                    c = linkColor;
                } else {
                    bApplyColor = false;
                    c = Color.magenta; // make the compiler happy
                }

                if (bApplyColor) {
                    rText.SetLinkColor(i, c);
                }
            }
        }

        private TextMeshProUGUI m_rTxt;
        void OnEnable() {
            m_rTxt = GetComponent<TextMeshProUGUI>();
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable() {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }

        void ON_TEXT_CHANGED(UnityEngine.Object obj) {
            if (obj == m_rTxt) {
                refresh();
            }

            //try {
            //    TextMeshProUGUI rTxt = GetComponent<TextMeshProUGUI>();
            //    if (rTxt == obj) refresh();
            //} catch (Exception rEx) {
            //    Debug.Log("invalid", gameObject);
            //    //reference might be invalid
            //}


            //if (rTxt != null && obj == rTxt) {
            //    refresh();
            //}

            //if (obj == GetComponent<TextMeshProUGUI>()) {
            //    refresh();
            //}
        }

    }

}
