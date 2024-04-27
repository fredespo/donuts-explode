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
using UnityEngine.UI;

using TMPro;

using BinaryCharm.SemanticColorPalette.Colorers.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers.TMPro {

    [Serializable]
    public struct SCP_TMP_DropdownColorDefs {
        public SCP_SelectableColorsDef selectable;

        // label
        public SCP_TMP_TextColorsDef captionText;
        public SCP_TMP_TextColorsDef itemText;

        // targetGraphics
        public SCP_ColorId targetGraphicMulColor;

        // item
        // public SCP_ToggleColorsDef itemToggle; // no: handles labelTextColor
        public SCP_SelectableColorsDef itemSelectable;
        public SCP_ColorId itemCheckmarkColor;
        public SCP_ColorId itemTargetGraphicsMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Dropdown (TextMeshPro) Colorer")]
    [RequireComponent(typeof(TMP_Dropdown))]
    public class SCP_TMP_DropdownColorer : SCP_AColorer<SCP_TMP_DropdownColorDefs> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TMP_Dropdown rComponent = GetComponent<TMP_Dropdown>();
            applyPalette(rPalette, rComponent, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, TMP_Dropdown rDropdown, in SCP_TMP_DropdownColorDefs colorIds) {
            Selectable rSelectable = rDropdown.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rDropdown.targetGraphic, colorIds.targetGraphicMulColor);
            SCP_TMP_TextColorer.applyPalette(rPalette, rDropdown.captionText, colorIds.captionText);
            SCP_TMP_TextColorer.applyPalette(rPalette, rDropdown.itemText, colorIds.itemText);

            Toggle rToggle = rDropdown.itemText.gameObject.transform.parent.GetComponent<Toggle>();
            //SCP_ToggleColorer.applyPalette(p, rToggle, d.itemToggle); // no: handles labelTextColor
            SCP_SelectableColorer.applyPalette(rPalette, rToggle, colorIds.itemSelectable);
            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.targetGraphic, colorIds.itemTargetGraphicsMulColor);
            SCP_AGraphicColorer.applyPalette(rPalette, rToggle.graphic, colorIds.itemCheckmarkColor);

        }

    }

}

#endif
