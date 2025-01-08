using Gley.Common;
using Gley.Notifications.Internal;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.Notifications.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;

        private Vector2 scrollPosition = Vector2.zero;
        NotificationsData notificationSettongs;

        string info = "This asset requires Mobile Notifications by Unity \n" +
            "Go to Window -> Package Manager and install Mobile Notifications";
        private bool useForAndroid;
        private bool useForIos;
        private string additionalSettings = "To setup notification images open:\n" +
            "Edit -> Project Settings -> Mobile Notifications";

        private bool usePlaymaker;
        private bool useUVS;

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

            notificationSettongs = EditorUtilities.LoadOrCreateDataAsset<NotificationsData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);

            useForAndroid = notificationSettongs.useForAndroid;
            useForIos = notificationSettongs.useForIos;
            usePlaymaker = notificationSettongs.usePlaymaker;
            useUVS = notificationSettongs.useUVS;
        }


        private void SaveSettings()
        {
            SetPreprocessorDirectives();
            if (useForAndroid)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NOTIFICATIONS_ANDROID, false, BuildTargetGroup.Android);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NOTIFICATIONS_ANDROID, true, BuildTargetGroup.Android);
            }
            if (useForIos)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NOTIFICATIONS_IOS, false, BuildTargetGroup.iOS);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_NOTIFICATIONS_IOS, true, BuildTargetGroup.iOS);
            }

            notificationSettongs.useForAndroid = useForAndroid;
            notificationSettongs.useForIos = useForIos;
            notificationSettongs.usePlaymaker = usePlaymaker;
            notificationSettongs.useUVS = useUVS;

            EditorUtility.SetDirty(notificationSettongs);
        }


        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));

            GUILayout.Label("Enable visual scripting tool support:", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();

            GUILayout.Label("Select your platforms:", EditorStyles.boldLabel);
            useForAndroid = EditorGUILayout.Toggle("Android", useForAndroid);
            useForIos = EditorGUILayout.Toggle("iOS", useForIos);
            EditorGUILayout.Space();


            EditorGUILayout.LabelField(info);
            if (GUILayout.Button("Install Mobile Notifications by Unity"))
            {
                Gley.Common.ImportRequiredPackages.ImportPackage("com.unity.mobile.notifications", CompleteMethod);
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(additionalSettings);
            if (GUILayout.Button("Open Mobile Notification Settings"))
            {
                SettingsService.OpenProjectSettings("Project/Mobile Notifications");
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Example Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.notificationExample}");
            }

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();


            GUILayout.EndScrollView();
        }


        private void CompleteMethod(string message)
        {
            Debug.Log(message);
        }


        private void SetPreprocessorDirectives()
        {
            if (usePlaymaker)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.iOS);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.iOS);
            }

            if (useUVS)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.iOS);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.iOS);
            }
        }
    }
}