using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot.Extra
{
    public class DownscaleScreenshotPostProcess : ScreenshotProcess
    {
        public FilterMode m_FilterMode = FilterMode.Bilinear;
        public TextureWrapMode m_WrapMode = TextureWrapMode.Clamp;
        public override void Process(ScreenshotResolution res)
        {
            int width = (int)(res.m_Texture.width / res.m_Scale);
            int height = (int)(res.m_Texture.height / res.m_Scale);
            ScreenshotResize.ResizeScreenshot(res, width, height, false, m_FilterMode, m_WrapMode);
        }
    }
}

