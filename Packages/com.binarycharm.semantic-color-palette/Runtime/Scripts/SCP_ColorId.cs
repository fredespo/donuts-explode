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

namespace BinaryCharm.SemanticColorPalette {

    // note: if/when C# 10 will be available in Unity, it might work to just do
    // global using SCP_ColorId = System.Int32;

    /// <summary>
    /// An identifier for a color defined in a SCP_Palette. Simple int wrapper
    /// (implicit conversions operators provided) with two static constants
    /// for the reserved 0 and -1 values, improving readability.
    /// </summary>
    [Serializable]
    public struct SCP_ColorId {

        [SerializeField] internal int m_id;

        /// <summary>
        /// Reserved value that tells the system to skip coloring an item
        /// </summary>
        public static SCP_ColorId DO_NOT_APPLY = 0;

        /// <summary>
        /// Reserved value that indicates an invalid identifier
        /// </summary>
        public static SCP_ColorId INVALID = -1;

        /// <summary>
        /// Constructor accepting the `int` that will be wrapped.
        /// </summary>
        /// <param name="id">The `int`value to wrap.</param>
        public SCP_ColorId(int id) {
            m_id = id;
        }

        /// <summary>
        /// Implicit conversion from int.
        /// </summary>
        /// <param name="i">The int to convert.</param>
        public static implicit operator SCP_ColorId(int i) {
            return new SCP_ColorId(i);
        }

        /// <summary>
        /// Implicit conversion to int.
        /// </summary>
        /// <param name="s">The color id to convert.</param>
        public static implicit operator int(SCP_ColorId s) {
            return s.m_id;
        }

    }

}
