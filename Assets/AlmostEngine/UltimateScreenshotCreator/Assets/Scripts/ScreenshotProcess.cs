using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
    public class ScreenshotProcess : MonoBehaviour
    {
		[HideInInspector]
		public ScreenshotBatch m_Batch;

        /// <summary>
        /// Override that method for a simple process method. 
        /// </summary>
        public virtual void Process(ScreenshotResolution res)
        {
        }

        /// <summary>
        /// Override that coroutine if you want to create a process requiring several frames.
        /// Note that in editor mode, if the game is not playing, you may have to force a gameview repaint with ForceGameViewRepaint() or the coroutine will be stuck.
        /// Time related coroutines will not work if the game is not playing. It is advised to have a fallback with ForceGameViewRepaint() or the coroutine will be stuck.
        /// </summary>
        public virtual IEnumerator ProcessCoroutine(ScreenshotResolution res)
        {
            ForceGameViewRepaint();
            yield return new WaitForEndOfFrame();
        }

        protected void ForceGameViewRepaint()
        {
#if UNITY_EDITOR
            // Dirty hack: we force a gameview repaint, to prevent the coroutine to stay locked.
            if (!Application.isPlaying)
            {
                GameViewUtils.GetGameView().Repaint();
            }
#endif
        }
    }
}

