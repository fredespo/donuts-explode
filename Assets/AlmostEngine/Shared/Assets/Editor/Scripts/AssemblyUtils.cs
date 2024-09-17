using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace AlmostEngine
{
    public static class AssemblyUtils
    {

        public static bool HasReference(string assemblyName, string assemblyRefName)
        {
            var assemblyFilePath = FindAsmdef(assemblyName);
            if (assemblyFilePath != "")
            {
                if (System.IO.File.ReadAllText(assemblyFilePath).Contains(assemblyRefName))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ToggleAssemblyReferenceFromAssemblyName(string assemblyName, string assemblyRefName, bool enable)
        {
            var assemblyPath = FindAsmdef(assemblyName);
            if (assemblyPath != "")
            {
                ToggleAssemblyReference(assemblyPath, assemblyRefName, enable);
            }
        }

        public static void ToggleAssemblyReferenceFromAssemblyNameALL(string assemblyNameContains, string assemblyRefName, bool enable)
        {
            var assemblies = FindAllAsmdefs(assemblyNameContains);
            foreach (var assemblyPath in assemblies)
            {
                ToggleAssemblyReference(assemblyPath, assemblyRefName, enable);
            }
        }


        public static void ToggleAssemblyReference(string assemblyFilePath, string assemblyRefName, bool enable)
        {
            if (!System.IO.File.Exists(assemblyFilePath))
            {
                Debug.LogError("Impossible to edit the assembly file, file doesn't exist: " + assemblyFilePath);
                return;
            }
            var file = System.IO.File.ReadAllText(assemblyFilePath);
            var assemblyName = System.IO.Path.GetFileNameWithoutExtension(assemblyFilePath);
            var refName = System.IO.Path.GetFileNameWithoutExtension(assemblyRefName);
            if (enable && !file.Contains(refName))
            {
                file = file.Replace("\"references\": [", "\"references\": [" + "\"" + refName + "\",");
                file = file.Replace(",]", "]");
                Debug.Log("Adding assembly reference " + refName + " to " + assemblyName + "\n" + file);
                System.IO.File.WriteAllText(assemblyFilePath, file);
                AssetDatabase.Refresh();
            }
            else if (!enable && file.Contains(refName))
            {
                file = file.Replace("\"" + refName + "\",", "");
                file = file.Replace("\"" + refName + "\"", "");
                Debug.Log("Removing assembly reference " + refName + " from " + assemblyName + "\n" + file);
                System.IO.File.WriteAllText(assemblyFilePath, file);
                AssetDatabase.Refresh();
            }
        }

        public static string FindAsmdef(string name)
        {
            var guids = AssetDatabase.FindAssets("t:asmdef").ToList<string>();
            foreach (var guid in guids)
            {
                // AssetDatabase
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asmname = System.IO.Path.GetFileNameWithoutExtension(path);
                if (asmname == name)
                {
                    // Debug.Log("Found asmdef guid " + guid + " path " + path + " name " + asmname);
                    return path;
                }
            }
            return "";
        }

        public static List<string> FindAllAsmdefs(string name)
        {
            var guids = AssetDatabase.FindAssets("t:asmdef").ToList<string>();
            var files = new List<string>();
            foreach (var guid in guids)
            {
                // AssetDatabase
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.Contains(name))
                {
                    // Debug.Log("Found asmdef guid " + guid + " path " + path);
                    files.Add(path);
                }
            }
            return files;
        }
    }
}
