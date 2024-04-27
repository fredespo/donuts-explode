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

namespace BinaryCharm.Common.Extensions {

    public static class ColorEx {

        #region RGBA extensions -----------------------------------------------

        public static Color withR(this Color v, float r) {
            return new Color(r, v.g, v.b, v.a);
        }

        public static Color withG(this Color v, float g) {
            return new Color(v.r, g, v.b, v.a);
        }

        public static Color withB(this Color v, float b) {
            return new Color(v.r, v.g, b, v.a);
        }

        public static Color withA(this Color v, float a) {
            return new Color(v.r, v.g, v.b, a);
        }

        #endregion ------------------------------------------------------------


        #region HSV extensions ------------------------------------------------

        public static float getHue(this Color c) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return h;
        }

        public static float getSaturation(this Color c) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return s;
        }

        public static float getValue(this Color c) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return v;
        }

        public static Color withHue(this Color c, float hue) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return Color.HSVToRGB(hue, s, v);
        }

        public static Color withHue(this Color c, int hue) {
            return c.withHue(hue / 360f);
        }

        public static Color withSaturation(this Color c, float saturation) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return Color.HSVToRGB(h, saturation, v);
        }

        public static Color withSaturation(this Color c, int saturation) {
            return c.withSaturation(saturation / 100f);
        }

        public static Color withValue(this Color c, float value) {
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            return Color.HSVToRGB(h, s, value);
        }

        public static Color withValue(this Color c, int value) {
            return c.withValue(value / 100f);
        }

        #endregion ------------------------------------------------------------

        #region Tint/Shade extensions -----------------------------------------

        public static Color getTint(this Color c, float fTintAmount01) {
            float r = c.r;
            float g = c.g;
            float b = c.b;

            float ret_r = r + ((1 - r) * fTintAmount01);
            float ret_g = g + ((1 - g) * fTintAmount01);
            float ret_b = b + ((1 - b) * fTintAmount01);
            return new Color(ret_r, ret_g, ret_b, c.a);
        }

        public static Color getShade(this Color c, float fShadeAmount01) {
            float r = c.r * (1f - fShadeAmount01);
            float g = c.g * (1f - fShadeAmount01);
            float b = c.b * (1f - fShadeAmount01);

            return new Color(r, g, b, c.a);
        }

        #endregion ------------------------------------------------------------
    }

}
