using UnityEngine;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine.Screenshot
{
#if UNITY_EDITOR && UNITY_2017_1_OR_NEWER
    [CreateAssetMenu(fileName = "Custom preset", menuName = "AlmostEngine/Ultimate Screenshot Creator/Custom preset")]
#endif
    [System.Serializable]
    public class ScreenshotResolutionAsset : ScriptableObject
    {
        public ScreenshotResolution m_Resolution;

#if UNITY_EDITOR && !UNITY_2017_1_OR_NEWER
		[MenuItem ("Tools/Ultimate Screenshot Creator/Create Resolution Preset")]
		public static void CreateAsset ()
		{
			ProjectWindowUtil.ShowCreatedAsset (ScriptableObjectUtils.CreateAsset<ScreenshotResolutionAsset> ("Custom preset", "Presets/Custom"));
		}
#endif

        public override string ToString()
        {
            string s = name + "    -";
            if (m_Resolution.m_Platform.ToString() != "")
            {
                s += "    " + m_Resolution.m_Platform.ToString();
            }
            // if (m_Resolution.m_Category.ToString() != "")
            // {
            //     s += " - " + m_Resolution.m_Category.Replace("Devices/", "");
            // }
            s += "    " + m_Resolution.m_Width + "x" + m_Resolution.m_Height;
            if (m_Resolution.m_PPI > 0)
            {
                s += "    " + m_Resolution.m_PPI + " PPI";
            }
            return s;
        }

    }
}

