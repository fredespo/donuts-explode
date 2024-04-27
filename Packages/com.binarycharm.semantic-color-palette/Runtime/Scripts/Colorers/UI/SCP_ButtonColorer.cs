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

using UnityEngine;
using UnityEngine.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers.UI {

    [Serializable]
    public struct SCP_ButtonColorsDef {
        public SCP_SelectableColorsDef selectable;
        public SCP_ColorId labelTextColor;
        public SCP_ColorId targetGraphicMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Button Colorer")]
    [RequireComponent(typeof(Button))]
    public class SCP_ButtonColorer : SCP_AColorer<SCP_ButtonColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Button rBtn = GetComponent<Button>();
            applyPalette(rPalette, rBtn, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Button rButton, in SCP_ButtonColorsDef def) {
            Selectable rSelectable = rButton.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, def.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rButton.targetGraphic, def.targetGraphicMulColor);

            Text rLabel = rButton.GetComponentInChildren<Text>();
            if (rLabel != null) {
                SCP_TextColorer.applyPalette(rPalette, rLabel, def.labelTextColor);
            }
        }

    }

}
