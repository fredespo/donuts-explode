using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot.Extra
{
    /// <summary>
    /// The Greybox class is used to display selected screenshots in a specific windows, with some action buttons.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class GreyboxCanvas : MonoBehaviour
    {

        public RectTransform m_ImageContainer;
        public RectTransform m_PreviousButton;
        public RectTransform m_NextButton;
        public RectTransform m_CloseButton;
        public RectTransform m_ShareButton;
        public Text m_PageText;
        public Text m_FileName;

        ScreenshotGallery m_Gallery;
        int m_CurrentImageId;

        public void SetImage(ScreenshotGallery gallery, int i)
        {
            m_Gallery = gallery;
            m_CurrentImageId = i;

            UpdateUI();
        }

        public TextureExporter.ImageFile GetCurrentImage()
        {
            if (m_CurrentImageId < 0 || m_Gallery.m_ImageFiles.Count < m_CurrentImageId)
            {
                Debug.LogError("Error getting the image, invalid ID");
            }
            return m_Gallery.m_ImageFiles[m_CurrentImageId];
        }

        public Texture2D GetCurrentImageTexture()
        {
            var image = GetCurrentImage();
            if (image == null)
            {
                return null;
            }
            return image.m_Texture;
        }

        public string GetCurrentImagePath()
        {
            var image = GetCurrentImage();
            if (image == null)
            {
                return "";
            }
            return image.m_Fullname;
        }

        public virtual void UpdateUI()
        {
            // We can not update just after the gameobject is enabled because the UI rect transform values are invalids.
            StartCoroutine(DelayedUpdate());
        }

        public IEnumerator DelayedUpdate()
        {
            yield return new WaitForEndOfFrame();
            DoUpdate();
        }

        public virtual void DoUpdate()
        {
            TextureExporter.ImageFile imageFile = GetCurrentImage();
            if (imageFile == null)
            {
                return;
            }

            // Update the texture image
            RawImage img = m_ImageContainer.GetComponentInChildren<RawImage>();
            img.texture = imageFile.m_Texture;

            // Scale the texture to fit its parent size
            float parentRatio = m_ImageContainer.rect.width / m_ImageContainer.rect.height;
            float ratio = (float)imageFile.m_Texture.width / (float)imageFile.m_Texture.height;
            float scaleCoeff = ratio / parentRatio;
            if (scaleCoeff >= 1f)
            {
                img.GetComponentInChildren<RawImage>().transform.localScale = new Vector3(1f, 1f / scaleCoeff, 1f);
            }
            else
            {
                img.GetComponentInChildren<RawImage>().transform.localScale = new Vector3(scaleCoeff, 1f, 1f);
            }

            // Update navigation buttons
            if (m_PreviousButton != null)
            {
                if (m_CurrentImageId == 0)
                {
                    m_PreviousButton.gameObject.SetActive(false);
                }
                else
                {
                    m_PreviousButton.gameObject.SetActive(true);
                }
            }
            if (m_NextButton != null)
            {
                if (m_CurrentImageId >= m_Gallery.m_ImageFiles.Count - 1)
                {
                    m_NextButton.gameObject.SetActive(false);
                }
                else
                {
                    m_NextButton.gameObject.SetActive(true);
                }
            }

            // Update share button
            if (m_ShareButton != null)
            {
#if !USC_EXCLUDE_SHARE
                m_ShareButton.gameObject.SetActive(ShareUtils.CanShare());
#endif
            }

            // Update texts
            m_FileName.text = imageFile.m_Name;
            m_PageText.text = (m_CurrentImageId + 1).ToString() + "/" + m_Gallery.m_ImageFiles.Count;
        }

        public virtual string GetShareTitle()
        {
            return GetCurrentImage().m_Name;
        }

        public virtual string GetShareText()
        {
            return GetCurrentImage().m_Name + "\n" + GetCurrentImage().m_CreationDate.ToString();
        }

        #region Callbacks

        public virtual void NextPageCallback()
        {
            m_CurrentImageId++;
            UpdateUI();
        }

        public virtual void PreviousPageCallback()
        {
            m_CurrentImageId--;
            UpdateUI();
        }

        public virtual void CloseCallback()
        {
            this.gameObject.SetActive(false);
            m_Gallery.gameObject.SetActive(true);
            m_Gallery.UpdateGallery();
        }

        public virtual void RemoveCallback()
        {
            this.gameObject.SetActive(false);
            m_Gallery.gameObject.SetActive(true);
            m_Gallery.RemoveImage(m_CurrentImageId);
            m_Gallery.UpdateGallery();
            m_CurrentImageId = -1;
        }

        public virtual void ShareCallback()
        {
#if !USC_EXCLUDE_SHARE
            ShareUtils.ShareImage(GetCurrentImageTexture(), GetCurrentImage().m_Name, GetShareTitle(), GetShareTitle(), GetShareText());
#endif
        }

        #endregion

    }

}
