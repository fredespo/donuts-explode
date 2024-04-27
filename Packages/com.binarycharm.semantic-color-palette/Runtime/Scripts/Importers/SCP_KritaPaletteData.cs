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

using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

using BinaryCharm.SemanticColorPalette.Utils;
using BinaryCharm.SemanticColorPalette.Importers.Krita;

namespace BinaryCharm.SemanticColorPalette.Importers {

    /// <summary>
    /// Krita palette (.kpl) data parser.
    /// Used as reference:
    /// https://docs.krita.org/en/untranslatable_pages/kpl_defintion.html
    /// </summary>
    public class SCP_KritaPaletteData : SCP_APaletteData {

        public SCP_KritaPaletteData(byte[] rKplData) {

            Stream data = new MemoryStream(rKplData);
            ZipArchive archive = new ZipArchive(data);

            //string mimetype = new StreamReader(archive.GetEntry("mimetype").Open()).ReadToEnd();
            //bool bValidMimetype = mimetype == "krita/x-colorset";
            //worth throwing an exception if !bValidMimetype?

            StreamReader colorSetStreamReader = new StreamReader(archive.GetEntry("colorset.xml").Open());

            XmlSerializer serializer = new XmlSerializer(typeof(ColorSet));
            ColorSet rColorSet = serializer.Deserialize(colorSetStreamReader) as ColorSet;

            foreach(ColorSetEntry rCS in rColorSet.colorSetEntries) {
                addUniqueColorDef(rCS.Name, rCS.def.getColor());
            }
            foreach (Group rGrp in rColorSet.groupEntries) {
                foreach(ColorSetEntry rCS in rGrp.entries) {
                    addUniqueColorDef(rGrp.Name + "." + rCS.Name, rCS.def.getColor());
                }
            }
        }

    }

}


#region Implementation details -------------------------------------------------

namespace BinaryCharm.SemanticColorPalette.Importers.Krita {

    [XmlRoot(ElementName = "ColorSet")]
    public class ColorSet {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "comment")]
        public string Comment { get; set; }

        [XmlAttribute(AttributeName = "columns")]
        public int Columns { get; set; }

        [XmlAttribute(AttributeName = "rows")]
        public int Rows { get; set; }

        [XmlAttribute(AttributeName = "readonly")]
        public bool Readonly { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string version { get; set; }

        [XmlElement(ElementName = "Group")]
        public List<Group> groupEntries;

        [XmlElement(ElementName = "ColorSetEntry")]
        public List<ColorSetEntry> colorSetEntries;
    }

    [XmlRoot(ElementName = "Group")]
    public class Group {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "rows")]
        public int Rows { get; set; }

        [XmlElement(ElementName = "ColorSetEntry")]
        public List<ColorSetEntry> entries;
    }

    [XmlRoot(ElementName = "ColorSetEntry")]
    public class ColorSetEntry {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        public enum eBitDepth {
            U8,
            U16,
            F16,
            F32
        };

        [XmlAttribute(AttributeName = "bitdepth")]
        public eBitDepth BitDepth { get; set; }

        [XmlAttribute(AttributeName = "spot")]
        public bool Spot { get; set; }

        [XmlElement(ElementName = "Position")]
        public Position Position { get; set; }

        [XmlElement(typeof(RGB), ElementName = "RGB")]
        [XmlElement(typeof(CMYK), ElementName = "CMYK")]
        public ACreateSwatchDef def;
    }

    [XmlRoot(ElementName = "Position")]
    public class Position {
        [XmlAttribute(AttributeName = "row")]
        public int Row { get; set; }

        [XmlAttribute(AttributeName = "column")]
        public int Column { get; set; }
    }

    public abstract class ACreateSwatchDef {
        public abstract Color getColor();
    }

    [XmlRoot(ElementName = "CMYK")]
    public class CMYK : ACreateSwatchDef {
        [XmlAttribute(AttributeName = "space")]
        public string Space { get; set; }

        [XmlAttribute(AttributeName = "c")]
        public double C { get; set; }
        [XmlAttribute(AttributeName = "m")]
        public double M { get; set; }
        [XmlAttribute(AttributeName = "y")]
        public double Y { get; set; }
        [XmlAttribute(AttributeName = "k")]
        public double K { get; set; }

        public override Color getColor() {
            return SCP_ColorUtils.CMYKToRGB((float)C, (float)M, (float)Y, (float)K);
        }
    }

    [XmlRoot(ElementName = "RGB")]
    public class RGB : ACreateSwatchDef {
        [XmlAttribute(AttributeName = "space")]
        public string Space { get; set; }

        [XmlAttribute(AttributeName = "r")]
        public double R { get; set; }

        [XmlAttribute(AttributeName = "g")]
        public double G { get; set; }

        [XmlAttribute(AttributeName = "b")]
        public double B { get; set; }

        public override Color getColor() {
            return new Color((float)R, (float)G, (float)B);
        }
    }

    [XmlRoot(ElementName = "sRGB")]
    public class SRGB : ACreateSwatchDef {
        [XmlAttribute(AttributeName = "r")]
        public double R { get; set; }

        [XmlAttribute(AttributeName = "g")]
        public double G { get; set; }

        [XmlAttribute(AttributeName = "b")]
        public double B { get; set; }

        public override Color getColor() {
            return new Color((float)R, (float)G, (float)B);
        }
    }

    [XmlRoot(ElementName = "Lab")]
    public class Lab : ACreateSwatchDef {
        [XmlAttribute(AttributeName = "space")]
        public string Space { get; set; }

        [XmlAttribute(AttributeName = "L")]
        public double L { get; set; }

        [XmlAttribute(AttributeName = "a")]
        public double a { get; set; }

        [XmlAttribute(AttributeName = "b")]
        public double b { get; set; }

        public override Color getColor() {
            return SCP_ColorUtils.LabToRGB((float)L, (float)a, (float)b);
        }
    }

    [XmlRoot(ElementName = "XYZ")]
    public class XYZ : ACreateSwatchDef {
        [XmlAttribute(AttributeName = "space")]
        public string Space { get; set; }

        [XmlAttribute(AttributeName = "x")]
        public double x { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public double y { get; set; }

        [XmlAttribute(AttributeName = "z")]
        public double z { get; set; }

        public override Color getColor() {
            return SCP_ColorUtils.XyzToRGB((float)x, (float)y, (float)z);
        }
    }
}

#endregion----------------------------------------------------------------------