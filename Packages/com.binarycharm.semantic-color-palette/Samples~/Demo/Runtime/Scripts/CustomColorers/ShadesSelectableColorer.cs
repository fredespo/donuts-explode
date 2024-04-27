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
using UnityEngine.UI;

using BinaryCharm.Common.Extensions;
using BinaryCharm.SemanticColorPalette.Colorers;
using BinaryCharm.SemanticColorPalette;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomColorers {

    public class ShadesSelectableColorer : SCP_AColorer {

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            SCP_ColorId colorId = GetColorId();
            if (colorId == SCP_ColorId.DO_NOT_APPLY) return;

            Color c = rPalette.GetColor(colorId);
            Selectable rSelectable = GetComponent<Selectable>();

            ColorBlock cb = rSelectable.colors;
            cb.normalColor = c.getShade(0f);
            cb.highlightedColor = c.getShade(0.2f);
            cb.selectedColor = c.getShade(0.4f);
            cb.pressedColor = c.getShade(0.6f);
            cb.disabledColor = c.getShade(0.8f);

            rSelectable.colors = cb;
        }

    }

}
