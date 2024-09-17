using UnityEngine;
using System.Collections.Generic;

namespace AlmostEngine.SimpleLocalization
{
	#if UNITY_2017_1_OR_NEWER
	[CreateAssetMenu (fileName = "LocalizationLanguages", menuName = "AlmostEngine/Simple Localization/LocalizationLanguages")]
	#endif
	public class SimpleLocalizationLanguagesAsset : ScriptableObject
	{
		public string m_DefaultLanguage = "eng";
		public List<string> m_Languages = new List<string> ();


		#region Static

		public static string m_CurrentLanguageID = "";

		public static void SetLanguage (string id)
		{
			m_CurrentLanguageID = id;
			ISimpleLocalizer[] localizers = GameObject.FindObjectsOfType<ISimpleLocalizer> ();
			foreach (var l in localizers) {
				l.OnLanguageChanged (id);
			}
		}

		#endregion
	}
}

