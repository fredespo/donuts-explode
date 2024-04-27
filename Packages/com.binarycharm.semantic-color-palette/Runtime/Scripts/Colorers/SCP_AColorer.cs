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

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Abstract base class for all colorer implementations using multiple
    /// @BinaryCharm.SemanticColorPalette.SCP_ColorId definitions wrapped in a
    /// struct.
    /// </summary>
    /// 
    /// <typeparam name="T">The struct type containing the appropriate color ids
    /// </typeparam>
    public abstract class SCP_AColorer<T> : SCP_AColorerBase, SCP_IColorer<T> where T : struct {

        [SerializeField] internal T m_colors;

        public T GetColorIds() {
            return m_colors;
        }

        public void SetColorIds(in T colorIds) {
            m_colors = colorIds;
            refresh();
        }

    }

    /// <summary>
    /// Abstract base class for all Colorer implementations using a single
    /// @BinaryCharm.SemanticColorPalette.SCP_ColorId definition.
    /// </summary>
    public abstract class SCP_AColorer : SCP_AColorerBase, SCP_IColorer {

        [SerializeField] internal SCP_ColorId m_color;

        public SCP_ColorId GetColorId() {
            return m_color;
        }

        public void SetColorId(SCP_ColorId colorId) {
            m_color = colorId;
            refresh();
        }

    }

}
