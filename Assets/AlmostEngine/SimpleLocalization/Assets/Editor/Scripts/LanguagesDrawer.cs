using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace AlmostEngine.SimpleLocalization
{
	public class LanguagesDrawer
	{
		public static void DrawGUI (SimpleLocalizationLanguagesAsset id)
		{
			// GENERAL
			EditorGUILayout.Separator ();
			EditorGUILayout.Separator ();
			EditorGUILayout.LabelField ("Global localization settings", EditorStyles.boldLabel);
			SerializedObject asset = new SerializedObject (id);
			asset.Update ();
			EditorGUILayout.PropertyField (asset.FindProperty ("m_DefaultLanguage"));
			EditorGUILayout.PropertyField (asset.FindProperty ("m_Languages"), true);
			asset.ApplyModifiedProperties ();

			// Current
			EditorGUILayout.HelpBox ("Current active language: " + (SimpleLocalizationLanguagesAsset.m_CurrentLanguageID == "" ? "default (" + id.m_DefaultLanguage + ")" : SimpleLocalizationLanguagesAsset.m_CurrentLanguageID), MessageType.Info);
		}
	}
}

