using Gley.Common;
using Gley.CrossPromo.Internal;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.CrossPromo.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;
        private static string rootWithoutAssets;

        private CrossPromoData crossPromoSettings;
        private Vector2 scrollPosition = Vector2.zero;
        private PlatformSettings googlePlay;
        private PlatformSettings appStore;
        private int nrOfTimesToShow;
        private bool doNotShowAfterImageClick;
        private bool allowMultipleDisplaysPerSession;
        private bool usePlaymaker;
        private bool useUVS;


        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder, out rootWithoutAssets);
        }


        private void OnEnable()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties(), out rootWithoutAssets);
            }

            crossPromoSettings = EditorUtilities.LoadOrCreateDataAsset<CrossPromoData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);


            LoadSettings(crossPromoSettings);
        }


        private void OnGUI()
        {
            //platform settings
            EditorStyles.label.wordWrap = true;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));
            EditorGUILayout.LabelField("Cross Promo Settings", EditorStyles.boldLabel);

            GUILayout.Label("Enable visual scripting tool support:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Select the platforms to enable Cross Promo:");
            EditorGUILayout.Space();
            ShowPlatformSettings("Google Play(Android)", googlePlay, "GooglePlayPromoFile");
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            ShowPlatformSettings("App Store(iOS)", appStore, "AppStorePromoFile");

            //general settings
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Display Settings", EditorStyles.boldLabel);
            nrOfTimesToShow = EditorGUILayout.IntField("Nr. of times to show", nrOfTimesToShow);
            EditorGUILayout.LabelField("After showing the Cross Promo popup from the specified number of times, it will not show again until Game to Promote is changed. Set number to 0 for showing every time (Try not to annoy your users by showing this every time)");

            EditorGUILayout.Space();
            doNotShowAfterImageClick = EditorGUILayout.Toggle("Stop showing after click", doNotShowAfterImageClick);
            EditorGUILayout.LabelField("After user has clicked your promo image do not show it anymore. He already seen your game and he already have it or it is not interested in it.");

            EditorGUILayout.Space();
            allowMultipleDisplaysPerSession = EditorGUILayout.Toggle("Multiple displays/session", allowMultipleDisplaysPerSession);
            EditorGUILayout.LabelField("If unchecked Cross Promo Popup will be displayed only once per session.");

            //save settings
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Auto-load Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.autoLoadScene}");
            }

            if (GUILayout.Button("Open Load & Show Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.loadAndShowScene}");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.Space();
            GUILayout.EndScrollView();
        }


        private void ShowPlatformSettings(string name, PlatformSettings platformSettings, string fileName)
        {
            Color defaultColor = GUI.color;
            Color blackColor = new Color(0.65f, 0.65f, 0.65f, 1);
            GUI.color = blackColor;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.color = defaultColor;
            platformSettings.enabled = EditorGUILayout.Toggle(name, platformSettings.enabled);
            if (platformSettings.enabled)
            {
                platformSettings.promoFile.gameName = EditorGUILayout.TextField("Game to promote", platformSettings.promoFile.gameName);
                EditorGUILayout.BeginHorizontal();
                platformSettings.promoFile.storeLink = EditorGUILayout.TextField("Store link", platformSettings.promoFile.storeLink);
                if (GUILayout.Button("Test", GUILayout.Width(60)))
                {
                    Application.OpenURL(platformSettings.promoFile.storeLink);
                }
                EditorGUILayout.EndHorizontal();
                for (int i = 0; i < platformSettings.promoFile.imageUrls.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    platformSettings.promoFile.imageUrls[i] = EditorGUILayout.TextField("Promo image link " + (i + 1), platformSettings.promoFile.imageUrls[i]);
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        platformSettings.promoFile.imageUrls.RemoveAt(i);
                    }

                    if (GUILayout.Button("Test", GUILayout.Width(60)))
                    {
                        Application.OpenURL(platformSettings.promoFile.imageUrls[i]);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add new image URL"))
                {
                    platformSettings.promoFile.imageUrls.Add("");
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                if (GUILayout.Button("Generate file"))
                {
                    GenerateFile(platformSettings.promoFile, fileName);
                }

                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                platformSettings.externalFileLink = EditorGUILayout.TextField("External file URL", platformSettings.externalFileLink);
                if (GUILayout.Button("Test", GUILayout.Width(60)))
                {
                    Application.OpenURL(platformSettings.externalFileLink);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GUI.color = defaultColor;
        }


        private void LoadSettings(CrossPromoData crossPromoSettings)
        {
            googlePlay = crossPromoSettings.googlePlaySettings;
            appStore = crossPromoSettings.appStoreSettings;
            nrOfTimesToShow = crossPromoSettings.nrOfTimesToShow;
            doNotShowAfterImageClick = crossPromoSettings.doNotShowAfterImageClick;
            allowMultipleDisplaysPerSession = crossPromoSettings.allowMultipleDisplaysPerSession;
            usePlaymaker = crossPromoSettings.usePlaymaker;
            useUVS = crossPromoSettings.useUVS;
        }


        private void SaveSettings()
        {
            crossPromoSettings.usePlaymaker = usePlaymaker;
            crossPromoSettings.useUVS = useUVS;
            crossPromoSettings.googlePlaySettings = googlePlay;
            crossPromoSettings.appStoreSettings = appStore;
            crossPromoSettings.nrOfTimesToShow = nrOfTimesToShow;
            crossPromoSettings.doNotShowAfterImageClick = doNotShowAfterImageClick;
            crossPromoSettings.allowMultipleDisplaysPerSession = allowMultipleDisplaysPerSession;
            GameObject popup = AssetDatabase.LoadAssetAtPath($"{rootFolder}/Prefabs/CrossPromoPrefab.prefab", typeof(GameObject)) as GameObject;
            crossPromoSettings.crossPromoPopup = popup;
            SetPreprocessorDirectives();
            EditorUtility.SetDirty(crossPromoSettings);
        }


        private void SetPreprocessorDirectives()
        {
            if (usePlaymaker)
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.iOS);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.iOS);
            }

            if (useUVS)
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.iOS);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.iOS);
            }
        }


        private void GenerateFile(PromoFile promoFile, string fileName)
        {
            Gley.Common.EditorUtilities.CreateFolder($"{rootFolder}/PromoFiles");

            string json = JsonUtility.ToJson(promoFile);
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/PromoFiles/" + fileName + ".txt", json);
            AssetDatabase.Refresh();
        }
    }
}
