using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot.Extra
{
    public class RotateScreenshotPostProcess : ScreenshotProcess
    {

        public enum RotationType { ROTATE_LEFT, ROTATE_RIGHT };
        public RotationType m_Type;

        public override void Process(ScreenshotResolution res)
        {
            if (m_Type == RotationType.ROTATE_LEFT)
            {
                ScreenshotRotation.RotateScreenshotLeft(res);
            }
            else
            {
                ScreenshotRotation.RotateScreenshotRight(res);
            }

        }
    }
}