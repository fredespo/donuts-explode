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

using Sys = System;

namespace BinaryCharm.Common.System {

    /// <summary>
    /// Class similar to System.BitConverter, but interpreting byte arrays
    /// given as input to its methods as big-endian values. 
    /// Does not implement all the functionalites of System.BitConverter, 
    /// but is useful when parsing binary file formats with values memorized 
    /// in big-endian, as the ACO/ASE formats.
    /// </summary>
    public static class BigEndianBitConverter {

        public static ushort ToUInt16(byte[] rBuf, int offset) {
            ushort ret = (ushort)(
                  (rBuf[offset] << 8)
                | (rBuf[offset + 1])
            );
            return ret;
        }

        public static short ToInt16(byte[] rBuf, int offset) {
            short ret = (short)(
                  (rBuf[offset] << 8)
                | (rBuf[offset + 1])
            );
            return ret;
        }

        public static uint ToUInt32(byte[] rBuf, int offset) {
            uint ret = (uint)(
                  (rBuf[offset] << 24)
                | (rBuf[offset + 1] << 16)
                | (rBuf[offset + 2] << 8)
                | (rBuf[offset + 3])
            );
            return ret;
        }

        public static int ToInt32(byte[] rBuf, int offset) {
            int ret = (int)(
                  (rBuf[offset] << 24)
                | (rBuf[offset + 1] << 16)
                | (rBuf[offset + 2] << 8)
                | (rBuf[offset + 3])
            );
            return ret;
        }

        public static ulong ToUInt64(byte[] rBuf, int offset) {
            ulong ret = (ulong)(
                  (rBuf[offset] << 56)
                | (rBuf[offset + 1] << 48)
                | (rBuf[offset + 2] << 40)
                | (rBuf[offset + 3] << 32)
                | (rBuf[offset + 4] << 24)
                | (rBuf[offset + 5] << 16)
                | (rBuf[offset + 6] << 8)
                | (rBuf[offset + 7])
            );
            return ret;
        }

        public static long ToInt64(byte[] rBuf, int offset) {
            long ret = (long)(
                  (rBuf[offset] << 56)
                | (rBuf[offset + 1] << 48)
                | (rBuf[offset + 2] << 40)
                | (rBuf[offset + 3] << 32)
                | (rBuf[offset + 4] << 24)
                | (rBuf[offset + 5] << 16)
                | (rBuf[offset + 6] << 8)
                | (rBuf[offset + 7])
            );
            return ret;
        }

        public static float ToSingle(byte[] rBuf, int offset) {
            byte[] rLittleEndian = new byte[] {
                rBuf[offset+3], rBuf[offset+2], rBuf[offset+1], rBuf[offset],
            };
            float ret = Sys.BitConverter.ToSingle(rLittleEndian, 0);
            return ret;
        }

        public static double ToDouble(byte[] rBuf, int offset) {
            byte[] rLittleEndian = new byte[] {
                rBuf[offset+7], rBuf[offset+6], rBuf[offset+5], rBuf[offset+4],
                rBuf[offset+3], rBuf[offset+2], rBuf[offset+1], rBuf[offset],
            };
            double ret = Sys.BitConverter.ToDouble(rLittleEndian, 0);
            return ret;
        }

    }

}
