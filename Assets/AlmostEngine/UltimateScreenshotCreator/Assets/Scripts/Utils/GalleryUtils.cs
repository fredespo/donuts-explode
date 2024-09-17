using UnityEngine;


namespace AlmostEngine.Screenshot
{
    public class GalleryUtils
    {
        public static bool AddToGallery(string fullpath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                // Update android gallery
                try {
                    AndroidUtils.AddImageToGallery(fullpath);
                } catch {
                    Debug.LogError ("Failed to add image to Android Gallery");
                    return false;
                }

#elif UNITY_IOS && !UNITY_EDITOR
                // Update ios gallery
                try {
                    iOsUtils.AddImageToGallery(fullpath);
                } catch {
                    Debug.LogError ("Failed to add image to iOS Gallery");
                    return false;
                }
#endif
            return true;
        }
    }
}