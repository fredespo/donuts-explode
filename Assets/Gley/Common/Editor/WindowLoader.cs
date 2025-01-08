using System;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Gley.Common
{
    public class WindowLoader : EditorWindow
    {
        internal static T LoadWindow<T>(ISettingsWindowProperties windowProperties, out string rootFolder) where T : EditorWindow
        {
            rootFolder = GetRootFolder(windowProperties);

            StreamReader reader = new StreamReader($"{rootFolder}{windowProperties.VersionFilePath}");
            string longVersion = JsonUtility.FromJson<AssetVersion>(reader.ReadToEnd()).longVersion;
            T window = (T)GetWindow(typeof(T));
            window.titleContent = new GUIContent(windowProperties.WindowName + longVersion);
            window.minSize = new Vector2(windowProperties.MinWidth, windowProperties.MinHeight);
            window.Show();
            return (T)Convert.ChangeType(window, typeof(T));
        }

        internal static T LoadWindow<T>(ISettingsWindowProperties windowProperties, out string rootFolder,out string rootWithoutAssets) where T : EditorWindow
        {
            T result = LoadWindow<T>(windowProperties, out rootFolder);
            rootWithoutAssets = rootFolder.Substring(7, rootFolder.Length - 7);
            return result;
        }

        internal static string GetRootFolder(ISettingsWindowProperties windowProperties)
        {
            string rootFolder = EditorUtilities.FindFolder(windowProperties.FolderName, windowProperties.ParentFolder);
            if (rootFolder == null)
            {
                throw new Exception($"Folder Not Found: '{windowProperties.ParentFolder}/{windowProperties.FolderName}'");
            }
            return rootFolder;
        }

        internal static string GetRootFolder(ISettingsWindowProperties windowProperties, out string rootWithoutAssets)
        {
            string rootFolder = EditorUtilities.FindFolder(windowProperties.FolderName, windowProperties.ParentFolder);
            if (rootFolder == null)
            {
                throw new Exception($"Folder Not Found: '{windowProperties.ParentFolder}/{windowProperties.FolderName}'");
            }
            rootWithoutAssets = rootFolder.Substring(7, rootFolder.Length - 7);
            return rootFolder;
        }
    }
}
