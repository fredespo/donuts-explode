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

using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace BinaryCharm.Common.Extensions {

    public static class TMP_TextEx {

        public delegate Color CharVertexColorGetter(int iCharIndex, int iVertIndex);

        public static void SetLinkColor(this TMP_Text rText, int iLinkIndex, Color c) {
            rText.SetLinkCharacterVerticesColors(iLinkIndex, (iChar, iVert) => c);
        }

        public static void SetLinkColor(this TMP_Text rText, int iLinkIndex, List<Color32[]> characterVerticesColors) {
            rText.SetLinkCharacterVerticesColors(iLinkIndex, (iChar, iVert) => characterVerticesColors[iChar][iVert]);
        }

        public static void SetLinkCharacterVerticesColors(this TMP_Text rText, int iLinkIndex, CharVertexColorGetter getColorForCharVertex) {
            //if (iLinkIndex < 0 || iLinkIndex > rText.textInfo.linkCount - 1) return;
            TMP_LinkInfo linkInfo;
            try {
                linkInfo = rText.textInfo.linkInfo[iLinkIndex];
            } catch (IndexOutOfRangeException rEx) {
                // workaround: textInfo.linkInfo[] does not look always
                // always in sync with rText.textInfo.linkCount
                return;
            }

            for (int i = 0; i < linkInfo.linkTextLength; i++) {
                int iCharIndex = linkInfo.linkTextfirstCharacterIndex + i;
                var charInfo = rText.textInfo.characterInfo[iCharIndex];
                int iMeshIndex = charInfo.materialReferenceIndex;
                int iVertIndex = charInfo.vertexIndex;

                Color32[] vertexColors = rText.textInfo.meshInfo[iMeshIndex].colors32;
                if (charInfo.isVisible) {
                    vertexColors[iVertIndex + 0] = getColorForCharVertex(i, iVertIndex + 0);
                    vertexColors[iVertIndex + 1] = getColorForCharVertex(i, iVertIndex + 1);
                    vertexColors[iVertIndex + 2] = getColorForCharVertex(i, iVertIndex + 2);
                    vertexColors[iVertIndex + 3] = getColorForCharVertex(i, iVertIndex + 3);
                }
            }

            // update text geometry
            rText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        public static List<Color32[]> GetLinkCharsVerticesColors(this TMP_Text rText, int iLinkIndex) {
            TMP_LinkInfo linkInfo = rText.textInfo.linkInfo[iLinkIndex];
            List<Color32[]> rRet = new List<Color32[]>(linkInfo.linkTextLength);

            for (int i = 0; i < linkInfo.linkTextLength; i++) {
                int iCharIndex = linkInfo.linkTextfirstCharacterIndex + i;
                var charInfo = rText.textInfo.characterInfo[iCharIndex];
                int iMeshIndex = charInfo.materialReferenceIndex;

                Color32[] vertexColors = rText.textInfo.meshInfo[iMeshIndex].colors32;
                Color32[] vertexColorCopy = new Color32[vertexColors.Length];
                Array.Copy(vertexColors, vertexColorCopy, vertexColors.Length);

                rRet.Add(vertexColorCopy);
            }
            return rRet;
        }

    }

}

#endif