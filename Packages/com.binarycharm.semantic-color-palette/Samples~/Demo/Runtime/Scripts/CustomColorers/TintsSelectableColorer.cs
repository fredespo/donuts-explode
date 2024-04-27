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
using BinaryCharm.SemanticColorPalette;
using BinaryCharm.SemanticColorPalette.Colorers;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomColorers {

    public class TintsSelectableColorer : SCP_AColorer {

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            SCP_ColorId colorId = GetColorId();
            if (colorId == SCP_ColorId.DO_NOT_APPLY) return;

            Color c = rPalette.GetColor(colorId);
            Selectable rSelectable = GetComponent<Selectable>();

            ColorBlock cb = rSelectable.colors;

            cb.normalColor = c.getTint(0f);
            cb.highlightedColor = c.getTint(0.2f);
            cb.selectedColor = c.getTint(0.4f);
            cb.pressedColor = c.getTint(0.6f);
            cb.disabledColor = c.getTint(0.8f);

            rSelectable.colors = cb;
        }

    }

}
