#if UNITY_IOS

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace AlmostEngine.Screenshot
{
    public class iOsUtils
    {

#if !USC_EXCLUDE_IOS_GALLERY && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _AddImageToGallery(string file);

        [DllImport("__Internal")]
        private static extern bool _HasGalleryAuthorization();

        [DllImport("__Internal")]
        private static extern void _RequestGalleryAuthorization();
#endif

        /// <summary>
        /// Call the native iOs code to add the image at the given path to the phone gallery.
        /// </summary>
        /// <param name="file">The file path.</param>
        public static void AddImageToGallery(string file)
        {
#if !USC_EXCLUDE_IOS_GALLERY && !UNITY_EDITOR
                _AddImageToGallery(file);
#elif !UNITY_EDITOR
                Debug.LogError ("Failed to add image to gallery, the app was build with gallery permission disabled.");
#endif
        }

        /// <summary>
        /// Determines if the app has gallery authorization.
        /// Note that for this method to work the native plugin "Plugins/iOS/iOSUtils.m" must have "Platform Settings/Rarely Used Frameworks/Photos" dependency checked.
        /// </summary>
        /// <returns><c>true</c> if has gallery authorization; otherwise, <c>false</c>.</returns>
        public static bool HasGalleryAuthorization()
        {
#if !USC_EXCLUDE_IOS_GALLERY && !UNITY_EDITOR
                return _HasGalleryAuthorization();
#else
                return false;
#endif
        }

        /// <summary>
        /// Requests the gallery authorization. 
        /// Note that for this method to work the native plugin "Plugins/iOS/iOSUtils.m" must have "Platform Settings/Rarely Used Frameworks/Photos" dependency checked.
        /// </summary>
        public static void RequestGalleryAuthorization()
        {
#if !USC_EXCLUDE_IOS_GALLERY && !UNITY_EDITOR
                _RequestGalleryAuthorization();
#endif
        }

    }
}

#endif