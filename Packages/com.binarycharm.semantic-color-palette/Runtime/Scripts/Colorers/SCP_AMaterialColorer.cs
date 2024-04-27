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
using System.Collections.Generic;

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Abstract base class for colorer implementations acting on a
    /// @UnityEngine.Material that require a specific set of 
    /// @BinaryCharm.SemanticColorPalette.SCP_ColorId wrapped in a struct.
    /// </summary>
    /// 
    /// <typeparam name="T">The struct type containing the appropriate color
    /// ids.</typeparam>
    public abstract class SCP_AMaterialColorer<T> : SCP_AMaterialColorerBase, SCP_IColorer<T> {
        
        [SerializeField] internal T m_color;

        public T GetColorIds() {
            return m_color;
        }

        public void SetColorIds(in T def) {
            m_color = def;
            refresh();
        }

    }


    /// <summary>
    /// Structure defining a (name, value) pair where each entry is made of
    /// a shader property name and a color id.
    /// </summary>
    [Serializable]
    public struct SCP_MaterialColorDef {

        /// <summary>
        /// A shader property name (such as "_Color").
        /// </summary>
        public string m_sShaderPropName;

        /// <summary>
        /// A @BinaryCharm.SemanticColorPalette.SCP_ColorId. The paired shader
        /// color property will be set to the color indicated by this color id.
        /// </summary>
        public SCP_ColorId m_colorId;

        public SCP_MaterialColorDef(string sColorPropertyName, SCP_ColorId colorId) {
            m_sShaderPropName = sColorPropertyName;
            m_colorId = colorId;
        }
    }


    /// <summary>
    /// Abstract base class for colorer implementations acting on a
    /// @UnityEngine.Material that require an arbitrary number of color
    /// properties (depending on the shader color properties).
    /// </summary>
    public abstract class SCP_AMaterialColorer : SCP_AMaterialColorerBase, SCP_IColorer<SCP_MaterialColorDef[]> {

        [SerializeField] internal SCP_MaterialColorDef[] m_colors = new SCP_MaterialColorDef[0];

        private MaterialColorPropsColorer m_rMaterialColorPropsColorer = null;


        #region IColorer ------------------------------------------------------

        /// <inheritdoc/>
        public SCP_MaterialColorDef[] GetColorIds() {
            return m_colors;
        }

        /// <inheritdoc/>
        public void SetColorIds(in SCP_MaterialColorDef[] def) {
            m_colors = def;
            m_rMaterialColorPropsColorer = new MaterialColorPropsColorer(def);
            refresh();
        }

        #endregion ------------------------------------------------------------


        #region SCP_AMaterialColorerBase --------------------------------------

        protected override void applyPaletteImpl(Material rMat, SCP_IPaletteCore rPalette) {
            if (m_rMaterialColorPropsColorer == null) m_rMaterialColorPropsColorer = new MaterialColorPropsColorer(m_colors);
            m_rMaterialColorPropsColorer.applySemanticPalette(rPalette, rMat);
        }

        #endregion ------------------------------------------------------------


        private void OnValidate() {
            m_rMaterialColorPropsColorer = new MaterialColorPropsColorer(m_colors);
            refresh();
        }

        public static void applyPalette(SCP_IPaletteCore rPalette, Material rMaterial, SCP_MaterialColorDef[] def) {
            MaterialColorPropsColorer rCM = new MaterialColorPropsColorer(def);
            rCM.applySemanticPalette(rPalette, rMaterial);
        }


        #region MaterialColorPropsColorer private utility class ---------------
        private class MaterialColorPropsColorer {

            private struct ShaderPropAndColor {
                public readonly int m_iShaderPropId;
                public readonly SCP_ColorId m_colorId;

                public ShaderPropAndColor(int iShaderPropId, SCP_ColorId colorId) {
                    m_iShaderPropId = iShaderPropId;
                    m_colorId = colorId;
                }
            }

            private ShaderPropAndColor[] m_cache;

            public MaterialColorPropsColorer(SCP_MaterialColorDef[] def) {
                List<ShaderPropAndColor> rCacheList = new List<ShaderPropAndColor>();
                int iNumDefs = def.Length;
                for (int i = 0; i < iNumDefs; ++i) {
                    SCP_ColorId colorId = def[i].m_colorId;
                    if (colorId == SCP_ColorId.DO_NOT_APPLY) continue;

                    string sPropName = def[i].m_sShaderPropName;
                    int iPropId = Shader.PropertyToID(sPropName);
                    rCacheList.Add(new ShaderPropAndColor(iPropId, colorId));
                }
                m_cache = rCacheList.ToArray();
            }

            public void applySemanticPalette(SCP_IPaletteCore rPalette, Material rMat) {
                foreach(ShaderPropAndColor rEntry in m_cache) {
                    rMat.SetColor(rEntry.m_iShaderPropId, rPalette.GetColor(rEntry.m_colorId));
                }
            }

        }

        #endregion ------------------------------------------------------------

    }

}
