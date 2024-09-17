using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AlmostEngine.Screenshot
{
#if UNITY_EDITOR && UNITY_2017_1_OR_NEWER
    [CreateAssetMenu(fileName = "Custom collection", menuName = "AlmostEngine/Ultimate Screenshot Creator/Preset Collection")]
#endif
    [System.Serializable]
    public class PresetCollectionAsset : ScriptableObject
    {
#if UNITY_EDITOR && !UNITY_2017_1_OR_NEWER
		[MenuItem ("Tools/Ultimate Screenshot Creator/Create Preset Collection")]
		public static void CreateAsset ()
		{
			ProjectWindowUtil.ShowCreatedAsset (ScriptableObjectUtils.CreateAsset<PresetCollectionAsset> ("Custom collection", "Presets/Collections"));
		}
#endif

        public List<ScreenshotResolutionAsset> m_Presets = new List<ScreenshotResolutionAsset>();

    }
}

