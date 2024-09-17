using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
    [ExecuteInEditMode]
    /// <summary>
    /// Can crop the screenshot using the specified RectTransform.
    /// Add it to one scene objects and set the RectTransform.	
    /// </summary>
    public class ScreenshotCutter : MonoBehaviour
    {

        public RectTransform m_SelectionArea;
        public bool m_HideSelectionAreaDuringCapture = false;
        public int m_CropBorder = 1;

        void OnEnable()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate -= StartCallback;
            ScreenshotTaker.onResolutionUpdateStartDelegate += StartCallback;

            ScreenshotTaker.onResolutionUpdateEndDelegate -= EndCallback;
            ScreenshotTaker.onResolutionUpdateEndDelegate += EndCallback;
        }

        void OnDisable()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate -= StartCallback;
            ScreenshotTaker.onResolutionUpdateEndDelegate -= EndCallback;
        }

        void StartCallback(ScreenshotResolution res)
        {
            if (m_SelectionArea == null)
                return;

            if (m_HideSelectionAreaDuringCapture)
            {
                Hide();
            }
        }

        void EndCallback(ScreenshotResolution res)
        {
            if (m_SelectionArea == null)
                return;

            if (m_HideSelectionAreaDuringCapture)
            {
                Show();
            }
            CropScreenshot(res, m_SelectionArea, m_CropBorder);
        }

        bool m_WasActive = true;

        void Hide()
        {
            m_WasActive = m_SelectionArea.gameObject.activeSelf;
            m_SelectionArea.gameObject.SetActive(false);
        }

        void Show()
        {
            m_SelectionArea.gameObject.SetActive(m_WasActive);
        }

        public static void CropScreenshot(ScreenshotResolution res, RectTransform selection, int cropBorder = 0)
        {
            if (res == null || res.m_Texture == null)
            {
                Debug.LogError("Can not crop, null texture.");
                return;
            }

            var cropped = CropTexture(res.m_Texture, selection, cropBorder);

            // Replace the texture
            GameObject.DestroyImmediate(res.m_Texture);
            res.m_Texture = cropped;
        }

        public static Texture2D CropTexture(Texture2D toCrop, RectTransform selection, int cropBorder = 0)
        {
            // Get the selection image coordinates
            Vector3[] corners = new Vector3[4];
            selection.GetWorldCorners(corners);

            // Create cropped texture
            int x0 = (int)corners[0].x + cropBorder;
            int y0 = (int)corners[0].y + cropBorder;
            int width = (int)(corners[2].x - corners[0].x) - 2 * cropBorder;
            int height = (int)(corners[1].y - corners[0].y) - 2 * cropBorder;

            // Clamp coordinates
            x0 = (int)Mathf.Clamp(x0, 0f, toCrop.width - 1);
            y0 = (int)Mathf.Clamp(y0, 0f, toCrop.height - 1);
            width = (int)Mathf.Clamp(width, 0f, toCrop.width - x0);
            height = (int)Mathf.Clamp(height, 0f, toCrop.height - y0);

            // Crop the texture
            return CropTexture(toCrop, x0, y0, width, height);
        }

        public static Texture2D CropTexture(Texture2D toCrop, int x0, int y0, int width, int height)
        {

            Texture2D cropped = new Texture2D(width, height, toCrop.format, false);

            if (width <= 2 || height <= 2)
                return null;

            // Copy the content
            Color[] pixels = toCrop.GetPixels(x0, y0, width, height);
            cropped.SetPixels(pixels);
            cropped.Apply();

            // Debug.Log("Texture cropped to (" + x0 + ", " + y0 + ", " + (x0 + width - 1) + ", " + (y0 + height - 1) + ")");

            return cropped;

        }
    }
}
