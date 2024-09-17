using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot
{
    public class ShareUtils
    {
        public static bool CanShare()
        {
#if !USC_EXCLUDE_SHARE && UNITY_EDITOR
            return true;
#elif !USC_EXCLUDE_SHARE && (UNITY_IOS || UNITY_ANDROID)
            return true;
#elif !USC_EXCLUDE_SHARE && UNITY_WEBGL
            return WebGLUtils.CanShare();
#else
            return false;
#endif
        }

        public static void ShareImage(Texture2D texture, string screenshotName, string title = "", string subject = "", string description = "", TextureExporter.ImageFileFormat imageFormat = TextureExporter.ImageFileFormat.PNG, int JPGQuality = 70)
        {

#if !USC_EXCLUDE_SHARE && !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            string format = (imageFormat == TextureExporter.ImageFileFormat.JPG) ? "jpeg" : "png";
            string filename = screenshotName + "." + format;
            // On iOS and Android share using Native Share
            var nativeShare = new NativeShareNamespace.NativeShare();
            nativeShare.AddFile(texture, filename);
            nativeShare.SetTitle(title);
            nativeShare.SetSubject(subject);
            nativeShare.SetText(description);
            nativeShare.Share();
#elif !USC_EXCLUDE_SHARE && !UNITY_EDITOR && UNITY_WEBGL
            // On WebGL share using custom share plugin
            // Convert texture to bytes
            byte[] bytes = null;
            if (imageFormat == TextureExporter.ImageFileFormat.JPG)
            {
                bytes = texture.EncodeToJPG(JPGQuality);
            }
            else
            {
                bytes = texture.EncodeToPNG();
            }
            // Try sharing
            try
            {
                string format = (imageFormat == TextureExporter.ImageFileFormat.JPG) ? "jpeg" : "png";
                WebGLUtils.ShareImage(bytes, screenshotName, format);
            }
            catch
            {
                Debug.LogError("Failed to share image.");
            }
#else
            Debug.LogError("Share not supported on this platform.");
#endif
        }
    }
}
