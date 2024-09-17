#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AlmostEngine
{
	public static class ScriptableObjectUtils
	{
		public static T CreateAsset<T> (string name, string relativePath = "Resources") where T : ScriptableObject
		{
			string fullpath = Application.dataPath + "/" + relativePath;
			if (!Directory.Exists (fullpath)) {
				Directory.CreateDirectory (fullpath);
			}

			string fullname = "Assets/" + relativePath + "/" + name + ".asset";

			T asset = ScriptableObject.CreateInstance<T> ();
			AssetDatabase.CreateAsset (asset, fullname);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			Debug.Log ("Asset created at " + fullname);

			return asset;
		}

	}
}

#endif