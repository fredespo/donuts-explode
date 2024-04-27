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

    [RequireComponent(typeof(Image))]
    public class ShadeImageColorer : SCP_AColorer {

        [SerializeField] [Range(0f, 1f)] internal float m_fShadeAmount;
        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            SCP_ColorId colorId = GetColorId();
            if (colorId == SCP_ColorId.DO_NOT_APPLY) return;

            Color c = rPalette.GetColor(colorId);
            Color shadedColor = c.getShade(m_fShadeAmount);

            GetComponent<Graphic>().color = shadedColor;
        }

    }

}
