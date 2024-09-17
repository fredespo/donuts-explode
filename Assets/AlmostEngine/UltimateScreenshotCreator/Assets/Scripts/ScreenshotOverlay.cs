using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
	[System.Serializable]
	/// <summary>
	/// Screenshot overlay are Canvas that are rendered over the screenshots.
	/// </summary>
	public class ScreenshotOverlay
	{
		public Canvas m_Canvas;
		public bool m_Active = true;


		public class Settings
		{
			public bool m_Enabled;
			public bool m_GameObjectEnabled;

			public Settings (bool enabled, bool go)
			{
				m_Enabled = enabled;
				m_GameObjectEnabled = go;
			}
		};

		public Stack<Settings> m_SettingStack = new Stack<Settings> ();

		public ScreenshotOverlay ()
		{
		}

		public ScreenshotOverlay (Canvas canvas)
		{
			m_Canvas = canvas;
		}

		public void ApplySettings ()
		{
			if (m_Canvas == null)
				return;

			// Save current settings
			m_SettingStack.Push (new Settings (m_Canvas.enabled, m_Canvas.gameObject.activeSelf));

			if (!m_Canvas.transform.gameObject.activeInHierarchy) {
				// If object if prefab we create a clone instance
				m_Instance = GameObject.Instantiate (m_Canvas);
                m_Instance.hideFlags = HideFlags.DontSave;
				m_Instance.enabled = true;
				m_Instance.gameObject.SetActive (true);
				m_Instance.name = m_Instance.name + " - temporary instance, remove if still exists after capture process";
			} else {
				// Apply settings
				m_Canvas.enabled = m_Active;
				m_Canvas.gameObject.SetActive (m_Active);
			}
		}

		Canvas m_Instance;

		public void Disable ()
		{
			if (m_Canvas == null)
				return;
			
			// Save current settings
			m_SettingStack.Push (new Settings (m_Canvas.enabled, m_Canvas.gameObject.activeSelf));
			
			// Apply settings
			m_Canvas.enabled = false;
		}

		public void RestoreSettings ()
		{
			if (m_Canvas == null)
				return;

			if (m_SettingStack.Count <= 0)
				return;

			Settings s = m_SettingStack.Pop ();

			if (m_Instance != null) {
				// If the canvas was instantiated we destroy the instance
				GameObject.DestroyImmediate (m_Instance.gameObject);
				m_Instance = null;
			} else {
				// Restore the desstings
				m_Canvas.enabled = s.m_Enabled;
				m_Canvas.gameObject.SetActive (s.m_GameObjectEnabled);
			}
		}

	}
}