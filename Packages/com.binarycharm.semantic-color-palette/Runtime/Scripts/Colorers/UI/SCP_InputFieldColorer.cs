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
    public struct SCP_InputFieldColorsDef {
        public SCP_SelectableColorsDef selectable;

        public SCP_ColorId selectionColor;
        public SCP_ColorId customCaretColor;

        public SCP_ColorId targetGraphicMulColor;

        // text children
        public SCP_ColorId textColor;
        public SCP_ColorId placeholderTextColor;
    }

    [AddComponentMenu("Semantic Color Palette/Input Field Colorer")]
    [RequireComponent(typeof(InputField))]
    public class SCP_InputFieldColorer : SCP_AColorer<SCP_InputFieldColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            InputField rIF = GetComponent<InputField>();
            applyPalette(rPalette, rIF, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, InputField rInputField, in SCP_InputFieldColorsDef colorIds) {
            Selectable rSelectable = rInputField.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            if (colorIds.selectionColor != SCP_ColorId.DO_NOT_APPLY) {
                rInputField.selectionColor = rPalette.GetColor(colorIds.selectionColor);
            }
            if (colorIds.customCaretColor != SCP_ColorId.DO_NOT_APPLY) {
                rInputField.caretColor = rPalette.GetColor(colorIds.customCaretColor);
            }

            SCP_AGraphicColorer.applyPalette(rPalette, rInputField.targetGraphic, colorIds.targetGraphicMulColor);
            SCP_TextColorer.applyPalette(rPalette, rInputField.textComponent, colorIds.textColor);
            SCP_AGraphicColorer.applyPalette(rPalette, rInputField.placeholder, colorIds.placeholderTextColor);
        }

    }

}
