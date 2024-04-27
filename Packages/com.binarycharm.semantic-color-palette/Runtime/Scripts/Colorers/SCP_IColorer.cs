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

namespace BinaryCharm.SemanticColorPalette.Colorers {

    /// <summary>
    /// Interface defining methods to read/write the color ids configuration of 
    /// a colorer. An instance of `T` will contain the color ids needed by the
    /// actual colorer implementation.
    /// </summary>
    /// <typeparam name="T">A type containing the appropriate color ids.</typeparam>
    public interface SCP_IColorer<T> {

        /// <summary>
        /// Sets the colorer configuration.
        /// </summary>
        /// <param name="colorIds">The colorer configuration.</param>
        void SetColorIds(in T colorIds);

        /// <summary>
        /// Gets the colorer configuration.
        /// </summary>
        /// <returns>The colorer configuration.</returns>
        T GetColorIds();

    }

    /// <summary>
    /// Interface defining methods to read/write the color id configuration of 
    /// a simple colorer (that is: a colorer configured through a single color
    /// id).
    /// </summary>
    public interface SCP_IColorer {

        /// <summary>
        /// Sets the colorer configuration.
        /// </summary>
        /// <param name="colorId">A color identifier.</param>
        void SetColorId(SCP_ColorId colorId);

        /// <summary>
        /// Gets the colorer configuration.
        /// </summary>
        /// <returns>A color identifier.</returns>
        SCP_ColorId GetColorId();

    }

}
