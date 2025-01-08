using Gley.About;
using Gley.Localization.Internal;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Gley.Localization.Editor
{
    public class GoogleTranslation
    {
        private EditorFileLoaded fileLoader;
        private TranslatedWord translatedWord;
        private SupportedLanguages toLanguage;
        private bool translationInProgress;
        private SupportedLanguages defaultLanguage;
        private List<SystemLanguage> supportedLanguages;
        private int currentLanguage = -1;


        public GoogleTranslation(TranslatedWord translatedWord, SupportedLanguages defaultLanguage, List<SystemLanguage> supportedLanguages)
        {
            Debug.Log(supportedLanguages.Count);
            if (translationInProgress == false)
            {
                this.translatedWord = translatedWord;
                this.defaultLanguage = defaultLanguage;
                this.supportedLanguages = supportedLanguages;
                EditorApplication.update = TranslateAll;
            }
        }


        private void TranslateAll()
        {
            if (!translationInProgress)
            {
                for (int i = 0; i < supportedLanguages.Count; i++)
                {
                    if (defaultLanguage.ToString() != supportedLanguages[i].ToString())
                    {
                        if (currentLanguage < i)
                        {
                            Debug.Log("Translate to " + supportedLanguages[i]);
                            currentLanguage = i;
                            translationInProgress = true;
                            TranslateWord(translatedWord.GetWord(defaultLanguage), defaultLanguage, translatedWord, (SupportedLanguages)supportedLanguages[currentLanguage]);
                            break;
                        }
                    }
                }
                if (translationInProgress == false)
                {
                    Debug.Log("Translation Done");
                    EditorApplication.update = null;
                }
            }
            else
            {
                if (fileLoader.IsDone())
                {
                    translationInProgress = false;
                    string result = fileLoader.GetResult();
                    if (result != null)
                    {
                        var N = JSONNode.Parse(result);
                        string translatedText = N[0][0][0];
                        for (int i = 1; i < N[0].Count; i++)
                        {
                            translatedText += " " + N[0][i][0];
                        }
                        if ((int)toLanguage == (int)SystemLanguage.Hebrew || (int)toLanguage == (int)SystemLanguage.Arabic)
                        {
                            translatedText = Reverse(translatedText);
                        }
                        translatedWord.SetWord(translatedText, toLanguage);
                    }
                    else
                    {
                        Debug.LogWarning("Google's Maximum API calls reached. Try again later");
                    }
                }
            }
        }


        /// <summary>
        /// Make a request to Google Translate to translate a word
        /// </summary>
        /// <param name="from">word to translate</param>
        /// <param name="fromLanguage">original language</param>
        /// <param name="translatedWord">translated word</param>
        /// <param name="toLanguage">language to translate in</param>
        public GoogleTranslation(string from, SupportedLanguages fromLanguage, TranslatedWord translatedWord, SupportedLanguages toLanguage)
        {
            if (translationInProgress == false)
            {
                translationInProgress = true;
                EditorApplication.update = MyUpdate;
                TranslateWord(from, fromLanguage, translatedWord, toLanguage);
            }
            else
            {
                Debug.Log("Translation in progress");
            }
        }


        void TranslateWord(string from, SupportedLanguages fromLanguage, TranslatedWord translatedWord, SupportedLanguages toLanguage)
        {
            this.translatedWord = translatedWord;
            fileLoader = new EditorFileLoaded();
            this.toLanguage = toLanguage;
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
              + LanguageCode.GetLanguageCode((SystemLanguage)fromLanguage) + "&tl=" + LanguageCode.GetLanguageCode((SystemLanguage)toLanguage) + "&dt=t&q=" + UnityWebRequest.EscapeURL(from);
            fileLoader.LoadFile(url);
        }


        /// <summary>
        /// Editor update method
        /// </summary>
        private void MyUpdate()
        {
            if (fileLoader.IsDone())
            {
                Debug.Log("Translation Done");
                translationInProgress = false;
                EditorApplication.update = null;
                string result = fileLoader.GetResult();
                if (result != null)
                {
                    JSONNode N = JSONNode.Parse(result);
                    string translatedText = N[0][0][0];
                    for (int i = 1; i < N[0].Count; i++)
                    {
                        translatedText += " " + N[0][i][0];
                    }
                    if ((int)toLanguage == (int)SystemLanguage.Hebrew || (int)toLanguage == (int)SystemLanguage.Arabic)
                    {
                        translatedText = Reverse(translatedText);
                    }
                    translatedWord.SetWord(translatedText, toLanguage);
                }
                else
                {
                    Debug.LogWarning("Google's Maximum API calls reached. Try again later");
                }
            }
        }


        string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
