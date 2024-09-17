using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.SimpleLocalization
{

	[ExecuteInEditMode]
	[RequireComponent (typeof(Text))]
	public class SimpleTextLocalizer : ISimpleLocalizer
	{
		#region Data

		[HideInInspector]
		public string m_OriginalText = "";

		[System.Serializable]
		public class Localization :  SerializableDictionary<string, string>
		{
		}

		public Localization m_Localisations = new  Localization ();

		#endregion



		Text m_Text;


		void OnEnable ()
		{
			if (m_Text == null) {
				m_Text = GetComponent<Text> ();
			}
			OnLanguageChanged (SimpleLocalizationLanguagesAsset.m_CurrentLanguageID);
		}

		public override void OnLanguageChanged (string ID)
		{
			if (m_Localisations.ContainsKey (ID) && m_Localisations [ID] != "") {
				m_Text.text = m_Localisations [ID];
			} else {
				Restore ();
			}			
		}

		public override void Save ()
		{
			m_OriginalText = m_Text.text;
		}

		public override void Restore ()
		{
			if (m_Text == null || m_OriginalText == "")
				return;
			m_Text.text = m_OriginalText;
		}

	}
}