using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace AlmostEngine
{
    public static class IconsUtils
    {
        public static GUIContent TryGetIcon(string name)
        {
            GUIContent icon = null;
            Debug.unityLogger.logEnabled = false;
            if (!string.IsNullOrEmpty(name))
            {
                icon = EditorGUIUtility.IconContent(name);
            }
            Debug.unityLogger.logEnabled = true;
            if (icon == null || icon.image == null)
            {
                // If icon does not exist, return empty text gui content
                return new GUIContent("");
            }
            else
            {
                return icon;
            }
        }
    }
}