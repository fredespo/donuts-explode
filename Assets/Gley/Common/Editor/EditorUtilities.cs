using UnityEditor;
using UnityEngine;

namespace Gley.Common
{
    public class EditorUtilities
    {
        /// <summary>
        /// Create a folder at path recursively
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string[] folders = path.Split('/');
                string tempPath = "";
                for (int i = 0; i < folders.Length - 1; i++)
                {
                    tempPath += folders[i];
                    if (!AssetDatabase.IsValidFolder(tempPath + "/" + folders[i + 1]))
                    {
                        AssetDatabase.CreateFolder(tempPath, folders[i + 1]);
                        AssetDatabase.Refresh();
                    }
                    tempPath += "/";
                }
            }
        }


        public static string FindFolder(string folderName, string parent)
        {
            string result = null;
            var folders = AssetDatabase.GetSubFolders("Assets");
            foreach (var folder in folders)
            {
                result = Recursive(folder, folderName, parent);
                if (result != null)
                {
                    return result;
                }
            }
            return result;
        }

        public static T LoadOrCreateDataAsset<T>(string rootFolder, string path, string name) where T : ScriptableObject
        {
            string assetPath = ($"{rootFolder}/{path}/{name}.asset");
            T result = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (result == null)
            {
                T asset = ScriptableObject.CreateInstance<T>();
                CreateFolder($"{rootFolder}/{path}");
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            return result;
        }


        public static T CreateDataAsset<T>(T data, string rootFolder, string path, string name) where T : ScriptableObject
        {
            string assetPath = ($"{rootFolder}/{path}/{name}.asset");
            //T asset = ScriptableObject.CreateInstance<T>();
            CreateFolder($"{rootFolder}/{path}");
            AssetDatabase.CreateAsset(data, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        static string Recursive(string currentFolder, string folderToSearch, string parent)
        {
            if (currentFolder.EndsWith($"{parent}/{folderToSearch}"))
            {
                return currentFolder;
            }
            var folders = AssetDatabase.GetSubFolders(currentFolder);
            foreach (var fld in folders)
            {
                string result = Recursive(fld, folderToSearch, parent);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

    }
}
