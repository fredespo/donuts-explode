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
    /// Interface to abstract getting and setting a @UnityEngine.Gradient 
    /// property, to minimize code duplication in colorers inheriting from
    /// @BinaryCharm.SemanticColorPalette.Colorers.SCP_AGradientColorer.
    /// </summary>
    public interface SCP_IGradientAccessor {

        /// <summary>
        /// Get the currently set @UnityEngine.Gradient.
        /// </summary>
        /// <returns>The currently set gradient.</returns>
        Gradient GetCurrGradient();

        /// <summary>
        /// Sets the @UnityEngine.Gradient property to 
        /// <paramref name="rGradient"/>.
        /// </summary>
        /// <param name="rGradient">The gradient to set.</param>
        void SetCurrGradient(Gradient rGradient);

    } 

}
