#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Preview
{

    public class ResolutionWindowBase : EditorWindow, ISerializationCallbackReceiver
    {
        protected PreviewConfigAsset m_ConfigAsset;

        protected SerializedObject m_SerializedConfig;

        protected Texture2D m_BackgroundTexture;
        protected GUIStyle m_NameStyle;
        protected Vector2 m_ScrollviewPos;
        protected int m_WindowWidth;
        public bool m_IsUpdating = false;


        public static UnityAction onUpdateBeginDelegate = () =>
        {
        };
        public static UnityAction onUpdateEndDelegate = () =>
        {
        };



        void Awake()
        {
            Reset();

        }





#if UNITY_2017_2_OR_NEWER
        protected void PlaymodeStateChanged(PlayModeStateChange change)
        {

            //			Debug.Log ("Change " + change.ToString ());

            switch (change)
            {
                case PlayModeStateChange.ExitingEditMode:
                case PlayModeStateChange.ExitingPlayMode:
                    // BackUpDevicePreview();
                    break;
                case PlayModeStateChange.EnteredEditMode:
                case PlayModeStateChange.EnteredPlayMode:
                    RestoreDevicePreview();
                    break;
            }
            Repaint();
        }

#endif

        public void OnBeforeSerialize()
        {
            // Debug.Log("OnBeforeSerialize");
            // BackUpDevicePreview();
        }

        public void OnAfterDeserialize()
        {
            // Debug.Log("OnAfterDeserialize");
        }

        void Reset()
        {
            //			Debug.Log ("Reset");

            m_ConfigAsset = ResolutionSettingsWindow.GetConfig();

            DestroyManager();

            if (m_Camera)
            {
                GameObject.DestroyImmediate(m_Camera.gameObject);
            }
            if (m_Canvas)
            {
                GameObject.DestroyImmediate(m_Canvas.gameObject);
            }

            m_IsUpdating = false;
        }

        protected virtual void OnEnable()
        {
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged += StateChange;
            EditorApplication.playModeStateChanged += PlaymodeStateChanged;
#else
			EditorApplication.playmodeStateChanged += StateChange;
#endif

#if UNITY_2018_1_OR_NEWER
            EditorApplication.quitting += BackUpDevicePreview;
#endif


            EditorSceneManager.sceneOpened += SceneOpenedCallback;
            // EditorSceneManager.sceneClosed += SceneClosedCallback;

            RestoreDevicePreview();
            m_NeedsBackup = false;
        }


        void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
        {
            // Debug.Log("Scene opened " + scene.name);
            RestoreDevicePreview();
        }
        void SceneClosedCallback(Scene scene)
        {
            // Debug.Log("Scene closed " + scene.name);
            // BackUpDevicePreview();
        }

        protected virtual void OnDisable()
        {

#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged -= StateChange;
            EditorApplication.playModeStateChanged -= PlaymodeStateChanged;
#else
			EditorApplication.playmodeStateChanged -= StateChange;
#endif
            EditorSceneManager.sceneOpened -= SceneOpenedCallback;
            // EditorSceneManager.sceneClosed -= SceneClosedCallback;

        }

#if UNITY_2017_2_OR_NEWER
        void StateChange(PlayModeStateChange state)
#else
		void StateChange ()
#endif
        {
            Reset();
        }

        #region Events

        protected virtual void HandleEditorEvents()
        {
        }

        // string m_ActiveSceneName = "";

        void Update()
        {
            // if (m_ActiveSceneName != SceneManager.GetActiveScene().name) {
            //     m_ActiveSceneName = SceneManager.GetActiveScene().name;
            //     BackUpDevicePreview();
            // }
            // if (!m_IsUpdating && m_NeedsBackup){
            //     BackUpDevicePreview();
            // }

            // Debug.Log("Maximum texture size: " + SystemInfo.maxTextureSize);


            AutoRefresh();
            UpdateAllEmptyResolution();
            DeviceInfo.m_IsLivePreview = m_ConfigAsset.m_AutoRefresh;
        }

        DateTime m_LastEditorUpdate = new DateTime();

        void AutoRefresh()
        {
            if (m_IsUpdating)
                return;

            if (EditorApplication.isPaused)
                return;

            if (Application.isPlaying && m_ConfigAsset.m_RefreshMode == PreviewConfigAsset.AutoRefreshMode.ONLY_IN_EDIT_MODE)
                return;

            if (!Application.isPlaying && m_ConfigAsset.m_RefreshMode == PreviewConfigAsset.AutoRefreshMode.ONLY_IN_PLAY_MODE)
                return;

            if (Application.isPlaying && Time.timeSinceLevelLoad < 0.1f)
                return;

            if (!m_IsUpdating && m_ConfigAsset.m_AutoRefresh && (DateTime.Now - m_LastEditorUpdate).TotalSeconds > m_ConfigAsset.m_RefreshDelay)
            {
                UpdateAllRequiredResolutions();
            }
        }

        void UpdateAllEmptyResolution()
        {
            if (m_IsUpdating)
                return;

            if (m_ConfigAsset.m_AutoGenerateEmptyPreview == false)
                return;

            if (m_Loading)
                return;

            List<ScreenshotResolution> toUpdate = new List<ScreenshotResolution>();
            foreach (ScreenshotResolution resolution in GetResolutionsToUpdate())
            {
                if (resolution.m_Texture == null || resolution.m_Texture.width == 0)
                {
                    toUpdate.Add(resolution);
                }
            }
            if (toUpdate.Count > 0)
            {
                Reset();
                InitTempManager();
                m_ScreenshotTaker.StartCoroutine(UpdateCoroutine(toUpdate));
            }
        }

        #endregion

        #region UI

        void OnGUI()
        {
            if (EditorApplication.isCompiling)
            {
                Reset();
            }

            m_WindowWidth = (int)position.width;

            HandleEditorEvents();

            InitStyle();

            // Init manager serializer
            m_SerializedConfig = new SerializedObject(m_ConfigAsset);
            m_SerializedConfig.Update();

            // Draw GUI
            EditorGUI.BeginChangeCheck();
            DrawToolBarGUI();
            if (EditorGUI.EndChangeCheck())
            {
                ResolutionWindowBase.RepaintWindows();
            }

            DrawPreviewGUI();

            // APply properties
            m_SerializedConfig.ApplyModifiedProperties();

        }

        void InitStyle()
        {
            if (m_BackgroundTexture == null)
            {
                m_BackgroundTexture = new Texture2D(1, 1);
                m_BackgroundTexture.SetPixel(0, 0, Color.white);
                m_BackgroundTexture.Apply();
            }

            if (m_NameStyle == null)
            {
                m_NameStyle = new GUIStyle();
                m_NameStyle.wordWrap = true;
                m_NameStyle.alignment = TextAnchor.MiddleCenter;
            }
        }

        static float PixelsPerPoint()
        {
#if UNITY_5_4_OR_NEWER
            return EditorGUIUtility.pixelsPerPoint;
#else
			return 1f;
#endif
        }

        protected Vector2 GetRenderSize(ScreenshotResolution resolution, float zoom, PreviewConfigAsset.GalleryDisplayMode mode, bool withBorders = true)
        {
            int displayWidth, displayHeight;
            int width = resolution.ComputeTargetWidth();
            int height = resolution.ComputeTargetHeight();

            // We use the texture when available to get the size with borders
            if (withBorders && resolution.m_Texture != null)
            {
                width = resolution.m_Texture.width;
                height = resolution.m_Texture.height;
            }

            if (width <= 0 || height <= 0)
            {
                EditorGUILayout.LabelField("Invalid dimensions for resolution " + resolution.ToString());
                return Vector2.zero;
            }

            // Compute the box dimensions depending on the display mode
            if (mode == PreviewConfigAsset.GalleryDisplayMode.RATIOS)
            {
                displayWidth = (int)((m_WindowWidth - m_ConfigAsset.m_MarginHorizontal) * zoom);
                displayHeight = (int)(displayWidth * height / width);
            }
            else if (mode == PreviewConfigAsset.GalleryDisplayMode.PIXELS || resolution.m_PPI <= 0)
            {
                displayWidth = (int)(width * zoom / PixelsPerPoint());
                displayHeight = (int)(height * zoom / PixelsPerPoint());
            }
            else
            {
                var ppi = resolution.m_ForcedUnityPPI > 0 ? resolution.m_ForcedUnityPPI : resolution.m_PPI;
                displayWidth = (int)(width * zoom / ppi * m_ConfigAsset.m_ScreenPPI / PixelsPerPoint());
                displayHeight = (int)(height * zoom / ppi * m_ConfigAsset.m_ScreenPPI / PixelsPerPoint());
            }

            // Invert the scaling
            displayWidth = (int)((float)displayWidth / resolution.m_Scale);
            displayHeight = (int)((float)displayHeight / resolution.m_Scale);

            return new Vector2(displayWidth, displayHeight);
        }


        protected Vector2 scroll;
        protected int height;
        protected int width;

        protected virtual void DrawPreviewGUI()
        {
        }

        protected virtual void DrawToolBarGUI()
        {
        }

        #endregion



        #region Actions

        public void UpdateAllRequiredResolutions()
        {
            Reset();
            InitTempManager();
            m_ScreenshotTaker.StartCoroutine(UpdateCoroutine(GetResolutionsToUpdate()));
        }

        protected virtual void UpdateWindowResolutions()
        {
            Reset();
            InitTempManager();
            m_ScreenshotTaker.StartCoroutine(UpdateCoroutine(m_ConfigAsset.m_Config.GetActiveResolutions(), true));
        }

        public virtual void Export()
        {
            //m_ConfigAsset.m_Config.ExportToFiles(GetResolutionsToUpdate());
            if (m_ConfigAsset.m_Selected < m_ConfigAsset.m_Config.GetActiveResolutions().Count)
            {
                m_ConfigAsset.m_Config.ExportToFiles(new List<ScreenshotResolution> { m_ConfigAsset.m_Config.GetActiveResolutions()[m_ConfigAsset.m_Selected] });
            }
        }

        protected static ScreenshotTaker m_ScreenshotTaker;

        protected void InitTempManager()
        {
            if (m_ScreenshotTaker != null)
            {
                if (!Application.isPlaying)
                {
                    m_ScreenshotTaker.Reset();
                }
                return;
            }

            if (m_SafeAreaInstance != null)
            {
                GameObject.DestroyImmediate(m_SafeAreaInstance);
            }


            GameObject obj = new GameObject();
            obj.name = "Temporary screenshot taker - remove if still exists in scene in edit mode.";
            obj.hideFlags = HideFlags.HideAndDontSave;

            // Create the screenshot taker
            m_ScreenshotTaker = obj.AddComponent<ScreenshotTaker>();
#if (UNITY_EDITOR) && (!UNITY_5_4_OR_NEWER)
			m_ScreenshotTaker.m_ForceLayoutPreservation = false;
#endif
            m_ScreenshotTaker.m_GameViewResizingWaitingMode = m_ConfigAsset.m_Config.m_GameViewResizingWaitingMode;
            m_ScreenshotTaker.m_GameViewResizingWaitingFrames = m_ConfigAsset.m_Config.m_ResizingWaitingFrames;
            m_ScreenshotTaker.m_GameViewResizingWaitingTime = m_ConfigAsset.m_Config.m_ResizingWaitingTime;
        }

        protected void DestroyManager()
        {
            //			Debug.Log ("Destroy manager");

            if (m_ScreenshotTaker == null)
                return;

            if (Application.isPlaying)
                return;

            m_ScreenshotTaker.Reset();
            GameObject.DestroyImmediate(m_ScreenshotTaker.gameObject);
            m_ScreenshotTaker = null;
        }

        GameObject m_SafeAreaInstance;


        static EditorWindow m_PreviousWindowFocus;

        protected IEnumerator UpdateCoroutine(List<ScreenshotResolution> resolutions, bool restoreFocus = false)
        {
            if (m_IsUpdating)
                yield break;



            // Save current focused window
            m_PreviousWindowFocus = EditorWindow.focusedWindow;
            m_IsUpdating = true;
            m_NeedsBackup = true;

            onUpdateBeginDelegate();


            // Safe area in auto refresh
            if (Application.isPlaying && m_ConfigAsset.m_AutoRefresh && m_ConfigAsset.m_DrawSafeArea && m_SafeAreaInstance == null)
            {
                m_SafeAreaInstance = GameObject.Instantiate(m_ConfigAsset.m_SafeAreaCanvas.gameObject);
                m_SafeAreaInstance.hideFlags = HideFlags.DontSave;
            }

            foreach (ScreenshotResolution res in resolutions)
            {
                // Debug.Log("Start update " + res);


                DeviceInfo.SetSimulatedDevice(res);

                // Restore texture from cache if possible
                if (m_ConfigAsset.m_DrawingMode != PreviewConfigAsset.DrawingMode.TEXTURE_ONLY)
                {
                    if (m_PreviewTextureCache.ContainsKey(res) && m_PreviewTextureCache[res] != null)
                    {
                        // Debug.Log("Restore texture from cache " + m_PreviewTextureCache[res].width + "x" + m_PreviewTextureCache[res].height);
                        res.m_Texture = m_PreviewTextureCache[res];
                    }
                }

                // Clear the texture if needed (to fix "washed" textures because texture.resize() returns invalid color space in URP/HDRP)
                if (res.m_Texture != null && (res.ComputeTargetWidth() != res.m_Texture.width || res.ComputeTargetHeight() != res.m_Texture.height || res.m_Texture.format != TextureFormat.RGB24))
                {
                    // Debug.LogWarning("Reset texture to NULL");
                    res.m_Texture = null;
                }

                // Low memory usage
                if (m_ConfigAsset.m_OptimizePreviewResolution)
                {
                    res.m_Scale = 1f;
                    var renderSize = GetRenderSize(res, GetZoom(), GetDisplayMode(), false);
                    float lowMemoryScale = renderSize.x / res.ComputeTargetWidth();
                    lowMemoryScale *= 1.1f; // slighlty oversampled
                    lowMemoryScale = Mathf.Clamp01(lowMemoryScale);
                    res.m_Scale = lowMemoryScale;
                }

                // Capture the textures
                yield return m_ScreenshotTaker.StartCoroutine(m_ScreenshotTaker.CaptureResolutionCoroutine(res, new List<Camera>(),
                    m_SafeAreaInstance == null && m_ConfigAsset.m_DrawSafeArea && DeviceInfo.HasSimulatedSafeAreaValue() ? new List<Canvas> { m_ConfigAsset.m_SafeAreaCanvas } : new List<Canvas>(),
                    ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING));

                // Capture the masks
                if (m_ConfigAsset.m_DrawingMode != PreviewConfigAsset.DrawingMode.TEXTURE_ONLY)
                {
                    yield return m_ScreenshotTaker.StartCoroutine(CaptureMasks(res));
                }

                if (m_ConfigAsset.m_AutoRefresh != true)
                {
                    DeviceInfo.SetSimulatedDevice(null);
                }

            }



            if (!(Application.isPlaying && m_ConfigAsset.m_AutoRefresh) || !m_ConfigAsset.m_DrawSafeArea)
            {
                GameObject.DestroyImmediate(m_SafeAreaInstance);
            }


            m_LastEditorUpdate = DateTime.Now;
            Repaint();
            DestroyManager();

            onUpdateEndDelegate();



            // Force final repaint
            if (!Application.isPlaying)
            {
                GameViewController.SetGameViewSize(Screen.width, Screen.height);
                GameViewController.RestoreGameViewSize();

                SceneView.RepaintAll();
                Repaint();
            }

            // Restore previous focus windows
            if (m_PreviousWindowFocus != null)
            {
                m_PreviousWindowFocus.Focus();
                Repaint();
            }


            if (m_ConfigAsset.m_BackupPreviewToDisk)
            {
                BackUpDevicePreview();
            }


            m_IsUpdating = false;

        }


        protected virtual float GetZoom()
        {
            return m_ConfigAsset.m_PreviewGalleryZoom;
        }
        protected virtual PreviewConfigAsset.GalleryDisplayMode GetDisplayMode()
        {
            return m_ConfigAsset.m_GalleryDisplayMode;
        }


        Camera m_Camera;
        Canvas m_Canvas;


        Dictionary<ScreenshotResolution, Texture2D> m_PreviewTextureCache = new Dictionary<ScreenshotResolution, Texture2D>();
        Dictionary<ScreenshotResolution, Texture2D> m_FullPreviewTextureCache = new Dictionary<ScreenshotResolution, Texture2D>();

        protected IEnumerator CaptureMasks(ScreenshotResolution res)
        {
            if (m_ConfigAsset.m_DeviceRendererCamera == null)
                yield break;


            // Init camera
            m_Camera = GameObject.Instantiate(m_ConfigAsset.m_DeviceRendererCamera);
            m_Camera.hideFlags = HideFlags.DontSave;
            m_Camera.clearFlags = CameraClearFlags.Color;
            m_Camera.backgroundColor = EditorGUIUtility.isProSkin
                ? new Color32(56, 56, 56, (byte)(m_ConfigAsset.m_TransparentDeviceBackground ? 0 : 255))
                : new Color32(194, 194, 194, (byte)(m_ConfigAsset.m_TransparentDeviceBackground ? 0 : 255));

            // Set preview camera as Main camera
            // to prevent Camera.main being null for a few frames
            m_Camera.tag = "MainCamera";

            // Init canvas instance
            m_Canvas = res.m_DeviceCanvas;
            if (m_Canvas == null)
            {
                m_Canvas = m_ConfigAsset.m_DefaultDeviceCanvas;
            }
            if (m_Canvas == null)
                yield break;
            m_Canvas = GameObject.Instantiate(m_Canvas);
            m_Canvas.hideFlags = HideFlags.DontSave;

            // Texture
            MaskRenderer mask = m_Canvas.GetComponent<MaskRenderer>();
            mask.m_Texture.texture = res.m_Texture;

            if (m_ConfigAsset.m_DrawingMode == PreviewConfigAsset.DrawingMode.SCREEN_MASK)
            {
                mask.m_Mask.rectTransform.anchoredPosition = Vector2.zero;
                mask.m_Device.gameObject.SetActive(false);
                if (mask.m_BorderTop)
                {
                    mask.m_BorderTop.gameObject.SetActive(false);
                    mask.m_BorderLeft.gameObject.SetActive(false);
                    mask.m_BorderRight.gameObject.SetActive(false);
                    mask.m_BorderBottom.gameObject.SetActive(false);
                }
            }

            // Scale the UI 
            Vector3 panelScale = Vector3.one;
            int widthWithDevice = res.ComputeTargetWidth();
            int heightWithDevice = res.ComputeTargetHeight();


            if (res.m_Orientation == ScreenshotResolution.Orientation.PORTRAIT)
            {

                // Resize the whole canvas
                panelScale.x = (float)res.ComputeTargetWidth() / (float)mask.m_Mask.GetComponent<RectTransform>().rect.width;
                panelScale.y = (float)res.ComputeTargetHeight() / (float)mask.m_Mask.GetComponent<RectTransform>().rect.height;
                mask.m_Panel.localScale = panelScale;

                // Add the border margin
                if (m_ConfigAsset.m_DrawingMode == PreviewConfigAsset.DrawingMode.FULL_DEVICE)
                {
                    widthWithDevice = (int)(mask.m_Device.GetComponent<RectTransform>().rect.width * panelScale.x);
                    heightWithDevice = (int)(mask.m_Device.GetComponent<RectTransform>().rect.height * panelScale.y);
                }

            }
            else
            {
                float rotation = (res.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE) ? 90f : -90f;

                // Rotate the whole canvas
                mask.m_Panel.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);

                // Resize the whole canvas
                panelScale.x = (float)res.ComputeTargetWidth() / (float)mask.m_Mask.GetComponent<RectTransform>().rect.height;
                panelScale.y = (float)res.ComputeTargetHeight() / (float)mask.m_Mask.GetComponent<RectTransform>().rect.width;
                mask.m_Panel.localScale = new Vector3(panelScale.y, panelScale.x, 1f);

                // Rotate the texture and scale it to match the screen size
                mask.m_Texture.transform.localRotation = Quaternion.AngleAxis(-rotation, Vector3.forward);
                float tmp = mask.m_Texture.rectTransform.sizeDelta.x;
                mask.m_Texture.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mask.m_Texture.rectTransform.sizeDelta.y);
                mask.m_Texture.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tmp);

                // Add the border margin
                if (m_ConfigAsset.m_DrawingMode == PreviewConfigAsset.DrawingMode.FULL_DEVICE)
                {
                    widthWithDevice = (int)(mask.m_Device.GetComponent<RectTransform>().rect.height * panelScale.x);
                    heightWithDevice = (int)(mask.m_Device.GetComponent<RectTransform>().rect.width * panelScale.y);
                }

            }

            // Compute target resolution
            if (m_ConfigAsset.m_DrawingMode == PreviewConfigAsset.DrawingMode.SCREEN_MASK)
            {
                mask.m_Device.gameObject.SetActive(false);
            }
            else
            {
                mask.m_Device.gameObject.SetActive(true);
            }

            // Get or create the texture
            Texture2D fullPreviewTexture = null;
            if (m_FullPreviewTextureCache.ContainsKey(res) && m_FullPreviewTextureCache[res] != null)
            {
                // Debug.Log("Get full preview texture from cache " + widthWithDevice + "x" + heightWithDevice);
                fullPreviewTexture = m_FullPreviewTextureCache[res];
            }
            else
            {
                // Debug.LogWarning("Create new full preview texture " + widthWithDevice + "x" + heightWithDevice);
                fullPreviewTexture = new Texture2D(widthWithDevice, heightWithDevice, TextureFormat.ARGB32, false, false);
            }

#if UNITY_2021_3_OR_NEWER
            // Hide all UI toolkit elements before rendering the device
            var activeUIToolkitDocuments = GameObject.FindObjectsOfType<UnityEngine.UIElements.UIDocument>();
            foreach (var uiDocument in activeUIToolkitDocuments)
            {
                uiDocument.gameObject.SetActive(false);
            }
#endif

            // Capture the device
            // Debug.Log("Capture mask " + widthWithDevice + "x" + heightWithDevice);
            yield return m_ScreenshotTaker.StartCoroutine(m_ScreenshotTaker.CaptureToTextureCoroutine(fullPreviewTexture, widthWithDevice, heightWithDevice,
                new List<Camera> { m_Camera }, new List<Canvas> { m_Canvas },
                ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING, 8, false, ScreenshotTaker.ColorFormat.RGBA, false));


#if UNITY_2021_3_OR_NEWER
            // Restore UI toolkit elements
            foreach (var uiDocument in activeUIToolkitDocuments)
            {
                uiDocument.gameObject.SetActive(true);
            }
#endif


            // Save textures to cache
            // Debug.Log("Saving full preview texture to cache " + fullPreviewTexture.width + "x" + fullPreviewTexture.height);
            // Debug.Log("Saving preview texture to cache " + res.m_Texture.width + "x" + res.m_Texture.height);
            m_FullPreviewTextureCache[res] = fullPreviewTexture;
            m_PreviewTextureCache[res] = res.m_Texture;

            // Replace the texture
            res.m_Texture = fullPreviewTexture;

            // Clean
            GameObject.DestroyImmediate(m_Camera.gameObject);
            GameObject.DestroyImmediate(m_Canvas.gameObject);
        }

        protected virtual List<ScreenshotResolution> GetResolutionsToUpdate()
        {
            List<ScreenshotResolution> resolutions = new List<ScreenshotResolution>();

            // Look if the preview or gallery window is open
            if (ResolutionGalleryWindow.IsOpen())
            {
                // If the gallery is open, we update all resolutions
                resolutions.AddRange(m_ConfigAsset.m_Config.GetActiveResolutions());

            }
            else if (ResolutionPreviewWindow.IsOpen())
            {
                // If only the preview is open, we only update the selected resolution
                if (m_ConfigAsset.m_Selected < m_ConfigAsset.m_Config.GetActiveResolutions().Count)
                {
                    resolutions.Add(m_ConfigAsset.m_Config.GetActiveResolutions()[m_ConfigAsset.m_Selected]);
                }
            }

            return resolutions;
        }

        public static void RepaintWindows()
        {
            if (ResolutionGalleryWindow.IsOpen())
            {
                ResolutionGalleryWindow.m_Window.Repaint();
            }
            if (ResolutionPreviewWindow.IsOpen())
            {
                ResolutionPreviewWindow.m_Window.Repaint();
            }
            if (ResolutionSettingsWindow.IsOpen())
            {
                ResolutionSettingsWindow.m_Window.Repaint();
            }
        }


        #endregion




        #region Serialize temp screenshots

        protected bool m_Loading = false;
        protected bool m_NeedsBackup = false;

        string m_TempFolder = "Temp_DevicePreview";


        // protected Dictionary<ScreenshotResolution, Texture2D> m_PreviewTables = new Dictionary<ScreenshotResolution, Texture2D>();

        // public void SavePreviewTable()
        // {
        //     foreach (var res in m_ConfigAsset.m_Config.GetActiveResolutions())
        //     {
        //         Debug.Log("save res " + res.ToString() + " texture " + res.m_Texture.width);
        //         if (res.m_Texture != null)
        //         {
        //             m_PreviewTables[res] = res.m_Texture;
        //         }
        //     }
        // }

        // public void LoadPreviewTable()
        // {
        //     foreach (var res in m_ConfigAsset.m_Config.GetActiveResolutions())
        //     {
        //         Debug.Log("restore res " + res.ToString());
        //         if (m_PreviewTables.ContainsKey(res))
        //         {
        //             if (m_PreviewTables[res] != null)
        //             {
        //                 res.m_Texture = m_PreviewTables[res];
        //             }
        //         }
        //     }
        // }


        protected void BackUpDevicePreview()
        {
            if (!m_NeedsBackup)
                return;

            // Do not backup on auto refresh to prevent too long updates
            if (m_ConfigAsset.m_AutoRefresh)
                return;

            if (m_ConfigAsset.m_BackupPreviewToDisk == false)
                return;

            // Debug.Log("Backup");
            // Export screenshots to temporary folder
            foreach (ScreenshotResolution resolution in m_ConfigAsset.m_Config.GetActiveResolutions())
            {
                if (resolution.m_Texture == null || resolution.m_Texture.width == 0)
                    continue;


                string filename = Application.persistentDataPath + "/" + m_TempFolder + "/" + resolution.m_ResolutionName + ".png";

                TextureExporter.ExportToFile(resolution.m_Texture, filename, TextureExporter.ImageFileFormat.PNG, 100, false, false);
            }
        }

        protected void RestoreDevicePreview()
        {
            if (m_ConfigAsset == null || m_ConfigAsset.m_BackupPreviewToDisk == false)
                return;

            // Debug.Log("Restore");

            m_Loading = true;
            m_NeedsBackup = false;
            m_ConfigAsset = ResolutionSettingsWindow.GetConfig();
            // Load screenshots from temporary folder
            foreach (ScreenshotResolution resolution in m_ConfigAsset.m_Config.GetActiveResolutions())
            {

                if (resolution.m_Texture != null)
                    continue;

                string filename = Application.persistentDataPath + "/" + m_TempFolder + "/" + resolution.m_ResolutionName + ".png";

                //				Debug.Log ("Loading tmp image " + filename);

                if (!System.IO.File.Exists(filename))
                    continue;

                resolution.m_Texture = TextureExporter.LoadFromFile(filename);
            }
            m_Loading = false;
        }

        #endregion

    }
}

#endif