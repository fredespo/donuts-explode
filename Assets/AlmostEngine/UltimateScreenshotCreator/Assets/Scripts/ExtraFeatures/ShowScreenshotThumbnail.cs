using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;

namespace AlmostEngine.Screenshot.Extra
{
    /// <summary>
    /// Shows a screenshot thumbnail for a few seconds after the capture.
    /// </summary>
    public class ShowScreenshotThumbnail : MonoBehaviour
    {
        ScreenshotManager m_ScreenshotManager;
        public Canvas m_Canvas;
        public RectTransform m_ImageContainer;
        public RawImage m_Texture;

        public static UnityAction onThumbnailStartDelegate = () => { };
        public static UnityAction onThumbnailEndDelegate = () => { };

        public float m_DisplayDuration = 2f;

        void OnEnable()
        {
            ScreenshotManager.onCaptureEndDelegate += OnCaptureEndDelegate;
        }

        void OnDisable()
        {
            ScreenshotManager.onCaptureEndDelegate -= OnCaptureEndDelegate;
        }

        #region Event callbacks

        public void OnCaptureEndDelegate()
        {
            StartCoroutine(ShowCoroutine());
        }

        #endregion

        public IEnumerator ShowCoroutine()
        {
            m_ScreenshotManager = GameObject.FindObjectOfType<ScreenshotManager>();
            if (m_ScreenshotManager == null)
                yield break;

            // Show canvas
            m_Canvas.gameObject.SetActive(true);
            m_Canvas.enabled = true;
			onThumbnailStartDelegate();

            // Update the texture image
            m_Texture.texture = m_ScreenshotManager.GetLastScreenshotTexture();

            // Scale the texture to fit its parent size
            m_Texture.SetNativeSize();
            float scaleCoeff = m_ImageContainer.rect.height / m_Texture.texture.height;
            m_Texture.transform.localScale = new Vector3(scaleCoeff, scaleCoeff, scaleCoeff);

            // Duration
            yield return new WaitForSeconds(m_DisplayDuration);

            // Hide canvas
            m_Canvas.enabled = false;
			onThumbnailEndDelegate();
        }

    }
}
