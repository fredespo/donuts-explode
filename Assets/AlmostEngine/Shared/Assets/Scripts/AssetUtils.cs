#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlmostEngine
{
    public class AssetUtils
    {
        public static string GetActiveSceneFolder()
        {
            string path = EditorSceneManager.GetActiveScene().path;
            path = path.Substring(0, path.LastIndexOf("/") + 1);
            return path + EditorSceneManager.GetActiveScene().name + "/";
        }
        public static List<T> LoadAll<T>(List<string> paths = null, bool includeSubAssets = false) where T : Object
        {
            return LoadAll<T, T>(paths, includeSubAssets);
        }

        public static List<AssetType> LoadAll<AssetType, ClassType>(List<string> paths = null, bool includeSubAssets = false) where AssetType : Object
        {
            string filter = "t:";
            if (typeof(AssetType) == typeof(GameObject))
            {
                filter += "prefab";
            }
            else
            {
                filter += typeof(AssetType).ToString();
            }
            List<string> guids = AssetDatabase.FindAssets(filter).ToList<string>();
            // Debug.Log("T : " + typeof(AssetType).ToString() + " Found guids " + guids.Count);

            List<AssetType> assets = new List<AssetType>();

            foreach (var id in guids)
            {
                if (!AssetPathUtils.InPaths(AssetDatabase.GUIDToAssetPath(id), paths))
                    continue;

                // Debug.Log("Found id " + id);
                if (includeSubAssets)
                {
                    var assetsAtPath = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(id));
                    foreach (var obj in assetsAtPath)
                    {
                        if (obj != null && obj is ClassType)
                        {
                            assets.Add((AssetType)obj);
                        }

                    }
                }
                else
                {
                    var obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(id), typeof(AssetType));
                    if (obj != null && obj is ClassType)
                    {
                        assets.Add((AssetType)obj);
                    }
                }

            }

            return assets;
        }

        public static T GetFirst<T>() where T : ScriptableObject
        {
            List<T> objs = LoadAll<T>();
            if (objs.Count == 0)
            {
                return null;
            }
            else
            {
                return objs[0];
            }
        }


        public static T Create<T>(string name, string path = "Assets/", bool preventOverride = true) where T : ScriptableObject
        {
            string fullpath = path + name + ".asset";
            if (preventOverride)
            {
                fullpath = PathUtils.PreventOverwrite(fullpath);
                // fullpath = fullpath.Replace(".asset", "");
                // fullpath = fullpath + ".asset";
            }
            T asset = ScriptableObject.CreateInstance<T>();
            asset.name = name;
            PathUtils.CreateExportDirectory(fullpath);
            AssetDatabase.CreateAsset(asset, fullpath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Asset " + name + " created at " + fullpath);
            return asset;
        }

        public static T GetFirstOrCreate<T>(string name, string path = "Assets/") where T : ScriptableObject
        {
            T asset = GetFirst<T>();
            if (asset == null)
            {
                asset = Create<T>(name, path);
            }
            return asset;
        }


        public static T GetOrCreate<T>(string name, string path = "Assets/") where T : ScriptableObject
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path + name + ".asset");
            if (asset == null)
            {
                asset = Create<T>(name, path);
            }
            return asset;
        }

        public static string FindAssetPath(string name)
        {
            string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(name);
            string[] assets = AssetDatabase.FindAssets(nameWithoutExtension);
            for (int i = 0; i < assets.Length; ++i)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[i]);
                if (path.Contains(name))
                {
                    return path;
                }
            }
            return "";
        }

        public static string FindAssetDirectory(string name)
        {
            string path = FindAssetPath(name);
            return System.IO.Path.GetDirectoryName(path);
        }

    }
}



#endif
