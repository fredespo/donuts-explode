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
    public struct SCP_TMP_InputFieldColorDefs {
        public SCP_SelectableColorsDef selectable;

        public SCP_ColorId selectionColor;
        public SCP_ColorId customCaretColor;

        public SCP_ColorId targetGraphicMulColor;

        // text children
        public SCP_TMP_TextColorsDef text;
        public SCP_ColorId placeholderTextColor;
    }

    [AddComponentMenu("Semantic Color Palette/Input Field (TextMeshPro) Colorer")]
    [RequireComponent(typeof(TMP_InputField))]
    public class SCP_TMP_InputFieldColorer : SCP_AColorer<SCP_TMP_InputFieldColorDefs> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TMP_InputField rInputField = GetComponent<TMP_InputField>();
            applyPalette(rPalette, rInputField, m_colors);
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, TMP_InputField rInputField, in SCP_TMP_InputFieldColorDefs colorIds) {
            Selectable rSelectable = rInputField.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            if (colorIds.selectionColor != SCP_ColorId.DO_NOT_APPLY) {
                rInputField.selectionColor = rPalette.GetColor(colorIds.selectionColor);
            }
            if (colorIds.customCaretColor != SCP_ColorId.DO_NOT_APPLY) {
                rInputField.caretColor = rPalette.GetColor(colorIds.customCaretColor);
            }
            
            SCP_AGraphicColorer.applyPalette(rPalette, rInputField.targetGraphic, colorIds.targetGraphicMulColor);
            SCP_TMP_TextColorer.applyPalette(rPalette, rInputField.textComponent, colorIds.text);
            SCP_AGraphicColorer.applyPalette(rPalette, rInputField.placeholder, colorIds.placeholderTextColor);
        }

    }

}

#endif
