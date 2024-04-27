using UnityEngine;

namespace BinaryCharm.SemanticColorPalette {

    /// <summary>
    /// Interface defining the only methods that a colorer should use to 
    /// apply palette colors to its target elements.
    /// </summary>
    public interface SCP_IPaletteCore {

        /// <summary>
        /// Retrieves the color id associated to a color name.
        /// </summary>
        /// <param name="sColorName">Name of the color id to retrieve.</param>
        /// <returns>The color id associated to <paramref name="sName"/></returns>
        SCP_ColorId GetColorIdByName(string sColorName);

        /// <summary>
        /// Retrieves the color value associated to a color id.
        /// </summary>
        /// <param name="colorId">Identifier of the color to retrieve.</param>
        /// <returns>The color associated to <paramref name="colorId"/></returns>
        Color GetColor(SCP_ColorId colorId);
    }

}
