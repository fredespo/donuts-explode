using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
    [System.Serializable]
    public class ScreenshotBatch
    {
        public bool m_Active = true;
        public string m_Name = "";

        public List<ScreenshotProcess> m_PreProcess = new List<ScreenshotProcess>();
        public List<ScreenshotProcess> m_PostProcess = new List<ScreenshotProcess>();


        [System.Serializable]
        public class ActiveItem
        {
            public bool m_Active = true;
            public string m_Name = "";
            [HideInInspector]
            public int m_Id = -1;
        }

        public bool m_OverrideActiveResolutions = false;
        public List<ActiveItem> m_ActiveResolutions = new List<ActiveItem>();

        public bool m_OverrideActiveComposer = false;
        public List<ActiveItem> m_ActiveCompositions = new List<ActiveItem>();



    }
}

