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
    public struct SCP_SelectableColorsDef {
        public SCP_ColorId normal;
        public SCP_ColorId highlighted;
        public SCP_ColorId pressed;
        public SCP_ColorId selected;
        public SCP_ColorId disabled;
    }

    [AddComponentMenu("Semantic Color Palette/Selectable Colorer")]
    [RequireComponent(typeof(Selectable))]
    public class SCP_SelectableColorer : SCP_AColorer<SCP_SelectableColorsDef> {

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore p) {
            Selectable rSelectable = GetComponent<Selectable>();
            applyPalette(p, rSelectable, m_colors);
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, Selectable rSelectable, in SCP_SelectableColorsDef colorIds) {
            ColorBlock cb = rSelectable.colors;

            if (colorIds.normal != SCP_ColorId.DO_NOT_APPLY) {
                cb.normalColor = rPalette.GetColor(colorIds.normal);
            }
            if (colorIds.highlighted != SCP_ColorId.DO_NOT_APPLY) {
                cb.highlightedColor = rPalette.GetColor(colorIds.highlighted);
            }
            if (colorIds.selected != SCP_ColorId.DO_NOT_APPLY) {
                cb.selectedColor = rPalette.GetColor(colorIds.selected);
            }
            if (colorIds.pressed != SCP_ColorId.DO_NOT_APPLY) {
                cb.pressedColor = rPalette.GetColor(colorIds.pressed);
            }
            if (colorIds.disabled != SCP_ColorId.DO_NOT_APPLY) {
                cb.disabledColor = rPalette.GetColor(colorIds.disabled);
            }

            rSelectable.colors = cb;
        }
    }

}
