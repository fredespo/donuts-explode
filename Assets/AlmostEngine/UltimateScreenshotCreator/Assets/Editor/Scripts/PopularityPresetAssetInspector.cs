using UnityEngine;
using UnityEditor;
using System.Collections;


namespace AlmostEngine.Screenshot
{
	[CustomEditor (typeof(PopularityPresetAsset))]
	public class PopularityPresetAssetInspector : Editor
	{
		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			if (GUILayout.Button ("Sort")) {
				((PopularityPresetAsset)target).Sort ();
				EditorUtility.SetDirty (target);
			}
		}
	}
}