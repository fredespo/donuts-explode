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

#if TEXTMESHPRO_PRESENT

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using UnityEngine;

using TMPro;

namespace BinaryCharm.SemanticColorPalette.Colorers.TMPro {

    [AddComponentMenu("Semantic Color Palette/Text (TextMeshPro) Rich Text Colorer")]
    [RequireComponent(typeof(TMP_Text))]
    public class SCP_TMP_RichTextColorer : SCP_AColorerBase {

        [SerializeField] internal string m_sUnprocessedText = null;

        private ColorableText m_rColorableText = null;

        #region SCP_AColorerBase ----------------------------------------------

        protected override void applyPalette(SCP_IPaletteCore rPalette) {
            ColorableText rCT = getColorableText();
            rCT.applyPalette(rPalette);
            GetComponent<TMP_Text>().SetText(rCT.getText());
        }

        #endregion ------------------------------------------------------------

        private void OnValidate() {
            m_rColorableText = new ColorableText(getText()); // text might be changed
            refresh();
        }

        public void SetText(in string sText) {
            m_sUnprocessedText = sText;
            m_rColorableText = new ColorableText(m_sUnprocessedText);
            refresh();
        }

        private string getText() {
            if (m_sUnprocessedText == null) { // component has just been added
                m_sUnprocessedText = GetComponent<TMP_Text>().text;
            }
            return m_sUnprocessedText;
        }

        private ColorableText getColorableText() {
            if (m_rColorableText == null) {
                m_rColorableText = new ColorableText(getText());
            }
            return m_rColorableText;
        }

        public static void SetText(SCP_IPaletteCore rPalette, TMP_Text rText, in string sText) {
            ColorableText rCT = new ColorableText(sText);
            rCT.applyPalette(rPalette);
            rText.SetText(rCT.getText());
        }

        #region ColorableText private utility class ---------------------------

        private class ColorableText {

            private class ColorSubstitution {
                // We store string color identifiers and not SCP_ColorId values to
                // correctly handle runtime palette changes, because the same
                // color names in the rich text might be resolved to  different
                // SCP_ColorId values in different SCP_Palette instances.
                public readonly string sColorName;

                // Where to replace the RGB value in the string buffer?
                public readonly int iStrBufIndex;

                public ColorSubstitution(string sColorName, int iStrBufIndex) {
                    this.sColorName = sColorName;
                    this.iStrBufIndex = iStrBufIndex;
                }
            }
          
            // These color names are handled natively by TextMeshPro, so if
            // we find them in a color tag we skip them (and so not treat them 
            // as color identifiers for a SCP_Palette).
            private static HashSet<string> s_TextMeshProHandledColors = new HashSet<string>() {
                "black", "blue", "green", "orange", "purple", "red", "white", "yellow"
            };

            private StringBuilder m_rProcessedText;
            private List<ColorSubstitution> m_rColorSubstitutions;
            private string m_sOutputText;

            public ColorableText(string sUnprocessedText) {
                m_rProcessedText = new StringBuilder();
                m_rColorSubstitutions = new List<ColorSubstitution>();

                const string sPATTERN = "<color=\"?[^#][^>]*>";
                const string sPLACEHOLDER_PREFIX = "<color=#";
                const string sPLACEHOLDER = sPLACEHOLDER_PREFIX + "000000>";

                MatchCollection rMatches = Regex.Matches(sUnprocessedText, sPATTERN);

                int iLastIndex = 0;
                foreach (Match rMatch in rMatches) {
                    int iMatchStart = rMatch.Index;
                    string sFragment = sUnprocessedText.Substring(iLastIndex, iMatchStart - iLastIndex);
                    iLastIndex += sFragment.Length + rMatch.Length;

                    m_rProcessedText.Append(sFragment);

                    string sColorId = rMatch.Value.Substring(7).Trim(new char[] { '"', '>' });

                    string sToAppend;
                    if (s_TextMeshProHandledColors.Contains(sColorId)) {
                        // leave untouched: color name handled natively by TMPro
                        sToAppend = rMatch.Value; 
                    }
                    else {
                        // insert placeholder that will be replaced by a HTML RGB value
                        sToAppend = sPLACEHOLDER;
                        ColorSubstitution rCS = new ColorSubstitution(
                            sColorId, 
                            m_rProcessedText.Length + sPLACEHOLDER_PREFIX.Length
                        );
                        m_rColorSubstitutions.Add(rCS);
                    }

                    m_rProcessedText.Append(sToAppend);
                }
                m_rProcessedText.Append(sUnprocessedText.Substring(iLastIndex));
            }

            public void applyPalette(SCP_IPaletteCore rPalette) {
                foreach (ColorSubstitution cs in m_rColorSubstitutions) {
                    SCP_ColorId colorId = rPalette.GetColorIdByName(cs.sColorName);
                    Color color = rPalette.GetColor(colorId);

                    string sColorString = ColorUtility.ToHtmlStringRGB(color);
                    for (int i = 0; i < sColorString.Length; ++i) {
                        m_rProcessedText[cs.iStrBufIndex + i] = sColorString[i];
                    }
                }

                string sProcessedText = m_rProcessedText.ToString();

                // https://forum.unity.com/threads/programmatically-set-text-doesnt-format-by-itself.486198/
                m_sOutputText = Regex.Unescape(sProcessedText); 
            }

            public string getText() {
                return m_sOutputText;
            }

        }

        #endregion ------------------------------------------------------------

    }

}

#endif
