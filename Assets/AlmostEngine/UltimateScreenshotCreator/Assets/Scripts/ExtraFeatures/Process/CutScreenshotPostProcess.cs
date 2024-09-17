using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot.Extra
{
    public class CutScreenshotPostProcess : ScreenshotProcess
    {

        public RectTransform m_SelectionArea;
        public int m_Border = 0;

        public override void Process(ScreenshotResolution res)
        {
            ScreenshotCutter.CropScreenshot(res, m_SelectionArea, m_Border);
        }
    }
}

