using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine.Screenshot
{
    public class UltimateScreenshotCreator
    {
        public static string VERSION = "Ultimate Screenshot Creator v1.11.4";
        public static string AUTHOR = "(c)Arnaud Emilien - support@wildmagegames.com";

#if UNITY_EDITOR
        public static void About()
        {
            EditorUtility.DisplayDialog("About", VERSION + "\n" + AUTHOR, "Close");
        }
#endif
    }
}

