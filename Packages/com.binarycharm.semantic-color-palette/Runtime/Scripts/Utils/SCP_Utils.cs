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
using System.IO;

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Utils {

    /// <summary>
    /// Static utility class acting on `Palettes`, offering methods related to
    /// data persistence and exchange.
    /// </summary>
    public static class SCP_Utils {

        /// <summary>
        /// Saves <paramref name="rPalette"/> to a JSON file at path 
        /// <paramref name="sFilePath"/>, overwriting if it already exists.
        /// </summary>
        /// 
        /// <param name="rPalette">A @SCP_Palette to save.</param>
        /// <param name="sFilePath">The destination path for the file to be 
        /// written.</param>
        public static void SavePalette(SCP_Palette rPalette, string sFilePath) {
            string sJsonData = JsonUtility.ToJson(rPalette, true);
            File.WriteAllText(sFilePath, sJsonData);
        }

        /// <summary>
        /// Loads a @SCP_Palette from a JSON file previously saved with 
        /// @BinaryCharm.SemanticColorPalette.Utils.SCP_Utils.SavePalette
        /// </summary>
        /// 
        /// <param name="sFilePath">The path of the JSON file to load.</param>
        /// <returns>The @SCP_Palette represented by the JSON file.</returns>
        public static SCP_Palette LoadPalette(string sFilePath) {
            string sTextData = File.ReadAllText(sFilePath);
            return SCP_Palette.CreateByJsonData(sTextData);
        }

        /// <summary>
        /// Extracts the color definitions data from a @SCP_Palette, as a 
        /// dictionary of string -> Color pairs.
        /// </summary>
        /// 
        /// <param name="rPalette">The input @SCP_Palette</param>
        /// <returns>A dictionary of the palette color definitions</returns>
        public static Dictionary<string, Color> ExtractColorDefs(SCP_Palette rPalette) {
            int iNumElems = rPalette.GetNumElems();
            Dictionary<string, Color> rColorDefs = new Dictionary<string, Color>();
            for (int i = 0; i < iNumElems; ++i) {
                string sName = rPalette.GetColorNameByIndex(i);
                Color color = rPalette.GetColorByIndex(i);
                rColorDefs.Add(sName, color);
            }
            return rColorDefs;
        }

        /// <summary>
        /// Utility method that overwrites the color definitions of a 
        /// @SCP_Palette by the definitions passed in input as a string -> Color
        /// dictionary.
        /// </summary>
        /// 
        /// <remarks>
        /// The method only overwrites the color definitions for
        /// which <paramref name="rPalette"/> has a matching identifier.
        /// So, the overwrite can be partial, which can be useful if you want to
        /// "patch" only some colors, or if you are overwriting a palette with
        /// external data saved in the past (before new definitions were added
        /// to the palette). In casse of a partial overwrite, throws an 
        /// exception - if a partial update is expected/desired, catch it.
        /// </remarks>
        /// 
        /// <param name="rPalette">The input @SCP_Palette</param>
        /// <returns>A dictionary of the palette color definitions</returns>
        public static void UpdateColorDefs(SCP_Palette rPalette, Dictionary<string, Color> rColorDefs) {
            int iNumUpdatedDefs = 0;
            foreach (var kv in rColorDefs) {
                string sColorName = kv.Key;
                SCP_ColorId colorId = rPalette.GetColorIdByName(sColorName);
                if (colorId != SCP_ColorId.INVALID) {
                    rPalette.SetColor(colorId, kv.Value);
                    ++iNumUpdatedDefs;
                }
            }
            
            if (iNumUpdatedDefs != rPalette.GetNumElems()) {
                string sError = iNumUpdatedDefs == 0 ?
                    "No color definitions found during import: 0 colors updated" :
                    "Palette imported partially: " + iNumUpdatedDefs + " updated";
                throw new System.Exception("Palette mismatch: " + sError);
            }
        }

    }

}
