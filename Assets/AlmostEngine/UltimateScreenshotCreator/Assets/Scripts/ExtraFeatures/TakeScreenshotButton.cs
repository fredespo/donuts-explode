using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.Screenshot.Extra
{
	/// <summary>
	/// Add this component to a button to take a screenshot when pressed.
	/// </summary>
	[RequireComponent (typeof(Button))]
	public class TakeScreenshotButton : MonoBehaviour
	{
		Button m_Button;
		ScreenshotManager m_ScreenshotManager;

		void Start ()
		{
			m_ScreenshotManager = GameObject.FindObjectOfType<ScreenshotManager> ();
			m_Button = GetComponent<Button> ();
			m_Button.onClick.AddListener (OnClickCallback);
		}

		void OnClickCallback ()
		{
			if (m_ScreenshotManager) {
				m_ScreenshotManager.Capture ();
			}
		}

	}
}