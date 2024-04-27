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
using UnityEngine.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers.UI {

    [AddComponentMenu("Semantic Color Palette/Graphic Material Colorer")]
    [RequireComponent(typeof(Graphic))]
    public class SCP_GraphicMaterialColorer : SCP_AMaterialColorer {

        #region SCP_AMaterialColorerBase --------------------------------------

        /// <inheritdoc/>
        internal override Material getSharedMaterialImpl() {
            return GetComponent<Graphic>().material;
        }

        /// <inheritdoc/>
        protected override void setSharedMaterialImpl(Material rMaterial) {
            GetComponent<Graphic>().material = rMaterial;
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, Graphic rGraphic, SCP_MaterialColorDef[] colorIds) {
            SCP_AMaterialColorer.applyPalette(rPalette, rGraphic.material, colorIds);
        }

    }

}
