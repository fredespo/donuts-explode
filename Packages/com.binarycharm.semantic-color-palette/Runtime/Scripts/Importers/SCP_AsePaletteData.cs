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
using System.Collections.Generic;

using UnityEngine;

using BinaryCharm.Common.System;
using BinaryCharm.SemanticColorPalette.Utils;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// Adobe Swatch Exchange (.ase) data parser.
    /// Special thanks: http://www.selapa.net/swatches/colors/fileformats.php
    /// </summary>
    public class SCP_AsePaletteData : SCP_APaletteData {

        private const uint SIGNATURE = 0x41534546; // {'A', 'S', 'E', 'F'}

        public SCP_AsePaletteData(byte[] rAseData) {
            int iByteOffset = 0;

            uint iSignature = BigEndianBitConverter.ToUInt32(rAseData, iByteOffset);
            iByteOffset += 4;

            if (iSignature != SIGNATURE) {
                throw new Exception("Invalide file signature!");
            }

            uint iMajorVer = BigEndianBitConverter.ToUInt16(rAseData, iByteOffset);
            iByteOffset += 2;

            uint iMinorVer = BigEndianBitConverter.ToUInt16(rAseData, iByteOffset);
            iByteOffset += 2;

            uint iNumBlocks = BigEndianBitConverter.ToUInt32(rAseData, iByteOffset);
            iByteOffset += 4;

            BlockData[] rBlockData = new BlockData[iNumBlocks];
            for (int i = 0; i < iNumBlocks; ++i) {
                rBlockData[i] = new BlockData(rAseData, iByteOffset);
                iByteOffset += rBlockData[i].getBytesSize();
            }

            Stack<string> rLabel = new Stack<string>();
            for (int i = 0; i < iNumBlocks; ++i) {
                BlockData rBD = rBlockData[i];
                
                if (rBD.blockType == BlockData.eBlockType.groupStart) {
                    GroupStartBlock rGSB = (GroupStartBlock)rBD.blockContent;
                    rLabel.Push(rGSB.str);
                } 
                if (rBD.blockType == BlockData.eBlockType.groupEnd) {
                    rLabel.Pop();
                }
                if (rBD.blockType == BlockData.eBlockType.colorEntry) {
                    ColorEntryBlock rCEB = (ColorEntryBlock)rBD.blockContent;

                    string[] sStackReverse = rLabel.ToArray();
                    Array.Reverse(sStackReverse);
                    string sLabel = string.Join(".", sStackReverse) + "." + rCEB.str;

                    Color c = Color.magenta;
                    switch (rCEB.colorMode) {
                        case ColorEntryBlock.eColorMode.CMYK:
                            c = SCP_ColorUtils.CMYKToRGB(
                                rCEB.colorValues[0],
                                rCEB.colorValues[1],
                                rCEB.colorValues[2],
                                rCEB.colorValues[3]
                            );
                            break;
                        case ColorEntryBlock.eColorMode.RGB:
                            c = new Color(
                                rCEB.colorValues[0],
                                rCEB.colorValues[1], 
                                rCEB.colorValues[2]
                            );
                            break;
                        case ColorEntryBlock.eColorMode.LAB:
                            c = SCP_ColorUtils.LabToRGB(
                                rCEB.colorValues[0] * 100f,
                                rCEB.colorValues[1],
                                rCEB.colorValues[2]
                            );
                            break;
                        case ColorEntryBlock.eColorMode.Gray:
                            c = SCP_ColorUtils.GreyToRGB(rCEB.colorValues[0]);
                            break;
                        default:
                            break;
                    }
                    addUniqueColorDef(sLabel, c);
                }

            }
        }

        #region Private implementation details --------------------------------
        private interface IBlockContent { }

        private class BlockData {

            public enum eBlockType : ushort {
                groupStart = 0xc001,
                groupEnd = 0xc002,
                colorEntry = 0x0001
            };

            public readonly eBlockType blockType;
            public readonly int blockLength;
            public readonly IBlockContent blockContent;
            public BlockData(byte[] rAseData, int iByteOffset) {
                ushort iBlockType = BigEndianBitConverter.ToUInt16(rAseData, iByteOffset);
                iByteOffset += 2;
                blockType = (eBlockType)iBlockType;

                blockLength = (int) BigEndianBitConverter.ToUInt32(rAseData, iByteOffset);
                iByteOffset += 4;

                switch (blockType) {
                    case eBlockType.groupStart:
                        blockContent = new GroupStartBlock(rAseData, iByteOffset);
                        break;
                    case eBlockType.groupEnd:
                        // do nothing
                        break;
                    case eBlockType.colorEntry:
                        blockContent = new ColorEntryBlock(rAseData, iByteOffset);
                        break;
                    default:
                        break;
                }

            }

            public int getBytesSize() {
                return blockLength + sizeof(ushort) + sizeof(int);
            }
        }

        private class GroupStartBlock : IBlockContent {

            public readonly ushort utfLen;
            public readonly string str;

            public GroupStartBlock(byte[] rAseData, int iByteOffset) {
                utfLen = BigEndianBitConverter.ToUInt16(rAseData, iByteOffset);
                iByteOffset += 2;

                int iNumStringBytesWithNoTerminator = 2 * (int)(utfLen - 1);
                str = Encoding.BigEndianUnicode.GetString(rAseData, iByteOffset, iNumStringBytesWithNoTerminator);
            }

            internal int getBytesSize() {
                return sizeof(ushort) + ((int)utfLen * sizeof(ushort));
            }
        }

        private class ColorEntryBlock : GroupStartBlock {

            public enum eColorMode {
                CMYK = 0x434d594b, // {'C', 'M', 'Y', 'K'}
                RGB = 0x52474220,  // {'R', 'G', 'B', ' '}
                LAB = 0x4C414220,  // {'L', 'A', 'B', ' '}
                Gray = 0x47726179  // {'G', 'r', 'a', 'y'}
            };

            public enum eColorType : ushort {
                global,
                spot,
                normal
            };

            public readonly eColorMode colorMode;
            public readonly float[] colorValues;
            public readonly eColorType colorType;

            public ColorEntryBlock(byte[] rAseData, int iByteOffset) : base(rAseData, iByteOffset) {

                iByteOffset += base.getBytesSize();

                colorMode = (eColorMode) BigEndianBitConverter.ToUInt32(rAseData, iByteOffset);
                iByteOffset += 4;

                switch (colorMode) {
                    case eColorMode.Gray:
                        colorValues = new float[1];
                        colorValues[0] = BigEndianBitConverter.ToSingle(rAseData, iByteOffset);
                        iByteOffset += 4;
                        Debug.Log("Grey: " + colorValues[0]);
                        break;
                    case eColorMode.CMYK:
                        colorValues = new float[4];
                        for (int i = 0; i < 4; ++i) {
                            colorValues[i] = BigEndianBitConverter.ToSingle(rAseData, iByteOffset);
                            iByteOffset += 4;
                        }
                        Debug.Log("CMYK: " 
                            + colorValues[0] + " "
                            + colorValues[1] + " "
                            + colorValues[2] + " "
                            + colorValues[3]);
                        break;
                    case eColorMode.RGB:
                    case eColorMode.LAB:
                        colorValues = new float[3];
                        for (int i = 0; i < 3; ++i) {
                            colorValues[i] = BigEndianBitConverter.ToSingle(rAseData, iByteOffset);
                            iByteOffset += 4;
                        }
                        Debug.Log("RGB/LAB: "
                            + colorValues[0] + " "
                            + colorValues[1] + " "
                            + colorValues[2]);
                        break;
                    default:
                        Debug.Log("unhandled color mode: " + (int)colorMode);
                        break;
                }
                Debug.Log(colorMode);

                colorType = (eColorType) BigEndianBitConverter.ToUInt16(rAseData, iByteOffset);
                iByteOffset += 2;
            }
        }

        #endregion ------------------------------------------------------------

    }

}
