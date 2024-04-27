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

namespace BinaryCharm.SemanticColorPalette.Colorers
{

    /// <summary>
    /// Abstract base class for the colorers operating on elements inheriting 
    /// from @UnityEngine.UI.Graphic.
    /// </summary>
    [RequireComponent(typeof(Graphic))]
    public class SCP_AGraphicColorer : SCP_AColorer
    {

        #region SCP_AColorerBase ----------------------------------------------

        /// <inheritdoc/>
        protected override void applyPalette(SCP_IPaletteCore rPalette)
        {
            Graphic rGraphic = GetComponent<Graphic>();
            applyPalette(rPalette, rGraphic, m_color);
        }

        #endregion ------------------------------------------------------------

        public static void applyPalette(SCP_IPaletteCore rPalette, Graphic rGraphic, SCP_ColorId colorId)
        {
            if (colorId != SCP_ColorId.DO_NOT_APPLY)
            {
                rGraphic.color = rPalette.GetColor(colorId);
            }
        }

    }

}
