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
using System.Xml.Linq;

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// SVG data parser. Fetches the "fill" values of any "rect" definition.
    /// </summary>
    public class SCP_SvgPaletteData : SCP_APaletteData {

        public SCP_SvgPaletteData(string sSvgData) {
            XDocument rXmlDoc = XDocument.Parse(sSvgData);
            XElement rRootElem = rXmlDoc.Root;
            XNamespace rNamespace = rRootElem.GetDefaultNamespace();

            IEnumerable<XElement> rRectElems = rXmlDoc.Descendants(rNamespace + "rect");
            foreach (XElement rRectElem in rRectElems) {
                string sFill = rRectElem.Attribute("fill").Value;

                Color c;
                bool bParsedOk = ColorUtility.TryParseHtmlString(sFill, out c);
                if (!bParsedOk) {
                    throw new System.Exception("invalid fill value in SVG: " + sFill);
                }

                addUniqueColorDef(null, c);
            }
        }

    }

}
