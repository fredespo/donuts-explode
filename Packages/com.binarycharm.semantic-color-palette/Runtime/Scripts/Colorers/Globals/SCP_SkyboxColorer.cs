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

namespace BinaryCharm.SemanticColorPalette.Colorers.Globals {

    [Serializable]
    public struct SCP_SkyboxColorsDef {
        public SCP_ColorId skyTint;
        public SCP_ColorId groundColor;
    }

    [AddComponentMenu("Semantic Color Palette/Skybox Colorer")]
    public class SCP_SkyboxColorer : SCP_AColorer<SCP_SkyboxColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            applyPalette(rPalette, m_colors);
        }

        #endregion ------------------------------------------------------------


        private static int s_iSkyTintPropertyId = Shader.PropertyToID("_SkyTint");
        private static int s_iGroundColorPropertyId = Shader.PropertyToID("_GroundColor");

        public static void applyPalette(SCP_IPaletteCore rPalette, in SCP_SkyboxColorsDef colorIds) {
            if (colorIds.skyTint != SCP_ColorId.DO_NOT_APPLY) {
                RenderSettings.skybox.SetColor(s_iSkyTintPropertyId, rPalette.GetColor(colorIds.skyTint));
            }
            if (colorIds.groundColor != SCP_ColorId.DO_NOT_APPLY) {
                RenderSettings.skybox.SetColor(s_iGroundColorPropertyId, rPalette.GetColor(colorIds.groundColor));
            }
        }

    }

}
