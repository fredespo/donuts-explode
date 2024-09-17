using UnityEngine;

using System.Collections.Generic;

#if UNITY_EDITOR
using AlmostEngine.Screenshot;
#endif

namespace AlmostEngine.Example.Preview
{

    [ExecuteInEditMode]
    public class PlatformActivate : MonoBehaviour
    {
        public List<GameObject> m_Targets = new List<GameObject>();

        void Awake()
        {
            Check();

        }

        public virtual bool IsGoodPlatform()
        {
            return true;
        }

        public void Check()
        {
            SetEnable(IsGoodPlatform());
        }

        public void SetEnable(bool enable)
        {
            foreach (var target in m_Targets)
            {
#if UNITY_EDITOR
                m_WasActive[target] = target.activeSelf;
#endif
                target.SetActive(enable);
            }
        }


        #region In Editor Simulation

#if UNITY_EDITOR
        Dictionary<GameObject, bool> m_WasActive = new Dictionary<GameObject, bool>();

        void OnEnable()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate += OnResolutionUpdateStart;
            ScreenshotTaker.onResolutionUpdateEndDelegate += OnResolutionUpdateEnd;
        }

        void OnDisable()
        {
            ScreenshotTaker.onResolutionUpdateStartDelegate -= OnResolutionUpdateStart;
            ScreenshotTaker.onResolutionUpdateEndDelegate -= OnResolutionUpdateEnd;
        }

        void OnResolutionUpdateStart(ScreenshotResolution res)
        {
            Check();
        }

        void OnResolutionUpdateEnd(ScreenshotResolution res)
        {
            Restore();
        }

        public void Restore()
        {
            foreach (var target in m_Targets)
            {
                if (m_WasActive.ContainsKey(target))
                {
                    target.SetActive(m_WasActive[target]);
                }
            }
        }
#endif

        #endregion


    }
}
