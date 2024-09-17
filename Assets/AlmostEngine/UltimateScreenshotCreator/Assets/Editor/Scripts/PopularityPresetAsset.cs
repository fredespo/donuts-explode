using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AlmostEngine.Screenshot
{
#if UNITY_EDITOR && UNITY_2017_1_OR_NEWER
    [CreateAssetMenu(fileName = "Custom popularity", menuName = "AlmostEngine/Ultimate Screenshot Creator/Popularity Preset")]
#endif
    [System.Serializable]
    public class PopularityPresetAsset : ScriptableObject
    {
#if UNITY_EDITOR && !UNITY_2017_1_OR_NEWER
		[MenuItem ("Tools/Ultimate Screenshot Creator/Create Popularity Preset")]
		public static void CreateAsset ()
		{
			ProjectWindowUtil.ShowCreatedAsset (ScriptableObjectUtils.CreateAsset<PopularityPresetAsset> ("Custom popularity", "Presets/Popularity"));
		}
#endif

        [System.Serializable]
        public class Stat
        {
            public ScreenshotResolutionAsset m_Resolution;
            public float m_Frequency;
        }

        public enum Type { Percent, Units };
        public Type m_Type = Type.Percent;

        public List<Stat> m_Stats = new List<Stat>();

        public void Sort()
        {
            m_Stats.Sort((a, b) => b.m_Frequency.CompareTo(a.m_Frequency));
        }

        public bool Contains(ScreenshotResolutionAsset preset)
        {
            return m_Stats.Find(x => x.m_Resolution == preset) != null;
        }

        public float GetPopularity(ScreenshotResolutionAsset preset)
        {
            var s = m_Stats.Find(x => x.m_Resolution == preset);
            if (s != null)
            {
                return s.m_Frequency;
            }
            return -1f;
        }
    }
}

