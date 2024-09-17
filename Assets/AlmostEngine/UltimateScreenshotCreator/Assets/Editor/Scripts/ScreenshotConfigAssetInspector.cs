using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AlmostEngine.Screenshot
{
		[CustomEditor (typeof(ScreenshotConfigAsset))]
		public class ScreenshotConfigAssetInspector : Editor
		{
				public override void OnInspectorGUI ()
				{

						EditorGUILayout.HelpBox ("This asset contains the settings used by the ScreenshotWindow.", MessageType.Info);

						if (GUILayout.Button ("Open Screenshot Window")) {
								ScreenshotWindow.Init ();
						}
				}
		}
}