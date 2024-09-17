using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
	/// <summary>
	/// Use this script to display a validation canvas before to export the screenshot textures.
	/// </summary>
	public class ValidationCanvas : MonoBehaviour
	{

		public ScreenshotManager m_ScreenshotManager;
		public Canvas m_Canvas;
		public RectTransform m_ImageContainer;
		public RawImage m_Texture;


		/// <summary>
		/// Call this method to start a screenshot capture process and display the validation canvas when the capture is completed.
		/// </summary>
		public void Capture ()
		{
			if (m_ScreenshotManager == null) {
				m_ScreenshotManager = GameObject.FindObjectOfType<ScreenshotManager> ();
			}

			// Start listening to end capture event
			ScreenshotManager.onCaptureEndDelegate += OnCaptureEndDelegate;

			// Call update to only capture the texture without exporting
			m_ScreenshotManager.UpdateAll ();
		}

		#region Event callbacks

		public void OnCaptureEndDelegate ()
		{
			// Stop listening the callback
			ScreenshotManager.onCaptureEndDelegate -= OnCaptureEndDelegate;

			// Update the texture image
			m_Texture.texture = m_ScreenshotManager.GetLastScreenshotTexture();

			// Scale the texture to fit its parent size
			m_Texture.SetNativeSize ();
			float scaleCoeff = m_ImageContainer.rect.height / m_Texture.texture.height;
			m_Texture.transform.localScale = new Vector3 (scaleCoeff, scaleCoeff, scaleCoeff);

			// Show canvas
			this.gameObject.SetActive (true);
			m_Canvas.enabled = true;
		}

		#endregion

		#region UI callbacks

		public void OnDiscardCallback ()
		{
			// Hide canvas
			this.gameObject.SetActive (false);
			m_Canvas.enabled = false;
		}

		public void OnSaveCallback ()
		{
			// Export the screenshots to files
			m_ScreenshotManager.ExportAllToFiles ();

			// Hide canvas
			this.gameObject.SetActive (false);
			m_Canvas.enabled = false;
		}

		public void OnShareCallback ()
		{
			m_ScreenshotManager.ShareAll ();

			// Hide canvas
			this.gameObject.SetActive (false);
			m_Canvas.enabled = false;
		}

		#endregion
	}
}
