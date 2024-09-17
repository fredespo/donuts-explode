using UnityEngine;
using System.Collections;

#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
namespace AlmostEngine
{
		[System.Serializable]
		/// <summary>
		/// Hotkey are used to create keyboard combinaisons.
		/// </summary>
		public class HotKey
		{
				public bool m_Shift = false;
				public bool m_Control = false;
				public bool m_Alt = false;
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM
		public UnityEngine.InputSystem.Key m_NewInputKey = UnityEngine.InputSystem.Key.None;
#endif
				public KeyCode m_Key = KeyCode.None;

				public HotKey ()
				{
				}

				public HotKey (bool shift, bool control, bool alt, KeyCode key)
				{
						m_Shift = shift;
						m_Control = control;
						m_Alt = alt;
						m_Key = key;
				}

				/// <summary>
				/// Handles ingame hotkeys. Determines whether this hotkey is pressed. Only works when the application is playing.
				/// </summary>
				/// <returns><c>true</c> if this instance is pressed; otherwise, <c>false</c>.</returns>
				public bool IsPressed ()
				{
#if ENABLE_INPUT_SYSTEM && USC_INPUT_SYSTEM

            if (m_NewInputKey == UnityEngine.InputSystem.Key.None)
                return false;
			if ((!m_Shift || Keyboard.current.shiftKey.isPressed)
				&& (!m_Control || Keyboard.current.ctrlKey.isPressed)
				&& (!m_Alt || Keyboard.current.altKey.isPressed)
                && Keyboard.current[m_NewInputKey].wasPressedThisFrame)
            {
					return true;
			}
#else
						if (m_Key == KeyCode.None)
								return false;

						if ((!m_Shift || Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
						 && (!m_Control || Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
						 && (!m_Alt || Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt))
             && Input.GetKeyUp(m_Key))
            {
								return true;
						}
#endif
						return false;
				}

				#if UNITY_EDITOR
				/// <summary>
				/// Handles editor hotkeys. Determines whether the event e corresponds to the hotkey.
				/// </summary>
				/// <returns><c>true</c> if this instance is pressed; otherwise, <c>false</c>.</returns>
				/// <param name="e">E.</param>
				public bool IsPressed (Event e)
				{
						if (e == null)
								return false;

						if (m_Key == KeyCode.None)
								return false;
			
						if (e.type == EventType.KeyUp
						 && (!m_Shift || e.shift)
						 && (!m_Control || e.control)
						 && (!m_Alt || e.alt)
             && (e.keyCode == m_Key))
            {
								return true;
						}
						return false;
				}
				#endif

		}
}
