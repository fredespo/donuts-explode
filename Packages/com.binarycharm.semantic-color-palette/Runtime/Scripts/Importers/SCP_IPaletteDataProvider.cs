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

using System.Collections.Generic;

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// Interface defining the methods providing the minimal amount of data
    /// needed to build a color palette:
    /// - a name for the palette
    /// - a set of name -> color mappings where names are unique identifiers
    /// </summary>
    public interface SCP_IPaletteDataProvider {

        /// <summary>
        /// Returns the palette readable name.
        /// </summary>
        /// <returns>The palette readable name.</returns>
        string getPaletteName();
        
        /// <summary>
        /// Returns the color definitions that make up the palette, a set of
        /// name -> color mappings where names are unique identifiers.
        /// </summary>
        /// <returns>The name-> color mappings, as a dictionary.</returns>
        Dictionary<string, Color> getPaletteData();

    }

}
