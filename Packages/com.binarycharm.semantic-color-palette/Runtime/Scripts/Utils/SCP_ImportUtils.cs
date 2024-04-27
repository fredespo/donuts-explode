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
using System.IO;

using BinaryCharm.SemanticColorPalette.Importers;

namespace BinaryCharm.SemanticColorPalette.Utils {

    /// <summary>
    /// Static class offering methods to import palettes from third-party 
    /// formats.
    /// </summary>
    public static class SCP_ImportUtils {

        /// <summary>
        /// Creates a @SCP_Palette by loading a .SVG file from 
        /// <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportSvgPalette(string sFilePath) {
            return importTxtPalette<SCP_SvgPaletteData>(sFilePath);
        }

        /// <summary>
        /// Creates a @SCP_Palette by loading a .ACO (Adobe Color Swatch) file
        /// from <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportAcoPalette(string sFilePath) {
            return importBinPalette<SCP_AcoPaletteData>(sFilePath);
        }

        /// <summary>
        /// Creates a @SCP_Palette by loading a .ASE (Adobe Swatch Exchange)
        /// file from <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportAsePalette(string sFilePath) {
            return importBinPalette<SCP_AsePaletteData>(sFilePath);
        }

        /// <summary>
        /// Creates a @SCP_Palette by loading a .GPL (Gimp palette) file from 
        /// <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportGimpPalette(string sFilePath) {
            return importTxtPalette<SCP_GimpPaletteData>(sFilePath);
        }

        /// <summary>
        /// Creates a @SCP_Palette by loading a .KPL (Krita palette) file from 
        /// <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportKritaPalette(string sFilePath) {
            return importBinPalette<SCP_KritaPaletteData>(sFilePath);
        }

        /// <summary>
        /// Creates a @SCP_Palette by loading a .XML (Android color XML 
        /// resource) file from <paramref name="sFilePath"/>
        /// </summary>
        /// <param name="sFilePath">The path of the file to import.</param>
        /// <returns>The @SCP_Palette resulting from the import.</returns>
        public static SCP_Palette ImportAndroidPalette(string sFilePath) {
            return importTxtPalette<SCP_AndroidXmlPaletteData>(sFilePath);
        }

        #region Private core generic methods ----------------------------------

        private static SCP_Palette createByImportData(SCP_IPaletteDataProvider rData, string sFallbackName) {
            return SCP_Palette.CreateMain(rData.getPaletteName(), rData.getPaletteData());
        }

        private static SCP_Palette importBinPalette<T>(string sFilePath) where T : SCP_IPaletteDataProvider {
            byte[] rFileData = File.ReadAllBytes(sFilePath);
            string sFileName = Path.GetFileNameWithoutExtension(sFilePath);

            T rData = (T) Activator.CreateInstance(typeof(T), rFileData);
            return createByImportData(rData, sFileName);
        }

        private static SCP_Palette importTxtPalette<T>(string sFilePath) where T : SCP_IPaletteDataProvider {
            string sFileData = File.ReadAllText(sFilePath);
            string sFileName = Path.GetFileNameWithoutExtension(sFilePath);

            T rData = (T)Activator.CreateInstance(typeof(T), sFileData);
            return createByImportData(rData, sFileName);
        }

        #endregion ------------------------------------------------------------

    }

}
