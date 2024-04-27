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

    [AddComponentMenu("Semantic Color Palette/Trail Renderer Colorer")]
    [RequireComponent(typeof(TrailRenderer))]
    public class SCP_TrailRendererColorer : SCP_AGradientColorer {

        #region SCP_AGradientColorer ------------------------------------------

        internal override Gradient getCurrGradient() {
            TrailRenderer rRenderer = GetComponent<TrailRenderer>();
            return rRenderer.colorGradient;
        }

        internal override void setCurrGradient(Gradient rGradient) {
            TrailRenderer rRenderer = GetComponent<TrailRenderer>();
            rRenderer.colorGradient = rGradient;
        }

        #endregion ------------------------------------------------------------


        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            TrailRenderer rRenderer = GetComponent<TrailRenderer>();
            applyPalette(rPalette, rRenderer, m_gradientColors);
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, TrailRenderer rRenderer, SCP_ColorId[] colors) {
            TrailRendererGradientAccessor rAccessor = new TrailRendererGradientAccessor(rRenderer);
            SCP_AGradientColorer.applyPalette(rPalette, rAccessor, colors);
        }

        private class TrailRendererGradientAccessor : SCP_IGradientAccessor {

            private TrailRenderer m_rRenderer;

            public TrailRendererGradientAccessor(TrailRenderer rTR) {
                m_rRenderer = rTR; 
            }

            public Gradient GetCurrGradient() {
                return m_rRenderer.colorGradient;
            }

            public void SetCurrGradient(Gradient rGradient) {
                m_rRenderer.colorGradient = rGradient;
            }

        }

    }

}
