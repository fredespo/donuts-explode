using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Examples
{
    public class CaptureCameraToTextureExample : MonoBehaviour
    {
        public RawImage m_RawImage;
        public Camera m_Camera;
        public int m_Width = 1600;
        public int m_Height = 900;

        public void Capture()
        {
            m_RawImage.texture = SimpleScreenshotCapture.CaptureCameraToTexture(m_Width, m_Height, m_Camera);
        }
    }
}

