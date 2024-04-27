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

using System;

namespace BinaryCharm.SemanticColorPalette.Utils {

    /// <summary>
    /// Static class providing some color conversion utilities.
    /// </summary>
    public static class SCP_ColorUtils {

        /// <summary>
        /// Converts a Color from CMYK to Unity RGB representation.
        /// </summary>
        /// <param name="c">Cyan, in [0,1]</param>
        /// <param name="m">Magenta, in [0,1]</param>
        /// <param name="y">Yellow, in [0,1]</param>
        /// <param name="k">Black, in [0,1]</param>
        /// <returns>The Unity RGB Color resulting from the conversion</returns>
        public static Color CMYKToRGB(float c, float m, float y, float k) {
            float r = (1f - c) * (1 - k);
            float g = (1f - m) * (1 - k);
            float b = (1f - y) * (1 - k);
            return new Color(r, g, b);
        }

        /// <summary>
        /// Converts a single normalized float value to a Unity RGB color
        /// having that value in the r, g, b components.
        /// </summary>
        /// <param name="g">Gray value, in [0, 1]</param>
        /// <returns>The Unity RGB Color defined as new Color(g, g, g)</returns>
        public static Color GreyToRGB(float g) {
            Color ret = new Color(g, g, g);
            return ret;
        }

        /// <summary>
        /// Converts a Color from Lab to Unity RGB representation.
        /// </summary>
        /// <param name="l">Lightness, in [0-100]</param>.
        /// <param name="a">Chrominance A, in [-128-128]</param>.
        /// <param name="b">Chrominance B, in [-128-128]</param>.
        /// <returns>The Unity RGB Color resulting from the conversion.</returns>
        public static Color LabToRGB(float l, float a, float b) {
            
            Vec3 white = D50WhiteAlt;
            Matrix3x3 adaption = BradfordAdaptation;
            CompandingFunc companding = sRGBCompanding;
            RgbModelData rgbModel = sRGB;

            Vec3 lab = new Vec3(l, a, b);
            Vec3 xyz = labToXyz(lab, white);
            Vec3 rgb = xyzToRgb(xyz, white, adaption, rgbModel, companding);

            // this should be better, but the uncommented option matches
            // Photoshop RGB values more closely
            //return new Color((float)rgb.x, (float)rgb.y, (float)rgb.z);

            //byte ir = (byte)(rgb.x * 255);
            //byte ig = (byte)(rgb.y * 255);
            //byte ib = (byte)(rgb.z * 255);
            //return new Color32(ir, ig, ib, 255);

            float rfr = (float)(rgb.x * 255.0);
            float rfg = (float)(rgb.y * 255.0);
            float rfb = (float)(rgb.z * 255.0);

            byte ir = (byte)Mathf.RoundToInt(rfr);
            byte ig = (byte)Mathf.RoundToInt(rfg);
            byte ib = (byte)Mathf.RoundToInt(rfb);

            //byte ir = (byte)rfr;
            //byte ig = (byte)rfg;
            //byte ib = (byte)rfb;

            return new Color32(ir, ig, ib, 255);
        }

        /// <summary>
        /// Converts a Color from XYZ to Unity RGB representation.
        /// </summary>
        /// <param name="x">X component</param>.
        /// <param name="y">Y component</param>.
        /// <param name="z">Z component</param>.
        /// <returns>The Unity RGB Color resulting from the conversion.</returns>
        public static Color XyzToRGB(float x, float y, float z) {
            Vec3 white = D50WhiteAlt;
            Matrix3x3 adaption = BradfordAdaptation;
            CompandingFunc companding = sRGBCompanding;
            RgbModelData rgbModel = sRGB;

            Vec3 xyz = new Vec3(x, y, z); 
            Vec3 rgb = xyzToRgb(xyz, white, adaption, rgbModel, companding);

            return new Color((float)rgb.x, (float)rgb.y, (float)rgb.z);
        }

        #region Implementation details and parameters -------------------------

        // CIE consts ---------------------------------------------------------
        // CIE standard
        //private const double CIE_e = 0.008856;
        //private const double CIE_k = 903.3;
        //private const double CIE_ke = 8.0;

        // intent of the CIE standard
        // see http://www.brucelindbloom.com/index.html?Eqn_Lab_to_XYZ.html
        private const double CIE_e = 216 / 24389.0;
        private const double CIE_k = 24389 / 27.0;
        private const double CIE_ke = 8.0;
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // White reference values ---------------------------------------------
        // D65 Bruce Lindbloom http://www.brucelindbloom.com/index.html?ColorCalculator.html
        private static readonly Vec3 D65White = new Vec3(0.95047, 1.0, 1.08883);

        // D50 Bruce Lindbloom http://www.brucelindbloom.com/index.html?ColorCalculator.html
        private static readonly Vec3 D50WhiteAlt = new Vec3(0.96422, 1.0, 0.82521);

        // D50 Photoshop https://color-image.com/2011/10/the-reference-white-in-adobe-photoshop-lab-mode/
        private static readonly Vec3 D50White = new Vec3(0.9642, 1.0, 0.8249);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // Adaptation matrices ------------------------------------------------
        private static readonly Matrix3x3 BradfordAdaptation = new Matrix3x3 {
            m00 =  0.8951f, m01 = -0.7502f, m02 =  0.0389f,
            m10 =  0.2664f, m11 =  1.7135f, m12 = -0.0685f,
            m20 = -0.1614f, m21 =  0.0367f, m22 =  1.0296f,
        };
        
        private static readonly Matrix3x3 VonKriesAdaptation = new Matrix3x3 {
            m00 =  0.40024, m01 = -0.22630, m02 = 0.00000,
            m10 =  0.70760, m11 =  1.16532, m12 = 0.00000,
            m20 = -0.08081, m21 =  0.04570, m22 = 0.91822,
        };

        // no adaptation
        private static readonly Matrix3x3 Identity = new Matrix3x3 {
            m00 = 1.0, m01 = 0.0, m02 = 0.0,
            m10 = 0.0, m11 = 1.0, m12 = 0.0,
            m20 = 0.0, m21 = 0.0, m22 = 1.0,
        };
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // RGB Models defs ----------------------------------------------------
        private static RgbModelData sRGB = new RgbModelData(
            0.64, 0.33,
            0.30, 0.60,
            0.15, 0.06,
            new Vec3(0.95047, 1.0, 1.08883)
        );

        private static RgbModelData CIERGB = new RgbModelData(
            0.735, 0.265,
            0.274, 0.717,
            0.167, 0.009,
            new Vec3(1.0, 1.0, 1.0)
        );
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        // Companding functions -----------------------------------------------
        private delegate double CompandingFunc(double v);
        private static double sRGBCompanding(double v) {
            int sign = 1;
            if (v < 0) {
                sign = -1;
                v = -v;
            }
            double c =
                v <= 0.0031308 ?
                12.92 * v :
                (1.055 * Math.Pow(v, 1.0 / 2.4)) - 0.055;
            return c * sign;
        }

        private static double LCompanding(double v) {
            int sign = 1;
            if (v < 0) {
                sign = -1;
                v = -v;
            }
            double c =
                v <= CIE_e ?
                v * CIE_k / 100.0 :
                1.16 * Math.Pow(v, 1f / 3f) - 0.16;
            return c * sign;
        }

        private static double GammaCompanding(double v, double gamma) {
            int sign = 1;
            if (v < 0) {
                sign = -1;
                v = -v;
            }
            double c = Math.Pow(v, 1.0 / gamma);
            return c * sign;
        }

        private static CompandingFunc getGammaCompanding(double gammaValue) {
            return (double v) => { return GammaCompanding(v, gammaValue); };
        }
        // --------------------------------------------------------------------
        // --------------------------------------------------------------------

        private static Vec3 labToXyz(Vec3 lab, Vec3 white) {
            double fl = lab.x;
            double fa = lab.y;
            double fb = lab.z;

            double fy = (fl + 16) / 116.0;
            double fx = (fa * 0.002) + fy;
            double fz = fy - (fb * 0.005);

            double fx3 = fx * fx * fx;
            double fz3 = fz * fz * fz;

            double xr = (fx3 > CIE_e) ? fx3 : ((116 * fx) - 16) / CIE_k;
            double yr = (fl > CIE_ke) ? (fy * fy * fy) : fl / CIE_k;
            double zr = (fz3 > CIE_e) ? fz3 : ((116 * fz) - 16) / CIE_k;

            double x = xr * white.x;
            double y = yr * white.y;
            double z = zr * white.z;

            return new Vec3(x, y, z);
        }

        private static Vec3 xyzToRgb(Vec3 xyz, Vec3 white, Matrix3x3 adaptation, RgbModelData md, CompandingFunc dCompanding) {
            double x = xyz.x;
            double y = xyz.y;
            double z = xyz.z;

            var As = white.x * adaptation.m00 + white.y * adaptation.m10 + white.z * adaptation.m20;
            var Bs = white.x * adaptation.m01 + white.y * adaptation.m11 + white.z * adaptation.m21;
            var Cs = white.x * adaptation.m02 + white.y * adaptation.m12 + white.z * adaptation.m22;

            var Ad = md.RefWhiteRGB.x * adaptation.m00 + md.RefWhiteRGB.y * adaptation.m10 + md.RefWhiteRGB.z * adaptation.m20;
            var Bd = md.RefWhiteRGB.x * adaptation.m01 + md.RefWhiteRGB.y * adaptation.m11 + md.RefWhiteRGB.z * adaptation.m21;
            var Cd = md.RefWhiteRGB.x * adaptation.m02 + md.RefWhiteRGB.y * adaptation.m12 + md.RefWhiteRGB.z * adaptation.m22;

            var X1 = x * adaptation.m00 + y * adaptation.m10 + z * adaptation.m20;
            var Y1 = x * adaptation.m01 + y * adaptation.m11 + z * adaptation.m21;
            var Z1 = x * adaptation.m02 + y * adaptation.m12 + z * adaptation.m22;

            X1 *= (Ad / As);
            Y1 *= (Bd / Bs);
            Z1 *= (Cd / Cs);

            Matrix3x3 adaptationI = adaptation.Inverse();
            double x2 = X1 * adaptationI.m00 + Y1 * adaptationI.m10 + Z1 * adaptationI.m20;
            double y2 = X1 * adaptationI.m01 + Y1 * adaptationI.m11 + Z1 * adaptationI.m21;
            double z2 = X1 * adaptationI.m02 + Y1 * adaptationI.m12 + Z1 * adaptationI.m22;

            Matrix3x3 MtxXYZ2RGB = calcRgb2XyzMatrix(md);
            double r = dCompanding(x2 * MtxXYZ2RGB.m00 + y2 * MtxXYZ2RGB.m10 + z2 * MtxXYZ2RGB.m20);
            double g = dCompanding(x2 * MtxXYZ2RGB.m01 + y2 * MtxXYZ2RGB.m11 + z2 * MtxXYZ2RGB.m21);
            double b = dCompanding(x2 * MtxXYZ2RGB.m02 + y2 * MtxXYZ2RGB.m12 + z2 * MtxXYZ2RGB.m22);

            return new Vec3(r, g, b);
        }

        private static Matrix3x3 calcRgb2XyzMatrix(RgbModelData md) {
            Matrix3x3 m = new Matrix3x3();
            m.m00 = md.xr / md.yr; 
            m.m01 = md.xg / md.yg;
            m.m02 = md.xb / md.yb;
            m.m10 = 1.0;
            m.m11 = 1.0;
            m.m12 = 1.0;
            m.m20 = (1.0 - md.xr - md.yr) / md.yr;
            m.m21 = (1.0 - md.xg - md.yg) / md.yg;
            m.m22 = (1.0 - md.xb - md.yb) / md.yb;

            Matrix3x3 mi = m.Inverse();
            var sr = md.RefWhiteRGB.x * mi.m00 + md.RefWhiteRGB.y * mi.m01 + md.RefWhiteRGB.z * mi.m02;
            var sg = md.RefWhiteRGB.x * mi.m10 + md.RefWhiteRGB.y * mi.m11 + md.RefWhiteRGB.z * mi.m12;
            var sb = md.RefWhiteRGB.x * mi.m20 + md.RefWhiteRGB.y * mi.m21 + md.RefWhiteRGB.z * mi.m22;

            Matrix3x3 MtxRGB2XYZ = new Matrix3x3();
            MtxRGB2XYZ.m00 = sr * m.m00;
            MtxRGB2XYZ.m01 = sg * m.m01;
            MtxRGB2XYZ.m02 = sb * m.m02;
            MtxRGB2XYZ.m10 = sr * m.m10;
            MtxRGB2XYZ.m11 = sg * m.m11;
            MtxRGB2XYZ.m12 = sb * m.m12;
            MtxRGB2XYZ.m20 = sr * m.m20;
            MtxRGB2XYZ.m21 = sg * m.m21;
            MtxRGB2XYZ.m22 = sb * m.m22;

            MtxRGB2XYZ.Transpose();

            Matrix3x3 MtxXYZ2RGB = MtxRGB2XYZ.Inverse();
            return MtxXYZ2RGB;
        }

        // --------------------------------------------------------------------
        // --------------------------------------------------------------------
        private struct RgbModelData {
            public readonly double xr;
            public readonly double yr;
            public readonly double xg;
            public readonly double yg;
            public readonly double xb;
            public readonly double yb;

            public readonly Vec3 RefWhiteRGB;

            public RgbModelData(double xr, double yr, double xg, double yg, double xb, double yb, Vec3 refWhiteRGB) {
                this.xr = xr;
                this.yr = yr;
                this.xg = xg;
                this.yg = yg;
                this.xb = xb;
                this.yb = yb;
                RefWhiteRGB = refWhiteRGB;
            }
        }

        private struct Vec3 {
            public double x;
            public double y;
            public double z;

            public Vec3(double x, double y, double z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        private struct Matrix3x3 {
            public double m00;
            public double m01;
            public double m02;
            public double m10;
            public double m11;
            public double m12;
            public double m20;
            public double m21;
            public double m22;

            public void Transpose() {
                double v = m01; m01 = m10; m10 = v;
                v = m02; m02 = m20; m20 = v;
                v = m12; m12 = m21; m21 = v;
            }

            public double Determinant() {
                double fDet = 
                    m00 * (m22 * m11 - m21 * m12) -
                    m10 * (m22 * m01 - m21 * m02) +
                    m20 * (m12 * m01 - m11 * m02);
                return fDet;
            }

            public Matrix3x3 Inverse() {
                double fScale = 1.0 / Determinant();

                Matrix3x3 i = new Matrix3x3();

                i.m00 = fScale * (m22 * m11 - m21 * m12);
                i.m01 = -fScale * (m22 * m01 - m21 * m02);
                i.m02 = fScale * (m12 * m01 - m11 * m02);

                i.m10 = -fScale * (m22 * m10 - m20 * m12);
                i.m11 = fScale * (m22 * m00 - m20 * m02);
                i.m12 = -fScale * (m12 * m00 - m10 * m02);

                i.m20 = fScale * (m21 * m10 - m20 * m11);
                i.m21 = -fScale * (m21 * m00 - m20 * m01);
                i.m22 = fScale * (m11 * m00 - m10 * m01);

                return i;
            }
        }

        #endregion ------------------------------------------------------------

    }

}
