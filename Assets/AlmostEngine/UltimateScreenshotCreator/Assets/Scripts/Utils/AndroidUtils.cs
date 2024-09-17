#if UNITY_ANDROID

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_2018_4_OR_NEWER
using UnityEngine.Android;
#endif

using AlmostEngine.Examples;

namespace AlmostEngine.Screenshot
{
    public class AndroidUtils
    {

        /// <summary>
        /// Returns the external picture directory.
        /// If the primary storage is not available or the permission to access the external storage is not granted,
        /// the method returns the first available media storage.
        /// </summary>
        /// <returns>The external picture directory.</returns>
        public static string GetExternalPictureDirectory()
        {
#if !UNITY_EDITOR
            if (IsPrimaryStorageAvailable() && !HasPermissionToAccessExternalStorage())
            {
                RequestPermissionToAccessExternalStorage();
            }
            if (IsPrimaryStorageAvailable() && HasPermissionToAccessExternalStorage())
            {
                return GetPrimaryStorage() + "/" + GetPictureFolderName();
            }
            else
            {
                Debug.LogWarning("Primary storage not available, fallback to media storage.");
                return GetFirstAvailableMediaStorage();
            }
#else
            return Application.persistentDataPath;
#endif
        }

        public static bool IsPrimaryStorageAvailable()
        {
#if !UNITY_EDITOR
            try
            {
                // Get storage state
                AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
                string state = environment.CallStatic<string>("getExternalStorageState");

                if (state == "mounted")
                    return true;

            }
            catch (System.Exception e)
            {
                Debug.LogError("AndroidUtils: Error getting external storage state: " + e.Message);
            }
#endif
            return false;
        }

        public static string GetPrimaryStorage()
        {
#if !UNITY_EDITOR
            try
            {
                AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
                AndroidJavaObject file = environment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"); //  This method was deprecated in Android API level 29 when an app targets Build.VERSION_CODES.Q.
                string path = file.Call<string>("getPath");
                return path;
            }
            catch (System.Exception e)
            {
                Debug.LogError("AndroidUtils: Error getting primary external storage directory: " + e.Message);
            }
#endif
            return "";
        }

        public static string GetExternalFilesDir()
        {
#if !UNITY_EDITOR
            try
            {
                AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject file = objActivity.Call<AndroidJavaObject>("getExternalFilesDir", GetPictureFolderName());
                string path = file.Call<string>("getPath");
                return path;
            }
            catch (System.Exception e)
            {
                Debug.LogError("AndroidUtils: Error getting external file dir: " + e.Message);
            }
#endif
            return "";
        }


        public static string GetFirstAvailableMediaStorage()
        {
#if !UNITY_EDITOR
            List<string> secondaryStorages = GetAvailableExternalMediaStorages();
            if (secondaryStorages.Count > 0)
                return secondaryStorages[0];

            // Fallback
            Debug.LogWarning("No media storage available, using persistentDataPath as fallback");
#endif
            return Application.persistentDataPath;
        }

        /// <summary>
        /// Gets all secondary storages and return only available ones.
        /// </summary>
        public static List<string> GetAvailableExternalMediaStorages()
        {
            List<string> storages = new List<string>();
#if !UNITY_EDITOR
            try
            {
                AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject[] files = objActivity.Call<AndroidJavaObject[]>("getExternalMediaDirs");
                for (int i = 0; i < files.Length; ++i)
                {
                    string path = files[i].Call<string>("getPath");
                    storages.Add(path);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("AndroidUtils: Error getting secondary external storage directory: " + e.Message);
            }
#endif
            return storages;

        }

        public static string GetPictureFolderName()
        {
#if !UNITY_EDITOR
            return GetDirectoryName("DIRECTORY_PICTURES");
#else
            return "Pictures";
#endif
        }

        public static string GetDirectoryName(string directoryType = "DIRECTORY_PICTURES")
        {
#if !UNITY_EDITOR
            AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
            return environment.GetStatic<string>(directoryType);
#else
            return "";
#endif
        }


        /// <summary>
        /// Determines if has access to external storage.
        /// </summary>
        /// <returns><c>true</c> if has access to external storage; otherwise, <c>false</c>.</returns>
        public static bool HasPermissionToAccessExternalStorage()
        {
#if !UNITY_EDITOR
            // checkSelfPermission not available for sdk < 23
            if (GetAndroidSDKVersion() < 23)
            {
                return true;
            }
            // Test storage permission
            if (!HasPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
                return false;
            // On Android 29 and later testing WRITE_EXTERNAL_STORAGE is not enough due to new external storage policy
            // WRITE_EXTERNAL_STORAGE can be true but the user still doesn't have access to the external storage
            // We try to create a dummy picture to see if it's really possible
            if (GetAndroidSDKVersion() >= 29)
            {
                try
                {
                    string dummyFilePath =  GetPrimaryStorage() + "/" + GetPictureFolderName()+"/"+"dummyPictureForStoragePermissionTest.png";
                    Debug.Log("Trying to create a dummy picture to test storage permission on Android SDK 29+ " + dummyFilePath);
                    System.IO.File.WriteAllText(dummyFilePath,"testing external storage permission on Android SDK 29+");
                    Debug.Log("Cleaning dummy picture.");
                    System.IO.File.Delete(dummyFilePath);
                }
                catch
                {
                    Debug.Log("External storage permission refused on Android SDK 29+.");
                    return false;
                }
            }
            return true;
#else
            return false;
#endif
        }

        public static void RequestPermissionToAccessExternalStorage()
        {
#if !UNITY_EDITOR
            RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
#endif
        }

        public static bool IsExternalStorageLegacy()
        {
#if !UNITY_EDITOR
            if (GetAndroidSDKVersion() < 29){
                return false;
            }
            AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
            return environment.CallStatic<bool>("isExternalStorageLegacy");
#else
            return false;
#endif
        }

        public static int GetAndroidSDKVersion()
        {
#if !UNITY_EDITOR
            AndroidJavaClass version = new AndroidJavaClass("android.os.Build$VERSION");
            return version.GetStatic<int>("SDK_INT");
#else
            return 0;
#endif
        }

        public static void RequestPermission(string permissionName)
        {
#if !UNITY_EDITOR && UNITY_2018_4_OR_NEWER
            Permission.RequestUserPermission(permissionName);
#elif !UNITY_EDITOR
			Debug.LogWarning ("Permission request only available with Unity 2018.4 or newer.");
#endif
        }

        public static bool HasPermission(string permissionName)
        {
#if !UNITY_EDITOR
            AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            int permission = objActivity.Call<int>("checkSelfPermission", permissionName);
            return (permission == 0);
#else
            return false;
#endif
        }

        /// <summary>
        /// Call the Media Scanner to add the media to the gallery
        /// </summary>
        public static void AddImageToGallery(string file)
        {
#if !UNITY_EDITOR
            AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass classMedia = new AndroidJavaClass("android.media.MediaScannerConnection");
            classMedia.CallStatic("scanFile", new object[4] { objActivity, new string[] { file }, null, null });
#endif
        }

    }
}

#endif