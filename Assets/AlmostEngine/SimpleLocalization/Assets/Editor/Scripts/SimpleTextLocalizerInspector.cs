using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace AlmostEngine.SimpleLocalization
{
	[CustomEditor (typeof(SimpleTextLocalizer))]
	public class SimpleTextLocalizerInspector : Editor
	{

		SimpleLocalizationLanguagesAsset m_IDs;

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.Separator ();

			if (m_IDs == null) {
				m_IDs = AssetUtils.GetFirstOrCreate<SimpleLocalizationLanguagesAsset>("LocalizationLanguages");
			}
			if (m_IDs == null)
				return;
			
			SimpleTextLocalizer obj = (SimpleTextLocalizer)target;


			// Default text
			if (SimpleLocalizationLanguagesAsset.m_CurrentLanguageID == "" && obj.GetComponent<Text> ().text != obj.m_OriginalText) {
				obj.Save ();
			}
			EditorGUILayout.LabelField ("Default (" + m_IDs.m_DefaultLanguage + "): " + obj.m_OriginalText);

			// Localizations
			if (m_IDs.m_Languages.Count == 0) {
				EditorGUILayout.HelpBox ("Languages list is empty. Please add languages in the global localization settings.", MessageType.Warning);
			}
			foreach (string id in m_IDs.m_Languages) {

				// Init localization value if needed
				if (!obj.m_Localisations.ContainsKey (id)) {

					obj.m_Localisations [id] = "";
					EditorUtility.SetDirty (obj);
				}


				// Localization edit field
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField (id, GUILayout.MaxWidth (50));
				string newLoc = EditorGUILayout.TextField (obj.m_Localisations [id]);
				EditorGUILayout.EndHorizontal ();

				// Set new localization if needed
				if (newLoc != obj.m_Localisations [id]) {
					obj.m_Localisations [id] = newLoc;
					EditorUtility.SetDirty (obj);
				}
			}



			// Global
			LanguagesDrawer.DrawGUI(m_IDs);


		}

	}
}