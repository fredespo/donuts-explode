using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot.Extra
{
    public class RawImageDisplayer : MonoBehaviour
    {
        public string m_FileName;
        public RawImage m_RawImage;
        float m_DefaultSize;
        float m_DefaultRatio;
        public enum RatioScaleMode
        {
            WIDTH,
            HEIGHT
        };
        public bool m_AdaptRatio = true;
        public RatioScaleMode m_RatioScaleMode;

        void Awake()
        {
            if (m_RawImage == null)
            {
                m_RawImage = GetComponent<RawImage>();
            }
            if (m_RatioScaleMode == RatioScaleMode.WIDTH)
            {
                m_DefaultSize = GetComponent<RectTransform>().rect.width;
            }
            else
            {
                m_DefaultSize = GetComponent<RectTransform>().rect.height;
            }
            m_DefaultRatio = GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.height;
            if (m_FileName != "")
            {
                Load();
            }
        }

        public void SetFilename(string filename)
        {
            m_FileName = filename;
        }

        public void Load()
        {
            if (m_RawImage == null)
                return;

            // Load the picture from file
            m_RawImage.texture = TextureExporter.LoadFromFile(m_FileName);

            // Scale the texture to fit its ratio
            if (m_AdaptRatio)
            {
                float ratio = (float)m_RawImage.texture.width / (float)m_RawImage.texture.height;
                float scaleCoeff = ratio / m_DefaultRatio;
                if (m_RatioScaleMode == RatioScaleMode.WIDTH)
                {
                    m_RawImage.transform.localScale = new Vector3(1f, 1f / scaleCoeff, 1f);
                }
                else
                {
                    m_RawImage.transform.localScale = new Vector3(scaleCoeff, 1f, 1f);
                }
            }
        }
    }
}