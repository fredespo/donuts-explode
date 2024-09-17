using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlmostEngine
{
    public static class MultiDisplayUtils
    {
        public static bool IsMultiDisplay()
        {
            
            // Not supported on Scriptable Rendering Pipeline
#if USC_UNITY_PIPELINE_HDRP || USC_UNITY_PIPELINE_URP
            return false;

            // On editor requires version able to simulate multi gameview
#elif (UNITY_EDITOR && !UNITY_2020_1_OR_NEWER)
            return false;
#else

            // If use added a MultiDisplayCameraCapture to a specific camera to be captured
            if (MultiDisplayCameraCapture.m_MultiDisplayCamera.Count > 0)
            {
                // Debug.Log("MULTI DISPLAY REQUEST m_MultiDisplayCamera " + MultiDisplayCameraCapture.m_MultiDisplayCamera.Count);
                return true;
            }

#if (UNITY_5_6_OR_NEWER)

            if (Display.displays.Length == 1)
                return false;

            // Debug.Log("MULTI DISPLAY REQUEST length " + Display.displays.Length);
            for (int i = 1; i < Display.displays.Length; ++i)
            {
                if (Display.displays[i].active)
                    return true;
            }

            return false;

#else

            return Display.displays.Length > 1;

#endif


#endif
        }



    }
}
