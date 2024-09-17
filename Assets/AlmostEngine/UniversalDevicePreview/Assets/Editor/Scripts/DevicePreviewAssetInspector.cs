using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AlmostEngine.Preview
{
	[CustomEditor (typeof(PreviewConfigAsset))]
	public class PreviewConfigAssetInspector : Editor
	{
		public override void OnInspectorGUI ()
		{
			EditorGUILayout.HelpBox ("This asset contains the settings used by the Universal Device Preview.", MessageType.Info);

			if (GUILayout.Button ("Open Settings")) {
				ResolutionSettingsWindow.Init ();
			}
		}
	}
}
