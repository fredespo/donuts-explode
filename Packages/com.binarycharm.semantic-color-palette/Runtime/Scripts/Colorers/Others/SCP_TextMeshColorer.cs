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

namespace BinaryCharm.SemanticColorPalette.Colorers.Others {

    [AddComponentMenu("Semantic Color Palette/TextMesh Colorer")]
    [RequireComponent(typeof(TextMesh))]
    public class SCP_TextMeshColorer : SCP_AColorer {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TextMesh rTM = GetComponent<TextMesh>();
            applyPalette(rPalette, rTM, m_color);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, TextMesh rTextMesh, SCP_ColorId colorId) {
            if (colorId != SCP_ColorId.DO_NOT_APPLY) {
                rTextMesh.color = rPalette.GetColor(colorId);
            }
        }

    }

}
