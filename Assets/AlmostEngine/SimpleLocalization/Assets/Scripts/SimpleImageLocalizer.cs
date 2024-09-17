using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.SimpleLocalization
{

	[ExecuteInEditMode]
	[RequireComponent (typeof(RawImage))]
	public class SimpleImageLocalizer : ISimpleLocalizer
	{
		#region Data

		[HideInInspector]
		public Texture m_OriginalTexture = null;

		[System.Serializable]
		public class Localization :  SerializableDictionary<string, Texture>
		{
		}

		public Localization m_Localisations = new  Localization ();

		#endregion



		RawImage m_Image;


		void OnEnable ()
		{
			if (m_Image == null) {
				m_Image = GetComponent<RawImage> ();
			}
			OnLanguageChanged (SimpleLocalizationLanguagesAsset.m_CurrentLanguageID);
		}

		public override void OnLanguageChanged (string ID)
		{
			if (m_Localisations.ContainsKey (ID) && m_Localisations [ID] != null) {
				m_Image.texture = m_Localisations [ID];
			} else {
				Restore ();
			}			
		}

		public override void Save ()
		{
			m_OriginalTexture = m_Image.texture;
		}

		public override void Restore ()
		{
			if (m_Image == null || m_OriginalTexture == null)
				return;
			m_Image.texture = m_OriginalTexture;
		}
	}
}