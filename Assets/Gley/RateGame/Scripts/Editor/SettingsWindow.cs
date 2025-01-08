using Gley.Common;
using Gley.RateGame.Internal;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.RateGame.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;

        private RateGameData rateSettings;
        private DisplayConditions firstShowSettings;
        private DisplayConditions postponeSettings;
        private Vector2 scrollPosition = Vector2.zero;
        private RatePopupTypes ratePopupType;
        private string iosAppID;
        private string googlePlayBundleID;
        private string mainText;
        private string yesButtonText;
        private string noButton;
        private string laterButton;
        private string sendButton;
        private string notNowButton;
        private string neverButton;
        private int minStarsToSend;
        private bool usePlaymaker;
        private bool useBolt;
        private bool clearSave;
        private bool useNativePopupAndroid;
        private bool useNativePopupIOS;


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

            rateSettings = EditorUtilities.LoadOrCreateDataAsset<RateGameData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);

            //load asset values
            iosAppID = rateSettings.iosAppID;
            ratePopupType = rateSettings.ratePopupType;
            googlePlayBundleID = rateSettings.googlePlayBundleID;
            mainText = rateSettings.mainText;
            yesButtonText = rateSettings.yesButton;
            noButton = rateSettings.noButton;
            laterButton = rateSettings.laterButton;
            sendButton = rateSettings.sendButton;
            notNowButton = rateSettings.notNowButton;
            neverButton = rateSettings.neverButton;
            firstShowSettings = rateSettings.firstShowSettings;
            postponeSettings = rateSettings.postponeSettings;
            minStarsToSend = rateSettings.minStarsToSend;
            usePlaymaker = rateSettings.usePlaymaker;
            useBolt = rateSettings.useBolt;
            useNativePopupAndroid = rateSettings.useNativePopup;
            useNativePopupIOS = rateSettings.useNativePopupiOS;

#if UNITY_EDITOR
            clearSave = rateSettings.clearSave;
#endif
        }


        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));

            EditorGUILayout.LabelField("Enable Visual Scripting Tool Support:", EditorStyles.boldLabel);
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useBolt = EditorGUILayout.Toggle("Unity Visual Scripting", useBolt);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Your App IDs:", EditorStyles.boldLabel);
            iosAppID = EditorGUILayout.TextField("iOS App ID", iosAppID);
            googlePlayBundleID = EditorGUILayout.TextField("Google Play bundle ID", googlePlayBundleID);
            EditorGUILayout.Space();
            useNativePopupAndroid = EditorGUILayout.Toggle("Use native popup Android", useNativePopupAndroid);
            useNativePopupIOS = EditorGUILayout.Toggle("Use native popup iOS", useNativePopupIOS);
            EditorGUILayout.Space();

            if (useNativePopupAndroid == false || useNativePopupIOS == false)
            {
                GUILayout.Label("Customize popup text:", EditorStyles.boldLabel);
                ratePopupType = (RatePopupTypes)EditorGUILayout.EnumPopup("Select Rate Popup type: ", ratePopupType);
                EditorGUILayout.LabelField("Start Popup: - a popup with 5 stars selectable by user");
                EditorGUILayout.LabelField("Yes/No Popup: - a popup that asks the user if he/she wants to rate the app");
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Main Popup Text:", GUILayout.Width(146));
                var areaStyle = new GUIStyle(GUI.skin.textArea);
                areaStyle.wordWrap = true;
                var width = position.width - 35;
                areaStyle.fixedHeight = 0;
                areaStyle.fixedHeight = areaStyle.CalcHeight(new GUIContent(mainText), width);
                mainText = EditorGUILayout.TextArea(mainText, areaStyle, GUILayout.Height(areaStyle.fixedHeight));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();

                if (ratePopupType == RatePopupTypes.StarsPopup)
                {
                    sendButton = EditorGUILayout.TextField("Send Button", sendButton);
                    minStarsToSend = EditorGUILayout.IntField("Min Stars to Open Store:", minStarsToSend);
                    EditorGUILayout.LabelField("Opens the store page to rate on if user gives more that " + minStarsToSend + " stars");
                    EditorGUILayout.Space();

                    notNowButton = EditorGUILayout.TextField("Not now button", notNowButton);
                    EditorGUILayout.LabelField("Closes the popup, but it will open again based on your conditions");
                    EditorGUILayout.Space();

                    neverButton = EditorGUILayout.TextField("Never button", neverButton);
                    EditorGUILayout.LabelField("Closes the popup, popup never opens again");
                    EditorGUILayout.Space();
                }

                if (ratePopupType == RatePopupTypes.YesNoPopup)
                {
                    yesButtonText = EditorGUILayout.TextField("Yes button ", yesButtonText);
                    EditorGUILayout.LabelField("Opens the store page to rate");
                    EditorGUILayout.Space();

                    noButton = EditorGUILayout.TextField("No button", noButton);
                    EditorGUILayout.LabelField("Closes the popup, popup never opens again(this user does not like your game)");
                    EditorGUILayout.Space();

                    laterButton = EditorGUILayout.TextField("Later button (opens again later)", laterButton);
                    EditorGUILayout.LabelField("Closes the popup, but it will open again based on your conditions");
                    EditorGUILayout.Space();
                }

                EditorGUILayout.LabelField("If a button text is empty, that button will not show. At least one button should be active");
                EditorGUILayout.Space();

            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("For Google Play");
                if (GUILayout.Button("Download Google In App Review SDK"))
                {
                    Application.OpenURL("https://developers.google.com/unity/packages#play_in-app_review");
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("For App Store");
                EditorGUILayout.LabelField("No SDK needed");
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Show Options:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("First Show:", EditorStyles.boldLabel);
            ShowDisplaySettings(firstShowSettings);

            EditorGUILayout.LabelField("Postponed:", EditorStyles.boldLabel);
            ShowDisplaySettings(postponeSettings);

#if UNITY_EDITOR
            clearSave = EditorGUILayout.Toggle("Clear Save", clearSave);
#endif

            //save settings
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Example Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.exampleScene}");
            }
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            GUILayout.EndScrollView();
        }


        private void ShowDisplaySettings(DisplayConditions showSettings)
        {
            showSettings.useSessionsCount = EditorGUILayout.Toggle("Use Sessions Count:", showSettings.useSessionsCount);
            if (showSettings.useSessionsCount)
            {
                showSettings.minSessiosnCount = EditorGUILayout.IntField("Number of Sessions:", showSettings.minSessiosnCount);
            }

            showSettings.useCustomEvents = EditorGUILayout.Toggle("Use Custom Events:", showSettings.useCustomEvents);
            if (showSettings.useCustomEvents)
            {
                showSettings.minCustomEvents = EditorGUILayout.IntField("Number of Custom Events:", showSettings.minCustomEvents);
            }
            showSettings.useInGameTime = EditorGUILayout.Toggle("Use In Game Time:", showSettings.useInGameTime);
            if (showSettings.useInGameTime)
            {
                showSettings.gamePlayTime = EditorGUILayout.IntField("Number of minutes:", showSettings.gamePlayTime);
            }
            showSettings.useRealTime = EditorGUILayout.Toggle("Use Real Time: ", showSettings.useRealTime);
            if (showSettings.useRealTime)
            {
                showSettings.realTime = EditorGUILayout.FloatField("Number Of Hours:", showSettings.realTime);
            }
            if (showSettings.useSessionsCount == false && showSettings.useCustomEvents == false && showSettings.useInGameTime == false)
            {
                EditorGUILayout.LabelField("The rate popup will be shown when ShowRatePopup() method is called (no delay)");
            }
            else
            {
                string text = "The rate popup will be shown after";
                if (showSettings.useSessionsCount)
                {
                    text += " " + showSettings.minSessiosnCount + " sessions";
                }

                if (showSettings.useCustomEvents)
                {
                    if (showSettings.useSessionsCount)
                    {
                        text += " and";
                    }
                    text += " " + showSettings.minCustomEvents + " custom events";
                }

                if (showSettings.useInGameTime)
                {
                    if (showSettings.useCustomEvents || showSettings.useSessionsCount)
                    {
                        text += " and";
                    }
                    text += " " + showSettings.gamePlayTime + " game play minutes";
                }

                if (showSettings.useRealTime)
                {
                    if (showSettings.useInGameTime || showSettings.useCustomEvents || showSettings.useSessionsCount)
                    {
                        text += " and";
                    }
                    text += " " + showSettings.realTime + " real time hours after app was first open";
                }
                EditorGUILayout.LabelField(text);
            }
        }


        private void SaveSettings()
        {
            rateSettings.iosAppID = iosAppID;
            rateSettings.googlePlayBundleID = googlePlayBundleID;
            rateSettings.ratePopupType = ratePopupType;
            rateSettings.mainText = mainText;
            rateSettings.yesButton = yesButtonText;
            rateSettings.noButton = noButton;
            rateSettings.laterButton = laterButton;
            rateSettings.sendButton = sendButton;
            rateSettings.notNowButton = notNowButton;
            rateSettings.neverButton = neverButton;
            rateSettings.firstShowSettings = firstShowSettings;
            rateSettings.postponeSettings = postponeSettings;
            rateSettings.minStarsToSend = minStarsToSend;
            rateSettings.clearSave = clearSave;
            rateSettings.usePlaymaker = usePlaymaker;
            rateSettings.useBolt = useBolt;
            rateSettings.useNativePopup = useNativePopupAndroid;
            rateSettings.useNativePopupiOS = useNativePopupIOS;
            SetSelectedPopup();
            SetPreprocessorDirectives();
            EditorUtility.SetDirty(rateSettings);
        }


        private void SetSelectedPopup()
        {
            GameObject popup = AssetDatabase.LoadAssetAtPath($"{rootFolder}/Prefabs/" + ratePopupType.ToString() + ".prefab", typeof(GameObject)) as GameObject;
            rateSettings.popupGameObject = popup;
            GameObject canvas = AssetDatabase.LoadAssetAtPath($"{rootFolder}/Prefabs/RatePopupCanvas.prefab", typeof(GameObject)) as GameObject;
            rateSettings.ratePopupCanvas = canvas;
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

            if (useBolt)
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.iOS);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.iOS);
            }

            if(useNativePopupAndroid)
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NATIVE_GOOGLEPLAY, false, BuildTargetGroup.Android);            
            }
            else
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NATIVE_GOOGLEPLAY, true, BuildTargetGroup.Android);               
            }

            if(useNativePopupIOS)
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NATIVE_APPSTORE, false, BuildTargetGroup.iOS);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NATIVE_APPSTORE, true, BuildTargetGroup.iOS);
            }
        }
    }
}

