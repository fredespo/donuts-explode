using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using AlmostEngine.SimpleLocalization;

namespace AlmostEngine.Screenshot.Extra
{
	public class ChangeLanguageProcess : ScreenshotProcess
	{
		public string m_LanguageID;

		public override void Process (ScreenshotResolution res)
		{
			Debug.Log ("Change language process! " + m_LanguageID);
			SimpleLocalizationLanguagesAsset.SetLanguage (m_LanguageID);

		}
	}
}

