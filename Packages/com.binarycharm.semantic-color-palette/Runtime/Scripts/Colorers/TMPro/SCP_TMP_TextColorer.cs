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

using TMPro;

namespace BinaryCharm.SemanticColorPalette.Colorers.TMPro {

    [Serializable]
    public struct SCP_TMP_TextColorsDef {
        public SCP_ColorId vertexColor;
        public SCP_ColorId gradientTopLeft;
        public SCP_ColorId gradientTopRight;
        public SCP_ColorId gradientBottomLeft;
        public SCP_ColorId gradientBottomRight;
    }

    [AddComponentMenu("Semantic Color Palette/Text (TextMeshPro) Colorer")]
    [RequireComponent(typeof(TMP_Text))]
    public class SCP_TMP_TextColorer : SCP_AColorer<SCP_TMP_TextColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TMP_Text rText = GetComponent<TMP_Text>();
            applyPalette(rPalette, rText, m_colors);
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, TMP_Text rText, in SCP_TMP_TextColorsDef colorIds) {
            if (colorIds.vertexColor != SCP_ColorId.DO_NOT_APPLY) {
                rText.color = rPalette.GetColor(colorIds.vertexColor);
            }

            VertexGradient currGradient = rText.colorGradient;
            rText.colorGradient = new VertexGradient(
                colorIds.gradientTopLeft == SCP_ColorId.DO_NOT_APPLY ? 
                    currGradient.topLeft : rPalette.GetColor(colorIds.gradientTopLeft),
                colorIds.gradientTopRight == SCP_ColorId.DO_NOT_APPLY ?
                    currGradient.topRight : rPalette.GetColor(colorIds.gradientTopRight),
                colorIds.gradientBottomLeft == SCP_ColorId.DO_NOT_APPLY ? 
                    currGradient.bottomLeft : rPalette.GetColor(colorIds.gradientBottomLeft),
                colorIds.gradientBottomRight == SCP_ColorId.DO_NOT_APPLY ? 
                    currGradient.bottomRight : rPalette.GetColor(colorIds.gradientBottomRight)
            );
        }

    }

}

#endif
