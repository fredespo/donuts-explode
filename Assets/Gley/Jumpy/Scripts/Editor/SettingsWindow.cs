using Gley.Common;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.Jumpy.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;

        private Vector2 scrollPosition;
        private static bool enablePlugins;
        private string[] packagesToImport = new string[] { "com.unity.purchasing", "com.unity.ads", "com.unity.mobile.notifications" };
        private int packageIndex;
        private int step;
        private bool installing;

        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder);
        }

        private void OnEnable()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties());
            }
#if GLEY_JUMPY
            enablePlugins = true;
#else
            enablePlugins = false;
#endif
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));
            EditorStyles.label.wordWrap = true;

            EditorGUILayout.LabelField("IMPORTANT:", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("If you want to use the complete Jumpy project, tick the box bellow to enable and import all required packages");
            enablePlugins = EditorGUILayout.Toggle("Enable Jumpy Plugins", enablePlugins);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("If you want to use just the plugins in a new game, delete the entire folder: \"Jumpy\"");
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Save"))
            {
                if (enablePlugins)
                {
                    if (EditorUtility.DisplayDialog("Are you sure?", "This will replace all your settings from Settings Window for all packages. You cannot undo it", "Yes (Load)", "Cancel"))
                    {
                        step = 0;
                        EnableJumpy();
                    }
                }
                else
                {
                    DisableJumpy();
                }
            }
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.gameScene}");
            }
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }

        private void OnInspectorUpdate()
        {
            if (installing == true)
            {
                if (EditorApplication.isCompiling == false)
                {
                    //InstallPlugins();
                    EnableJumpy();
                }
            }
        }

        void EnableJumpy()
        {
            installing = false;
            Debug.Log($"Installing step {step + 1}/6");
            switch (step)
            {
                case 0:
                    PreprocessorDirective.AddToCurrent(Gley.AllPlatformsSave.Internal.SupportedSaveMethods.JSONSerializationFileSave.ToString(), false);
                    installing = true;
                    step++;
                    break;
                case 1:
                    CopySettings();
                    PreprocessorDirective.AddToCurrent(Localization.Editor.SettingsWindowProperties.GLEY_LOCALIZATION, false);
                    PreprocessorDirective.AddToCurrent(DailyRewards.Editor.SettingsWindowProperties.GLEY_DAILY_REWARDS, false);

                    installing = true;
                    step++;
                    break;
                case 2:
                    InstallPlugins();
                    break;
                case 3:
#if UNITY_ANDROID
                    PreprocessorDirective.AddToCurrent(EasyIAP.Editor.SettingsWindowProperties.GLEY_IAP_GOOGLEPLAY, false);
                    PreprocessorDirective.AddToCurrent(MobileAds.Editor.SettingsWindowProperties.GLEY_UNITYADS, false);
#endif
                    installing = true;
                    step++;
                    break;
                case 4:
                    PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_JUMPY, false);
                    installing = true;
                    step++;
                    break;
                default:
                    AssetDatabase.Refresh();
                    Debug.Log("Done");
                    break;

            }
        }

        void DisableJumpy()
        {
            Localization.Editor.SettingsWindowProperties localizationProperties = new Localization.Editor.SettingsWindowProperties();
            File.Delete($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.LANGUAGES_FILEMANE}");
            File.Delete($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.LANGUAGES_FILEMANE}.meta");
            File.Delete($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.WORDID_FILEMANE}");
            File.Delete($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.WORDID_FILEMANE}.meta");

            DailyRewards.Editor.SettingsWindowProperties dailyProperties = new DailyRewards.Editor.SettingsWindowProperties();
            File.Delete($"{Application.dataPath}/{dailyProperties.ParentFolder}/{dailyProperties.FolderName}/{DailyRewards.Internal.Constants.TIMER_IDS_LOCATION}");
            File.Delete($"{Application.dataPath}/{dailyProperties.ParentFolder}/{dailyProperties.FolderName}/{DailyRewards.Internal.Constants.TIMER_IDS_LOCATION}.meta");

            EasyIAP.Editor.SettingsWindowProperties iapProperties = new EasyIAP.Editor.SettingsWindowProperties();
            File.Delete($"{Application.dataPath}/{iapProperties.ParentFolder}/{iapProperties.FolderName}/{EasyIAP.Internal.Constants.PRODUCT_NAMES_FILE}");
            File.Delete($"{Application.dataPath}/{iapProperties.ParentFolder}/{iapProperties.FolderName}/{EasyIAP.Internal.Constants.PRODUCT_NAMES_FILE}.meta");
            PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_JUMPY, true);
            PreprocessorDirective.AddToCurrent(Gley.AllPlatformsSave.Internal.SupportedSaveMethods.JSONSerializationFileSave.ToString(), true);
            PreprocessorDirective.AddToCurrent(Localization.Editor.SettingsWindowProperties.GLEY_LOCALIZATION, true);
            PreprocessorDirective.AddToCurrent(DailyRewards.Editor.SettingsWindowProperties.GLEY_DAILY_REWARDS, true);
#if UNITY_ANDROID
            PreprocessorDirective.AddToCurrent(EasyIAP.Editor.SettingsWindowProperties.GLEY_IAP_GOOGLEPLAY, true);
            PreprocessorDirective.AddToCurrent(MobileAds.Editor.SettingsWindowProperties.GLEY_UNITYADS, true);
#endif
            AssetDatabase.Refresh();
        }


        private void InstallPlugins()
        {
            installing = false;
            if (packageIndex < packagesToImport.Length)
            {
                Debug.Log($"Install Package {packagesToImport[packageIndex]}");
                Gley.Common.ImportRequiredPackages.ImportPackage(packagesToImport[packageIndex], CompleteMethod);
                packageIndex++;
            }
            else
            {
                step++;
                installing = true;
            }
        }

        private void CompleteMethod(string arg0)
        {
            if (arg0 != "InProgress")
            {
                Debug.Log(arg0);
            }
            if (arg0.Contains("Installed"))
            {
                Debug.Log("Install Next");
                installing = true;
            }
        }

        void CopySettings()
        {
            Jumpy.Editor.SettingsWindowProperties jumpyProperties = new Jumpy.Editor.SettingsWindowProperties();

            //copy save settings
            AllPlatformsSave.Editor.SettingsWindowProperties saveProperties = new AllPlatformsSave.Editor.SettingsWindowProperties();
            string from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{AllPlatformsSave.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            string resources = $"Assets/{saveProperties.ParentFolder}/{saveProperties.FolderName}/{AllPlatformsSave.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            string to = $"{resources}/{AllPlatformsSave.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            //copy cross promo
            CrossPromo.Editor.SettingsWindowProperties promoProperties = new CrossPromo.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{CrossPromo.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{promoProperties.ParentFolder}/{promoProperties.FolderName}/{CrossPromo.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{CrossPromo.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }
            //daily rewards
            DailyRewards.Editor.SettingsWindowProperties dailyProperties = new DailyRewards.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{DailyRewards.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{dailyProperties.ParentFolder}/{dailyProperties.FolderName}/{DailyRewards.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{DailyRewards.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            string text = "namespace Gley.DailyRewards\r\n{\r\n\tpublic enum TimerButtonIDs\r\n\t{\r\n\t\tRewardButton,\r\n\t}\r\n}";
            File.WriteAllText($"{Application.dataPath}/{dailyProperties.ParentFolder}/{dailyProperties.FolderName}/{DailyRewards.Internal.Constants.TIMER_IDS_LOCATION}", text);

            //copy easy IAP
            EasyIAP.Editor.SettingsWindowProperties iapProperties = new EasyIAP.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{EasyIAP.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{iapProperties.ParentFolder}/{iapProperties.FolderName}/{EasyIAP.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{EasyIAP.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            text = "namespace Gley.EasyIAP\r\n{\r\n\tpublic enum ShopProductNames\r\n\t{\r\n\t\tRemoveAds,\r\n\t}\r\n}";
            File.WriteAllText($"{Application.dataPath}/{iapProperties.ParentFolder}/{iapProperties.FolderName}/{EasyIAP.Internal.Constants.PRODUCT_NAMES_FILE}", text);

            //copy achievements
            GameServices.Editor.SettingsWindowProperties achProperties = new GameServices.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{GameServices.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{achProperties.ParentFolder}/{achProperties.FolderName}/{GameServices.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{GameServices.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            //copy localization
            Localization.Editor.SettingsWindowProperties localizationProperties = new Localization.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{Localization.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{Localization.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{Localization.Internal.Constants.LOCALIZATION_FILE}.json";
            resources = $"Assets/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{Localization.Internal.Constants.LOCALIZATION_FILE}.json";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            text = "namespace Gley.Localization\r\n{\r\n\tpublic enum WordIDs\r\n\t{\r\n\t\tAdsID = 9,\r\n\t\tBreakID = 11,\r\n\t\tClaimID = 13,\r\n\t\tCloseID = 12,\r\n\t\tHighscoreID = 4,\r\n\t\tLevelCompleteID = 5,\r\n\t\tPlayID = 0,\r\n\t\tPurchasesID = 2,\r\n\t\tRemoveID = 8,\r\n\t\tRestartID = 7,\r\n\t\tRestoreID = 1,\r\n\t\tScoreID = 3,\r\n\t\tSubmitID = 6,\r\n\t\tTapID = 10,\r\n\t}\r\n}";
            File.WriteAllText($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.WORDID_FILEMANE}", text);

            text = "namespace Gley.Localization\r\n{\r\n\tpublic enum SupportedLanguages\r\n\t{\r\n\t\tEnglish=10,\r\n\t\tFrench=14,\r\n\t\tSpanish=34,\r\n\t}\r\n}";
            File.WriteAllText($"{Application.dataPath}/{localizationProperties.ParentFolder}/{localizationProperties.FolderName}/{Localization.Internal.Constants.LANGUAGES_FILEMANE}", text);

            //copy ads
            MobileAds.Editor.SettingsWindowProperties adsProperties = new MobileAds.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{MobileAds.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{adsProperties.ParentFolder}/{adsProperties.FolderName}/{MobileAds.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{MobileAds.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{MobileAds.Internal.Constants.DATA_NAME_EDITOR}.asset";
            resources = $"Assets/{adsProperties.ParentFolder}/{adsProperties.FolderName}/{MobileAds.Internal.Constants.DATA_PATH_EDITOR}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{MobileAds.Internal.Constants.DATA_NAME_EDITOR}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            //copy notifications
            Notifications.Editor.SettingsWindowProperties notifProperties = new Notifications.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{Notifications.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{notifProperties.ParentFolder}/{notifProperties.FolderName}/{Notifications.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{Notifications.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

            //copy rate
            RateGame.Editor.SettingsWindowProperties rateProperties = new RateGame.Editor.SettingsWindowProperties();
            from = $"Assets/{jumpyProperties.ParentFolder}/{jumpyProperties.FolderName}/{SettingsWindowProperties.SETTINGS}/{RateGame.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            resources = $"Assets/{rateProperties.ParentFolder}/{rateProperties.FolderName}/{RateGame.Internal.Constants.RESOURCES_FOLDER}";
            EditorUtilities.CreateFolder(resources);
            to = $"{resources}/{RateGame.Internal.Constants.DATA_NAME_RUNTIME}.asset";
            if (!AssetDatabase.CopyAsset(from, to))
            {
                Debug.LogError($"Fail to copy asset {from}");
            }

        }
    }
}