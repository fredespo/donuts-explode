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
    public struct SCP_SliderColorsDef {
        public SCP_SelectableColorsDef selectable;
        public SCP_ColorId backgroundColor;
        public SCP_ColorId fillColor;
        public SCP_ColorId handleMulColor;
    }

    [AddComponentMenu("Semantic Color Palette/Slider Colorer")]
    [RequireComponent(typeof(Slider))]
    public class SCP_SliderColorer : SCP_AColorer<SCP_SliderColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            Slider rComponent = GetComponent<Slider>();
            applyPalette(rPalette, rComponent, m_colors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, Slider rSlider, in SCP_SliderColorsDef colorIds) {
            Selectable rSelectable = rSlider.GetComponent<Selectable>();
            SCP_SelectableColorer.applyPalette(rPalette, rSelectable, colorIds.selectable);

            //rComponent.handleRect.GetComponent<Image>() == s.targetGraphic
            SCP_AGraphicColorer.applyPalette(rPalette, rSlider.targetGraphic, colorIds.handleMulColor);
            
            Image rFillRectImage = rSlider.fillRect.GetComponent<Image>();
            if (rFillRectImage != null) {
                SCP_ImageColorer.applyPalette(rPalette, rFillRectImage, colorIds.fillColor);
            }

            // no explicit reference to background image :(
            // normally, "Background" is the first child
            Transform rBackgroundTr = rSlider.transform.childCount == 0 ?
                null : rSlider.transform.GetChild(0);
            Image rBackgroundImage = rBackgroundTr == null ? 
                null : rBackgroundTr.GetComponent<Image>();
            if (rBackgroundImage != null) {
                SCP_ImageColorer.applyPalette(rPalette, rBackgroundImage, colorIds.backgroundColor);
            }
        }

    }

}
