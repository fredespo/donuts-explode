using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AlmostEngine.Screenshot.Extra
{
	/// <summary>
	/// The Grid gallery script is a screenshot gallery example based on the GridLayoutGroup UI component.
	/// </summary>
	public class GridGalleryCanvas : ScreenshotGallery
	{
		#region UI references

		public GridLayoutGroup m_Grid;
		public RectTransform m_PreviousButton;
		public RectTransform m_NextButton;
		public Text m_PageText;

		#endregion

		[HideInInspector]
		public int m_CurrentPage = 0;

		public virtual int MaxPages ()
		{
			return  Mathf.CeilToInt ((float)m_ImageFiles.Count / (float)ImagesPerPage ());
		}

		public virtual int ImagesPerPage ()
		{
			int cols = Mathf.FloorToInt (m_Grid.GetComponent<RectTransform> ().rect.width / (m_Grid.cellSize.x + m_Grid.spacing.x));
			int lines = Mathf.FloorToInt (m_Grid.GetComponent<RectTransform> ().rect.height / (m_Grid.cellSize.y + m_Grid.spacing.y));
			return cols * lines;
		}

		public override void DoGalleryUpdate ()
		{
			if (m_CurrentPage >= MaxPages ()) {
				m_CurrentPage = MaxPages () - 1;
			}
			if (m_ImageFiles.Count > 0 && m_CurrentPage < 0) {
				m_CurrentPage = 0;
			}

			Clear ();

			// Generate an image object from each texture
			for (int j = 0; j < ImagesPerPage () && m_ImageFiles.Count > 0; ++j) {

				int i = m_CurrentPage * ImagesPerPage () + j;
				if (i >= m_ImageFiles.Count)
					break;

				// Create the object
				GameObject image = InstantiateImageObject (m_ImageFiles [i], i, m_Grid.transform);

				// Image scaling according to grid cell size
				float parentRatio = m_Grid.cellSize.x / m_Grid.cellSize.y;
				float ratio = (float)m_ImageFiles [i].m_Texture.width / (float)m_ImageFiles [i].m_Texture.height;
				float scaleCoeff = ratio / parentRatio;
				if (scaleCoeff >= 1f) {	
					image.GetComponentInChildren<RawImage> ().transform.localScale = new Vector3 (1f, 1f / scaleCoeff, 1f);
				} else {
					image.GetComponentInChildren<RawImage> ().transform.localScale = new Vector3 (scaleCoeff, 1f, 1f);
				}

			}

			// Set page buttons
			if (m_CurrentPage <= 0) {
				m_PreviousButton.gameObject.SetActive (false);
			} else {
				m_PreviousButton.gameObject.SetActive (true);
			}
			if (m_CurrentPage < MaxPages () - 1) {
				m_NextButton.gameObject.SetActive (true);
			} else {
				m_NextButton.gameObject.SetActive (false);
			}

			// Set page text
			m_PageText.text = (m_CurrentPage + 1).ToString () + "/" + MaxPages ().ToString ();
		}

		public virtual void NextPageCallback ()
		{
			m_CurrentPage++;
			UpdateGallery ();
		}

		public virtual void PreviousPageCallback ()
		{
			m_CurrentPage--;
			UpdateGallery ();
		}

		public virtual void CloseCallback ()
		{
			this.gameObject.SetActive (false);
		}
	}

}