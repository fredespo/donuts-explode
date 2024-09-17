#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using Type = System.Type;

namespace AlmostEngine.Screenshot
{
	/// <summary>
	/// Layout utils is used to save and load editor layouts.
	/// Part of this code was inspired and adapted from http://answers.unity3d.com/questions/382973/programmatically-change-editor-layout.html
	/// </summary>
	public static class LayoutUtils
	{

		private static MethodInfo m_SaveWindowLayout;
		private static MethodInfo m_LoadWindowLayout;

		static LayoutUtils ()
		{
			Type tyWindowLayout = Type.GetType ("UnityEditor.WindowLayout,UnityEditor");
			Type tyEditorUtility = Type.GetType ("UnityEditor.EditorUtility,UnityEditor");
			Type tyInternalEditorUtility = Type.GetType ("UnityEditorInternal.InternalEditorUtility,UnityEditor");
			if (tyWindowLayout != null && tyEditorUtility != null && tyInternalEditorUtility != null) {


				m_SaveWindowLayout = tyWindowLayout.GetMethod ("SaveWindowLayout", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);		
				if (m_SaveWindowLayout == null) {
					Debug.LogError ("Failed to init Layout methodinfo SaveWindowLayout.");
				}
		
				m_LoadWindowLayout = tyWindowLayout.GetMethod ("LoadWindowLayout", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, new Type[] {
					typeof(string),
					typeof(bool)
				}, null);	
				if (m_LoadWindowLayout == null) {
					Debug.LogError ("Failed to init Layout methodinfo LoadWindowLayout.");
				}


			}
		}
	
		// Save current window layout to file. `assetPath` must be relative to project directory.
		public static void SaveLayoutToFile (string assetPath)
		{
			if (m_SaveWindowLayout != null) {
				string path = Path.Combine (Directory.GetCurrentDirectory (), assetPath);
				m_SaveWindowLayout.Invoke (null, new object[] { path });
			} else {
				Debug.LogError ("Failed to save Layout.");
			}
		}
	
		// Load window layout from file. `assetPath` must be relative to project directory.
		public static void LoadLayoutFromFile (string assetPath)
		{
			try {
				if (m_LoadWindowLayout != null) {
					string path = Path.Combine (Directory.GetCurrentDirectory (), assetPath);
					m_LoadWindowLayout.Invoke (null, new object[] { path, false });
				} else {
					Debug.LogError ("Failed to load Layout.");
				}
			} catch {
				Debug.LogError ("Failed to load Layout.");
			}
		}
	}
}

#endif