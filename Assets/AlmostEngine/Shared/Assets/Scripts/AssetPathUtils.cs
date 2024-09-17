#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace AlmostEngine
{

	public static class AssetPathUtils
	{

	    public static string GetPath(Object obj)
	    {
	        string path = AssetDatabase.GetAssetPath(obj);
	        path = path.Substring(0, path.LastIndexOf("/") + 1);
	        return path;
	    }

	    public static bool InPaths(Object obj, List<string> paths)
	    {
	        return InPaths(AssetDatabase.GetAssetPath(obj), paths);
	    }
	    public static bool InPaths(string objPath, List<string> paths)
	    {
	        if (paths == null || paths.Count == 0)
	            return true;

	        foreach (var path in paths)
	        {
	            if (objPath.Contains(path))
	                return true;
	        }
	        return false;
	    }
	}
}

#endif
