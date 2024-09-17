using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot.Extra
{
    public class ResizeScreenshotPostProcess : ScreenshotProcess
    {
        public enum ResizeMode
        {
            SCALE,
            FIXED_RESOLUTION
        };

        [Header("Resize mode")]
        public ResizeMode m_ResizeType;
        public FilterMode m_FilterMode = FilterMode.Bilinear;
        public TextureWrapMode m_WrapMode = TextureWrapMode.Clamp;

        [Header("Scale settings")]
        public float m_Scale = .5f;

        [Header("Fixed resolution settings")]
        public int m_TargetWidth = 800;
        public int m_TargetHeight = 600;
        public bool m_PreserveOriginalRatio = true;

        public override void Process(ScreenshotResolution res)
        {
            int width = (int)(m_ResizeType == ResizeMode.FIXED_RESOLUTION ? m_TargetWidth : m_Scale * res.m_Texture.width);
            int height = (int)(m_ResizeType == ResizeMode.FIXED_RESOLUTION ? m_TargetHeight : m_Scale * res.m_Texture.height);
            bool ratio = m_ResizeType == ResizeMode.FIXED_RESOLUTION ? m_PreserveOriginalRatio : false;
            ScreenshotResize.ResizeScreenshot(res, width, height, ratio, m_FilterMode, m_WrapMode);
        }
    }
}

