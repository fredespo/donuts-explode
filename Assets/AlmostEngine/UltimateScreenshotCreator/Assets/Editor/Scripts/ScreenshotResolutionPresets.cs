using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot
{
    public class ScreenshotResolutionPresets
    {


        public static string ParseCategory(ScreenshotResolutionAsset asset)
        {
            var cat = AssetDatabase.GetAssetPath(asset);
            int s = cat.LastIndexOf("/");
            if (cat.Contains("Resources/"))
            {
                int r = cat.LastIndexOf("Resources/");
                cat = cat.Substring(r, s - r);
            }
            if (cat.Contains("Presets/"))
            {
                int p = cat.LastIndexOf("Presets/");
                cat = cat.Substring(p, s - p);
            }
            cat = cat.Replace("Presets/", "");
            cat = cat.Replace("Resources/", "");
            return cat;
        }

        public static void ExportPresets(List<ScreenshotResolution> resolutions)
        {
            foreach (ScreenshotResolution res in resolutions)
            {
                string name = res.m_ResolutionName == "" ? res.m_Width.ToString() + "x" + res.m_Height.ToString() : res.m_ResolutionName;
                ScreenshotResolutionAsset preset = ScriptableObjectUtils.CreateAsset<ScreenshotResolutionAsset>(name, "Assets/Editor/DevicePresets/CustomDevices/");
                preset.m_Resolution = new ScreenshotResolution(res);
                EditorUtility.SetDirty(preset);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        public static void ExportAsCollection(List<ScreenshotResolution> resolutions)
        {
            Debug.Log("Creating new custom collection");
            var collection = AssetUtils.Create<PresetCollectionAsset>("Custom collection", "Assets/Editor/DevicePresets/");
            var presets = AssetUtils.LoadAll<ScreenshotResolutionAsset>();

            foreach (var res in resolutions)
            {
                ScreenshotResolutionAsset preset = presets.Find(x => (((x.name == res.m_ResolutionName) || (x.name == "Custom " + res.m_ResolutionName))
                                                            && x.m_Resolution.m_Width == res.m_Width
                                                            && x.m_Resolution.m_Height == res.m_Height
                                                            && x.m_Resolution.m_Scale == res.m_Scale
                                                            // && x.m_Resolution.m_Orientation == res.m_Orientation
                                                            && x.m_Resolution.m_SafeAreaPortrait == res.m_SafeAreaPortrait
                                                            && x.m_Resolution.m_SafeAreaLandscapeLeft == res.m_SafeAreaLandscapeLeft
                                                            && x.m_Resolution.m_PPI == res.m_PPI
                                                            && x.m_Resolution.m_ForcedUnityPPI == res.m_ForcedUnityPPI
                                                            && x.m_Resolution.m_Platform == res.m_Platform
                                                            && x.m_Resolution.m_DeviceCanvas == res.m_DeviceCanvas));

                // If preset does not exist or was changed, create a new custom preset
                if (preset == null)
                {
                    Debug.Log("No identical preset found for " + res.m_ResolutionName + ", creating a new custom preset.");
                    preset = AssetUtils.Create<ScreenshotResolutionAsset>(res.m_ResolutionName, "Assets/Editor/DevicePresets/CustomDevices/");
                    preset.m_Resolution = new ScreenshotResolution(res);
                    preset.m_Resolution.m_Category = "Custom";
                    EditorUtility.SetDirty(preset);
                }

                collection.m_Presets.Add(preset);

            }
            EditorUtility.SetDirty(collection);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = collection;
        }

    }
}