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

    [AddComponentMenu("Semantic Color Palette/LineRenderer Colorer")]
    [RequireComponent(typeof(LineRenderer))]
    public class SCP_LineRendererColorer : SCP_AGradientColorer {

        #region SCP_AGradientColorer ------------------------------------------

        internal override Gradient getCurrGradient() {
            LineRenderer rRenderer = GetComponent<LineRenderer>();
            return rRenderer.colorGradient;
        }

        internal override void setCurrGradient(Gradient rGradient) {
            LineRenderer rRenderer = GetComponent<LineRenderer>();
            rRenderer.colorGradient = rGradient;
        }

        #endregion ------------------------------------------------------------


        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            LineRenderer rRenderer = GetComponent<LineRenderer>();
            applyPalette(rPalette, rRenderer, m_gradientColors);
        }

        #endregion ------------------------------------------------------------


        public static void applyPalette(SCP_IPaletteCore rPalette, LineRenderer rRenderer, SCP_ColorId[] colors) {
            LineRendererGradientAccessor rAccessor = new LineRendererGradientAccessor(rRenderer);
            SCP_AGradientColorer.applyPalette(rPalette, rAccessor, colors);
        }

        private class LineRendererGradientAccessor : SCP_IGradientAccessor {

            private LineRenderer m_rRenderer;

            public LineRendererGradientAccessor(LineRenderer rLR) {
                m_rRenderer = rLR;
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
