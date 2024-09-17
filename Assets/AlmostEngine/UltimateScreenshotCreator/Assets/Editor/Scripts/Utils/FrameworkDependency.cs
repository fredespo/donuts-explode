using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

#if UNITY_2018_1_OR_NEWER

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#elif UNITY_5_6_OR_NEWER

using UnityEditor.Build;

#endif


namespace AlmostEngine.Screenshot
{

    public static class FrameworkDependency
    {
        public static void AddiOSPhotosFrameworkDependency()
        {
            string pluginPath = AssetUtils.FindAssetPath("iOSUtils.m");
            if (pluginPath != "")
            {
                FrameworkDependency.AddFrameworkDependency(pluginPath, BuildTarget.iOS, "Photos");
            }
            else
            {
                Debug.LogError("iOSUtils plugin not found.");
            }
        }

        public static string frameworkDependenciesKey = "FrameworkDependencies";

        public static void AddFrameworkDependency(string pluginPath, BuildTarget target, string framework)
        {
            PluginImporter plugin = AssetImporter.GetAtPath(pluginPath) as PluginImporter;
            if (plugin == null)
                return;
            plugin.SetCompatibleWithPlatform(BuildTarget.iOS, true);
            string dependencies = plugin.GetPlatformData(target, frameworkDependenciesKey);
            if (!dependencies.Contains(framework))
            {
                plugin.SetPlatformData(target, frameworkDependenciesKey, dependencies + ";" + framework);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("Adding framework dependency to " + target + ": " + framework);
            }
        }
    }



#if UNITY_2018_1_OR_NEWER && UNITY_IOS && !USC_EXCLUDE_IOS_GALLERY


	class iOSFrameworkDependencyPreprocess : IPreprocessBuildWithReport
	{
        public int callbackOrder { get { return 1; } }
        public void OnPreprocessBuild(BuildReport report)
        {            
            FrameworkDependency.AddiOSPhotosFrameworkDependency();
        }
	}


#elif UNITY_5_6_OR_NEWER && UNITY_IOS && !USC_EXCLUDE_IOS_GALLERY

	class iOSFrameworkDependencyPreprocess : IPreprocessBuild
	{
        public int callbackOrder { get { return 1; } }
        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            FrameworkDependency.AddiOSPhotosFrameworkDependency();
        }
	}

#endif

}