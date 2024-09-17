using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using AlmostEngine.Screenshot;

namespace AlmostEngine.Examples
{
    public class CaptureScreenshotExample : MonoBehaviour
    {
        public int m_Width = 800;
        public int m_Height = 600;
        public int m_Scale = 1;
        public string m_FullpathA = "";
        public string m_FullpathB = "";
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
        public UnityEngine.InputSystem.Key m_ShortcutA = UnityEngine.InputSystem.Key.None;
        public UnityEngine.InputSystem.Key m_ShortcutB = UnityEngine.InputSystem.Key.None;
#else
		public KeyCode m_ShortcutA = KeyCode.F6;
		public KeyCode m_ShortcutB = KeyCode.F7;
#endif

        void Update()
        {

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            if (m_ShortcutA != UnityEngine.InputSystem.Key.None && Keyboard.current[m_ShortcutA].isPressed)
#else
			if (Input.GetKeyDown(m_ShortcutA))
#endif
            {
                // Capture the current screen at its current resolution, including UI
                SimpleScreenshotCapture.CaptureScreenToFile(m_FullpathA);
            }
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
            if (m_ShortcutB != UnityEngine.InputSystem.Key.None && Keyboard.current[m_ShortcutB].isPressed)
#else
			if (Input.GetKeyDown(m_ShortcutB))
#endif
            {
                // Capture the screen at a custom resolution using render to texture.
                // You must specify the list of cameras to be used in that mode.
                // Here we use Camera.main, the first scene camera tagged as "MainCamera"
                SimpleScreenshotCapture.CaptureCameraToFile(m_FullpathB, m_Width, m_Height, Camera.main, TextureExporter.ImageFileFormat.JPG, 70, 8);
            }
        }
    }
}

