using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlmostEngine.Screenshot.Extra
{
	/// <summary>
	/// The Screenshot gallery script is an abstract class to be used to create screenshots galleries.
	/// </summary>
	[RequireComponent (typeof(Canvas))]
	public abstract class ScreenshotGallery : MonoBehaviour
	{
		[Tooltip ("The path on the device from which the screenhots will be loaded. " +
		"To automatically use the same path than your Screenshot Manager export path, add a SetScreenshotManagerFolderPath component to that object: a path will be automatically set at startup.")]
		public string m_ScreenshotFolderPath = "";
		// public ScreenshotNameParser.DestinationFolder m_DestinationFolder = ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER;

		public enum DisplayOrder{ALPHABECICAL_ASCENDING,
		ALPHABECICAL_DECENDING,
		CREATION_DATE_ASCENDING,
		CREATION_DATE_DECENDING,
		};
		public DisplayOrder m_DisplayOrder;	

		[Tooltip ("The greybox object to use when an image is selected.")]
		public GreyboxCanvas m_GreyBox;

		[Tooltip ("The object to use as prefab to instantiate the gallery image object. Note that this object must have a RawImage component.")]
		public GameObject m_ImageItemPrefab;

		//	[HideInInspector]
		public List<TextureExporter.ImageFile> m_ImageFiles = new List<TextureExporter.ImageFile> ();

		[HideInInspector]
		public List<GameObject> m_ImageInstances = new List<GameObject> ();	

		#region Gallery management

		public virtual void Show ()
		{
			this.gameObject.SetActive (true);
			LoadImageFiles ();
			UpdateGallery ();
		}

		public virtual void UpdateGallery ()
		{
			// We can not update just after the gameobject is enabled because the UI rect transform values are invalids.
			StartCoroutine (DelayedUpdate ());
		}

		public IEnumerator DelayedUpdate ()
		{
			yield return new WaitForEndOfFrame ();
			DoGalleryUpdate ();
		}

		public virtual void LoadImageFiles ()
		{
			m_ImageFiles.Clear ();
			// string path = ScreenshotNameParser.ParsePath (m_DestinationFolder, m_ScreenshotFolderPath);
			if (!string.IsNullOrEmpty (m_ScreenshotFolderPath)) {
				m_ImageFiles = TextureExporter.LoadFromPath (m_ScreenshotFolderPath);
			}

			// Sort
			switch(m_DisplayOrder) {
				case DisplayOrder.ALPHABECICAL_ASCENDING:
				m_ImageFiles = m_ImageFiles.OrderBy(x=>x.m_Fullname).ToList();
				break;
				case DisplayOrder.ALPHABECICAL_DECENDING:
				m_ImageFiles = m_ImageFiles.OrderByDescending(x=>x.m_Fullname).ToList();
				break;
				case DisplayOrder.CREATION_DATE_ASCENDING:
				m_ImageFiles = m_ImageFiles.OrderBy(x=>x.m_CreationDate).ToList();
				break;
				case DisplayOrder.CREATION_DATE_DECENDING:
				m_ImageFiles = m_ImageFiles.OrderByDescending(x=>x.m_CreationDate).ToList();
				break;
			}
		}

		public virtual void Clear ()
		{
			foreach (GameObject go in m_ImageInstances) {
				GameObject.Destroy (go);
			}
			m_ImageInstances.Clear ();
		}

		public virtual GameObject InstantiateImageObject (TextureExporter.ImageFile image, int i, Transform parent)
		{
			// Instantiage prefab
			GameObject instance = GameObject.Instantiate (m_ImageItemPrefab);

			// Set the go parent
			instance.transform.SetParent (parent);
			instance.transform.localScale = Vector3.one;

			// Set texture
			RawImage img = instance.GetComponentInChildren<RawImage> ();
			if (img == null) {
				Debug.LogError ("Can not find the RawImage component in the gallery image. Be sure the canvas game object is enabled before calling this method.");
			}
			img.texture = image.m_Texture;

			// Add button callback
			Button btn = instance.GetComponentInChildren<Button> ();
			int index = i;
			btn.onClick.AddListener (() => OnSelectImageCallback (index));

			// Add image to list
			m_ImageInstances.Add (instance);

			return instance;
		}

		/// <summary>
		/// Updates the gallery. The gallery canvas game object must be enabled for this method to work properly.
		/// </summary>
		public abstract void DoGalleryUpdate ();

		public virtual void RemoveImage (int index)
		{
			if (File.Exists (m_ImageFiles [index].m_Fullname)) {
				File.Delete (m_ImageFiles [index].m_Fullname);
			}
			m_ImageFiles.RemoveAt (index);
		}

		#endregion

		#region Callbacks

		public virtual void OnSelectImageCallback (int id)
		{
			if (m_GreyBox != null) {
				this.gameObject.SetActive (false);
				m_GreyBox.gameObject.SetActive (true);
				m_GreyBox.SetImage (this, id);
			}
		}

		#endregion

	}

}
