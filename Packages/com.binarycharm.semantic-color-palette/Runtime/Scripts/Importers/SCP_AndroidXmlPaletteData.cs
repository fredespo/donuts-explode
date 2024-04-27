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
using System.Xml.Serialization;

using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Importers {

    using UColor = UnityEngine.Color;
    using Color = AndroidXml.Color;
    using Resources = AndroidXml.Resources;

    /// <summary>
    /// Parser for Android color XML resource file (usually "colors.xml").
    /// </summary>
    public class SCP_AndroidXmlPaletteData : SCP_APaletteData {

        public SCP_AndroidXmlPaletteData(string sXmlData) {
            using (TextReader reader = new StringReader(sXmlData)) {
                XmlSerializer serializer = new XmlSerializer(typeof(Resources));
                Resources rResources = serializer.Deserialize(reader) as Resources;

                foreach (Color c in rResources.Color) {
                    UColor unityColor;
                    bool bParsedOk = ColorUtility.TryParseHtmlString(c.Text, out unityColor);
                    if (!bParsedOk) throw new System.Exception("invalid xml");
                    addUniqueColorDef(c.Name, unityColor);
                }
            }
        }

    }

}


#region Implementation details -------------------------------------------------

namespace BinaryCharm.SemanticColorPalette.Importers.AndroidXml {

    [XmlRoot(ElementName = "color")]
    public class Color {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "resources")]
    public class Resources {
        [XmlElement(ElementName = "color")]
        public List<Color> Color { get; set; }
    }

}

#endregion----------------------------------------------------------------------
