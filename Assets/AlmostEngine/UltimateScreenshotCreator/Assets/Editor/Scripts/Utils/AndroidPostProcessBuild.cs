#if UNITY_ANDROID && USC_ANDROID_LEGACY_EXTERNAL_STORAGE

using System.IO;

using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Android;

namespace AlmostEngine.Screenshot
{
#if UNITY_2018_4_OR_NEWER
    public class AndroidPostProcessBuild : IPreprocessBuildWithReport, IPostprocessBuildWithReport, IPostGenerateGradleAndroidProject
    {


        bool m_BuildSuccess = false;
        public int callbackOrder { get { return 0; } }
#if !USC_EXCLUDE_SHARE
        static string NativeShareTodo = " in ScreenshotManager settings (INSTALL section / EXCLUDE FROM BUILD -> set enabled / SHARE -> set excluded)";
        static string NativeShareErrorSDK = "Warning: NativeShare dependency requires minimum target Android 12. In case of \"unexpected element<queries> found in <manifest>\" error, please change target SDK version to 31 minimum, or exclude the Share feature: ";
#if !UNITY_2020_1_OR_NEWER
        static string NativeShareErrorGradle = "Warning: NativeShare dependency requires Gradle 5.6.4. In case of \"Gradle Build Failed\" error, please update Gradle version, or exclude the Share feature: ";
#endif
#endif

        public void OnPreprocessBuild(BuildReport report)
        {
            // Register to update to check if build is sucess
            m_BuildSuccess = false;
            UnityEditor.EditorApplication.update += BuildCheck;
        }

        private void BuildCheck()
        {
            if (!UnityEditor.BuildPipeline.isBuildingPlayer)
            {
                UnityEditor.EditorApplication.update -= BuildCheck;
                if (m_BuildSuccess == false)
                {
                    OnPostprocessBuildFailure();
                }
            }
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            UnityEditor.EditorApplication.update -= BuildCheck;
            m_BuildSuccess = (report.summary.result == BuildResult.Succeeded || report.summary.result == BuildResult.Cancelled);
        }

        public void OnPostprocessBuildFailure()
        {
#if !USC_EXCLUDE_SHARE
#if !UNITY_2020_1_OR_NEWER
            Debug.LogError(NativeShareErrorGradle + NativeShareTodo);
#endif
            if ((int)UnityEditor.PlayerSettings.Android.minSdkVersion < 31)
            {
                Debug.LogError(NativeShareErrorSDK + NativeShareTodo);
        }
#endif
        }

        public void OnPostGenerateGradleAndroidProject(string basePath)
        {
            // Get the manifest
            string manifestPath = basePath + "/src/main/AndroidManifest.xml";
            string manifest = File.ReadAllText(manifestPath);

            // Check that the permission is not already enabled
            if (manifest.Contains("requestLegacyExternalStorage"))
            {
                return;
            }

            // Insert storage legacy entry
            int lastEntryIndex = manifest.IndexOf("<application") + 12;
            manifest = manifest.Insert(lastEntryIndex, " android:requestLegacyExternalStorage=\"true\" ");
            Debug.Log("Inserting requestLegacyExternalStorage to Android manifest.\n" + manifest);

            // Save manifest
            File.WriteAllText(manifestPath, manifest);
        }
    }
#endif
}

#endif
