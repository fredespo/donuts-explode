using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using AlmostEngine.Screenshot;

namespace AlmostEngine.Preview
{
    public class ResolutionSettingsWindow : EditorWindow
    {
        [MenuItem("Window/Almost Engine/Universal Device Preview/Settings")]
        public static void Init()
        {
            m_Window = (ResolutionSettingsWindow)EditorWindow.GetWindow(typeof(ResolutionSettingsWindow), false, "Devices");
            m_Window.Show();
        }

        [InitializeOnLoadMethod]
        // Load all gameview size at startup
        public static void InitOnLoad()
        {
            m_ConfigAsset = GetConfig(false);
            if (m_ConfigAsset != null)
            {
                InitAllGameviewSizes();
            }
        }

        public static ResolutionSettingsWindow m_Window;

        public static bool IsOpen()
        {
            return m_Window != null;
        }
        static PreviewConfigAsset m_ConfigAsset;

        ScreenshotConfigDrawer m_ConfigDrawer;
        SerializedObject serializedObject;

        Vector2 m_ScrollPos;

        public static PreviewConfigAsset GetConfig(bool allowCreation = true)
        {
            if (m_ConfigAsset == null)
            {
                PreviewConfigAsset asset = null;
                if (allowCreation)
                {
                    asset = AssetUtils.GetOrCreate<PreviewConfigAsset>("UniversalDevicePreviewConfig", "Assets/AlmostEngine/UniversalDevicePreview/Assets/Editor/");
                }
                else
                {
                    asset = AssetUtils.GetFirst<PreviewConfigAsset>();
                }
                m_ConfigAsset = asset;
            }

            if (m_ConfigAsset == null)
                return null;

            m_ConfigAsset.m_Config.m_CaptureMode = ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING;
            m_ConfigAsset.m_Config.m_CameraMode = ScreenshotConfig.CamerasMode.GAME_VIEW;
            m_ConfigAsset.m_Config.m_ResolutionCaptureMode = ScreenshotConfig.ResolutionMode.CUSTOM_RESOLUTIONS;
            m_ConfigAsset.m_Config.m_ShotMode = ScreenshotConfig.ShotMode.ONE_SHOT;
            m_ConfigAsset.m_Config.m_StopTimeOnCapture = false;
            m_ConfigAsset.m_Config.m_PlaySoundOnCapture = false;

            return m_ConfigAsset;
        }

        void OnEnable()
        {
            if (m_ConfigAsset == null)
            {
                m_ConfigAsset = GetConfig();
            }
            serializedObject = new SerializedObject(m_ConfigAsset);
            m_ConfigDrawer = new ScreenshotConfigDrawer();
            m_ConfigDrawer.m_ShowDetailedDevice = true;
            m_ConfigDrawer.Init(serializedObject, m_ConfigAsset, m_ConfigAsset.m_Config, serializedObject.FindProperty("m_Config"));
            m_ConfigDrawer.m_Selector.m_ShowDetailedDevice = true;
        }

        #region GUI

        void OnGUI()
        {
            InitAllGameviewSizes();

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();


            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            var col = GUI.color;
            var bColor = new Color(0.6f, 1f, 0.6f, 1.0f);
            GUI.color = Color.Lerp(col, bColor, 0.75f);


            // var gallerytitle = IconsUtils.TryGetIcon("BuildSettings.iPhone");
            var gallerytitle = IconsUtils.TryGetIcon("GridLayoutGroup Icon");
            gallerytitle.text = " Gallery Preview";
            // if (GUILayout.Button("Settings", EditorStyles.toolbarButton))
            if (GUILayout.Button(gallerytitle, EditorStyles.toolbarButton))
            // if (GUILayout.Button("Device Preview", EditorStyles.toolbarButton))
            {
                ResolutionGalleryWindow.Init();
            }

#if UNITY_2019_4_OR_NEWER
            var previewtitle = IconsUtils.TryGetIcon("AspectRatioFitter Icon");
#else
            var previewtitle = IconsUtils.TryGetIcon("BuildSettings.iPhone.Small");
#endif
            previewtitle.text = " Device Preview";
            if (GUILayout.Button(previewtitle, EditorStyles.toolbarButton))
            // if (GUILayout.Button("Device Preview", EditorStyles.toolbarButton))
            {
                ResolutionPreviewWindow.Init();
            }
            GUI.color = col;
            GUILayout.FlexibleSpace();


            // GUI.color = new Color(1f, 1f, 0.1f, 1.0f);  
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            var reviewtitle = IconsUtils.TryGetIcon("Favorite");
            reviewtitle.text = "Review";
            if (GUILayout.Button(reviewtitle, EditorStyles.toolbarButton))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/universal-device-preview-82015");
            }
            GUI.color = col;










            var abouttitle = IconsUtils.TryGetIcon("UnityEditor.InspectorWindow");
            abouttitle.text = " About";
            if (GUILayout.Button(abouttitle, EditorStyles.toolbarButton))
            {
                UniversalDevicePreview.About();
            }
            EditorGUILayout.EndHorizontal();



            m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);

            EditorGUILayout.Separator();
            DrawConfig();
            EditorGUILayout.Separator();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            DrawSupportGUI();


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            DrawContactGUI();

            EditorGUILayout.EndScrollView();


            if (EditorGUI.EndChangeCheck())
            {
                ResolutionWindowBase.RepaintWindows();
            }

            serializedObject.ApplyModifiedProperties();
        }


        protected int m_WindowWidth;

        protected void DrawGallerySettings()
        {

            var title = IconsUtils.TryGetIcon("EditorSettings Icon");
            title.text = " Preview settings".ToUpper();
            m_ConfigAsset.m_ShowGallery = EditorGUILayout.Foldout(m_ConfigAsset.m_ShowGallery, title);
            // m_ConfigAsset.m_ShowGallery = EditorGUILayout.Foldout(m_ConfigAsset.m_ShowGallery, "Preview settings".ToUpper());
            if (m_ConfigAsset.m_ShowGallery == false)
                return;
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Display", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ScreenPPI"));

            EditorGUILayout.HelpBox("Set ScreenPPI to the PPI of your display to have correct physical sizes in PPI mode.", MessageType.Info);


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Safe Area", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_DrawSafeArea"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SafeAreaCanvas"));
            EditorGUILayout.HelpBox("Note that safe area is only displayed with devices containing safe area data. ", MessageType.Info);
            var col = GUI.color;
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            EditorGUILayout.HelpBox("Send me your safe are data! If you get more safe area values, please send them to me, so I can update the asset presets and make them available to all users."
            + " To get them, simply run the CanvasExamples scene on your target device, and look at the device info values.", MessageType.Info);
            GUI.color = col;


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Drawing", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_DrawingMode"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TransparentDeviceBackground"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_DeviceRendererCamera"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Low memory usage", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_OptimizePreviewResolution"));
            EditorGUILayout.HelpBox("Automatically reduce the device preview resolution to match its display resolution on screen, based on the zoom, display mode and screen PPI, to reduce the graphic memory usage. ", MessageType.Info);
            if (m_ConfigAsset.m_OptimizePreviewResolution == true)
            {
                EditorGUILayout.HelpBox("Preview optimization is ENABLED. "
                + "Note that because the preview resolution is reduced, the resolution during preview does not match the target device resolution. "
                + "This could create inconsistencies with scripts directly refering to the Screen resolution.", MessageType.Warning);
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Auto Refresh", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AutoRefresh"));
            EditorGUILayout.HelpBox("Autorefresh recompute all previews periodically using the refresh delay." +
            " Because the gameview needs to be resized during the preview capture, it is recommended to use autorefresh with only one or very few devices at a time, and with a high delay value." +
            " Prefer using the hotkey (F5) to refresh manually the previews when you need it." +
            " For a live preview, it is recommended to use only one device, and to set DrawingMode to texture only.",
            MessageType.Info);


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Behavior", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AutoGenerateEmptyPreview"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_BackupPreviewToDisk"));



            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Gallery device names", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ShowResolution"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ShowPPI"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ShowRatio"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Misc.", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ZoomScrollSpeed"));

        }


        protected void DrawConfig()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

#if UNITY_2019_4_OR_NEWER
            var titleb = IconsUtils.TryGetIcon("AspectRatioFitter Icon");
#else
            var titleb = IconsUtils.TryGetIcon("BuildSettings.iPhone");
#endif



            titleb.text = " Devices".ToUpper();
            m_ConfigAsset.m_Config.m_ShowResolutions = EditorGUILayout.Foldout(m_ConfigAsset.m_Config.m_ShowResolutions, titleb);
            // m_ConfigAsset.m_Config.m_ShowResolutions = EditorGUILayout.Foldout(m_ConfigAsset.m_Config.m_ShowResolutions, "Devices".ToUpper());

            if (m_ConfigAsset.m_ExpandDevices)
            {
                m_ConfigDrawer.m_Expanded = true;
                if (GUILayout.Button("Hide device settings"))
                {
                    m_ConfigAsset.m_ExpandDevices = false;
                    m_ConfigDrawer.m_Expanded = false;
                    m_ConfigDrawer.CreateResolutionReorderableList();
                }
            }
            else
            {
                m_ConfigDrawer.m_Expanded = false;
                if (GUILayout.Button("Expand device settings "))
                {
                    m_ConfigAsset.m_ExpandDevices = true;
                    m_ConfigDrawer.m_Expanded = true;
                    m_ConfigDrawer.CreateResolutionReorderableList();
                }
            }

            EditorGUILayout.Separator();

            m_ConfigDrawer.DrawResolutionContentGUI();

            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawGallerySettings();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawFolderGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawNameGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawDelay(false);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            var title = IconsUtils.TryGetIcon("StandaloneInputModule Icon");
            title.text = " Hotkeys".ToUpper();
            m_ConfigAsset.m_Config.m_ShowUtils = EditorGUILayout.Foldout(m_ConfigAsset.m_Config.m_ShowUtils, title);
            // m_ConfigAsset.m_Config.m_ShowUtils = EditorGUILayout.Foldout(m_ConfigAsset.m_Config.m_ShowUtils, "Hotkeys".ToUpper());
            if (m_ConfigAsset.m_Config.m_ShowUtils != false)
            {

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Default hotkeys: Update preview (F5) Export preview (F6)");
                EditorGUILayout.HelpBox("You can customize the hotkeys by editing the UpdateDevicePreviewMenuItem.cs and ExportDevicePreviewMenuItem.cs scripts.", MessageType.Info);

            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawUsage();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawFeatureExclude();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

        }

        public static void DrawSupportGUI()
        {

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("SUPPORT");

            EditorGUILayout.HelpBox("If you notice any mistake in the device resolutions, PPI values, or preview pictures, please let me know. " +
            "Also, you can contact me if you want a specific device to be added. " +
            "All suggestions are welcome.", MessageType.Info);

            Color cc = GUI.color;
            GUI.color = new Color(0.55f, 0.7f, 1f, 1.0f);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("More assets from Wild Mage Games"))
            {
                Application.OpenURL("https://www.wildmagegames.com/unity/");
            }
            if (GUILayout.Button("Contact support"))
            {
                Application.OpenURL("mailto:support@wildmagegames.com");
            }
            EditorGUILayout.EndHorizontal();
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            var reviewtitle = IconsUtils.TryGetIcon("Favorite");
            reviewtitle.text = " Leave a Review";
            if (GUILayout.Button(reviewtitle, GUILayout.Height(50)))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/universal-device-preview-82015");
            }
            GUI.color = cc;

            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

        }


        protected void DrawContactGUI()
        {
            EditorGUILayout.LabelField(UniversalDevicePreview.VERSION, UIStyle.centeredGreyTextStyle);
            EditorGUILayout.LabelField(UniversalDevicePreview.AUTHOR, UIStyle.centeredGreyTextStyle);
        }

        #endregion

        static string m_GameviewSizeName = "UniversalDevicePreview - ";

        static void InitAllGameviewSizes()
        {
            // Remove all resolutions not in devices
            List<string> deviceSizes = new List<string>();
            foreach (ScreenshotResolution res in m_ConfigAsset.m_Config.GetActiveResolutions())
            {
                deviceSizes.Add(m_GameviewSizeName + res.ToString());
            }
            List<string> gameviewSizes = GameViewUtils.GetAllSizeNames();
            foreach (string size in gameviewSizes)
            {
                if (size.Contains(m_GameviewSizeName) && !deviceSizes.Contains(size))
                {
                    GameViewUtils.RemoveCustomSize(GameViewUtils.FindSize(size));
                }
            }

            // Add all new devices
            foreach (ScreenshotResolution res in m_ConfigAsset.m_Config.GetActiveResolutions())
            {
                if (!gameviewSizes.Contains(m_GameviewSizeName + res.ToString()))
                {
                    GameViewUtils.AddCustomSize(GameViewUtils.SizeType.FIXED_RESOLUTION, res.ComputeTargetWidth(), res.ComputeTargetHeight(), m_GameviewSizeName + res.ToString());
                }
            }

        }


    }
}