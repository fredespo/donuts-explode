using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot.Extra
{
    public class SetScreenshotScalePreProcess : ScreenshotProcess
    {
        public float m_NewScale = 2f;
        public override void Process(ScreenshotResolution res)
        {
            res.m_Scale = m_NewScale;
        }
    }
}