using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlmostEngine.Screenshot.Extra
{
	public class MessageCanvas : MonoBehaviour
	{
		public RectTransform m_MessagePanel;
		public GameObject m_MessagePrefab;
		public float m_DisplayTime = 5f;

		List<GameObject> m_Messages = new List<GameObject> ();

		void OnEnable ()
		{
			Clear ();

			ScreenshotManager.onResolutionExportSuccessDelegate -= ExportSuccessCallback;
			ScreenshotManager.onResolutionExportSuccessDelegate += ExportSuccessCallback;

			ScreenshotManager.onResolutionExportFailureDelegate -= ExportFailureCallback;
			ScreenshotManager.onResolutionExportFailureDelegate += ExportFailureCallback;
		}

		void OnDisable ()
		{
			ScreenshotManager.onResolutionExportSuccessDelegate -= ExportSuccessCallback;
			ScreenshotManager.onResolutionExportFailureDelegate -= ExportFailureCallback;
		}

		public void ExportSuccessCallback (ScreenshotResolution resolution)
		{
			DisplayMessage ("Screenshot created : " + resolution.m_FileName);
		}

		public void ExportFailureCallback (ScreenshotResolution resolution)
		{
			DisplayMessage ("FAILED to create : " + resolution.m_FileName);
		}

		void Clear ()
		{
			StopAllCoroutines ();
			foreach (GameObject o in m_Messages) {
				StartCoroutine (DestroyMessageCoroutine (o));
			}
		}

		public void DisplayMessage (string text)
		{
			GameObject message = (GameObject)GameObject.Instantiate (m_MessagePrefab);
			message.transform.SetParent (m_MessagePanel.transform);
			message.transform.localScale = Vector3.one;
			message.GetComponent<Text> ().text = text;
			m_Messages.Add (message);
			StartCoroutine (DestroyMessageCoroutine (message));
		}

		IEnumerator DestroyMessageCoroutine (GameObject message)
		{
			yield return new WaitForSeconds (m_DisplayTime);
			m_Messages.Remove (message);
			DestroyImmediate (message);
		}
	}
}