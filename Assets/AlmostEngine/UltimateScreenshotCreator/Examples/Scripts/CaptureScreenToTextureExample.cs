using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Examples
{
    public class CaptureScreenToTextureExample : MonoBehaviour
    {
        public RawImage m_RawImage;

        public void Capture()
        {
            SimpleScreenshotCapture.CaptureScreenToTexture(OnTextureCaptured);
        }

        void OnTextureCaptured(Texture2D texture)
        {
            m_RawImage.texture = texture;
        }
    }
}

