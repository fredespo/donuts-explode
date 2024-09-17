using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine.Screenshot
{
    [System.Serializable]
    public class ScreenshotConfig
    {
        public ScreenshotConfig()
        {
            m_GameViewResolution = new ScreenshotResolution();
        }

        #region DESTINATION

        public ScreenshotNameParser.DestinationFolder m_DestinationFolder = ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER;
        public string m_RelativePath = "Screenshots/";
        public string m_RootedPath = "";
        public bool m_ExportToPhoneGallery = true;
        [Tooltip("After the capture process, asynchronously encode and export the screenshot. Doesn't block the main thread but uses more memory. Only available with NET 4.0 and Net Standard 2.0.")]
        public bool m_ExportAsync = false;

        public string GetPath()
        {
            string path = m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER ? m_RootedPath : m_RelativePath;
            return ScreenshotNameParser.ParsePath(m_DestinationFolder, path);
        }

        #endregion

        #region NAME

        public string m_FileName = "{width}x{height}-screenshot";

        [Tooltip("Overwrite files or increment automatically the filenames.")]
        public bool m_OverwriteFiles = false;

        [Tooltip("How many left zeros should there be in the numbers.")]
        public int m_NumberLeftPadding = 0;

        [Tooltip("Use PNG to create screenshots with a transparent background.")]
        public TextureExporter.ImageFileFormat m_FileFormat;

        public float m_JPGQuality = 75f;

        public string m_ShareTitle = "Share the screenshot";
        public string m_ShareSubject = "Nice screenshot";
        public string m_ShareText = "Look at this nice screenshot.";

        #endregion

        #region CAPTURE MODES


        public ScreenshotTaker.CaptureMode m_CaptureMode = ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW;

        public enum AntiAliasing
        {
            NONE = 0,
            TWO = 2,
            FOUR = 4,
            EIGHT = 8
        }

        ;

        public AntiAliasing m_MultisamplingAntiAliasing = AntiAliasing.EIGHT;

        [Tooltip("RGB is the default color format.\n" +
        "Use RGBA to create screenshots with an alpha layer, enabling transparent backgrounds.")]
        public ScreenshotTaker.ColorFormat m_ColorFormat;

        [Tooltip("Force alpha layer to be recomputed. This is a costly process. " +
        "Use only if you have alpha problems in RGBA mode.")]
        public bool m_RecomputeAlphaLayer = false;
        [Tooltip("When alpha layer is recomputed in HDRP, disable all FX during alpha recomputation.")]
        public bool m_DisableHDRPFX = true;

        public enum ShotMode
        {
            ONE_SHOT,
            BURST
        }
        ;

        public ShotMode m_ShotMode;
        public int m_MaxBurstShotsNumber = 20;
        public int m_MaxShotPerSeconds = 30;
        public bool m_FixedFrameRate = true;

        #endregion

        #region CAMERAS

        public enum CamerasMode
        {
            GAME_VIEW,
            SCENE_VIEW,
            CUSTOM_CAMERAS
        }
        ;

        [Tooltip("GAME_VIEW will capture what you see on the screen.\n" +
        "CUSTOM_CAMERAS allows you to customize the cameras to be used, and their rendering properties.")]
        public CamerasMode m_CameraMode;


        [Tooltip("When enabled, one screenshot is taken for each camera, to be used as independent layers for compositing.")]
        public bool m_ExportToDifferentLayers = false;

        public List<ScreenshotCamera> m_Cameras = new List<ScreenshotCamera>();

        public List<ScreenshotCamera> GetActiveCameras()
        {
            List<ScreenshotCamera> cameras = new List<ScreenshotCamera>();

            if (m_CameraMode == ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS)
            {
                foreach (ScreenshotCamera camera in m_Cameras)
                {

                    // Ignore inactive ones
                    if (camera.m_Active == false)
                        continue;

                    // Ignore invalid ones
                    if (camera.m_Camera == null)
                        continue;

                    cameras.Add(camera);
                }
            }
#if UNITY_EDITOR
            if (m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL || m_CameraMode == CamerasMode.SCENE_VIEW)
            {
                cameras.Add(new ScreenshotCamera(SceneView.lastActiveSceneView.camera));
            }
#endif

            return cameras;
        }



        public void AlignToView()
        {
#if UNITY_EDITOR
            if (SceneView.lastActiveSceneView != null)
            {
                if (m_CameraMode == ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS)
                {
                    foreach (ScreenshotCamera camera in GetActiveCameras())
                    {
                        Undo.RecordObject(camera.m_Camera.transform, "Changed Camera position");
                        camera.m_Camera.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
                        camera.m_Camera.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                    }
                }
                else
                {
                    Undo.RecordObject(Camera.main.transform, "Changed Camera position");
                    Camera.main.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
                    Camera.main.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                }
            }
#endif
        }

        #endregion

        #region RESOLUTIONS

        public enum ResolutionMode
        {
            GAME_VIEW,
            CUSTOM_RESOLUTIONS
        }
        ;

        [Tooltip("GAME_VIEW will capture what you see on the screen.\n" +
        "CUSTOM_RESOLUTIONS allows you to customize the resolutions to be used.")]
        public ResolutionMode m_ResolutionCaptureMode = ResolutionMode.CUSTOM_RESOLUTIONS;

        public List<ScreenshotResolution> m_Resolutions = new List<ScreenshotResolution>();

        public ScreenshotResolution m_GameViewResolution;

        public ScreenshotResolution GetFirstActiveResolution()
        {
            var resolutions = GetActiveResolutions();
            if (resolutions.Count > 0)
            {
                return resolutions[0];
            }
            return m_GameViewResolution;
        }

        public List<ScreenshotResolution> GetActiveResolutions()
        {
            List<ScreenshotResolution> resolutions = new List<ScreenshotResolution>();

            if (m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW &&
                m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL &&
                m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL &&
                m_ResolutionCaptureMode == ScreenshotConfig.ResolutionMode.CUSTOM_RESOLUTIONS)
            {
                foreach (ScreenshotResolution resolution in m_Resolutions)
                {

                    // Ignore inactive ones
                    if (resolution.m_Active == false)
                        continue;

                    // Ignore invalid ones
                    if (!resolution.IsValid())
                        continue;

                    resolutions.Add(resolution);
                }
            }

            if (m_ResolutionCaptureMode == ScreenshotConfig.ResolutionMode.GAME_VIEW
                || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW
                || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL
                || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                resolutions.Add(m_GameViewResolution);
                return resolutions;
            }

            return resolutions;
        }


        public Vector2 GetFixedGameviewMultiDisplayResolution()
        {
            Camera camera = null;
            if (MultiDisplayCameraCapture.m_MultiDisplayCamera.Count > 0)
            {
                camera = MultiDisplayCameraCapture.m_MultiDisplayCamera[0].GetComponent<Camera>();
            }
            else if (m_Cameras.Count > 0)
            {
                camera = m_Cameras[0].m_Camera;
            }
            else
            {
                camera = Camera.main;
            }
            if (camera != null)
            {
                return new Vector2(camera.pixelRect.width, camera.pixelRect.height);
            }
            else
            {
                return Vector2.zero;
            }
        }

        public void UpdateGameviewResolution()
        {
            // Update the gameview screenshot resolution
            Vector2 size = GameViewController.GetCurrentGameViewSize();

#if UNITY_EDITOR
            // In Sceneview we set the size to sceneview windo size
            if (m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                size = GameViewUtils.GetSceneViewSize();
            }
#endif

            // In multi display we set the size to gameview associated to target multi display
            if (m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW && MultiDisplayUtils.IsMultiDisplay())
            {
                size = GetFixedGameviewMultiDisplayResolution();
                // Debug.Log("IS MULTI DISPLAY DETECTED " + size);
            }

            if (m_GameViewResolution == null)
            {
                InitGameViewResolution();
            }

            m_GameViewResolution.m_Width = (int)size.x;
            m_GameViewResolution.m_Height = (int)size.y;
        }

        protected void InitGameViewResolution()
        {
            m_GameViewResolution.m_Active = true;
            m_GameViewResolution.m_ResolutionName = "GameView";
            m_GameViewResolution.m_Scale = 1;
            m_GameViewResolution.m_Width = Screen.width;
            m_GameViewResolution.m_Height = Screen.height;
#if UNITY_EDITOR
            if (m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL || m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {
                m_GameViewResolution.m_ResolutionName = "SceneView";
                Vector2 size = GameViewUtils.GetSceneViewSize();
                m_GameViewResolution.m_Width = (int)size.x;
                m_GameViewResolution.m_Height = (int)size.y;
            }
#endif
            if (m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW && MultiDisplayUtils.IsMultiDisplay())
            {
                Vector2 size = GetFixedGameviewMultiDisplayResolution();
                m_GameViewResolution.m_Width = (int)size.x;
                m_GameViewResolution.m_Height = (int)size.y;
            }
        }

        public void UpdateResolutionFilenames(List<ScreenshotResolution> resolutions)
        {
            System.DateTime time = System.DateTime.Now;
            foreach (ScreenshotResolution resolution in resolutions)
            {
                UpdateFileName(resolution, time);
            }
        }

        public string ParseFileName(ScreenshotResolution resolution, System.DateTime time)
        {
            string path = m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER ? m_RootedPath : m_RelativePath;
            return ScreenshotNameParser.ParseFileName(m_FileName, resolution, m_DestinationFolder, path, m_FileFormat, m_OverwriteFiles, time, m_NumberLeftPadding);
        }

        public void UpdateFileName(ScreenshotResolution resolution, System.DateTime time)
        {
            resolution.m_FileName = ParseFileName(resolution, time);
        }

        public void UpdateRatios()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.UpdateRatio();
            }
        }

        public void SetAllPortait()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Orientation = ScreenshotResolution.Orientation.PORTRAIT;
            }
        }

        public void SetAllLandscape()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Orientation = ScreenshotResolution.Orientation.LANDSCAPE;
            }
        }

        public void SetAllLandscapeRight()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Orientation = ScreenshotResolution.Orientation.LANDSCAPE_RIGHT;
            }
        }

        public void SelectAllResolutions()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Active = true;
            }
        }

        public void ClearAllResolutions()
        {
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Active = false;
            }
        }

        public void RemoveAllResolutions()
        {
            m_Resolutions.Clear();
        }

        #endregion

        #region OVERLAYS

        [Tooltip("Capture or not the active UI Canvas.")]
        public bool m_CaptureActiveUICanvas = true;
        [Tooltip("When true, Canvas and UI elements that are not in any culling layers of the capture cameras will be disabled. Only work in custom camera mode.")]
        public bool m_ForceUICullingLayer = false;
        public List<ScreenshotOverlay> m_Overlays = new List<ScreenshotOverlay>();

        #endregion


        #region COMPOSITION

        public enum CompositionMode
        {
            SIMPLE_CAPTURE,
            COMPOSITION
        }
        ;

        public CompositionMode m_CompositionMode = CompositionMode.SIMPLE_CAPTURE;

        public List<ScreenshotComposition> m_Compositions = new List<ScreenshotComposition>();

        public List<ScreenshotComposition> GetActiveCompositions()
        {
            return m_Compositions.Where(x => x.m_Active == true).ToList<ScreenshotComposition>();
        }


        #endregion

        #region BATCHES

        public enum BatchMode
        {
            SIMPLE_CAPTURE,
            BATCHES
        }
        ;

        public BatchMode m_BatchMode = BatchMode.SIMPLE_CAPTURE;

        public List<ScreenshotBatch> m_Batches = new List<ScreenshotBatch>();

        public List<ScreenshotBatch> GetActiveBatches()
        {
            return m_Batches.Where(x => x.m_Active == true).ToList<ScreenshotBatch>();
        }

        public void UpdateBatchesData()
        {
            try
            {
                foreach (var c in m_Batches)
                {
                    ListExtras.Resize<ScreenshotBatch.ActiveItem>(c.m_ActiveResolutions, m_Resolutions.Count);
                    for (int i = 0; i < m_Resolutions.Count && i < c.m_ActiveResolutions.Count; ++i)
                    {
                        c.m_ActiveResolutions[i].m_Name = m_Resolutions[i].ToString();
                        c.m_ActiveResolutions[i].m_Id = i;
                        // c.m_ActiveResolutions[i].m_Resolution =  m_Resolutions[i];
                    }
                    ListExtras.Resize<ScreenshotBatch.ActiveItem>(c.m_ActiveCompositions, m_Compositions.Count);
                    for (int i = 0; i < m_Compositions.Count && i < c.m_ActiveCompositions.Count; ++i)
                    {
                        c.m_ActiveCompositions[i].m_Name = m_Compositions[i].m_Name;
                        c.m_ActiveCompositions[i].m_Id = i;
                        // c.m_ActiveCompositions[i].m_Composition =  m_Compositions[i];
                    }
                }
            }
            catch
            {
            }
        }


        #endregion


        #region PREVIEW

        public bool m_ShowGuidesInPreview = false;
        public Canvas m_GuideCanvas;
        public Color m_GuidesColor = Color.white;
        public bool m_ShowPreview = true;
        public float m_PreviewSize = 1f;

        [Tooltip("If set to true, the camera and overlay settings will be applied when the application starts playing.")]
        public bool m_PreviewInGameViewWhilePlaying = false;

        #endregion

        #region UTILS

        [Tooltip("If set to true, the manager is moved at startup in the persistent scene and not destroyed when the scene changes.")]
        public bool m_DontDestroyOnLoad = true;

        public bool m_StopTimeOnCapture = true;
        public bool m_PlaySoundOnCapture = true;
        public AudioClip m_ShotSound;

        [System.NonSerialized]
        public float m_Time = 1f;

        public void SetTime(float time)
        {
            if (time != m_Time)
            {
                m_Time = time;
                Time.timeScale = time;
            }
        }

        public void TogglePause()
        {
            if (m_Time == 0f)
            {
                SetTime(1f);
            }
            else
            {
                SetTime(0f);
            }
        }

        public void ClearCache()
        {
            m_GameViewResolution.m_Texture = null;
            foreach (ScreenshotResolution res in m_Resolutions)
            {
                res.m_Texture = null;
            }
        }

        public void ShareAll()
        {
            Share(GetActiveResolutions());
        }

        public void Share(List<ScreenshotResolution> resolutions)
        {
#if !USC_EXCLUDE_SHARE && (UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL)
            System.DateTime time = System.DateTime.Now;
            foreach (ScreenshotResolution resolution in resolutions)
            {
                // Parse
                UpdateFileName(resolution, time);
                var title = ScreenshotNameParser.ParseSymbols(m_ShareTitle, resolution, time, m_NumberLeftPadding);
                var subject = ScreenshotNameParser.ParseSymbols(m_ShareSubject, resolution, time, m_NumberLeftPadding);
                var text = ScreenshotNameParser.ParseSymbols(m_ShareText, resolution, time, m_NumberLeftPadding);
                // Share
                var name = System.IO.Path.GetFileNameWithoutExtension(resolution.m_FileName);
                ShareUtils.ShareImage(resolution.m_Texture, name, title, subject, text);
            }
#endif
        }

        public void ExportAllToFiles()
        {
            ExportToFiles(GetActiveResolutions());
        }

        public void ExportToFiles(List<ScreenshotResolution> resolutions)
        {
            System.DateTime time = System.DateTime.Now;
            foreach (ScreenshotResolution resolution in resolutions)
            {
                UpdateFileName(resolution, time);
                string filename = resolution.m_FileName;
                TextureExporter.ExportToFile(resolution.m_Texture, filename, m_FileFormat, (int)m_JPGQuality, true, m_ExportAsync,
                                            () => { Debug.Log("Screenshot created : " + filename); },
                                            () => { Debug.LogError("Failed to create screenshot : " + filename); });
            }
        }

        #endregion

        #region HOTKEYS

        public HotKey m_CaptureHotkey = new HotKey(false, false, false, KeyCode.None);
        public HotKey m_UpdatePreviewHotkey = new HotKey(false, false, false, KeyCode.None);
        public HotKey m_AlignHotkey = new HotKey(false, false, false, KeyCode.None);
        public HotKey m_PauseHotkey = new HotKey(false, false, false, KeyCode.None);


// #if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
//         public HotKey m_NewInputCaptureHotkey = new HotKey(false, false, false, KeyCode.None);
//         public HotKey m_NewInputUpdatePreviewHotkey = new HotKey(false, false, false, KeyCode.None);
//         public HotKey m_NewInputAlignHotkey = new HotKey(false, false, false, KeyCode.None);
//         public HotKey m_NewInputPauseHotkey = new HotKey(false, false, false, KeyCode.None);
// #endif

        #endregion


        #region ScreenshotTaker

        public ScreenshotTaker.GameViewResizingWaitingMode m_GameViewResizingWaitingMode;
        public float m_ResizingWaitingTime = 1f;
        public int m_ResizingWaitingFrames = 2;

        #endregion


        #region UI

        public int m_ItemsPerPage = 20;
        public bool m_ShowDestination = true;
        public bool m_ShowName = true;
        public bool m_ShowCaptureMode = true;
        public bool m_ShowResolutions = true;
        public bool m_ShowCameras = true;
        public bool m_ShowCanvas = true;
        public bool m_ShowComposition = true;
        public bool m_ShowBatches = true;
        public bool m_ShowPreviewGUI = true;
        public bool m_ShowCapture = true;
        public bool m_ShowHotkeys = true;
        public bool m_ShowGallery = true;
        public bool m_ShowUtils = true;
        public bool m_ShowUsage = true;
        public bool m_ShowInstall = true;
        public bool m_ShowDelay = true;

        public bool m_ShowShare = true;

        #endregion


    }
}
