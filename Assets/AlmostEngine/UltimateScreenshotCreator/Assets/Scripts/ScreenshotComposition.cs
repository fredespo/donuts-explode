using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
		[System.Serializable]
		public class ScreenshotComposition{
			public bool m_Active = true;
			public string m_Name = "New composition";
			public ScreenshotComposer m_Composer;
		};

}