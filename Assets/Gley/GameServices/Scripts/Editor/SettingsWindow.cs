using Gley.Common;
using Gley.GameServices.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.GameServices.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;
        private static string rootWithoutAssets;

        private GameServicesData gameServicesData;
        private List<Achievement> localAchievements;
        private List<Leaderboard> localLeaderboards;
        private Vector2 scrollPosition = Vector2.zero;
        private string googleAppId;
        private string errorText = "";
        private int step;
        private bool installing;
        private bool useForAndroid;
        private bool useForIos;
        private bool usePlaymaker;
        private bool useUVS;

        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder, out rootWithoutAssets);
        }

        private void OnInspectorUpdate()
        {
            if (installing == true)
            {
                if (EditorApplication.isCompiling == false)
                {
                    SaveSettings();
                }
            }
        }


        private void OnEnable()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties(), out rootWithoutAssets);
            }
            //load Game Serviced data
            gameServicesData = EditorUtilities.LoadOrCreateDataAsset<GameServicesData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);

            useForAndroid = gameServicesData.useForAndroid;
            useForIos = gameServicesData.useForIos;
            googleAppId = gameServicesData.googleAppId;
            usePlaymaker = gameServicesData.usePlaymaker;
            useUVS = gameServicesData.useUVS;

            localAchievements = new List<Achievement>();
            for (int i = 0; i < gameServicesData.allGameAchievements.Count; i++)
            {
                localAchievements.Add(gameServicesData.allGameAchievements[i]);
            }

            localLeaderboards = new List<Leaderboard>();
            for (int i = 0; i < gameServicesData.allGameLeaderboards.Count; i++)
            {
                localLeaderboards.Add(gameServicesData.allGameLeaderboards[i]);
            }
        }


        /// <summary>
        /// Saves the Settings Window data
        /// </summary>
        private void SaveSettings()
        {
            Debug.Log($"Saving {step + 1}/7");
            installing = false;
            switch (step)
            {
                case 0:
                    //setup preprocessor directives based on settings
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

                    //save id`s
                    gameServicesData.googleAppId = googleAppId;
                    gameServicesData.useForAndroid = useForAndroid;
                    gameServicesData.useForIos = useForIos;
                    gameServicesData.usePlaymaker = usePlaymaker;
                    gameServicesData.useUVS = useUVS;

                    gameServicesData.allGameAchievements = new List<Achievement>();
                    for (int i = 0; i < localAchievements.Count; i++)
                    {
                        gameServicesData.allGameAchievements.Add(localAchievements[i]);
                    }

                    gameServicesData.allGameLeaderboards = new List<Leaderboard>();
                    for (int i = 0; i < localLeaderboards.Count; i++)
                    {
                        gameServicesData.allGameLeaderboards.Add(localLeaderboards[i]);
                    }
                    EditorUtility.SetDirty(gameServicesData);
                    installing = true;
                    step++;
                    break;

                case 1:
                    Gley.Common.EditorUtilities.CreateFolder($"{rootFolder}/Plugins/Android/");
                    AssetDatabase.Refresh();
                    installing = true;
                    step++;
                    break;

                case 2:
                    if (!Directory.Exists($"{rootFolder}/Plugins/Android/GameServicesManifest.androidlib"))
                    {
                        AssetDatabase.CreateFolder($"{rootFolder}/Plugins/Android", "GameServicesManifest.androidlib");
                    }
                    AssetDatabase.Refresh();
                    installing = true;
                    step++;
                    break;

                case 3:
                    string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                   "<manifest xmlns:android = \"http://schemas.android.com/apk/res/android\"\n" +
                   "\tpackage=\"com.google.example.games.mainlibproj\"" +
                   "\tandroid:versionCode=\"1\"\n" +
                   "\tandroid:versionName=\"1.0\">\n" +
                   "\t<application>\n" +
                   "\t\t<meta-data android:name=\"com.google.android.gms.games.APP_ID\" android:value = \"\\" + googleAppId + "\" />\n" +
                   "\t\t<activity android:name=\"com.google.games.bridge.NativeBridgeActivity\" android:theme = \"@android:style/Theme.Translucent.NoTitleBar.Fullscreen\" />\n" +
                   "\t</application>\n" +
                   "</manifest>";

                    File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/Plugins/Android/GameServicesManifest.androidlib/AndroidManifest.xml", text);
                    AssetDatabase.Refresh();
                    installing = true;
                    step++;
                    break;

                case 4:
                    CreateEnumFiles();
                    installing = true;
                    step++;
                    break;

                case 5:
                    if (useForAndroid)
                    {
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_GAMESERVICES_ANDROID, false, BuildTargetGroup.Android);
                    }
                    else
                    {
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_GAMESERVICES_ANDROID, true, BuildTargetGroup.Android);
                    }
                    if (useForIos)
                    {
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_GAMESERVICES_IOS, false, BuildTargetGroup.iOS);
                    }
                    else
                    {
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_GAMESERVICES_IOS, true, BuildTargetGroup.iOS);
                    }
                    step++;
                    installing = true;
                    break;

                default:
                    errorText = "Save Success";
                    Debug.Log(errorText);
                    break;
            }
        }

        /// <summary>
        /// Display Settings Window
        /// </summary>
        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));
            GUILayout.Label("Select your platforms:", EditorStyles.boldLabel);
            useForAndroid = EditorGUILayout.Toggle("Android", useForAndroid);
            useForIos = EditorGUILayout.Toggle("iOS", useForIos);
            EditorGUILayout.Space();

            GUILayout.Label("Enable visual scripting tool support:", EditorStyles.boldLabel);
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();
            //Google play setup
            if (useForAndroid)
            {
                GUILayout.Label("Google Play Services Settings", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                if (GUILayout.Button("Download Google Play Games SDK"))
                {
                    Application.OpenURL("https://github.com/playgameservices/play-games-plugin-for-unity");
                }
                GUILayout.Label("You just need to import the SDK, no additional setup is required");
                EditorGUILayout.Space();

                googleAppId = EditorGUILayout.TextField("Google Play App ID", googleAppId);
                EditorGUILayout.Space();
            }


            if (useForAndroid || useForIos)
            {
                //achievement setup
                GUILayout.Label("Achievements Settings", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Achievement Name");
                if (useForAndroid)
                {
                    GUILayout.Label("Google Play ID");
                }
                if (useForIos)
                {
                    GUILayout.Label("Game Center ID");
                }
                GUILayout.Label("");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                for (int i = 0; i < localAchievements.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    localAchievements[i].name = EditorGUILayout.TextField(localAchievements[i].name);
                    localAchievements[i].name = Regex.Replace(localAchievements[i].name, @"^[\d-]*\s*", "");
                    localAchievements[i].name = localAchievements[i].name.Replace(" ", "");
                    localAchievements[i].name = localAchievements[i].name.Trim();
                    if (useForAndroid)
                    {
                        localAchievements[i].idGoogle = EditorGUILayout.TextField(localAchievements[i].idGoogle);
                    }
                    if (useForIos)
                    {
                        localAchievements[i].idIos = EditorGUILayout.TextField(localAchievements[i].idIos);
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        localAchievements.RemoveAt(i);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                EditorGUILayout.Space();

                if (GUILayout.Button("Add new achievement"))
                {
                    localAchievements.Add(new Achievement());
                }
                EditorGUILayout.Space();


                //leaderboard setup
                GUILayout.Label("Leaderboards Settings", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Leaderboard Name");
                if (useForAndroid)
                {
                    GUILayout.Label("Google Play ID");
                }
                if (useForIos)
                {
                    GUILayout.Label("Game Center ID");
                }
                GUILayout.Label("");
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                for (int i = 0; i < localLeaderboards.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    localLeaderboards[i].name = EditorGUILayout.TextField(localLeaderboards[i].name);
                    localLeaderboards[i].name = Regex.Replace(localLeaderboards[i].name, @"^[\d-]*\s*", "");
                    localLeaderboards[i].name = localLeaderboards[i].name.Replace(" ", "");
                    localLeaderboards[i].name = localLeaderboards[i].name.Trim();
                    if (useForAndroid)
                    {
                        localLeaderboards[i].idGoogle = EditorGUILayout.TextField(localLeaderboards[i].idGoogle);
                    }
                    if (useForIos)
                    {
                        localLeaderboards[i].idIos = EditorGUILayout.TextField(localLeaderboards[i].idIos);
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        localLeaderboards.RemoveAt(i);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                EditorGUILayout.Space();

                if (GUILayout.Button("Add new leaderboard"))
                {
                    localLeaderboards.Add(new Leaderboard());
                }
            }
            EditorGUILayout.Space();

            //save
            GUILayout.Label(errorText);
            if (GUILayout.Button("Save"))
            {
                if (CheckForNull() == false)
                {
                    step = 0;
                    SaveSettings();
                }
            }


            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Example Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.gameServicesExample}");
            }

            if (GUILayout.Button("Open Test Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.gameServicesTest}");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.Space();

            GUILayout.EndScrollView();
        }


        /// <summary>
        /// Checks if any ID is null or duplicate
        /// </summary>
        /// <returns></returns>
        private bool CheckForNull()
        {
            for (int i = 0; i < localAchievements.Count - 1; i++)
            {
                for (int j = i + 1; j < localAchievements.Count; j++)
                {
                    if (localAchievements[i].name == localAchievements[j].name)
                    {
                        errorText = localAchievements[i].name + " Already exists. No duplicates allowed";
                        return true;
                    }
                }
            }

            for (int i = 0; i < localLeaderboards.Count - 1; i++)
            {
                for (int j = i + 1; j < localLeaderboards.Count; j++)
                {
                    if (localLeaderboards[i].name == localLeaderboards[j].name)
                    {
                        errorText = localLeaderboards[i].name + " Already exists. No duplicates allowed";
                        return true;
                    }
                }
            }

            for (int i = 0; i < localAchievements.Count; i++)
            {
                if (String.IsNullOrEmpty(localAchievements[i].name))
                {
                    errorText = "Achievement name cannot be empty! Please fill all of them";
                    return true;
                }
                if (useForAndroid)
                {
                    if (String.IsNullOrEmpty(localAchievements[i].idGoogle))
                    {
                        errorText = "Google Play ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }
                if (useForIos)
                {
                    if (String.IsNullOrEmpty(localAchievements[i].idIos))
                    {
                        errorText = "Game Center ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }
            }

            for (int i = 0; i < localLeaderboards.Count; i++)
            {
                if (String.IsNullOrEmpty(localLeaderboards[i].name))
                {
                    errorText = "Leaderboard name cannot be empty! Please fill all of them";
                    return true;
                }
                if (useForAndroid)
                {
                    if (String.IsNullOrEmpty(localLeaderboards[i].idGoogle))
                    {
                        errorText = "Leaderboard`s Google Play ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }
                if (useForIos)
                {
                    if (String.IsNullOrEmpty(localLeaderboards[i].idIos))
                    {
                        errorText = "Leaderboard`s Game Center ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }
            }
            return false;
        }





        /// <summary>
        /// Automatically generates enums based on names added in Settings Window
        /// </summary>
        private void CreateEnumFiles()
        {

            string text =
            "#if GLEY_GAMESERVICES_ANDROID || GLEY_GAMESERVICES_IOS\n" +
            "namespace Gley.GameServices\n" +
            "{\n" +
            "\tpublic enum AchievementNames\n" +
            "\t{\n";
            for (int i = 0; i < localAchievements.Count; i++)
            {
                text += "\t\t" + localAchievements[i].name + ",\n";
            }
            text += "\t}\n";
            text += "}\n";
            text += "#endif";
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/Scripts/ToUse/AchievementNames.cs", text);

            text =
            "#if GLEY_GAMESERVICES_ANDROID || GLEY_GAMESERVICES_IOS\n" +
            "namespace Gley.GameServices\n" +
            "{\n" +
            "\tpublic enum LeaderboardNames\n" +
            "\t{\n";
            for (int i = 0; i < localLeaderboards.Count; i++)
            {
                text += "\t\t" + localLeaderboards[i].name + ",\n";
            }
            text += "\t}\n";
            text += "}\n";
            text += "#endif";
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/Scripts/ToUse/LeaderboardNames.cs", text);

            AssetDatabase.Refresh();
        }
    }
}
