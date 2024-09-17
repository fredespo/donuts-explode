using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

namespace AlmostEngine.Screenshot
{
    public class ScreenshotWindow : EditorWindow
    {


        [MenuItem("Window/Almost Engine/Screenshot Window")]
        public static void Init()
        {
            ScreenshotWindow window = (ScreenshotWindow)EditorWindow.GetWindow(typeof(ScreenshotWindow), false, "Screenshot");
            window.Show();
        }

        public static ScreenshotWindow m_Window;


        ScreenshotConfigDrawer m_ConfigDrawer;
        ScreenshotConfigAsset m_ConfigAsset;
        SerializedObject serializedConfigObject;

        ScreenshotConfigAsset m_ConfigAssetInstance;

        Vector2 m_ScrollPos;


        public static bool IsOpen()
        {
            return m_Window != null;
        }

        void OnEnable()
        {
            m_Window = this;
            InitConfig();

#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged += StateChange;
#else
			EditorApplication.playmodeStateChanged += StateChange;
#endif

#if UNITY_2019_1_OR_NEWER
			SceneView.duringSceneGui += HandleEventsDelegate;
#else
            SceneView.onSceneGUIDelegate += HandleEventsDelegate;
#endif


        }

        void OnDisable()
        {
            m_Window = null;
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged -= StateChange;
#else
			EditorApplication.playmodeStateChanged -= StateChange;
#endif

#if UNITY_2019_1_OR_NEWER
			SceneView.duringSceneGui -= HandleEventsDelegate;
#else
            SceneView.onSceneGUIDelegate -= HandleEventsDelegate;
#endif
        }

#if UNITY_2017_2_OR_NEWER
        void StateChange(PlayModeStateChange state)




#else
		void StateChange ()
#endif
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)
            {
                // Instantiate the manager within the scene when the game starts playing
                InitTempManager();
            }
            else
            {
                // Destroy it when the game stops playing
                DestroyManager();
            }
        }

        void Clear()
        {
            DestroyManager();
            InitConfig();
        }

        void InitConfig()
        {
            m_ConfigAsset = AssetUtils.GetFirstOrCreate<ScreenshotConfigAsset>("ScreenshotWindowConfig", "Assets/");

            // We trick the editor by creating an instance to ref the config data
            // So we can ref scene objects because it beleaves it is not an asset but a scene object
            m_ConfigAssetInstance = ScriptableObject.CreateInstance<ScreenshotConfigAsset>();
            m_ConfigAssetInstance.m_Config = m_ConfigAsset.m_Config;
            serializedConfigObject = new SerializedObject(m_ConfigAssetInstance);

            // Init the config drawer
            m_ConfigDrawer = new ScreenshotConfigDrawer();
            m_ConfigDrawer.Init(serializedConfigObject, m_ConfigAsset, m_ConfigAsset.m_Config, serializedConfigObject.FindProperty("m_Config"));


        }

        #region Events

        protected void HandleEventsDelegate(SceneView sceneview)
        {
            HandleEditorEvents(true);
        }

        void HandleEditorEvents(bool sceneView = false)
        {
            // Hotkeys
            Event e = Event.current;

            if (e == null)
                return;

            if (m_ConfigAsset.m_Config.m_AlignHotkey.IsPressed(e))
            {
                m_ConfigAsset.m_Config.AlignToView();
                e.Use();
            }

            if (m_ConfigAsset.m_Config.m_UpdatePreviewHotkey.IsPressed(e))
            {
                UpdatePreview();
                e.Use();
            }

            if (m_ConfigAsset.m_Config.m_CaptureHotkey.IsPressed(e))
            {
                Capture();
                e.Use();
            }

            if (m_ConfigAsset.m_Config.m_PauseHotkey.IsPressed(e))
            {
                m_ConfigAsset.m_Config.TogglePause();
                e.Use();
            }

        }

        #endregion

        #region GUI

        void OnGUI()
        {

            if (EditorApplication.isCompiling)
            {
                Clear();
            }

            if (serializedConfigObject == null || m_ConfigAssetInstance == null)
            {
                InitConfig();
            }

            serializedConfigObject.Update();

            DrawToolBarGUI();

            m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);

            DrawConfig();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            DrawSupportGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            DrawContactGUI();

            EditorGUILayout.EndScrollView();

            serializedConfigObject.ApplyModifiedProperties();
        }


        protected void DrawConfig()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCaptureModeGUI();
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

            if (m_ConfigAsset.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW && m_ConfigAsset.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL && m_ConfigAsset.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                m_ConfigDrawer.DrawResolutionGUI();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

            if (m_ConfigAsset.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL && m_ConfigAsset.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                m_ConfigDrawer.DrawCamerasGUI();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
            }

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawOverlaysGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCompositionGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawBatchesGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawPreviewGUI();

            if (GUILayout.Button("Update"))
            {
                UpdatePreview();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawCaptureGUI();
            DrawCaptureButtonsGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            // EditorGUILayout.BeginVertical(GUI.skin.box);
            // m_ConfigDrawer.DrawShareGUI();
            // EditorGUILayout.EndVertical();
            // EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawUtilsGUI();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawDelay();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            m_ConfigDrawer.DrawHotkeysGUI();
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


        protected void DrawToolBarGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);


            var col = GUI.color;
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            if (GUILayout.Button(GetCaptureButtonText(), EditorStyles.toolbarButton))
            {
                Capture();
            }
            GUI.color = col;

            if (GUILayout.Button("Preview", EditorStyles.toolbarButton))
            {
                UpdatePreview();
            }

            GUILayout.FlexibleSpace();


            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            var reviewtitle = IconsUtils.TryGetIcon("Favorite");
            reviewtitle.text = "Review";
            if (GUILayout.Button(reviewtitle, EditorStyles.toolbarButton))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/ultimate-screenshot-creator-82008");
            }
            GUI.color = col;



            var abouttitle = IconsUtils.TryGetIcon("UnityEditor.InspectorWindow");
            abouttitle.text = " About";
            if (GUILayout.Button(abouttitle, EditorStyles.toolbarButton))
            {
                UltimateScreenshotCreator.About();
            }

            EditorGUILayout.EndHorizontal();
        }

        protected void DrawCaptureButtonsGUI()
        {

            EditorGUILayout.BeginHorizontal();

            // BUTTONS
            Color c = GUI.color;
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            if (m_ConfigAsset.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && !Application.isPlaying)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button(GetCaptureButtonText(), GUILayout.Height(50)))
            {
                Capture();
            }
            GUI.enabled = true;
            GUI.color = c;

            if (GUILayout.Button("Show", GUILayout.MaxWidth(70), GUILayout.Height(50)))
            {
                EditorUtility.RevealInFinder(m_ConfigAsset.m_Config.GetPath());
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        string GetCaptureButtonText()
        {
            if (m_Manager != null && m_Manager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && m_Manager.m_IsBurstActive == true)
            {
                return "Stop";
            }
            else if (m_ConfigAsset.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST)
            {
                return "Start Burst";
            }
            else
            {
                return "Capture";
            }
        }



        #endregion

        #region UI Callbacks


        public void Capture()
        {
            if (m_Manager != null && m_Manager.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && m_Manager.m_IsBurstActive == true)
            {
                m_Manager.StopBurst();
                DestroyManager();
                return;
            }

            InitTempManager();
            m_Manager.StartCoroutine(CaptureCoroutine());
        }

        public void UpdatePreview()
        {
            InitTempManager();
            m_Manager.StartCoroutine(UpdatePreviewCoroutine());
        }

        #endregion

        #region Screenshot Manager

        ScreenshotManager m_Manager;
        ScreenshotTaker m_ScreenshotTaker;

        void InitTempManager()
        {
            if (m_Manager != null)
                return;

            GameObject obj = new GameObject();
            obj.name = "Temporary screenshot manager - remove if still exists in scene in edit mode.";
            obj.hideFlags = HideFlags.HideAndDontSave;

            // First we create the screenshot taker
            m_ScreenshotTaker = obj.AddComponent<ScreenshotTaker>();
            m_ScreenshotTaker.m_GameViewResizingWaitingMode = m_ConfigAsset.m_Config.m_GameViewResizingWaitingMode;
            m_ScreenshotTaker.m_GameViewResizingWaitingFrames = m_ConfigAsset.m_Config.m_ResizingWaitingFrames;
            m_ScreenshotTaker.m_GameViewResizingWaitingTime = m_ConfigAsset.m_Config.m_ResizingWaitingTime;

            // Then the manager
            m_Manager = obj.AddComponent<ScreenshotManager>();
            m_Manager.m_Config = m_ConfigAsset.m_Config;
            m_Manager.Awake();
        }

        void DestroyManager()
        {
            if (m_Manager == null)
                return;

            if (Application.isPlaying)
                return;

            GameObject.DestroyImmediate(m_Manager.gameObject);
            m_Manager = null;
        }

        IEnumerator CaptureCoroutine()
        {
            yield return m_Manager.StartCoroutine(m_Manager.CaptureAllCoroutine());
            DestroyManager();
        }


        IEnumerator UpdatePreviewCoroutine()
        {
            yield return m_Manager.StartCoroutine(m_Manager.UpdatePreviewCoroutine());
            DestroyManager();
        }

        #endregion




        public static void DrawSupportGUI()
        {

            if (typeof(ScreenshotManager).Assembly.GetType("AlmostEngine.Preview.UniversalDevicePreview") == null)
            {

                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.LabelField("UPGRADE");

                EditorGUILayout.HelpBox("Upgrade to Universal Device Preview to get an accurate preview of your game on more than 200 devices. " +
                "It contains hundreds of resolution and device presets, popularity presets to be sure that your game is adapted to your target devices, and much more.", MessageType.Info);

                Color c = GUI.color;
                GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
                if (GUILayout.Button("Upgrade to Universal Device Preview", GUILayout.Height(30)))
                {
                    Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/universal-device-preview-gallery-82015");
                }
                GUI.color = c;

                EditorGUILayout.EndVertical();

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
            }



            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("SUPPORT");

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
            reviewtitle.text = "Leave a Review";
            if (GUILayout.Button(reviewtitle, GUILayout.Height(50)))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/ultimate-screenshot-creator-82008");
            }
            // GUI.color = col;
            // if (GUILayout.Button("Leave a Review"))
            // {
            //     Application.OpenURL("https://assetstore.unity.com/packages/tools/utilities/ultimate-screenshot-creator-82008");
            // }
            GUI.color = cc;


            EditorGUILayout.EndVertical();


        }


        public static void DrawContactGUI()
        {
            EditorGUILayout.LabelField(UltimateScreenshotCreator.VERSION, UIStyle.centeredGreyTextStyle);
            EditorGUILayout.LabelField(UltimateScreenshotCreator.AUTHOR, UIStyle.centeredGreyTextStyle);
        }
    }
}
