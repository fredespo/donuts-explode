using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.Screenshot.Extra
{
    [ExecuteInEditMode]
    /// <summary>
    /// Add this component to all objects you want to hide during the capture process.
    /// Note that it works only in play mode.
    /// </summary>
    public class HideOnCapture : MonoBehaviour
    {
        void OnEnable()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate -= Hide;
            ScreenshotTaker.onResolutionUpdateStartDelegate += Hide;
            ScreenshotTaker.onResolutionUpdateEndDelegate -= Show;
            ScreenshotTaker.onResolutionUpdateEndDelegate += Show;
        }

        void OnDestroy()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate -= Hide;
            ScreenshotTaker.onResolutionUpdateEndDelegate -= Show;
        }

        void Hide(ScreenshotResolution res)
        {
            this.gameObject.SetActive(false);
        }

        void Show(ScreenshotResolution res)
        {
            this.gameObject.SetActive(true);
        }
    }
}