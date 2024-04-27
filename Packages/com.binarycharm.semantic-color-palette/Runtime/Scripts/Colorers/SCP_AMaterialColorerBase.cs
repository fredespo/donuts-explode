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

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Abstract base class for colorer implementations acting on a
    /// @UnityEngine.Material. The class handles the optional creation and use 
    /// of a material instance, which is needed when the coloring must not be
    /// applied globally to all the renderers referring to the material.
    /// @BinaryCharm.SemanticColorPalette.SCP_ColorId wrapped in a struct.
    /// </summary>
    public abstract class SCP_AMaterialColorerBase : SCP_AColorerBase {

        [SerializeField] internal bool m_bUseMaterialInstance = false;
        [SerializeField] internal bool m_bSavedOriginalMaterial = false;

        [SerializeField] internal Material m_rOriginalMaterial;
        [SerializeField] internal Material m_rInstancedMaterial;

#if UNITY_EDITOR
        /// <summary>
        /// Used internally to have the "use material instance" feature working
        /// when you duplicate a GameObject
        /// </summary>
        [SerializeField] internal string m_sObjectId;
#endif

        /// <summary>
        /// Fetches the shared @UnityEngine.Material from the renderer.
        /// </summary>
        /// <returns>The renderer shared material.</returns>
        internal abstract Material getSharedMaterialImpl();

        /// <summary>
        /// Sets <paramref name="rMat"/> as the renderer shared material.
        /// </summary>
        /// 
        /// <param name="rMat">A material instance.</param>
        protected abstract void setSharedMaterialImpl(Material rMat);

        /// <summary>
        /// Updates the color(s) of the @UnityEngine.Material handled by the 
        /// colorer according to <paramref name="rPalette"/>.
        /// 
        /// </summary>
        /// <param name="rMat">The material where colors will be applied.</param>
        /// <param name="rPalette">The palette to be used as color source.</param>
        protected abstract void applyPaletteImpl(Material rMat, SCP_IPaletteCore rPalette);


        #region SCP_AColorerBase ----------------------------------------------

        /// <inheritdoc/>
        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Material rMat;
            if (m_bUseMaterialInstance && m_rInstancedMaterial != null) {
                rMat = m_rInstancedMaterial;
            }
            else if (m_bSavedOriginalMaterial && m_rOriginalMaterial != null) {
                rMat = m_rOriginalMaterial;
            }
            else {
                rMat = getSharedMaterialImpl();
            }

            if (rMat == null) return;

            applyPaletteImpl(rMat, rPalette);
            setSharedMaterialImpl(rMat);

            if (!m_bUseMaterialInstance && m_rOriginalMaterial != null) {
                m_rOriginalMaterial = null;
                m_rInstancedMaterial = null;
                m_bSavedOriginalMaterial = false;
            }
        }

        #endregion ------------------------------------------------------------

        /// <summary>
        /// This setting determines if the coloring is applied to the 
        /// "shared material" of the renderer (meaning that il will affect 
        /// all the instances using such material), or if you want to create
        /// an instance (copy) of the original material and change only that
        /// specific instance. 
        /// </summary>
        /// 
        /// <param name="bVal">true creates an instance of the renderer 
        /// material and lets the colorer use that, false does not create
        /// any instances and lets the colorer change the shared material.
        /// </param>
        public void SetUsingMaterialInstance(bool bVal) {
            m_bUseMaterialInstance = bVal;
            instanceSetup();
            refresh();
        }

        /// <summary>
        /// Checks if the colorer is using a material instance or the shared
        /// material.
        /// </summary>
        /// 
        /// <returns>true if using a material instance, false if using 
        /// the shared material</returns>
        public bool IsUsingMaterialInstance() {
            return m_bUseMaterialInstance;
        }

        private void instanceSetup() {
            if (m_bUseMaterialInstance) {
                if (m_rOriginalMaterial == null) {
                    Material rOriginal = getSharedMaterialImpl();
                    m_rOriginalMaterial = rOriginal;
                    m_bSavedOriginalMaterial = true;

                    Material rInstanced = new Material(rOriginal);
                    rInstanced.name = rOriginal.name + " (SCP Instance)";
                    m_rInstancedMaterial = rInstanced;
                } 
            } 
        }

    }

}
