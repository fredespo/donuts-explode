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
using UnityEngine.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers.UI {

    [Serializable]
    public struct SCP_ScrollbarColorsDef {
        public SCP_SelectableColorsDef selectable;
        public SCP_ColorId backgroundColor;
        public SCP_ColorId handleMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Scrollbar Colorer")]
    [RequireComponent(typeof(Scrollbar), typeof(Image))]
    public class SCP_ScrollbarColorer : SCP_AColorer<SCP_ScrollbarColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Scrollbar rComponent = GetComponent<Scrollbar>();
            applyPalette(rPalette, rComponent, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Scrollbar rScrollbar, in SCP_ScrollbarColorsDef colorIds) {
            Selectable rSelectable = rScrollbar.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            SCP_AGraphicColorer.applyPalette(rPalette, rScrollbar.targetGraphic, colorIds.handleMulColor);

            Image rImage = rScrollbar.GetComponent<Image>();
            SCP_ImageColorer.applyPalette(rPalette, rImage, colorIds.backgroundColor);
        }

    }

}
