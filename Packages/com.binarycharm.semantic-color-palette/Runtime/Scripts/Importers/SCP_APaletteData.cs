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
    /// Abstract base class for importers implementations, that should
    /// - optionally assign a name to m_sName, if the format specifies one
    /// - call addUniqueColorDef for each imported name->color pair 
    /// Takes care of possible color name duplications/missing names.
    /// </summary>
    public abstract class SCP_APaletteData : SCP_IPaletteDataProvider {

        protected string m_sName = "Unnamed Semantic Palette";
        private Dictionary<string, Color> m_colorDefs = new Dictionary<string, Color>();

        public Dictionary<string, Color> getPaletteData() {
            return m_colorDefs;
        }

        public string getPaletteName() {
            return m_sName;
        }

        protected void addUniqueColorDef(string sColorName, Color c) {
            if (string.IsNullOrEmpty(sColorName)) {
                sColorName = "? #" + ColorUtility.ToHtmlStringRGB(c);
            }

            string sBaseColorName = sColorName;
            int iCopyNo = 1;
            while (m_colorDefs.ContainsKey(sColorName)) {
                sColorName = sBaseColorName + " " + iCopyNo;
                ++iCopyNo;
            }
            m_colorDefs[sColorName] = c;
        }

    }

}
