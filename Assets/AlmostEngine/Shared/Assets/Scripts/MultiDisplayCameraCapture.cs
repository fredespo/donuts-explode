using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AlmostEngine
{
    /// Add this component to the camera you want to capture in multi display.
    /// This component is also automatically added to the main camera to wait for the end of the render pass and copy the framebuffer content in multi display mode.
    /// It is used only on multi-display settings.
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class MultiDisplayCameraCapture : MonoBehaviour
    {
        public static List<MultiDisplayCameraCapture> m_MultiDisplayCamera = new List<MultiDisplayCameraCapture>();

        public Texture2D m_TargetTexture;
        public bool m_WaitForRendering = false;

        RenderTexture m_TempRenderTexture = null;

        void OnEnable()
        {
            m_MultiDisplayCamera.Add(this);
        }

        void OnDisable()
        {
            m_MultiDisplayCamera.Remove(this);
        }

        public void WaitRenderingAndCopyCameraToTexture(Texture2D targetTexture)
        {
            m_TargetTexture = targetTexture;
            m_WaitForRendering = true;
        }

        public bool CopyIsOver()
        {
            return !m_WaitForRendering;
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest);

            if (m_WaitForRendering && m_TargetTexture != null && m_TempRenderTexture == null)
            {
                if (!Application.isPlaying)
                {
                    // Simply Read pixels
                    m_TargetTexture.ReadPixels(new Rect(0, 0, m_TargetTexture.width, m_TargetTexture.height), 0, 0);
                    m_TargetTexture.Apply(false);
                    m_WaitForRendering = false;
                }
                else
                {
                    // ASYNC gpu read pixels
                    // Copy buffer to temporary render texture
                    m_TempRenderTexture = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
                    Graphics.Blit(src, m_TempRenderTexture);
                    // Async read it
                    StartCoroutine(AsyncReadCoroutine());
                }
            }
        }

        IEnumerator AsyncReadCoroutine()
        {
            UnityEngine.Rendering.AsyncGPUReadbackRequest request = UnityEngine.Rendering.AsyncGPUReadback.Request(m_TempRenderTexture, 0, m_TargetTexture.format);
            while (!request.done)
            {
                yield return new WaitForEndOfFrame();
            }

            if (request.hasError == false)
            {
                // Get raw data from request
                byte[] rawByteArray = request.GetData<byte>().ToArray();
                // Load it to the texture
                m_TargetTexture.LoadRawTextureData(rawByteArray);
                // Update the texture
                m_TargetTexture.Apply(false);
            }
            else
            {
                Debug.LogError("Error with async read request");
            }
            // Release temp data
            if (m_TempRenderTexture != null)
            {
                m_TempRenderTexture.Release();
                m_TempRenderTexture = null;
            }
            // Set waiting task as done
            m_WaitForRendering = false;
        }

    }
}