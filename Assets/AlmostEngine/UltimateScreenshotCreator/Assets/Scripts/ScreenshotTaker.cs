using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AlmostEngine.Screenshot
{
    /// <summary>
    /// The Screenshot taker is a component used to capture screenshots, with various capture modes and custom settings.
    /// </summary>
    [ExecuteInEditMode]
    public class ScreenshotTaker : MonoBehaviour
    {
        public enum ColorFormat
        {
            RGB,
            RGBA
        }
        ;

        public enum CaptureMode
        {
            GAMEVIEW_RESIZING,
            RENDER_TO_TEXTURE,
            FIXED_GAME_VIEW,
            FIXED_SCENE_VIEW_EXPERIMENTAL,
            FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL
        }
        ;
        public enum GameViewResizingWaitingMode
        {
            FRAMES,
            TIME
        }
        ;


        /// <summary>
        /// The texture containing the last screenshot taken.
        /// </summary>
        public Texture2D m_Texture;


        [HideInInspector]
        public GameViewResizingWaitingMode m_GameViewResizingWaitingMode;
        [HideInInspector]
        public float m_GameViewResizingWaitingTime = 1f;
        [HideInInspector]
        public int m_GameViewResizingWaitingFrames = 2;




#if (UNITY_EDITOR) && (!UNITY_5_4_OR_NEWER)
		[HideInInspector]
		public bool m_NeedRestoreLayout;

		[Tooltip ("If true, the editor layout is saved and restored before and after each capture process.")]
		public bool m_ForceLayoutPreservation = true;
#endif

        [HideInInspector]
        public static bool m_IsRunning = false;
        List<ScreenshotCamera> m_Cameras = new List<ScreenshotCamera>();
        List<ScreenshotCamera> m_SceneCameras = new List<ScreenshotCamera>();
        List<ScreenshotOverlay> m_Overlays = new List<ScreenshotOverlay>();
        List<ScreenshotOverlay> m_DisabledOverlays = new List<ScreenshotOverlay>();


        #region BEHAVIOR

        void Update()
        {
#if UNITY_EDITOR
            if (EditorApplication.isCompiling)
            {
                Reset();
                return;
            }
#endif
        }

        public void Reset()
        {
            StopAllCoroutines();

            RestoreTime();
            RestoreSettings();

            m_IsRunning = false;
        }

        void OnDestroy()
        {
            Reset();
        }

        #endregion

        #region CACHE

        // public bool m_UseRenderTextureCache = true;

        /// <summary>
        /// Clears the cache of RenderTexture used to capture the screenshots.
        /// </summary>
        public void ClearRenderTextureCache()
        {
            foreach (var rt in m_RenderTextureCache)
            {
                GameObject.DestroyImmediate(rt);
            }
            m_RenderTextureCache.Clear();
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// Delegate called when the capture starts.
        /// </summary>
        public static UnityAction<ScreenshotResolution> onResolutionUpdateStartDelegate = (ScreenshotResolution res) =>
        {
        };

        /// <summary>
        /// Delegate called when the capture ends.
        /// </summary>
        public static UnityAction<ScreenshotResolution> onResolutionUpdateEndDelegate = (ScreenshotResolution res) =>
        {
        };

        /// <summary>
        /// Delegate called when the screen is resized.
        /// </summary>
        public static UnityAction<ScreenshotResolution> onResolutionScreenResizedDelegate = (ScreenshotResolution res) =>
        {
        };

        #endregion

        #region STATIC_API

        /// <summary>
        /// Captures the current screen at its current resolution.
        /// Must be called from a coroutine after a WaitForEndOfFrame.
        /// </summary>
        public static Texture2D CaptureScreenToTexture(ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            int width = Screen.width;
            int height = Screen.height;
            Texture2D texture = null;
            GetOrCreateTexture(ref texture, width, height, colorFormat);
            CaptureScreenToTexture(texture);
            return texture;
        }

        public static Texture2D CaptureCamerasToTexture(int width, int height, List<Camera> cameras, ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            Texture2D texture = null;
            GetOrCreateTexture(ref texture, width, height, colorFormat);
            RenderTexture renderTexture = new RenderTexture(width, height, 24);
            CaptureCamerasToTexture(cameras, texture, renderTexture);
            return texture;
        }


        #endregion


        #region COROUTINES_API 


        /// <summary>
        /// Captures the current screen at its current resolution.
        /// The texture will be resized if needed to match the capture settings.
        /// </summary>
        public IEnumerator CaptureScreenToTextureCoroutine(Texture2D texture,
                                                            bool captureGameUI = true,
                                                            ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                            bool recomputeAlphaMask = false)
        {
            Vector2 size = GameViewController.GetCurrentGameViewSize();
            yield return StartCoroutine(CaptureToTextureCoroutine(texture, (int)size.x, (int)size.y, null, null,
                ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW,
                8, captureGameUI, colorFormat, recomputeAlphaMask));
        }

        /// <summary>
        /// Captures the scene with the specified width, height, using the mode RENDER_TO_TEXTURE.
        /// Screenspace Overlay Canvas can not be captured with this mode.
        /// The texture will be resized if needed to match the capture settings.
        /// </summary>
        public IEnumerator CaptureCamerasToTextureCoroutine(Texture2D texture, int width, int height,
                                                             List<Camera> cameras,
                                                             int antiAliasing = 8,
                                                             ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                             bool recomputeAlphaMask = false)
        {
            yield return StartCoroutine(CaptureToTextureCoroutine(texture, width, height, cameras, null, ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE, antiAliasing, true, colorFormat, recomputeAlphaMask));
        }

        /// <summary>
        /// Captures the game with the specified width, height.
        /// The texture will be resized if needed to match the capture settings.
        /// </summary>
        public IEnumerator CaptureToTextureCoroutine(Texture2D texture, int width, int height,
                                                      List<Camera> cameras = null,
                                                      List<Canvas> canvas = null,
                                                      ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
                                                      int antiAliasing = 8,
                                                      bool captureGameUI = true,
                                                      ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                      bool recomputeAlphaMask = false)
        {
            // Check texture
            if (texture == null)
            {
                Debug.LogError("The texture can not be null. You must provide a texture initialized with any width and height.");
                yield break;
            }

            // Update resolution item
            ScreenshotResolution captureResolution = new ScreenshotResolution(width, height);
            captureResolution.m_Texture = texture;

            yield return StartCoroutine(CaptureResolutionCoroutine(captureResolution, cameras, canvas, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask));

        }


        public IEnumerator CaptureResolutionCoroutine(ScreenshotResolution captureResolution,
                                                       List<Camera> cameras = null,
                                                       List<Canvas> canvas = null,
                                                       ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
                                                       int antiAliasing = 8,
                                                       bool captureGameUI = true,
                                                       ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                       bool recomputeAlphaMask = false)
        {

            // Debug.Log("Capturing resolution " + captureResolution);

            // Create camera items
            List<ScreenshotCamera> screenshotCameras = new List<ScreenshotCamera>();
            if (cameras != null)
            {
                foreach (Camera camera in cameras)
                {
                    ScreenshotCamera scamera = new ScreenshotCamera(camera);
                    screenshotCameras.Add(scamera);
                }
            }

            // Create the overlays items
            List<ScreenshotOverlay> screenshotCanvas = new List<ScreenshotOverlay>();
            if (canvas != null)
            {
                foreach (Canvas c in canvas)
                {
                    ScreenshotOverlay scanvas = new ScreenshotOverlay(c);
                    screenshotCanvas.Add(scanvas);
                }
            }

            yield return StartCoroutine(CaptureAllCoroutine(new List<ScreenshotResolution> { captureResolution },
                screenshotCameras, screenshotCanvas,
                captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask));

        }


        #endregion


        #region Capture



        public IEnumerator CaptureAllCoroutine(List<ScreenshotResolution> resolutions,
                                                List<ScreenshotCamera> cameras,
                                                List<ScreenshotOverlay> overlays,
                                                CaptureMode captureMode,
                                                int antiAliasing = 8,
                                                bool captureGameUI = true,
                                                ColorFormat colorFormat = ColorFormat.RGB,
                                                bool recomputeAlphaMask = false,
                                                bool stopTime = false,
                                                bool restore = true,
                                                bool forceUICullingLayer = false,
                                                bool disableHDRPFX = true)
        {

            // Debug.Log("Capture all Frame " + Time.frameCount);

            if (resolutions == null)
            {
                Debug.LogError("Resolution list is null.");
                yield break;
            }
            if (cameras == null)
            {
                Debug.LogError("Cameras list is null.");
                yield break;
            }
            if (cameras.Count == 0 && captureMode == CaptureMode.RENDER_TO_TEXTURE)
            {
                cameras.Add(new ScreenshotCamera(Camera.main));
            }
            if (overlays == null)
            {
                Debug.LogError("Overlays list is null.");
                yield break;
            }
            if (captureMode == CaptureMode.RENDER_TO_TEXTURE && !UnityVersion.HasPro())
            {
                Debug.LogError("RENDER_TO_TEXTURE requires Unity Pro or Unity 5.0 and later.");
                yield break;
            }

            // If a capture is in progress we wait until we can take the screenshot
            if (m_IsRunning == true)
            {
                Debug.LogWarning("A capture process is already running.");
            }
            while (m_IsRunning == true)
            {
                yield return null;
            }


#if (!UNITY_EDITOR && !UNITY_STANDALONE_WIN)
            if (captureMode == CaptureMode.GAMEVIEW_RESIZING) {
                Debug.LogError ("GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");
                yield break;
            }
#endif

            // Init
            m_IsRunning = true;

            // Stop the time so all screenshots are exactly the same
            if (Application.isPlaying && stopTime)
            {
                StopTime();
            }


            // Apply settings: enable and disable the cameras and canvas
            ApplySettings(cameras, overlays, captureMode, captureGameUI, forceUICullingLayer);


            // Save the screen config to be restored after the capture process
            if (captureMode == CaptureMode.GAMEVIEW_RESIZING)
            {
                GameViewController.SaveCurrentGameViewSize();

#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
                yield return null;
                yield return new WaitForEndOfFrame();
#endif
            }

            // Capture all resolutions
            foreach (ScreenshotResolution resolution in resolutions)
            {
                if (!resolution.IsValid())
                    continue;

                // Delegate call
                onResolutionUpdateStartDelegate(resolution);

                if (colorFormat == ColorFormat.RGBA && recomputeAlphaMask)
                {
                    yield return StartCoroutine(CaptureAlphaMaskCoroutine(resolution, captureMode, antiAliasing, colorFormat, disableHDRPFX));
                }
                else
                {
                    yield return StartCoroutine(CaptureResolutionTextureCoroutine(resolution, captureMode, antiAliasing, colorFormat));
                }

                // Delegate call
                onResolutionUpdateEndDelegate(resolution);




#if UNITY_EDITOR
                // Dirty hack: we force a gameview repaint, to prevent the coroutine to stay locked.
                if (!Application.isPlaying)
                {
                    GameViewUtils.GetGameView().Repaint();
                }
#endif


            }

            // Restore screen config
            if (restore && captureMode == CaptureMode.GAMEVIEW_RESIZING)
            {
                GameViewController.RestoreGameViewSize();
            }


#if (UNITY_EDITOR && !UNITY_5_4_OR_NEWER)
			// Call restore layout for old unity versions
			if (restore && captureMode == CaptureMode.GAMEVIEW_RESIZING) {		
				m_NeedRestoreLayout = true;
			} 
#endif

#if UNITY_EDITOR
            // Dirty hack, try to force an editor Update() to get the gameview back to normal
            if (!Application.isPlaying)
            {
                GameViewUtils.GetGameView().Repaint();
            }
#endif

            // Restore everything
            if (Application.isPlaying && stopTime)
            {
                RestoreTime();
            }
            if (Application.isEditor || restore)
            {
                RestoreSettings();
            }

            // End
            m_IsRunning = false;
        }


        /// <summary>
        /// Captures the resolution texture.
        /// </summary>
        IEnumerator CaptureResolutionTextureCoroutine(ScreenshotResolution resolution, CaptureMode captureMode, int antiAliasing, ColorFormat colorFormat)
        {
            // Debug.Log("CaptureResolutionTextureCoroutine Frame " + Time.frameCount + " time " + Time.time);

            var captureScreenWatch = new System.Diagnostics.Stopwatch();
            var captureResolutionTextureWatch = new System.Diagnostics.Stopwatch();
            var gameviewCaptureWatch = new System.Diagnostics.Stopwatch();
            var gameviewRepaintWatch = new System.Diagnostics.Stopwatch();
            var gameviewWaitWatch = new System.Diagnostics.Stopwatch();
            captureResolutionTextureWatch.Start();

            // Init texture
            m_Texture = GetOrCreateTexture(resolution, colorFormat, captureMode == CaptureMode.FIXED_GAME_VIEW ? true : false);

            if (captureMode == CaptureMode.GAMEVIEW_RESIZING)// && (Screen.width != m_Texture.width || Screen.height != m_Texture.height))
            {

                // Debug.Log("GAMEVIEW_RESIZING " + Time.frameCount + " time " + Time.time);

                // Force screen size change
                GameViewController.SetGameViewSize(m_Texture.width, m_Texture.height);
                yield return new WaitForEndOfFrame();

                // Force wait
                if (!Application.isPlaying)
                {
                    // Useless texture update in editor mode when game is not running,
                    // that takes some computational times to be sure that the UI is updated at least one time before the capture
                    if (MultiDisplayUtils.IsMultiDisplay())
                    {
                        yield return MultiDisplayCopyRenderBufferToTextureCoroutine(m_Texture);
                    }
                    else
                    {
                        CaptureScreenToTexture(m_Texture);
                    }
                }

                // Delegate call to notify screen is resized
                onResolutionScreenResizedDelegate(resolution);


                // Wait several frames
                // Particularly needed for special effects using several frame to compute their effects, like temporal anti aliasing
                if (m_GameViewResizingWaitingMode == GameViewResizingWaitingMode.FRAMES || !Application.isPlaying)
                {
                    for (int i = 0; i < m_GameViewResizingWaitingFrames; ++i)
                    {
#if UNITY_EDITOR
                        if (!Application.isPlaying)
                        {
                            GameViewUtils.GetGameView().Repaint();
                        }
#endif
                        yield return new WaitForEndOfFrame();
#if UNITY_EDITOR
                        if (!Application.isPlaying && i < 2)
                        {
                            // Useless update to force waiting for the gamview to be correctly updated
                            CaptureScreenToTexture(m_Texture);
                        }
#endif
                    }
                }
                else
                {
#if (UNITY_5_4_OR_NEWER)
                    yield return new WaitForSecondsRealtime(m_GameViewResizingWaitingTime);
#else
					if (Time.timeScale > 0f) {
						yield return new WaitForSeconds (m_GameViewResizingWaitingTime);
					}
#endif
                    yield return new WaitForEndOfFrame();
                }



                // Capture the screen content
                if (MultiDisplayUtils.IsMultiDisplay())
                {
                    yield return MultiDisplayCopyRenderBufferToTextureCoroutine(m_Texture);
                }
                else
                {
                    CaptureScreenToTexture(m_Texture);
                }

                //				Debug.Log ("End Capture");

            }
            else if (captureMode == CaptureMode.RENDER_TO_TEXTURE)
            {

                // Wait for the end of rendering
                yield return new WaitForEndOfFrame();

                // Do not need to wait anything, just capture the cameras
                RenderTexture renderTexture = GetOrCreateRenderTexture(resolution, antiAliasing);
                CaptureCamerasToTexture(m_Cameras, m_Texture, renderTexture);


            }
            else if (captureMode == CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL)
            {

#if UNITY_EDITOR
                GameViewUtils.GetSceneView().Focus();
                GameViewUtils.GetSceneView().Repaint();
                CaptureSceneviewWithGizmosToTexture(m_Texture);
#else
                Debug.Log("Capture mode " + captureMode + " is not available in build");
#endif
            }
            else if (captureMode == CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL)
            {
#if UNITY_EDITOR
                GameViewUtils.GetSceneView().Focus();

                GameViewUtils.GetSceneView().Repaint();
                CaptureSceneviewToTexture(m_Texture);
#else
                Debug.Log("Capture mode " + captureMode + " is not available in build");
#endif
            }
            else  // FIXED_GAME_VIEW
            {
                // Debug.Log("FIXED " + Time.frameCount + " time " + Time.time);

                gameviewCaptureWatch.Start();
#if UNITY_EDITOR
                // Force repaint in case the gameview is not focus to prevent a lock
                if (!Application.isPlaying)
                {
                    gameviewRepaintWatch.Start();
                    GameViewUtils.GetGameView().Repaint();
                    gameviewRepaintWatch.Stop();
                }
#endif

                gameviewWaitWatch.Start();
                // Wait for the end of rendering
                yield return new WaitForEndOfFrame();
                gameviewWaitWatch.Stop();

                // Capture the screen content
                captureScreenWatch.Start();
                if (MultiDisplayUtils.IsMultiDisplay())
                {
                    yield return MultiDisplayCopyRenderBufferToTextureCoroutine(m_Texture);
                }
                else
                {
                    CaptureScreenToTexture(m_Texture);
                }
                captureScreenWatch.Stop();
                gameviewCaptureWatch.Stop();
            }

            // Debug.Log("CaptureResolutionTextureCoroutine end frame " + Time.frameCount + " time " + Time.time);

            captureResolutionTextureWatch.Stop();
            // Debug.Log("- - - - gameviewRepaintWatch time " + gameviewRepaintWatch.ElapsedMilliseconds + " ms");
            // Debug.Log("- - - - captureScreenWatch time " + captureScreenWatch.ElapsedMilliseconds + " ms");
            // Debug.Log("- - - gameviewCaptureWatch time " + gameviewCaptureWatch.ElapsedMilliseconds + " ms");
            // Debug.Log("- - captureResolutionTextureWatch time " + captureResolutionTextureWatch.ElapsedMilliseconds + " ms");

        }

#if UNITY_EDITOR
        static void CaptureSceneviewToTexture(Texture2D targetTexture)
        {
            RenderTexture previousActiveRt = RenderTexture.active;
            RenderTexture.active = GameViewUtils.GetSceneView().camera.targetTexture;

            GameViewUtils.GetSceneView().camera.Render();
            targetTexture.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
            RenderTexture.active = previousActiveRt;

            targetTexture.Apply(false);
        }
        static void CaptureSceneviewWithGizmosToTexture(Texture2D targetTexture)
        {
            var sceneView = GameViewUtils.GetSceneView();
            Color[] pixels = UnityEditorInternal.InternalEditorUtility.ReadScreenPixel(sceneView.position.position, (int)sceneView.position.width, (int)sceneView.position.height);
            targetTexture.SetPixels(pixels);

            targetTexture.Apply(false);
        }
#endif

        static void CaptureScreenToTexture(Texture2D targetTexture)
        {
            targetTexture.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
            targetTexture.Apply(false);
        }

        public void CaptureCamerasToTexture(List<ScreenshotCamera> cameras, Texture2D targetTexture, RenderTexture renderTexture)
        {
            if (cameras == null)
            {
                Debug.LogError("Cameras is null");
                return;
            }
            List<Camera> cams = cameras.Where(x => x.m_Active == true && x.m_Camera != null && x.m_Camera.enabled == true).Select(x => x.m_Camera).ToList();
            CaptureCamerasToTexture(cams, targetTexture, renderTexture);
        }

        public static void CaptureCamerasToTexture(List<Camera> cameras, Texture2D targetTexture, RenderTexture renderTexture)
        {
            // Remember active render texture
            RenderTexture previousActiveRt = RenderTexture.active;
            // Set active render texture
            RenderTexture.active = renderTexture;

            // Render all cameras in custom render texture
            foreach (Camera camera in cameras)
            {
                // Remember target texture
                RenderTexture previousCamRT = camera.targetTexture;
                // Render into texture
                camera.targetTexture = renderTexture;
                camera.Render();
                // Restore target texture
                camera.targetTexture = previousCamRT;
            }

            // Copy render buffer to texture
            targetTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            targetTexture.Apply(false);

            // Restore active render texture
            RenderTexture.active = previousActiveRt;
        }

        #endregion


        #region Transparency

        IEnumerator CaptureAlphaMaskCoroutine(ScreenshotResolution resolution, CaptureMode captureMode, int antiAliasing, ColorFormat colorFormat, bool disableHDRPFX = true)
        {
            // First we capture the game as it is
            yield return StartCoroutine(CaptureResolutionTextureCoroutine(resolution, captureMode, antiAliasing, colorFormat));

            // Copy texture to buffer
            Texture2D defaultTexture = new Texture2D(resolution.m_Texture.width, resolution.m_Texture.height, resolution.m_Texture.format, false, false);
            CloneTexture(resolution.m_Texture, defaultTexture);


            // Backup first camera clear mode
            var firstCamera = GetFirstActiveCamera();
            var clearMode = firstCamera.clearFlags;
            var clearColor = firstCamera.backgroundColor;

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            var additionaldata = firstCamera.GetComponent<HDAdditionalCameraData>();
            var previousLayer = additionaldata.volumeLayerMask;
            var previousHDClear = additionaldata.backgroundColorHDR;
            if (additionaldata != null)
            {
            additionaldata.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
            }
            
            // Disable all HDRP EFFECTS effects
            if (additionaldata != null && disableHDRPFX)
            {
                additionaldata.volumeLayerMask = 0;
            }
#endif

            // CAPTURE WITH WHITE BACKGROUND
            // Set clear white alpha
            firstCamera.clearFlags = CameraClearFlags.Color;
            firstCamera.backgroundColor = new Color(1f, 1f, 1f, 1f);

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            if (additionaldata != null)
            {
            additionaldata.backgroundColorHDR = new Color(1f, 1f, 1f, 1f);            
            }
#endif


            // Capture the texture
            yield return StartCoroutine(CaptureResolutionTextureCoroutine(resolution, captureMode, antiAliasing, colorFormat));

            // Copy white texture to buffer
            Texture2D whiteTexture = new Texture2D(resolution.m_Texture.width, resolution.m_Texture.height, resolution.m_Texture.format, false, false);
            CloneTexture(resolution.m_Texture, whiteTexture);

            // CAPTURE WITH BLACK BACKGROUND
            // Set clear black alpha
            firstCamera.clearFlags = CameraClearFlags.Color;
            firstCamera.backgroundColor = new Color(0f, 0f, 0f, 1f);

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            if (additionaldata != null)
            {
            additionaldata.backgroundColorHDR = new Color(0f, 0f, 0f, 1f);
            }
#endif

            // Capture the texture, again
            yield return StartCoroutine(CaptureResolutionTextureCoroutine(resolution, captureMode, antiAliasing, colorFormat));

            // Copy black texture
            Texture2D blackTexture = new Texture2D(resolution.m_Texture.width, resolution.m_Texture.height, resolution.m_Texture.format, false, false);
            CloneTexture(resolution.m_Texture, blackTexture);

            // RECOMPUTE ALPHA
            // Compute the difference in the pixel color and use it as new alpha value
            Color color;
            for (int x = 0; x < resolution.m_Texture.width; ++x)
            {
                for (int y = 0; y < resolution.m_Texture.height; ++y)
                {
                    var colorDifference = whiteTexture.GetPixel(x, y).r - blackTexture.GetPixel(x, y).r;
                    var alpha = 1f - colorDifference;
                    if (alpha == 0)
                    {
                        // Set color to clear if fully transparent
                        color = Color.clear;
                    }
                    else
                    {
                        // Set color to previously captured scene
                        color = defaultTexture.GetPixel(x, y);
                    }
                    // Override new alpha value
                    color.a = alpha;
                    resolution.m_Texture.SetPixel(x, y, color);
                }
            }
            resolution.m_Texture.Apply();

            // Clean
            DestroyImmediate(whiteTexture);
            DestroyImmediate(blackTexture);
            DestroyImmediate(defaultTexture);

            // Restore clear settings
            firstCamera.clearFlags = clearMode;
            firstCamera.backgroundColor = clearColor;
#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            if (additionaldata != null)
            {
            additionaldata.volumeLayerMask = previousLayer;
            additionaldata.backgroundColorHDR = previousHDClear;
            }
#endif

        }

        public static Texture2D CloneTexture(Texture2D src)
        {
            var clone = new Texture2D(src.width, src.height, src.format, src.mipmapCount > 0);
            CloneTexture(src, clone);
            return clone;
        }

        public static void CloneTexture(Texture2D src, Texture2D dest)
        {
#if UNITY_5_4_OR_NEWER
            Graphics.CopyTexture(src, 0, 0, dest, 0, 0);
#else
			dest.Resize(src.width, src.height, src.format, false);
            for (int x = 0; x < src.width; ++x)
            {
                for (int y = 0; y < src.height; ++y)
                {
					dest.SetPixel(x,y,src.GetPixel(x,y));
				}
			}
			dest.Apply();
#endif
        }

        #endregion

        #region Multi Display


        Camera GetFirstActiveCamera()
        {
            // Get last camera on the list
            for (int i = 0; i < m_Cameras.Count; i++)
            {
                if (m_Cameras[i].m_Active && m_Cameras[i].m_Camera != null && m_Cameras[i].m_Camera.enabled == true)
                {
                    return m_Cameras[i].m_Camera;
                }
            }
            // If not cameras on the list, get the active camera for display 1 with the higher depth
            Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
            Camera best = null;
            foreach (Camera cam in cameras)
            {

#if (UNITY_5_4_OR_NEWER)
                if (cam.enabled == true && cam.targetDisplay == 0)
                {
#else
				if (cam.enabled == true) {
#endif
                    if (best == null || cam.depth < best.depth)
                    {
                        best = cam;
                    }
                }
            }
            if (best != null)
                return best;
            // Return camera tagged as MainCamera
            return Camera.main;
        }

        Camera GetLastActiveCamera()
        {
            // Get last camera on the list
            for (int i = m_Cameras.Count - 1; i >= 0; i--)
            {
                if (m_Cameras[i].m_Active && m_Cameras[i].m_Camera != null && m_Cameras[i].m_Camera.enabled == true)
                {
                    return m_Cameras[i].m_Camera;
                }
            }
            // If not cameras on the list, get the active camera for display 1 with the higher depth
            Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
            Camera best = null;
            foreach (Camera cam in cameras)
            {

#if (UNITY_5_4_OR_NEWER)
                if (cam.enabled == true && cam.targetDisplay == 0)
                {
#else
				if (cam.enabled == true) {
#endif
                    if (best == null || cam.depth > best.depth)
                    {
                        best = cam;
                    }
                }
            }
            if (best != null)
                return best;
            // Return camera tagged as MainCamera
            return Camera.main;
        }

        IEnumerator MultiDisplayCopyRenderBufferToTextureCoroutine(Texture2D targetTexture)
        {
            // We get the last camera on the list or the last rendered camera
            Camera cameraToCapture = null;


            if (MultiDisplayCameraCapture.m_MultiDisplayCamera.Count > 0)
            {
                cameraToCapture = MultiDisplayCameraCapture.m_MultiDisplayCamera[0].GetComponent<Camera>();
            }
            else
            {
                cameraToCapture = GetLastActiveCamera();
            }

            // On multi display we need to wait for the last camera to capture to be rendered
            if (cameraToCapture != null)
            {
                MultiDisplayCameraCapture captureMultiCam = cameraToCapture.GetComponent<MultiDisplayCameraCapture>();
                bool wasCreated = false;
                // Add a capture camera component and start the capture process
                if (captureMultiCam == null)
                {
                    captureMultiCam = cameraToCapture.gameObject.AddComponent<MultiDisplayCameraCapture>();
                    wasCreated = true;
                }
                captureMultiCam.WaitRenderingAndCopyCameraToTexture(targetTexture);
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
#endif
                // Wait for capture
                while (!captureMultiCam.CopyIsOver())
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        // Force redraw
                        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                    }
#endif
                    // Force redraw
                    yield return null;
                }
                // Clean
                if (wasCreated)
                {
                    GameObject.DestroyImmediate(captureMultiCam);
                }

            }
            else
            {
                // Just read the actual render buffer
                CaptureScreenToTexture(targetTexture);
            }
        }

        #endregion


        #region TEXTURE


        public static Texture2D GetOrCreateTexture(ScreenshotResolution resolution, ColorFormat colorFormat, bool noScale = false)
        {
            // Compute real dimensions
            int width = noScale ? resolution.m_Width : resolution.ComputeTargetWidth();
            int height = noScale ? resolution.m_Height : resolution.ComputeTargetHeight();

            // Init the texture
            GetOrCreateTexture(ref resolution.m_Texture, width, height, colorFormat);

            return resolution.m_Texture;
        }

        public static void GetOrCreateTexture(ref Texture2D texture, int width, int height, ColorFormat colorFormat)
        {
            // Debug.Log("GetOrCreateTexture" + width + "x" + height);
            // Create texture if needed
            TextureFormat format = colorFormat == ColorFormat.RGBA ? TextureFormat.ARGB32 : TextureFormat.RGB24;
            if (texture == null)
            {
                // Debug.LogWarning("Create texture " + width + "x" + height);
                texture = new Texture2D(width, height, format, false);
            }
            else if (texture.width != width || texture.height != height || texture.format != format)
            {
                // Debug.LogWarning("Resize texture " + width + "x" + height + " " + format);
                // Debug.Log("texture.width " + texture.width);
                // Debug.Log("texture.height " + texture.height);
                // Debug.Log("texture.format " + texture.format);
#if UNITY_2021_3_OR_NEWER
                texture.Reinitialize(width, height, format, false);
#else
                texture.Resize(width, height, format, false);
#endif
            }
            else
            {
                // Debug.Log("Cache");
            }
        }

        static List<RenderTexture> m_RenderTextureCache = new List<RenderTexture>();
        static int m_MaxRenderTextureCache = 2;

        RenderTexture GetOrCreateRenderTexture(ScreenshotResolution resolution, int antiAliasing = 0, int depth = 32, RenderTextureFormat format = RenderTextureFormat.ARGBHalf)
        {
            // Compute real resolutions
            int width = resolution.ComputeTargetWidth();
            int height = resolution.ComputeTargetHeight();

            return GetOrCreateRenderTexture(width, height, antiAliasing, depth, format);
        }

        public static RenderTexture GetOrCreateRenderTexture(int width, int height, int antiAliasing = 0, int depth = 32, RenderTextureFormat format = RenderTextureFormat.ARGBHalf)
        {
            RenderTexture rt = null;

            // Search in cache
            // Debug.Log("Render texture cache " + m_RenderTextureCache.Count);
            int rtIndex = -1;
            rtIndex = m_RenderTextureCache.FindIndex(x => x.width == width && x.height == height && ((antiAliasing == 0) || (x.antiAliasing == antiAliasing)) && x.depth == depth && x.format == format);
            if (rtIndex >= 0)
            {
                // Debug.Log("Render texture found in cache " + width + "x" + height);
                // Get the render texture
                rt = m_RenderTextureCache[rtIndex];
                // Remove from cache list, to reinsert it later
                m_RenderTextureCache.RemoveAt(rtIndex);
            }
            // Not in cache
            else
            {
                // Create new render texture
                // Debug.Log("NEW Render texture " + width + "x" + height);
                rt = new RenderTexture(width, height, depth, format);
                if (antiAliasing != 0)
                {
                    // Debug.Log("NEW Render texture " + width + "x" + height + " with anti aliasing level of " + antiAliasing);
                    rt.antiAliasing = antiAliasing;
                }
            }
            // Make space in cache if necessary
            // Remove in queue
            while (m_RenderTextureCache.Count > m_MaxRenderTextureCache)
            {
                var toRemove = m_RenderTextureCache[m_RenderTextureCache.Count - 1];
                m_RenderTextureCache.RemoveAt(m_RenderTextureCache.Count - 1);
                GameObject.DestroyImmediate(toRemove);
            }
            // Insert in cache head
            m_RenderTextureCache.Insert(0, rt);

            // Return it
            return rt;
        }

        #endregion


        #region GENERAL SETTINGS

        List<RectTransform> disabledUIElements = new List<RectTransform>();

        public void ApplySettings(List<ScreenshotCamera> cameras, List<ScreenshotOverlay> overlays, CaptureMode captureMode, bool renderUI, bool forceUICullingLayer)
        {

            // SET CAMERAS	
            m_Cameras = cameras;
            m_SceneCameras = FindAllOtherCameras(m_Cameras);
            if (captureMode != CaptureMode.RENDER_TO_TEXTURE && captureMode != CaptureMode.FIXED_SCENE_VIEW_EXPERIMENTAL && captureMode != CaptureMode.FIXED_SCENE_VIEW_WITH_GIZMOS_EXPERIMENTAL && m_Cameras.Count > 0)
            {
                DisableCameras(m_SceneCameras);
            }
            ApplyCameraSettings(m_Cameras, captureMode);

            // SET OVERLAYS
            m_Overlays = overlays.Where(x => x.m_Active == true).ToList();
            m_DisabledOverlays.Clear();
            if (forceUICullingLayer && m_Cameras.Count > 0 && renderUI == true)
            {
                // If camera are enabled and we want to render the ui
                // We disable all canvas that are not in any of the camera culling layers
                m_DisabledOverlays = FindAllOtherCanvas(m_Overlays).Where(ov => (ov.m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                                                                                && (m_Cameras.FindAll(ca => (ca.m_CullingMask & (1 << ov.m_Canvas.gameObject.layer)) != 0).Count == 0)
                                                                            ).ToList();
            }
            else if (captureMode != CaptureMode.RENDER_TO_TEXTURE && renderUI == false)
            {
                // If we do not want to render the UI, we disable all canvas
                m_DisabledOverlays = FindAllOtherCanvas(m_Overlays);
            }
            DisableCanvas(m_DisabledOverlays);
            ApplyOverlaySettings(m_Overlays);

            // Force UI culling mask
            if (forceUICullingLayer && m_Cameras.Count > 0 && renderUI == true)
            {
                disabledUIElements = GameObject.FindObjectsOfType<RectTransform>().Where(r => r.gameObject.activeInHierarchy
                                                                                                && (r.GetComponentInParent<Canvas>() != null)
                                                                                                && (m_Cameras.FindAll(ca => (ca.m_CullingMask & (1 << r.gameObject.layer)) != 0).Count == 0)
                                                                                            ).ToList();
                foreach (var r in disabledUIElements)
                {
                    r.gameObject.SetActive(false);
                }
            }
        }

        public void RestoreSettings()
        {
            // Restore cameras
            RestoreCameraSettings(m_Cameras);
            RestoreCameraSettings(m_SceneCameras);

            // Restore overlays
            RestoreOverlaySettings(m_Overlays);
            RestoreOverlaySettings(m_DisabledOverlays);

            // Restore disabled ui elements
            foreach (var r in disabledUIElements)
            {
                if (r != null)
                {
                    r.gameObject.SetActive(true);
                }
            }
            disabledUIElements.Clear();
        }

        float m_PreviousTimeScale = 1f;

        void StopTime()
        {
            m_PreviousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            // Debug.Log("Stopping taker time " + Time.time);
        }

        void RestoreTime()
        {
            Time.timeScale = m_PreviousTimeScale;
            // Debug.Log("Restoring taker time " + Time.time);
        }

        #endregion


        #region CANVAS SETTINGS

        List<ScreenshotOverlay> FindAllOtherCanvas(List<ScreenshotOverlay> overlays)
        {
            List<ScreenshotOverlay> sceneUIOverlaysCanvas = new List<ScreenshotOverlay>();

            // Find all canvas using screenspaceoverlay on the scene
            Canvas[] canvas = GameObject.FindObjectsOfType<Canvas>();
            foreach (Canvas canva in canvas)
            {
                if (canva.gameObject.activeInHierarchy == true
                    && canva.enabled == true)
                {

                    // Ignore overlays canvas
                    bool contains = false;
                    foreach (ScreenshotOverlay overlay in overlays)
                    {
                        if (overlay.m_Canvas == canva && overlay.m_Active)
                            contains = true;
                    }
                    if (contains)
                        continue;

                    // Add canvas to list
                    sceneUIOverlaysCanvas.Add(new ScreenshotOverlay(canva));
                }
            }
            return sceneUIOverlaysCanvas;
        }

        void DisableCanvas(List<ScreenshotOverlay> overlays)
        {
            if (overlays == null)
                return;
            foreach (ScreenshotOverlay overlay in overlays)
            {
                if (overlay == null)
                    continue;
                overlay.Disable();
            }
        }

        void ApplyOverlaySettings(List<ScreenshotOverlay> overlays)
        {
            if (overlays == null)
                return;
            foreach (ScreenshotOverlay overlay in overlays)
            {
                if (overlay == null)
                    continue;
                if (overlay.m_Active && overlay.m_Canvas != null)
                {
                    overlay.ApplySettings();
                }
            }
        }

        public void RestoreOverlaySettings(List<ScreenshotOverlay> overlays)
        {
            if (overlays == null)
                return;
            foreach (ScreenshotOverlay overlay in overlays)
            {
                if (overlay == null)
                    continue;
                if (overlay.m_Active && overlay.m_Canvas != null)
                {
                    overlay.RestoreSettings();
                }
            }
        }

        #endregion


        #region CAMERAS SETTINGS


        List<ScreenshotCamera> FindAllOtherCameras(List<ScreenshotCamera> cameras)
        {
            List<ScreenshotCamera> cams = new List<ScreenshotCamera>();
            Camera[] sceneCameras = GameObject.FindObjectsOfType<Camera>();
            foreach (Camera camera in sceneCameras)
            {
                bool contains = false;
                foreach (ScreenshotCamera cam in cameras)
                {
                    if (cam.m_Camera == camera && cam.m_Active == true)
                    {
                        contains = true;
                    }
                }
                if (!contains)
                {
                    cams.Add(new ScreenshotCamera(camera));
                }
            }
            return cams;
        }

        void DisableCameras(List<ScreenshotCamera> cameras)
        {
            if (cameras == null)
                return;

            foreach (ScreenshotCamera camera in cameras)
            {
                if (camera == null)
                    continue;
                camera.Disable();
            }
        }

        public void RestoreCameraSettings(List<ScreenshotCamera> cameras)
        {
            if (cameras == null)
                return;

            foreach (ScreenshotCamera camera in cameras)
            {
                if (camera == null)
                    continue;
                if (camera.m_Active == false)
                    continue;
                if (camera.m_Camera == null)
                    continue;
                camera.RestoreSettings();
            }
        }

        void ApplyCameraSettings(List<ScreenshotCamera> cameras, CaptureMode captureMode)
        {
            if (cameras == null)
                return;

            foreach (ScreenshotCamera camera in cameras)
            {
                if (camera == null)
                    continue;
                if (camera.m_Active == false)
                    continue;
                if (camera.m_Camera == null)
                    continue;

                camera.ApplySettings(captureMode == CaptureMode.RENDER_TO_TEXTURE);
            }
        }

        #endregion



    }
}
