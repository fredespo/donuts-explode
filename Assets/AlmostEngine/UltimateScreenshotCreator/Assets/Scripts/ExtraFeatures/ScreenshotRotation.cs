using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
    [ExecuteInEditMode]
    public class ScreenshotRotation : MonoBehaviour
    {
        void OnEnable()
        {
            ScreenshotTaker.onResolutionUpdateEndDelegate -= EndCallback;
            ScreenshotTaker.onResolutionUpdateEndDelegate += EndCallback;
        }

        void OnDisable()
        {
            ScreenshotTaker.onResolutionUpdateEndDelegate -= EndCallback;
        }

        void EndCallback(ScreenshotResolution res)
        {
            RotateScreenshotLeft(res);
        }

        public static void RotateScreenshotRight(ScreenshotResolution res)
        {
            if (res == null || res.m_Texture == null)
            {
                Debug.LogError("Can not rotate, null texture.");
                return;
            }

            var rotated = RotateTextureRight(res.m_Texture);

            // Replace the texture
            DestroyImmediate(res.m_Texture);
            res.m_Texture = rotated;
        }

        public static Texture2D RotateTextureRight(Texture2D tex)
        {
            Texture2D rotated = new Texture2D(tex.height, tex.width, tex.format, false);

            // Copy the content
            Color[] pixels = tex.GetPixels();
            Color[] rotatedPixels = new Color[pixels.Length];
            for (int x = 0; x < tex.width; ++x)
            {
                for (int y = 0; y < tex.height; ++y)
                {
                    rotatedPixels[(tex.width - 1 - x) * tex.height + y] = pixels[y * tex.width + x];
                }
            }
            rotated.SetPixels(rotatedPixels);
            rotated.Apply();
            return rotated;

        }

        public static void RotateScreenshotLeft(ScreenshotResolution res)
        {
            if (res == null || res.m_Texture == null)
            {
                Debug.LogError("Can not rotate, null texture.");
                return;
            }
            
            var rotated = RotateTextureLeft(res.m_Texture);

            // Replace the texture
            DestroyImmediate(res.m_Texture);
            res.m_Texture = rotated;
        }

        public static Texture2D RotateTextureLeft(Texture2D tex)
        {
            Texture2D rotated = new Texture2D(tex.height, tex.width, tex.format, false);

            // Copy the content
            Color[] pixels = tex.GetPixels();
            Color[] rotatedPixels = new Color[pixels.Length];
            for (int x = 0; x < tex.width; ++x)
            {
                for (int y = 0; y < tex.height; ++y)
                {
                    rotatedPixels[x * tex.height + tex.height - 1 - y] = pixels[y * tex.width + x];
                }
            }
            rotated.SetPixels(rotatedPixels);
            rotated.Apply();
            return rotated;

        }
    }
}
