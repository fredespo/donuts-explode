using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
    public class ScreenshotResize
    {
        public static void ResizeScreenshot(ScreenshotResolution res, int width, int height, bool preserveOriginalRatio, FilterMode filterMode, TextureWrapMode wrapMode)
        {
            if (res == null || res.m_Texture == null)
            {
                Debug.LogError("Can not resize, null texture.");
                return;
            }
            
            // Compute new resolution based on ratio preservation, if asked
            if (preserveOriginalRatio)
            {
                float ratio = (float)res.m_Width / (float)res.m_Height;
                if (width > 0)
                {
                    height = (int)(width / ratio);
                }
                else
                {
                    width = (int)(height * ratio);
                }
            }

            // Resize the texture
            var resized = ResizeTexture(res.m_Texture, width, height, filterMode, wrapMode);

            // Replace the texture
            GameObject.DestroyImmediate(res.m_Texture);
            res.m_Texture = resized;
        }

        public static Texture2D ResizeTexture(Texture2D src, int width, int height, FilterMode filterMode, TextureWrapMode wrapMode)
        {
            // Save texture modes
            var previousFilter = src.filterMode;
            var previousWrap = src.wrapMode;

            // Set new texture modes
            src.filterMode = filterMode;
            src.wrapMode = wrapMode;

            // Create a render texture and blit the source in it
            RenderTexture rt = new RenderTexture(width, height, 24);
            RenderTexture.active = rt;
            Graphics.Blit(src, rt);

            // Create a texture and read the render texture
            Texture2D result = new Texture2D(width, height);
            result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            result.Apply();

            // Restore texture modes
            src.filterMode = previousFilter;
            src.wrapMode = previousWrap;

            return result;
        }
    }
}
