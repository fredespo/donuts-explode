using UnityEngine;
using UnityEngine.UI;

namespace BinaryCharm.UI {

    [RequireComponent(typeof(Image))]
    public class SpinnerBhv : MonoBehaviour {

        [SerializeField]
        private Sprite[] m_rSpinnerFrames;

        private const float fFRAME_CHANGE_SEC = 0.1f;

        private Image m_rSpinnerImg;
        private int m_iFrameId;
        private float m_fLastFrameChangeTimestamp;

        void Awake() {
            m_rSpinnerImg = GetComponent<Image>();
            m_iFrameId = 0;
            m_fLastFrameChangeTimestamp = Time.time;
        }

        void Update() {
            if (Time.time > m_fLastFrameChangeTimestamp + fFRAME_CHANGE_SEC) {
                m_fLastFrameChangeTimestamp = Time.time;
                m_iFrameId = (m_iFrameId + 1) % m_rSpinnerFrames.Length;
                m_rSpinnerImg.sprite = m_rSpinnerFrames[m_iFrameId];
            }
        }

    }

}