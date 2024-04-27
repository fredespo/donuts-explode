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
    public struct SCP_TMP_FontMaterialColorsDef {
        // material color properties
        public SCP_ColorId faceColor;
        public SCP_ColorId outlineColor;
        public SCP_ColorId underlayColor;
        public SCP_ColorId glowColor;

        // material color properties with no ID in TMPro.ShaderUtilies - might break?
        public SCP_ColorId lightingSpecularColor;
        public SCP_ColorId envMapFaceColor;
        public SCP_ColorId envMapOutlineColor;
    }

    [AddComponentMenu("Semantic Color Palette/Text (TextMeshPro) Font Material Colorer")]
    [RequireComponent(typeof(TMP_Text))]
    public class SCP_TMP_FontMaterialColorer : SCP_AMaterialColorer<SCP_TMP_FontMaterialColorsDef> {

        #region SCP_AMaterialColorerBase --------------------------------------

        internal override Material getSharedMaterialImpl() {
            return GetComponent<TMP_Text>().fontSharedMaterial;
        }

        protected override void setSharedMaterialImpl(Material rMaterial) {
            GetComponent<TMP_Text>().fontSharedMaterial = rMaterial;
        }

        protected override void applyPaletteImpl(Material rMaterial, SCP_IPaletteCore rPalette) {
            applyPalette(rPalette, rMaterial, m_color);
        }

        #endregion ------------------------------------------------------------

        private static int s_iSpecularColorPropertyId = Shader.PropertyToID("_SpecularColor");
        private static int s_iReflectFaceColorPropertyId = Shader.PropertyToID("_ReflectFaceColor");
        private static int s_iReflectOutlineColorPropertyId = Shader.PropertyToID("_ReflectOutlineColor");

        public static void applyPalette(SCP_IPaletteCore rPalette, Material rMaterial, in SCP_TMP_FontMaterialColorsDef colorIds) {
            if (colorIds.faceColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(ShaderUtilities.ID_FaceColor, rPalette.GetColor(colorIds.faceColor));
            }
            if (colorIds.outlineColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(ShaderUtilities.ID_OutlineColor, rPalette.GetColor(colorIds.outlineColor));
            }
            if (colorIds.underlayColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, rPalette.GetColor(colorIds.underlayColor));
            }
            if (colorIds.glowColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(ShaderUtilities.ID_GlowColor, rPalette.GetColor(colorIds.glowColor));
            }

            if (colorIds.lightingSpecularColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(s_iSpecularColorPropertyId, rPalette.GetColor(colorIds.lightingSpecularColor));
            }
            if (colorIds.envMapFaceColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(s_iReflectFaceColorPropertyId, rPalette.GetColor(colorIds.envMapFaceColor));
            }
            if (colorIds.envMapOutlineColor != SCP_ColorId.DO_NOT_APPLY) {
                rMaterial.SetColor(s_iReflectOutlineColorPropertyId, rPalette.GetColor(colorIds.envMapOutlineColor));
            }
        }

        public static void applyPalette(SCP_IPaletteCore rPalette, TMP_Text rText, in SCP_TMP_FontMaterialColorsDef colorIds) {
            applyPalette(rPalette, rText.fontSharedMaterial, colorIds);
        }

    }

}

#endif
