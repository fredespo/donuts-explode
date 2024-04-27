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
    public struct SCP_ToggleColorsDef {
        public SCP_SelectableColorsDef selectable;

        // checkmark
        public SCP_ColorId checkmarkColor;
        public SCP_ColorId targetGraphicMulColor;

        // label
        public SCP_ColorId labelTextColor;
    }

    [AddComponentMenu("Semantic Color Palette/Toggle Colorer")]
    [RequireComponent(typeof(Toggle))]
    public class SCP_ToggleColorer : SCP_AColorer<SCP_ToggleColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Toggle rComponent = GetComponent<Toggle>();
            applyPalette(rPalette, rComponent, m_colors);
        }
        
        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, Toggle rToggle, in SCP_ToggleColorsDef colorIds) {
            Selectable rSelectable = rToggle.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.targetGraphic, colorIds.targetGraphicMulColor);
            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.graphic, colorIds.checkmarkColor);

            Text rLabel = rToggle.GetComponentInChildren<Text>();
            if (rLabel != null) {
                SCP_TextColorer.applyPalette(rPalette, rLabel, colorIds.labelTextColor);
            }
        }

    }

}
