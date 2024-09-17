using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Examples
{
    public class ScreenshotSettingsDebug : MonoBehaviour
    {

        public Text m_DebugText;

        void OnEnable()
        {
            ScreenshotManager manager = GameObject.FindObjectOfType<ScreenshotManager>();

            string debugString = "";

            debugString += "Export to gallery: " + manager.GetConfig().m_ExportToPhoneGallery + "\n";
            debugString += "Export path: " + manager.GetExportPath() + "\n";

#if !UNITY_EDITOR && UNITY_IOS
        debugString += "HasGalleryAuthorization " + iOsUtils.HasGalleryAuthorization() + "\n";
#endif

#if !UNITY_EDITOR && UNITY_ANDROID
        debugString += "GetAndroidSDKVersion " + AndroidUtils.GetAndroidSDKVersion() + "\n";
        debugString += "HasPermissionToAccessExternalStorage " + AndroidUtils.HasPermissionToAccessExternalStorage() + "\n";
        debugString += "IsPrimaryStorageAvailable " + AndroidUtils.IsPrimaryStorageAvailable() + "\n";
        debugString += "IsExternalStorageLegacy " + AndroidUtils.IsExternalStorageLegacy() + "\n";
        debugString += "GetPrimaryStorage " + AndroidUtils.GetPrimaryStorage() + "\n";
        debugString += "GetFirstAvailableMediaStorage " + AndroidUtils.GetFirstAvailableMediaStorage() + "\n";
#endif


            m_DebugText.text = debugString;
        }
    }
}