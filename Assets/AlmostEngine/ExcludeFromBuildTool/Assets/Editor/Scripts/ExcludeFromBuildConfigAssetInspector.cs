

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;


namespace AlmostEngine.ExcludeFromBuildTool
{
    [CustomEditor(typeof(ExcludeFromBuildConfigAsset))]
    public class ExcludeFromBuildConfigAssetInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Exclude"))
            {
                ExcludeFromBuild.Exclude(true);
            }
            if (GUILayout.Button("Restore"))
            {
                ExcludeFromBuild.Exclude(false);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(ExcludeFromBuildConfigAssetInspector.VERSION, EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.LabelField(ExcludeFromBuildConfigAssetInspector.AUTHOR, EditorStyles.centeredGreyMiniLabel);

        }

        public static string VERSION = "Exclude From Build Tool v1.0.0";
        public static string AUTHOR = "(c)Arnaud Emilien - support@wildmagegames.com";

    }
}
