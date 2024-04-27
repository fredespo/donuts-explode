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

namespace BinaryCharm.SemanticColorPalette.Colorers.Renderers {

    [AddComponentMenu("Semantic Color Palette/Renderer Material Colorer")]
    [RequireComponent(typeof(Renderer))]
    public class SCP_RendererMaterialColorer : SCP_AMaterialColorer {

        [SerializeField] internal int m_iMaterialId;

        public void SetMaterialIndex(int iMatIndex) {
            m_iMaterialId = iMatIndex + 1;
        }

        #region SCP_AMaterialColorerBase --------------------------------------

        /// <inheritdoc/>
        internal override Material getSharedMaterialImpl() {
            int iMatIndex = m_iMaterialId - 1;
            Material[] rMats = GetComponent<Renderer>().sharedMaterials;
            if (iMatIndex < 0 || iMatIndex >= rMats.Length) return null;
            return rMats[iMatIndex];
        }

        /// <inheritdoc/>
        protected override void setSharedMaterialImpl(Material rMat) {
            int iMatIndex = m_iMaterialId - 1;
            Renderer rMR = GetComponent<Renderer>();
            Material[] rSharedMats = rMR.sharedMaterials;
            rSharedMats[iMatIndex] = rMat;
            rMR.sharedMaterials = rSharedMats;
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Renderer rRenderer, SCP_MaterialColorDef[] def, int iMatIndex = 0) {
            Material[] rSharedMats = rRenderer.sharedMaterials;
            Material rMat = rSharedMats[iMatIndex];
            SCP_AMaterialColorer.applyPalette(rPalette, rMat, def);
        }

    }

}
