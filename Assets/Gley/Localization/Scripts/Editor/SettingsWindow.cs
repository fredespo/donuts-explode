using Gley.Common;
using Gley.Localization.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.Localization.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;
        private static string rootWithoutAssets;

        private AllAppTexts allWords;
        private LocalizationData localizationData;
        private List<SystemLanguage> supportedLanguages;
        private Vector2 scrollPosition = Vector2.zero;
        private SystemLanguage languageToAdd;
        private SupportedLanguages defaultLanguage;
        private int currentLanguage;
        private bool enableTMProSupport;
        private bool enableNGUISupport;
        private bool showLanguages;
        private bool usePlaymaker;
        private bool useUVS;

        const int buttonWidth = 70;



        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder, out rootWithoutAssets);
        }


        /// <summary>
        /// Load save values
        /// </summary>
        void OnEnable()
        {
            RefreshWindow();
        }


        void RefreshWindow()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties(), out rootWithoutAssets);
            }

            localizationData = EditorUtilities.LoadOrCreateDataAsset<LocalizationData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);


            allWords = CSVLoader.LoadJson();
            allWords.allText = allWords.allText.OrderBy(cond => cond.ID).ToList();

            defaultLanguage = localizationData.defaultLanguage;
            currentLanguage = localizationData.currentLanguage;
            enableTMProSupport = localizationData.enableTMProSupport;
            enableNGUISupport = localizationData.enableNGUISupport;
            usePlaymaker = localizationData.usePlaymaker;
            useUVS = localizationData.useUVS;
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Save data to asset
        /// </summary>
        private void SaveSettings()
        {
            localizationData.defaultLanguage = defaultLanguage;
            localizationData.currentLanguage = currentLanguage;
            localizationData.enableTMProSupport = enableTMProSupport;
            localizationData.enableNGUISupport = enableNGUISupport;
            localizationData.usePlaymaker = usePlaymaker;
            localizationData.useUVS = useUVS;
            SetPreprocessorDirectives();
            CreateSupportedLanguagesFile();
            CreateWordIDsFile();
            CSVLoader.SaveJson(allWords,rootWithoutAssets);
            EditorUtility.SetDirty(localizationData);
            AssetDatabase.Refresh();
        }


        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height - 220));

            if (supportedLanguages == null)
            {
                supportedLanguages = Enum.GetValues(typeof(SupportedLanguages)).Cast<SystemLanguage>().ToList();
            }

            GUILayout.Label("Enable Visual Scripting Tool:", EditorStyles.boldLabel);
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();

            #region Supported Tools 
            EditorGUILayout.LabelField("Enable support for:", EditorStyles.boldLabel);
            enableTMProSupport = EditorGUILayout.Toggle("TextMeshPro ", enableTMProSupport);
            enableNGUISupport = EditorGUILayout.Toggle("NGUI ", enableNGUISupport);
            EditorGUILayout.Space();
            #endregion

            #region Supported Languages
            EditorGUILayout.LabelField("Active Languages:", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            if (showLanguages)
            {
                if (GUILayout.Button("Hide Languages"))
                {
                    showLanguages = false;
                }
            }
            else
            {
                if (GUILayout.Button("Show Languages"))
                {
                    showLanguages = true;
                }
            }

            if (showLanguages)
            {
                for (int i = 0; i < supportedLanguages.Count; i++)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(supportedLanguages[i].ToString());
                    if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
                    {
                        supportedLanguages.RemoveAt(i);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }

                defaultLanguage = (SupportedLanguages)EditorGUILayout.EnumPopup("Default Language: ", defaultLanguage);
                EditorGUILayout.Space();

                languageToAdd = (SystemLanguage)EditorGUILayout.EnumPopup("New Language: ", languageToAdd);
                if (GUILayout.Button("Add"))
                {
                    supportedLanguages.Add(languageToAdd);
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            #endregion

            #region Game Texts
            EditorGUILayout.LabelField("Game Texts:", EditorStyles.boldLabel);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Default language " + defaultLanguage.ToString(), style);

            for (int i = 0; i < allWords.allText.Count; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                if (allWords.allText[i].folded == false)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.textArea);
                    //myFoldoutStyle.isHeightDependantOnWidth;
                    myFoldoutStyle.stretchWidth = false;
                    myFoldoutStyle.alignment = TextAnchor.MiddleLeft;
                    allWords.allText[i].folded = EditorGUILayout.Foldout(allWords.allText[i].folded, i + ". " + allWords.allText[i].ID);
                    allWords.allText[i].SetWord(EditorGUILayout.TextArea(allWords.allText[i].GetWord(defaultLanguage), GUILayout.MinWidth(300)), defaultLanguage);

                    if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
                    {
                        allWords.allText.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    allWords.allText[i].folded = EditorGUILayout.Foldout(allWords.allText[i].folded, i + ". " + allWords.allText[i].ID);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("ID " + allWords.allText[i].enumID, GUILayout.Width(100));
                    allWords.allText[i].ID = EditorGUILayout.TextField(allWords.allText[i].ID);
                    if (!string.IsNullOrEmpty(allWords.allText[i].ID))
                    {
                        allWords.allText[i].ID = Regex.Replace(allWords.allText[i].ID, @"^[\d-]*\s*", "");
                        allWords.allText[i].ID = Regex.Replace(allWords.allText[i].ID, "[^a-zA-Z0-9._]", "");
                        allWords.allText[i].ID = allWords.allText[i].ID.Replace(" ", "");
                        allWords.allText[i].ID = allWords.allText[i].ID.Trim();
                    }
                    if (GUILayout.Button("Remove", GUILayout.Width(buttonWidth)))
                    {
                        allWords.allText.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                    for (int j = 0; j < supportedLanguages.Count; j++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(supportedLanguages[j].ToString(), GUILayout.Width(100));
                        allWords.allText[i].SetWord(EditorGUILayout.TextArea(allWords.allText[i].GetWord((SupportedLanguages)supportedLanguages[j])), (SupportedLanguages)supportedLanguages[j]);
                        if (GUILayout.Button("Translate", GUILayout.Width(buttonWidth)))
                        {
                            Translate(allWords.allText[i].GetWord(defaultLanguage), defaultLanguage, (SupportedLanguages)supportedLanguages[j], allWords.allText[i]);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    if (GUILayout.Button("Translate All Languages"))
                    {
                        TranslateAll(allWords.allText[i], defaultLanguage, supportedLanguages);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
            EditorGUILayout.Space();

            if (GUILayout.Button("Add Word"))
            {
                allWords.allText.Add(new TranslatedWord(supportedLanguages, true));
                scrollPosition.y = Mathf.Infinity;
            }
            #endregion

            EditorGUILayout.Space();

            if (GUILayout.Button("Import from CSV"))
            {
                var path = EditorUtility.OpenFilePanel("Select .csv file", "", "csv");
                allWords = CSVLoader.LoadCSV(path);

            }
            if (GUILayout.Button("Export to CSV"))
            {
                var path = EditorUtility.SaveFilePanel(
                "Export translations as .csv",
                "",
                "Translations.csv",
                "csv");

                if (path.Length != 0)
                {
                    CSVLoader.SaveCSV(allWords, path);
                }
            }
            EditorGUILayout.Space();
            
            if(GUILayout.Button("Validate"))
            {
                ValidateTranslations();
            }

            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }

            EditorGUILayout.Space();
            GUILayout.Label("Example:", EditorStyles.boldLabel);
            if (GUILayout.Button("Load Example Settings"))
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "This will replace all your settings from Settings Window. You cannot undo it", "Yes (Load)", "Cancel"))
                {
                    SettingsWindowProperties w = new SettingsWindowProperties();
                    if (!AssetDatabase.CopyAsset($"Assets/{w.ParentFolder}/{w.FolderName}/{Internal.Constants.EXAMPE_SETTINGS_FOLDER}/{Internal.Constants.DATA_NAME_RUNTIME}.asset",
                        $"Assets/{w.ParentFolder}/{w.FolderName}/{Internal.Constants.RESOURCES_FOLDER}/{Internal.Constants.DATA_NAME_RUNTIME}.asset"))
                    {
                        Debug.LogError("Fail to copy asset");
                    }
                    else
                    {
                        if (!AssetDatabase.CopyAsset($"Assets/{w.ParentFolder}/{w.FolderName}/{Internal.Constants.EXAMPE_SETTINGS_FOLDER}/{Internal.Constants.LOCALIZATION_FILE}.json",
                        $"Assets/{w.ParentFolder}/{w.FolderName}/{Internal.Constants.RESOURCES_FOLDER}/{Internal.Constants.LOCALIZATION_FILE}.json"))
                        {
                            Debug.LogError("Fail to copy JSON file");
                        }
                        else
                        {
                            RefreshWindow();
                        }
                    }
                }
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Localization Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.localizationExample}");
            }

            if (GUILayout.Button("Open Component Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.componentExample}");
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }

            EditorGUILayout.Space();
        }

       

        private void ValidateTranslations()
        {
            bool success = true;
            for(int i=0;i<allWords.allText.Count;i++)
            {
                for(int j=0;j<allWords.allText[i].translations.Count;j++)
                {
                    if(string.IsNullOrEmpty(allWords.allText[i].translations[j].word))
                    {
                        Debug.LogError(allWords.allText[i].ID + " does not have a valid translation in " + allWords.allText[i].translations[j].language.ToString());
                        success = false;
                    }
                }
            }
            if(success)
            {
                Debug.Log("Validation: Passed");
            }
        }


        /// <summary>
        /// Translate a word using Google Translate
        /// </summary>
        /// <param name="wordToTranslate"></param>
        /// <param name="fromLanguage"></param>
        /// <param name="toLanguage"></param>
        /// <param name="translatedWord"></param>
        private void Translate(string wordToTranslate, SupportedLanguages fromLanguage, SupportedLanguages toLanguage, TranslatedWord translatedWord)
        {
            new GoogleTranslation(wordToTranslate, fromLanguage, translatedWord, toLanguage);
        }


        private void TranslateAll(TranslatedWord translatedWord, SupportedLanguages defaultLanguage, List<SystemLanguage> supportedLanguages)
        {
            new GoogleTranslation(translatedWord, defaultLanguage, supportedLanguages);
        }


        /// <summary>
        /// Generate SupportedLanguages enum based on settings 
        /// </summary>
        private void CreateSupportedLanguagesFile()
        {
            string text =
            "namespace Gley.Localization\n" +
            "{\n" +
            "\tpublic enum SupportedLanguages\n" +
            "\t{\n";
            for (int i = 0; i < supportedLanguages.Count; i++)
            {
                text +="\t\t"+supportedLanguages[i] + "=" + (int)supportedLanguages[i] + ",\n";
            }
            text += "\t}\n";
            text += "}";
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/Scripts/ToUse/SupportedLanguages.cs", text);
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Generate WordID enum based on settings
        /// </summary>
        private void CreateWordIDsFile()
        {
            if (CheckForDuplicates() == true)
            {
                return;
            }
            CreateUniqueEnumIDs();
            string text =
             "namespace Gley.Localization\n" +
            "{\n" +
            "\tpublic enum WordIDs\n" +
            "\t{\n";
            for (int i = 0; i < allWords.allText.Count; i++)
            {
                text +="\t\t"+ allWords.allText[i].ID + " = " + allWords.allText[i].enumID + ",\n";
            }
            text += "\t}\n";
            text += "}";
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/Scripts/ToUse/WordIDs.cs", text);
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Check for duplicate word IDs
        /// </summary>
        /// <returns></returns>
        private bool CheckForDuplicates()
        {
            bool duplicateFound = false;
            for (int i = 0; i < allWords.allText.Count - 1; i++)
            {
                for (int j = i + 1; j < allWords.allText.Count; j++)
                {
                    if (allWords.allText[i].ID == allWords.allText[j].ID)
                    {
                        duplicateFound = true;
                        Debug.LogError("Duplicate id found: " + allWords.allText[i].ID + " in positions " + i + ", " + j);
                    }
                }
            }
            return duplicateFound;
        }


        /// <summary>
        /// Create unique numeric enum id, used for rename
        /// </summary>
        private void CreateUniqueEnumIDs()
        {
            for (int i = 0; i < allWords.allText.Count - 1; i++)
            {
                for (int j = i + 1; j < allWords.allText.Count; j++)
                {
                    if (allWords.allText[i].enumID == allWords.allText[j].enumID)
                    {
                        allWords.allText[j].enumID = CreateNewEnumID();
                    }
                }
            }
        }


        /// <summary>
        /// Generate unique ID
        /// </summary>
        /// <returns></returns>
        private int CreateNewEnumID()
        {
            int enumID = 0;
            while (IDExists(enumID))
            {
                enumID++;
            }
            return enumID;
        }


        /// <summary>
        /// Check for duplicate enum ids
        /// </summary>
        /// <param name="enumID"></param>
        /// <returns></returns>
        private bool IDExists(int enumID)
        {
            for (int i = 0; i < allWords.allText.Count; i++)
            {
                if (enumID == allWords.allText[i].enumID)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Enable/disable support for external tools
        /// </summary>
        private void SetPreprocessorDirectives()
        {
            Gley.Common.PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_LOCALIZATION, false);
            if (enableTMProSupport)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_TMPRO_LOCALIZATION, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_TMPRO_LOCALIZATION, true);
            }

            if(enableNGUISupport)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_NGUI_LOCALIZATION, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_NGUI_LOCALIZATION, true);
            }

            if (usePlaymaker)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_PLAYMAKER_SUPPORT, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_PLAYMAKER_SUPPORT, true);
            }

            if (useUVS)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_UVS_SUPPORT, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_UVS_SUPPORT, true);
            }
        }
    }
}
