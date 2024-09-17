using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Screenshot
{
    public class ScreenshotComposer : MonoBehaviour
    {
        [Tooltip("The UI camera to be used to render the canvas.")]
        public Camera m_Camera;

        public Canvas m_Canvas;


        [Tooltip("The list of rawimage containing a game screenshot.")]
        public List<RawImage> m_Textures = new List<RawImage>();


        ScreenshotTaker m_ScreenshotTaker;

        public virtual IEnumerator CaptureCoroutine(ScreenshotResolution desiredCaptureResolution,
                                                     List<ScreenshotCamera> cameras,
                                                     List<ScreenshotOverlay> overlays,
                                                     ScreenshotTaker.CaptureMode captureMode,
                                                     int antiAliasing = 8,
                                                     bool captureGameUI = true,
                                                     ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
                                                     bool recomputeAlphaMask = false,
                                                     bool stopTime = false,
                                                     bool restore = true,
                                                     bool forceUICulling = false,
                                                     bool disableHDRPFX = true)
        {
            m_ScreenshotTaker = GameObject.FindObjectOfType<ScreenshotTaker>();

            // Capture all inner textures
            foreach (RawImage texture in m_Textures)
            {
                yield return m_ScreenshotTaker.StartCoroutine(CaptureInnerTextureCoroutine(texture, desiredCaptureResolution, cameras, overlays, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask, stopTime, restore, forceUICulling, disableHDRPFX));
            }

            // Capture the composition at the desired resolution size
            yield return m_ScreenshotTaker.StartCoroutine(CaptureCompositionCoroutine(desiredCaptureResolution));
        }


        public virtual IEnumerator CaptureInnerTextureCoroutine(RawImage texture, ScreenshotResolution desiredCaptureResolution, List<ScreenshotCamera> cameras, List<ScreenshotOverlay> overlays,
                                                                 ScreenshotTaker.CaptureMode captureMode, int antiAliasing = 8, bool captureGameUI = true,
                                                                 ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB, bool recomputeAlphaMask = false, bool stopTime = false, bool restore = true, bool forceUICulling = false,
                                                                 bool disableHDRPFX = true)
        {
            // Compute the texture resolution
            ScreenshotResolution tempRes = new ScreenshotResolution();
            yield return m_ScreenshotTaker.StartCoroutine(ComputeInnerTextureSizeCoroutine(texture, desiredCaptureResolution.ComputeTargetWidth(), desiredCaptureResolution.ComputeTargetHeight(), tempRes));

            // Capture the game view at the raw image resolution
            yield return m_ScreenshotTaker.StartCoroutine(m_ScreenshotTaker.CaptureAllCoroutine(new List<ScreenshotResolution> { tempRes },
                cameras, overlays, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask, stopTime, restore, forceUICulling, disableHDRPFX));

            // Set raw image texture using the previously captured texture
            texture.texture = tempRes.m_Texture;
        }

        protected float supersampleCoeff = 1.25f;
        protected int resizeframe = 3;

        /// <summary>
        /// Computes the inner image size to be used to capture the game screenshot.
        /// It enables the composition canvas at the desired composition resolution, and look for the raw image size and scale.
        /// </summary>
        public virtual IEnumerator ComputeInnerTextureSizeCoroutine(RawImage image, int compositionWidth, int compositionHeight, ScreenshotResolution resolution)
        {
            m_Canvas.gameObject.SetActive(true);

            // Resize the gameview at the composition resolution, and wait for a few frames
            GameViewController.SaveCurrentGameViewSize();
            for (int f = 0; f < resizeframe; ++f)
            {
                GameViewController.SetGameViewSize(compositionWidth, compositionHeight);
                yield return new WaitForEndOfFrame();
            }

            // Get the raw image size
            Rect r = image.rectTransform.rect;
            resolution.m_Width = (int)(r.width * image.rectTransform.lossyScale.x * supersampleCoeff);
            resolution.m_Height = (int)(r.height * image.rectTransform.lossyScale.y * supersampleCoeff);
            //			Debug.Log ("Raw image size " + resolution);

            // Restore all
            m_Canvas.gameObject.SetActive(false);
            GameViewController.RestoreGameViewSize();

        }

        public IEnumerator CaptureCompositionCoroutine(ScreenshotResolution desiredCaptureResolution)
        {
            yield return m_ScreenshotTaker.StartCoroutine(m_ScreenshotTaker.CaptureResolutionCoroutine(desiredCaptureResolution,
                new List<Camera> { m_Camera }, new List<Canvas> { m_Canvas },
                ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING, 8, false, ScreenshotTaker.ColorFormat.RGB, false));
        }
    }
}