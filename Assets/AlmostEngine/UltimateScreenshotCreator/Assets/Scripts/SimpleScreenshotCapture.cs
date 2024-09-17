using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
    /// <summary>
    /// Simple interface to capture screeshots to textures or to files.
    /// </summary>
    public static class SimpleScreenshotCapture
    {
        static ScreenshotTaker m_ScreenshotTaker;

        static void InitScreenshotTaker()
        {
            if (m_ScreenshotTaker != null)
                return;

            if (m_ScreenshotTaker == null)
            {
                GameObject go = new GameObject();
                go.name = "tmp Screenshot Capture";
                go.hideFlags = HideFlags.HideAndDontSave;
                m_ScreenshotTaker = go.AddComponent<ScreenshotTaker>();
            }
        }

        #region API


        /// <summary>
        /// Captures the current screen at its current resolution, including UI, to a texture.
        /// </summary>
        public static void CaptureScreenToTexture(UnityAction<Texture2D> onTextureCaptured, ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            InitScreenshotTaker();
            m_ScreenshotTaker.StartCoroutine(CaptureScreenToTextureCoroutine(onTextureCaptured, colorFormat));
        }

        /// <summary>
        /// Captures the camera to a texture.
        /// </summary>
        public static Texture2D CaptureCameraToTexture(int width, int height, Camera camera, ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            return CaptureCamerasToTexture(width, height, new List<Camera> { camera }, colorFormat);
        }

        /// <summary>
        /// Captures cameras to a texture.
        /// </summary>
        public static Texture2D CaptureCamerasToTexture(int width, int height, List<Camera> cameras, ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            return ScreenshotTaker.CaptureCamerasToTexture(width, height, cameras, colorFormat);
        }


        /// <summary>
        /// Captures the current screen at its current resolution, including UI.
        /// The filename must be a valid full name.
        /// </summary>
        public static void CaptureScreenToFile(string fileName,
                                          TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
                                          int JPGQuality = 100,
                                          bool captureGameUI = true,
                                          ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                          bool recomputeAlphaMask = false,
                                          bool exportToGallery = true,
                                          bool exportAsync = false)
        {
            Vector2 size = GameViewController.GetCurrentGameViewSize();
            CaptureToFile(fileName, (int)size.x, (int)size.y, fileFormat, JPGQuality, null, null,
                ScreenshotTaker.CaptureMode.FIXED_GAME_VIEW,
                8, captureGameUI, colorFormat, recomputeAlphaMask, exportToGallery, exportAsync);
        }

        public static void CaptureCameraToFile(string fileName,
                                   int width, int height,
                                   Camera camera,
                                   TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
                                   int JPGQuality = 100,
                                   int antiAliasing = 8,
                                   ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                   bool recomputeAlphaMask = false,
                                   bool exportToGallery = true,
                                   bool exportAsync = false)
        {
            CaptureToFile(fileName, width, height, fileFormat, JPGQuality, new List<Camera> { camera }, null, ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE, antiAliasing, false, colorFormat, recomputeAlphaMask, exportToGallery, exportAsync);
        }

        /// <summary>
        /// Captures the scene with the specified width, height, using the mode RENDER_TO_TEXTURE.
        /// Screenspace Overlay Canvas can not be captured with this mode.
        /// The filename must be a valid full name.
        /// </summary>
        public static void CaptureCamerasToFile(string fileName,
                                           int width, int height,
                                           List<Camera> cameras,
                                           TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
                                           int JPGQuality = 100,
                                           int antiAliasing = 8,
                                           ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                           bool recomputeAlphaMask = false,
                                           bool exportToGallery = true,
                                           bool exportAsync = false)
        {
            CaptureToFile(fileName, width, height, fileFormat, JPGQuality, cameras, null, ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE, antiAliasing, false, colorFormat, recomputeAlphaMask, exportToGallery, exportAsync);
        }



        /// <summary>
        /// Captures the game.
        /// The filename must be a valid full name.
        /// </summary>
        public static void CaptureToFile(string fileName,
                                    int width, int height,
                                    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
                                    int JPGQuality = 100,
                                    List<Camera> cameras = null,
                                    List<Canvas> canvas = null,
                                    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
                                    int antiAliasing = 8,
                                    bool captureGameUI = true,
                                    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                    bool recomputeAlphaMask = false,
                                    bool exportToGallery = true,
                                    bool exportAsync = false)
        {
            InitScreenshotTaker();
            m_ScreenshotTaker.StartCoroutine(CaptureToFileCoroutine(fileName, width, height, fileFormat, JPGQuality, cameras, canvas, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask, exportToGallery, exportAsync));
        }


        public static IEnumerator CaptureScreenToTextureCoroutine(UnityAction<Texture2D> onTextureCaptured = null,
                                          ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB)
        {
            yield return new WaitForEndOfFrame();
            var texture = ScreenshotTaker.CaptureScreenToTexture(colorFormat);
            onTextureCaptured(texture);
        }

        public static IEnumerator CaptureToTextureCoroutine(int width, int height,
                                                    List<Camera> cameras = null,
                                                    List<Canvas> canvas = null,
                                                    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
                                                    int antiAliasing = 8,
                                                    bool captureGameUI = true,
                                                    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                    bool recomputeAlphaMask = false,
                                                    bool exportToGallery = true,
                                                    bool exportAsync = false)
        {
            InitScreenshotTaker();

            // Create resolution item
            ScreenshotResolution captureResolution = new ScreenshotResolution();
            captureResolution.m_Width = width;
            captureResolution.m_Height = height;

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

            // Capture
            yield return m_ScreenshotTaker.StartCoroutine(m_ScreenshotTaker.CaptureAllCoroutine(new List<ScreenshotResolution> { captureResolution },
                screenshotCameras,
                screenshotCanvas,
                captureMode,
                antiAliasing,
                captureGameUI,
                colorFormat,
                recomputeAlphaMask,
                exportToGallery,
                exportAsync));
        }

        public static IEnumerator CaptureToFileCoroutine(string fileName,
                                                    int width, int height,
                                                    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
                                                    int JPGQuality = 100,
                                                    List<Camera> cameras = null,
                                                    List<Canvas> canvas = null,
                                                    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
                                                    int antiAliasing = 8,
                                                    bool captureGameUI = true,
                                                    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                    bool recomputeAlphaMask = false,
                                                    bool exportToGallery = true,
                                                    bool exportAsync = false)
        {
            InitScreenshotTaker();

            yield return CaptureToTextureCoroutine(width, height, cameras, canvas,
                captureMode,
                antiAliasing,
                captureGameUI,
                colorFormat,
                recomputeAlphaMask);

            // EXPORT
            TextureExporter.ExportToFile(m_ScreenshotTaker.m_Texture, fileName, fileFormat, JPGQuality, exportToGallery, exportAsync,
                                            () => { Debug.Log("Screenshot created : " + fileName); },
                                            () => { Debug.LogError("Failed to create screenshot : " + fileName); });
        }


    }


    #endregion


}




