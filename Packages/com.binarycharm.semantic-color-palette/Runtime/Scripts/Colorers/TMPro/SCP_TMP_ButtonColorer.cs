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
    public struct SCP_TMP_ButtonColorDefs {
        public SCP_SelectableColorsDef selectable;
        public SCP_TMP_TextColorsDef labelText;
        public SCP_ColorId targetGraphicMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Button (TextMeshPro) Colorer")]
    [RequireComponent(typeof(Button))]
    public class SCP_TMP_ButtonColorer : SCP_AColorer<SCP_TMP_ButtonColorDefs> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Button rBtn = GetComponent<Button>();
            applyPalette(rPalette, rBtn, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Button rButton, in SCP_TMP_ButtonColorDefs colorIds) {
            Selectable rSelectable = rButton.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rButton.targetGraphic, colorIds.targetGraphicMulColor);

            TMP_Text rLabel = rButton.GetComponentInChildren<TMP_Text>();
            if (rLabel != null) {
                SCP_TMP_TextColorer.applyPalette(rPalette, rLabel, colorIds.labelText);
            }
        }

    }

}

#endif
