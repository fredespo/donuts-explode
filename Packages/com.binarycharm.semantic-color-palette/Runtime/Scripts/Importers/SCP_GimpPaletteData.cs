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

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// Gimp Palette (.gpl) data parser.
    /// Used as reference:
    /// https://gitlab.gnome.org/GNOME/gimp/-/blob/gimp-2-10/app/core/gimppalette-load.c#L39
    /// </summary>
    public class SCP_GimpPaletteData : SCP_APaletteData {

        private const string sHEADER_LINE = "GIMP Palette";
        private const string sNAME_PREFIX = "Name: ";
        private const string sCOLUMNS_PREFIX = "Columns: ";

        public SCP_GimpPaletteData(string sGplData) {
            StringReader rSR = new StringReader(sGplData);

            int iLineNo = 1;
            string sLine = rSR.ReadLine();
            if (!sLine.StartsWith(sHEADER_LINE)) {
                throw new Exception("Missing magic header.");
            }

            ++iLineNo;
            sLine = rSR.ReadLine();
                
            if (sLine.StartsWith(sNAME_PREFIX)) {
                m_sName = sLine.Substring(sNAME_PREFIX.Length);

                ++iLineNo;
                sLine = rSR.ReadLine();
            } else {
                m_sName = "Unnamed GIMP Palette";
            }

            // skip optional "Columns: " line
            if (sLine.StartsWith(sCOLUMNS_PREFIX)) {
                ++iLineNo;
                sLine = rSR.ReadLine();
            }

            char[] rFieldSplitters = new char[] { ' ', '\t' };
            do {
                if (!sLine.StartsWith("#")) {
                    string[] colorLineTokens = sLine.Split(rFieldSplitters, 
                        StringSplitOptions.RemoveEmptyEntries);
                    if (colorLineTokens.Length < 3) {
                        throw new Exception(
                            "Invalid color definition at line " 
                            + iLineNo + ": "
                            + sLine
                        );
                    }

                    byte r = byte.Parse(colorLineTokens[0]);
                    byte g = byte.Parse(colorLineTokens[1]);
                    byte b = byte.Parse(colorLineTokens[2]);
                    Color c = new Color32(r, g, b, 255);

                    string sColorName = string.Join(" ", colorLineTokens, 3, colorLineTokens.Length - 3);

                    addUniqueColorDef(sColorName, c);
                }
                ++iLineNo;
                sLine = rSR.ReadLine();
            } while (sLine != null);
        }

    }

}
