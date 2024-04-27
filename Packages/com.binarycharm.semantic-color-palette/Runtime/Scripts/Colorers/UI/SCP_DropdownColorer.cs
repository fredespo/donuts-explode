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
    public struct SCP_DropdownColorsDef {
        public SCP_SelectableColorsDef selectable;

        // label
        public SCP_ColorId captionTextColor;
        public SCP_ColorId itemTextColor;

        // targetGraphics
        public SCP_ColorId targetGraphicMulColor;

        // item
        // public SCP_ToggleColorsDef itemToggle; // no: handles labelTextColor
        public SCP_SelectableColorsDef itemSelectable;
        public SCP_ColorId itemCheckmarkColor;
        public SCP_ColorId itemTargetGraphicsMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Dropdown Colorer")]
    [RequireComponent(typeof(Dropdown))]
    public class SCP_DropdownColorer : SCP_AColorer<SCP_DropdownColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Dropdown rComponent = GetComponent<Dropdown>();
            applyPalette(rPalette, rComponent, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Dropdown rDropdown, in SCP_DropdownColorsDef colorIds) {
            Selectable rSelectable = rDropdown.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rDropdown.targetGraphic, colorIds.targetGraphicMulColor);
            SCP_TextColorer.applyPalette(rPalette, rDropdown.captionText, colorIds.captionTextColor);
            SCP_TextColorer.applyPalette(rPalette, rDropdown.itemText, colorIds.itemTextColor);

            Toggle rToggle = rDropdown.itemText.gameObject.transform.parent.GetComponent<Toggle>();
            //SCP_ToggleColorer.applyPalette(p, rToggle, d.itemToggle); // no: handles labelTextColor
            SCP_SelectableColorer.applyPalette(rPalette, rToggle, colorIds.itemSelectable);
            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.targetGraphic, colorIds.itemTargetGraphicsMulColor);
            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.graphic, colorIds.itemCheckmarkColor);
        }

    }

}
