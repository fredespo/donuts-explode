using Gley.Common;
using Gley.DailyRewards.Internal;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.DailyRewards.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;
        private static string rootWithoutAssets;

        private DailyRewardsData dailyRewardsSettings;
        private List<TimerButtonProperties> localRewardButtons;
        private List<CalendarDayProperties> localCalendarDays;
        private GameObject calendarPrefab;
        private GameObject calendarCanvas;
        private Vector2 scrollPosition = Vector2.zero;
        private int hours;
        private int minutes;
        private int seconds;
        private bool availableAtStart;
        private bool resetAtEnd;
        private bool usePlaymaker;
        private bool useUVS;


        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder, out rootWithoutAssets);
        }


        /// <summary>
        /// Load data from asset
        /// </summary>
        private void OnEnable()
        {
            RefreshWindow();
        }


        void RefreshWindow()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties(), out rootWithoutAssets);
            }

            dailyRewardsSettings = EditorUtilities.LoadOrCreateDataAsset<DailyRewardsData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);


            localRewardButtons = new List<TimerButtonProperties>();

            for (int i = 0; i < dailyRewardsSettings.allTimerButtons.Count; i++)
            {
                localRewardButtons.Add(dailyRewardsSettings.allTimerButtons[i]);
            }

            calendarPrefab = dailyRewardsSettings.calendarPrefab;

            if (calendarPrefab == null)
            {
                GameObject popup = AssetDatabase.LoadAssetAtPath($"{rootFolder}/{Internal.Constants.CALENDAR_POPUP_LOCATION}", typeof(GameObject)) as GameObject;
                calendarPrefab = popup;

            }

            calendarCanvas = dailyRewardsSettings.calendarCanvas;

            if (calendarCanvas == null)
            {
                GameObject canvas = AssetDatabase.LoadAssetAtPath($"{rootFolder}/{Internal.Constants.CALENDAR_CANVAS_LOCATION}", typeof(GameObject)) as GameObject;
                calendarCanvas = canvas;
            }
            availableAtStart = dailyRewardsSettings.availableAtStart;
            resetAtEnd = dailyRewardsSettings.resetAtEnd;
            hours = dailyRewardsSettings.hours;
            minutes = dailyRewardsSettings.minutes;
            seconds = dailyRewardsSettings.seconds;
            usePlaymaker = dailyRewardsSettings.usePlaymaker;
            useUVS = dailyRewardsSettings.useUVS;

            localCalendarDays = new List<CalendarDayProperties>();
            for (int i = 0; i < dailyRewardsSettings.allDays.Count; i++)
            {
                localCalendarDays.Add(dailyRewardsSettings.allDays[i]);
            }
        }


        /// <summary>
        /// Save data to asset
        /// </summary>
        private void SaveSettings()
        {
            dailyRewardsSettings.allTimerButtons = new List<TimerButtonProperties>();

            for (int i = 0; i < localRewardButtons.Count; i++)
            {
                dailyRewardsSettings.allTimerButtons.Add(localRewardButtons[i]);
            }
            CreateEnumFile();

            dailyRewardsSettings.calendarPrefab = calendarPrefab;
            dailyRewardsSettings.calendarCanvas = calendarCanvas;
            dailyRewardsSettings.availableAtStart = availableAtStart;
            dailyRewardsSettings.resetAtEnd = resetAtEnd;
            dailyRewardsSettings.hours = hours;
            dailyRewardsSettings.minutes = minutes;
            dailyRewardsSettings.seconds = seconds;
            dailyRewardsSettings.usePlaymaker = usePlaymaker;
            dailyRewardsSettings.useUVS = useUVS;

            dailyRewardsSettings.allDays = new List<CalendarDayProperties>();
            for (int i = 0; i < localCalendarDays.Count; i++)
            {
                dailyRewardsSettings.allDays.Add(localCalendarDays[i]);
            }

            SetPreprocessorDirectives();

            EditorUtility.SetDirty(dailyRewardsSettings);
            AssetDatabase.Refresh();
        }


        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));

            GUILayout.Label("Enable Visual Scripting Tool:", EditorStyles.boldLabel);
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();

            #region TimerButtons
            EditorGUILayout.LabelField("TIMER BUTTON SETUP:", EditorStyles.boldLabel);
            for (int i = 0; i < localRewardButtons.Count; i++)
            {
                Color defaultColor = GUI.color;
                Color blackColor = new Color(0.65f, 0.65f, 0.65f, 1);
                GUI.color = blackColor;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.color = defaultColor;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Button ID (Unique)", GUILayout.Width(205));
                localRewardButtons[i].buttonID = EditorGUILayout.TextField(localRewardButtons[i].buttonID);
                localRewardButtons[i].buttonID = Regex.Replace(localRewardButtons[i].buttonID, @"^[\d-]*\s*", "");
                localRewardButtons[i].buttonID = Regex.Replace(localRewardButtons[i].buttonID, "[^a-zA-Z0-9._]", "");
                localRewardButtons[i].buttonID = localRewardButtons[i].buttonID.Replace(" ", "");
                localRewardButtons[i].buttonID = localRewardButtons[i].buttonID.Trim();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Time to pass: ", GUILayout.Width(205));
                EditorGUILayout.LabelField("h:", GUILayout.Width(20));
                localRewardButtons[i].hours = EditorGUILayout.IntField(localRewardButtons[i].hours);
                EditorGUILayout.LabelField("m:", GUILayout.Width(20));
                localRewardButtons[i].minutes = EditorGUILayout.IntField(localRewardButtons[i].minutes);
                EditorGUILayout.LabelField("s:", GUILayout.Width(20));
                localRewardButtons[i].seconds = EditorGUILayout.IntField(localRewardButtons[i].seconds);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Unlocked at start", GUILayout.Width(205));
                localRewardButtons[i].availableAtStart = EditorGUILayout.Toggle(localRewardButtons[i].availableAtStart);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Text after complete", GUILayout.Width(205));
                localRewardButtons[i].completeText = EditorGUILayout.TextField(localRewardButtons[i].completeText);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Interactable when unavailable", GUILayout.Width(205));
                localRewardButtons[i].interactable = EditorGUILayout.Toggle(localRewardButtons[i].interactable);
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Timer Button"))
                {
                    localRewardButtons.RemoveAt(i);
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Timer Button"))
            {
                localRewardButtons.Add(new TimerButtonProperties());
            }
            EditorGUILayout.Space();
            #endregion

            #region Calendar
            EditorGUILayout.LabelField("CALENDAR SETUP:", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Calendar Prefab", GUILayout.Width(205));
            calendarPrefab = (GameObject)EditorGUILayout.ObjectField(calendarPrefab, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Calendar Canvas", GUILayout.Width(205));
            calendarCanvas = (GameObject)EditorGUILayout.ObjectField(calendarCanvas, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("First day unlocked", GUILayout.Width(205));
            availableAtStart = EditorGUILayout.Toggle(availableAtStart);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Restart at end of days", GUILayout.Width(205));
            resetAtEnd = EditorGUILayout.Toggle(resetAtEnd);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Time to pass: ", GUILayout.Width(205));
            EditorGUILayout.LabelField("h:", GUILayout.Width(20));
            hours = EditorGUILayout.IntField(hours);
            EditorGUILayout.LabelField("m:", GUILayout.Width(20));
            minutes = EditorGUILayout.IntField(minutes);
            EditorGUILayout.LabelField("s:", GUILayout.Width(20));
            seconds = EditorGUILayout.IntField(seconds);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < localCalendarDays.Count; i++)
            {
                Color defaultColor = GUI.color;
                Color blackColor = new Color(0.65f, 0.65f, 0.65f, 1);
                GUI.color = blackColor;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.color = defaultColor;
                EditorGUILayout.LabelField("Day " + (i + 1));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Texture", GUILayout.Width(205));
                localCalendarDays[i].dayTexture = (Sprite)EditorGUILayout.ObjectField(localCalendarDays[i].dayTexture, typeof(Sprite), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Value", GUILayout.Width(205));
                localCalendarDays[i].rewardValue = EditorGUILayout.IntField(localCalendarDays[i].rewardValue);
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Remove Day"))
                {
                    localCalendarDays.RemoveAt(i);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Add Calendar Day"))
            {
                localCalendarDays.Add(new CalendarDayProperties());
            }
            EditorGUILayout.Space();
            #endregion

            //save settings
            EditorGUILayout.Space();
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
                        RefreshWindow();
                    }
                }
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Timer Button Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.timerButtonExample}");
            }

            if (GUILayout.Button("Open Calendar Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.calendarExample}");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.Space();
            GUILayout.EndScrollView();
            EditorGUILayout.Space();
        }


        /// <summary>
        /// Automatically generates enum based on names added in Settings Window
        /// </summary>
        private void CreateEnumFile()
        {
            if (CheckForDuplicates())
                return;
            string text =
            "namespace Gley.DailyRewards\n" +
            "{\n" +
            "\tpublic enum TimerButtonIDs\n" +
            "\t{\n";
            for (int i = 0; i < localRewardButtons.Count; i++)
            {
                text += "\t\t" + localRewardButtons[i].buttonID + ",\n";
            }
            text += "\t}\n";
            text += "}";
            File.WriteAllText($"{Application.dataPath}/{rootWithoutAssets}/{Internal.Constants.TIMER_IDS_LOCATION}", text);
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Check for duplicate or null IDs
        /// </summary>
        /// <returns>true if duplicate found</returns>
        private bool CheckForDuplicates()
        {
            if (localRewardButtons.Count < 2)
                return false;
            bool duplicateFound = false;
            for (int i = 0; i < localRewardButtons.Count - 1; i++)
            {
                for (int j = i + 1; j < localRewardButtons.Count; j++)
                {
                    if (string.IsNullOrEmpty(localRewardButtons[i].buttonID))
                    {
                        duplicateFound = true;
                        Debug.LogError("Button ID cannot be empty : at " + i);
                    }
                    if (localRewardButtons[i].buttonID == localRewardButtons[j].buttonID)
                    {
                        duplicateFound = true;
                        Debug.LogError("Duplicate id found: " + localRewardButtons[i].buttonID + " in positions " + i + ", " + j);
                    }
                }
            }
            if (string.IsNullOrEmpty(localRewardButtons[localRewardButtons.Count - 1].buttonID))
            {
                duplicateFound = true;
                Debug.LogError("Button ID cannot be empty : at " + (localRewardButtons.Count - 1));
            }
            return duplicateFound;
        }


        private void SetPreprocessorDirectives()
        {
            PreprocessorDirective.AddToCurrent(SettingsWindowProperties.GLEY_DAILY_REWARDS, false);
            if (usePlaymaker)
            {
                PreprocessorDirective.AddToCurrent(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false);
            }
            else
            {
                PreprocessorDirective.AddToCurrent(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true);
            }

            if (useUVS)
            {
                PreprocessorDirective.AddToCurrent(Common.Constants.GLEY_UVS_SUPPORT, false);
            }
            else
            {
                PreprocessorDirective.AddToCurrent(Common.Constants.GLEY_UVS_SUPPORT, true);
            }
        }
    }
}