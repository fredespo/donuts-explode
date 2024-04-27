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
using System.Text;

using UnityEngine;

using BinaryCharm.Common.System;
using BinaryCharm.SemanticColorPalette.Utils;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// Adobe Color Swatch (.aco) data parser.
    /// Special thanks: http://www.selapa.net/swatches/colors/fileformats.php
    /// </summary>
    public class SCP_AcoPaletteData : SCP_APaletteData {

        private readonly AcoV1Data v1Data;
        private readonly AcoV2Data v2Data;

        public SCP_AcoPaletteData(byte[] rAcoData) {
            int iByteOffset = 0;
            v1Data = null;
            v2Data = null;

            while (iByteOffset < rAcoData.Length) {
                eVersion verValue = (eVersion)BigEndianBitConverter.ToUInt16(rAcoData, iByteOffset);
                switch (verValue) {
                    case eVersion.v1:
                        v1Data = new AcoV1Data(rAcoData, iByteOffset + 2);
                        iByteOffset += v1Data.getBytesSize();
                        break;
                    case eVersion.v2:
                        v2Data = new AcoV2Data(rAcoData, iByteOffset + 2);
                        iByteOffset += v2Data.getBytesSize();
                        break;
                    default:
                        throw new Exception("Unsupported format version");
                }
            }

            if (v2Data != null) {
                for (int i = 0; i < v2Data.numColors; ++i) {
                    addUniqueColorDef(v2Data.colorNames[i].str, v2Data.colorDefinitions[i].getColor());
                }
            }
            else if (v1Data != null) {
                for (int i = 0; i < v1Data.numColors; ++i) {
                    addUniqueColorDef("", v1Data.colorDefinitions[i].getColor());
                }
            }
        }


        #region Private implementation details --------------------------------

        private enum eVersion : ushort {
            v1 = 1,
            v2 = 2
        }

        private enum eColorSpaceId : ushort {
            RGB = 0,
            HSB = 1,
            CMYK = 2,
            Lab = 7,
            Grayscale = 8
        }

        private class ColorData {
            public readonly eColorSpaceId colorSpaceId;
            public readonly ushort[] colorData = new ushort[4];

            public ColorData(byte[] rAcoData, int iByteOffset) {
                uint iColorSpace = BigEndianBitConverter.ToUInt16(rAcoData, iByteOffset);
                iByteOffset += 2;

                try {
                    colorSpaceId = (eColorSpaceId)iColorSpace;
                }
                catch (Exception) {
                    throw new Exception("Unsupported proprietary color space: code " + iColorSpace);
                }

                for (int i = 0; i < 4; ++i) {
                    colorData[i] = BigEndianBitConverter.ToUInt16(rAcoData, iByteOffset);
                    iByteOffset += 2;
                }
            }
            public const int BYTES_SIZE = sizeof(eColorSpaceId) + (4 * sizeof(ushort));

            public Color getColor() {
                float fDiv = (float)ushort.MaxValue;
                switch (colorSpaceId) {
                    case eColorSpaceId.RGB: {
                            float r = colorData[0] / fDiv;
                            float g = colorData[1] / fDiv;
                            float b = colorData[2] / fDiv;
                            return new Color(r, g, b);
                        }
                    case eColorSpaceId.HSB: {

                            float h = colorData[0] / fDiv;
                            float s = colorData[1] / fDiv;
                            float b = colorData[2] / fDiv;
                            return Color.HSVToRGB(h, s, b);
                        }
                    case eColorSpaceId.CMYK: {
                            float c = colorData[0] / fDiv;
                            float m = colorData[1] / fDiv;
                            float y = colorData[2] / fDiv;
                            float k = colorData[3] / fDiv;

                            return SCP_ColorUtils.CMYKToRGB(c, m, y, k);
                        }
                    case eColorSpaceId.Lab: {
                            short chrominance_a = (short)colorData[1];
                            short chrominance_b = (short)colorData[2];

                            float lightness = colorData[0] / 10000f;
                            float a = (chrominance_a + 12800) / 25500f;
                            float b = (chrominance_b + 12800) / 25500f;

                            return SCP_ColorUtils.LabToRGB(lightness, a, b);
                        }
                    case eColorSpaceId.Grayscale: {
                            float f = colorData[0] / 10000f;
                            return SCP_ColorUtils.GreyToRGB(f);
                        }
                    default:
                        throw new Exception("invalid color space");
                }
            }
        }

        private class StringData {

            public readonly uint utfLen;
            public readonly string str;

            internal StringData(byte[] rAcoData, int iByteOffset) {
                utfLen = BigEndianBitConverter.ToUInt32(rAcoData, iByteOffset);
                iByteOffset += 4;

                str = Encoding.BigEndianUnicode.GetString(rAcoData, iByteOffset, 2 * (int)utfLen);
            }

            internal int getBytesSize() {
                return sizeof(uint) + ((int)utfLen * sizeof(ushort));
            }
        }

        private abstract class AcoCommonData {
            internal readonly eVersion version;
            internal readonly ushort numColors;
            internal readonly ColorData[] colorDefinitions;

            protected AcoCommonData(byte[] rAcoData, ref int iByteOffset, eVersion version) {
                this.version = version;

                numColors = BigEndianBitConverter.ToUInt16(rAcoData, iByteOffset);
                iByteOffset += 2;

                colorDefinitions = new ColorData[numColors];
            }

            public int getBytesSize() {
                return sizeof(eVersion) + sizeof(ushort) + (numColors * ColorData.BYTES_SIZE);
            }
        }

        private class AcoV1Data : AcoCommonData {
            public AcoV1Data(byte[] rAcoData, int iByteOffset) : base(rAcoData, ref iByteOffset, eVersion.v1) {
                for (ushort i = 0; i < numColors; ++i) {
                    colorDefinitions[i] = new ColorData(rAcoData, iByteOffset);
                    iByteOffset += ColorData.BYTES_SIZE;
                }
            }
        }

        private class AcoV2Data : AcoCommonData {
            internal readonly StringData[] colorNames;

            public AcoV2Data(byte[] rAcoData, int iByteOffset) : base(rAcoData, ref iByteOffset, eVersion.v2) {
                colorNames = new StringData[numColors];
                for (ushort i = 0; i < numColors; ++i) {
                    colorDefinitions[i] = new ColorData(rAcoData, iByteOffset);
                    iByteOffset += ColorData.BYTES_SIZE;
                    colorNames[i] = new StringData(rAcoData, iByteOffset);
                    iByteOffset += colorNames[i].getBytesSize();
                }
            }

            public new int getBytesSize() {
                int ret = base.getBytesSize();
                for (int i = 0; i < colorNames.Length; ++i) {
                    ret += colorNames[i].getBytesSize();
                }
                return ret;
            }
        }

        #endregion ------------------------------------------------------------
    }

}
