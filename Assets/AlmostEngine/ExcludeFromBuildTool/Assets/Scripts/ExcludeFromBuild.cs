#if UNITY_EDITOR

// Script is not in Editor folder so it can be called from any script
// But is excluded from build

using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#if UNITY_2018_1_OR_NEWER

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#elif UNITY_5_6_OR_NEWER

using UnityEditor.Build;

#endif

namespace AlmostEngine.ExcludeFromBuildTool
{

    /// Helper class that enables automatic exclusion of files and paths from builds
    [InitializeOnLoad]
    public static class ExcludeFromBuild
    {
        #region EXCLUSION METHODS

        static ExcludeFromBuildConfigAsset config = null;



        public static void AutoExclude(bool exclude)
        {
            // Get config file
            config = GetConfigAsset(exclude);
            if (config == null)
            {
                Debug.LogWarning("Impossible to exclude or restore from build, ExcludeFromBuildConfigAsset not found.");
                return;
            }

            // Skip exclusion if disabled (but don't skip restore)
            if (config.m_AutoExclusionFromBuildEnabled == false)
            {
                return;
            }

            Exclude(exclude);
        }

        public static void Exclude(bool exclude)
        {
            // Get config file
            config = GetConfigAsset(exclude);
            if (config == null)
            {
                Debug.LogWarning("Impossible to exclude or restore from build, ExcludeFromBuildConfigAsset not found.");
                return;
            }

            if (exclude)
            {
                Debug.Log("Exclude from build: starting exclusion.");
            }
            else
            {
                Debug.Log("Exclude from build: starting restoration.");
            }

            // Track that the exclusion process started
            if (exclude)
            {
                config.m_ExclusionProcessStarted = true;
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            // Exclude objects
            ExcludeObjectsFromProject(exclude);

            // Exclude folders
            ExcludeFoldersFromProject(exclude);

            // Refresh asset database to take it into account
            AssetDatabase.Refresh();

            // Get new reference to config asset (first is lost after refresh)
            config = GetConfigAsset(false);

            // Track that the restoration is complete
            if (exclude == false)
            {
                config.m_ExclusionProcessStarted = false;
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        static void ExcludeFoldersFromProject(bool exclude)
        {
            // Sort folders
            var sortedPaths = SortPathsByHierarchy(config.m_FoldersToExclude, exclude);

            // Exclude or restore each path
            foreach (var folder in sortedPaths)
            {
                string fullpath = Application.dataPath + "/../" + folder;
                ExcludeFolderFromProject(fullpath, exclude);
            }
        }


        static void ExcludeFolderFromProject(string fullpath, bool exclude)
        {
            string parent = fullpath.Substring(0, fullpath.LastIndexOf("/") + 1);
            string folderName = fullpath.Substring(fullpath.LastIndexOf("/") + 1);
            var hiddenpath = parent + "." + folderName + "/";
            var hiddenmeta = parent + "." + folderName + ".meta";
            if (exclude)
            {
                if (System.IO.Directory.Exists(fullpath))
                {
                    Debug.Log("Excluding folder: " + fullpath);
                    System.IO.File.Move(fullpath + ".meta", hiddenmeta);
                    System.IO.Directory.Move(fullpath, hiddenpath);
                }
                else
                {
                    Debug.LogWarning("Could not exclude folder " + fullpath + ", folder not found.");
                }
            }
            else
            {
                if (System.IO.Directory.Exists(hiddenpath))
                {
                    Debug.Log("Restoring folder: " + fullpath);
                    System.IO.Directory.Move(hiddenpath, fullpath);
                    System.IO.Directory.Move(hiddenmeta, fullpath + ".meta");
                }
                else
                {
                    Debug.LogWarning("Could not restore folder " + fullpath + ", folder not found.");
                }
            }
        }

        static void ExcludeObjectsFromProject(bool exclude)
        {
            if (exclude)
            {
                var sortedObjects = SortObjectsByHierarchy(config.m_ObjectsToExclude, exclude);
                foreach (var obj in sortedObjects)
                {
                    ExcludeObjectFromProject(obj);
                }
            }
            else
            {
                var sortedPaths = SortPathsByHierarchy(config.m_ExcludedObjectPaths, exclude);
                foreach (var objPath in sortedPaths)
                {
                    RestoreObjectFromProject(objPath);
                }
            }
        }

        static void ExcludeObjectFromProject(Object obj)
        {
            string objectFullPath = Application.dataPath + "/../" + AssetDatabase.GetAssetPath(obj);
            string objectPath = System.IO.Path.GetDirectoryName(objectFullPath);
            string objectName = System.IO.Path.GetFileName(objectFullPath);
            string hiddenFullPath = objectPath + "/" + "." + objectName;

            config.m_ExcludedObjectPaths.Add(objectFullPath);

            Debug.Log("Excluding object: " + objectFullPath);
            System.IO.File.Move(objectFullPath, hiddenFullPath);
            System.IO.File.Move(objectFullPath + ".meta", hiddenFullPath + ".meta");
        }

        static void RestoreObjectFromProject(string objectFullPath)
        {
            string objectPath = System.IO.Path.GetDirectoryName(objectFullPath);
            string objectName = System.IO.Path.GetFileName(objectFullPath);
            string hiddenFullPath = objectPath + "/" + "." + objectName;

            if (!System.IO.File.Exists(hiddenFullPath) && !System.IO.Directory.Exists(hiddenFullPath))
            {
                Debug.LogWarning("Could not restore object " + hiddenFullPath + ", file not found.");
            }
            else
            {
                Debug.Log("Restoring object: " + objectFullPath);
                System.IO.File.Move(hiddenFullPath, objectFullPath);
                System.IO.File.Move(hiddenFullPath + ".meta", objectFullPath + ".meta");
                config.m_ExcludedObjectPaths.Remove(objectFullPath);
            }
        }


        static List<string> SortPathsByHierarchy(List<string> paths, bool exclude)
        {
            List<string> sortedPaths = new List<string>();
            if (exclude)
            {
                // To exclude we want deepest path first
                sortedPaths = paths.OrderByDescending(x => x).ToList();
            }
            else
            {
                // To restore we want higher path first
                sortedPaths = paths.OrderBy(x => x).ToList();
            }
            return sortedPaths;
        }
        static List<Object> SortObjectsByHierarchy(List<Object> objects, bool exclude)
        {
            List<Object> sortedPaths = new List<Object>();
            if (exclude)
            {
                // To exclude we want deepest path first
                sortedPaths = objects.OrderByDescending(x => x.name).ToList();
            }
            else
            {
                // To restore we want higher path first
                sortedPaths = objects.OrderBy(x => x.name).ToList();
            }
            return sortedPaths;
        }


        static ExcludeFromBuildConfigAsset GetConfigAsset(bool canCreate = true)
        {
            if (config != null)
                return config;
            if (canCreate)
            {
                config = AssetUtils.GetOrCreate<ExcludeFromBuildConfigAsset>("ExcludeFromBuildConfigAsset", "Assets/AlmostEngine/ExcludeFromBuildTool/Assets/Editor/");
            }
            else
            {
                config = AssetUtils.GetFirst<ExcludeFromBuildConfigAsset>();
            }
            return config;
        }

        #endregion

        #region PUBLIC MANAGEMENT OF FOLDERS

        public static bool IsEnabled()
        {
            return GetConfigAsset().m_AutoExclusionFromBuildEnabled;
        }

        public static void SetEnabled(bool enabled)
        {
            config = GetConfigAsset();
            config.m_AutoExclusionFromBuildEnabled = enabled;
            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void AddToExcludedFolders(List<string> fullpaths)
        {
            foreach (var path in fullpaths)
            {
                AddToExcludedFolders(path);
            }
        }

        public static void RemoveFromExcludedFolders(List<string> fullpaths)
        {
            foreach (var path in fullpaths)
            {
                RemoveFromExcludedFolders(path);
            }
        }

        public static void AddToExcludedFolders(string fullpath)
        {
            config = GetConfigAsset();
            if (!config.m_FoldersToExclude.Contains(fullpath))
            {
                Debug.Log("Adding " + fullpath + " to excluded folders");
                config.m_FoldersToExclude.Add(fullpath);
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        public static void RemoveFromExcludedFolders(string fullpath)
        {
            config = GetConfigAsset();
            if (config.m_FoldersToExclude.Contains(fullpath))
            {
                Debug.Log("Removing " + fullpath + " from excluded folders");
                config.m_FoldersToExclude.Remove(fullpath);
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        #endregion



        [MenuItem("Tools/Almost Engine/Exclude from build/Exclude")]
        public static void DoManualExclude()
        {
            Exclude(true);
        }

        [MenuItem("Tools/Almost Engine/Exclude from build/Restore")]
        public static void DoManualRestore()
        {
            Exclude(false);
        }


        #region AUTOMATIC RESTORATION LOGIC

        static ExcludeFromBuild()
        {
            // Automatically runned at startup and after each compilation
            // Detect failed restore when scripts are loaded
            config = GetConfigAsset(false);
            if (config == null)
            {
                return;
            }
            if (config.m_AutoExclusionFromBuildEnabled && config.m_ExclusionProcessStarted)
            {
                Debug.Log("Detecting non restored folder exclusion, restoring excluded folders.");
                AutoExclude(false);
            }
        }

        public static void OnBuildError(string condition, string stacktrace, LogType type)
        {
            // Detect build error and restore
            if (type == LogType.Error)
            {
                config = GetConfigAsset(false);
                if (config != null && config.m_AutoExclusionFromBuildEnabled)
                {
                    Debug.Log("Detecting build error, restoring excluded folders.");
                    // Stop listening to build error
                    Application.logMessageReceived -= ExcludeFromBuild.OnBuildError;
                    // Restore
                    AutoExclude(false);
                }
            }
        }

        #endregion
    }


    #region EXCLUDE FROM BUILD LOGIC
#if UNITY_2018_1_OR_NEWER
    class ExcludeFromBuildprocess : IPreprocessBuildWithReport, IPostprocessBuildWithReport
#elif UNITY_5_6_OR_NEWER
    class ExcludeFromBuildProcess : IPreprocessBuild, IPostprocessBuild
#endif
    {
        public int callbackOrder { get { return -100; } }

#if UNITY_2018_1_OR_NEWER
        public void OnPreprocessBuild(BuildReport report)
#elif UNITY_5_6_OR_NEWER
        public void OnPreprocessBuild(BuildTarget target, string path)
#endif
        {
            return; // TEMP FIX: disable that feature
            // // Debug.Log("Exclude from build: build starting, starting folders exclusion.");
            // // Start listening to build error
            // Application.logMessageReceived += ExcludeFromBuild.OnBuildError;
            // // Exclude folders when build starts
            // ExcludeFromBuild.AutoExclude(true);
        }


#if UNITY_2018_1_OR_NEWER
        public void OnPostprocessBuild(BuildReport report)
#elif UNITY_5_6_OR_NEWER
        public void OnPostprocessBuild(BuildTarget target, string path)
#endif
        {
            return; // TEMP FIX: disable that feature
            // // Debug.Log("Exclude from build: build complete, starting folders restoration.");
            // // Stop listening to build error
            // Application.logMessageReceived -= ExcludeFromBuild.OnBuildError;
            // // Restore folders when build ends
            // ExcludeFromBuild.AutoExclude(false);
        }

    }
    #endregion

}

#endif
