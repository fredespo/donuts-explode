
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using AlmostEngine.ExcludeFromBuildTool;

namespace AlmostEngine.Screenshot
{
    public class PhotoUsageDescription : ScriptableObject
    {
        public string m_UsageDescription = "This application requires the access to the photo library to allow the user to take screenshots that are automatically added to the Camera Roll.";

        static PhotoUsageDescription m_Usage;



        #region INSTALL MODE

        public bool m_FeatureSafeAreaComponent = true;
        public bool m_FeatureScreenshotCapture = true;
        public bool m_FeaturePhoneShare = true;
        public bool m_FeaturePhoneGallery = true;
        public bool m_FeatureSimpleLocalization = true;
        public bool m_FeatureExamples = true;


        public void ToggleSafeArea(bool enable)
        {
            Toggle(ref m_FeatureSafeAreaComponent, enable, new List<string> { "UniversalDevicePreview" });
        }

        public void ToggleScreenshot(bool enable)
        {
            if (enable == false)
            {
                ToggleShare(false);
                ToggleGallery(false);
                ToggleExamples(false);
            }
            Toggle(ref m_FeatureScreenshotCapture, enable, new List<string> { "UltimateScreenshotCreator" });
        }

        public void ToggleShare(bool enable)
        {
            if (enable == true)
            {
                ToggleScreenshot(true);
            }
            Toggle(ref m_FeaturePhoneShare, enable, new List<string> { "Dependencies/UnityNativeShare"});
            //, "UltimateScreenshotCreator/Assets/Scripts/ExtraFeatures/Share" });
            RemovePermissionNeeds.ToggleShare(m_FeaturePhoneShare);
        }

        public void ToggleGallery(bool enable)
        {
            if (enable == true)
            {
                ToggleScreenshot(true);
            }
            // Toggle(ref m_FeaturePhoneGallery, enable, new List<string> { "UltimateScreenshotCreator/Plugins/iOS" });
            m_FeaturePhoneGallery = enable;
            RemovePermissionNeeds.ToggleiOSGalleryPermission(m_FeaturePhoneGallery);
        }

        public void ToggleExamples(bool enable)
        {
            if (enable == true)
            {
                ToggleScreenshot(true);
            }
            Toggle(ref m_FeatureExamples, enable, new List<string> { "UltimateScreenshotCreator/Examples", "Shared/Examples" });
        }

        public void ToggleLocalization(bool enable)
        {
            Toggle(ref m_FeatureSimpleLocalization, enable, new List<string> { "SimpleLocalization", "UltimateScreenshotCreator/Assets/Scripts/ExtraFeatures/Localization" });
        }

        void Toggle(ref bool isEnabled, bool enable, List<string> toExclude)
        {
            List<string> fullpaths = new List<string>();
            var rootPath = AssetUtils.FindAssetDirectory("AlmostEngine.asmdef");
            foreach (var path in toExclude)
            {
                fullpaths.Add(rootPath + "/" + path);
            }
            if (enable)
            {
                ExcludeFromBuild.RemoveFromExcludedFolders(fullpaths);
            }
            else
            {
                ExcludeFromBuild.AddToExcludedFolders(fullpaths);
            }
            isEnabled = enable;


            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion




    }
}