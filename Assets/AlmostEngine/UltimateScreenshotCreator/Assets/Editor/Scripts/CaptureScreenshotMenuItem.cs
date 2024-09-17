//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//
//namespace AlmostEngine.Screenshot
//{
//		/// <summary>
//		/// Menu item for capturing a screenshot using the ScreenshotWindow.
//		/// Customize the MenuItem symbols "#c" or "_F12" to set the hotkeys you want, depending on your Unity version.
//		/// To create a hotkey you can use the following special characters: % (ctrl on Windows, cmd on macOS), # (shift), & (alt)
//		/// Examples: 
//		/// C " _c"
//		/// shift+C " #c"
//		/// alt+C " &c"
//		/// F12 " _F12"
//		/// Note that F1..12 hotkeys do not work on Unity 5.0 to 5.2
//		/// For further details, please refer thttps://docs.unity3d.com/ScriptReference/MenuItem.html
//		/// </summary>
//		public class CaptureMenuItem
//		{
//				#if UNITY_5_3_OR_NEWER
//				// For unity 5.3 and later, CUSTOMIZE THIS
//				[MenuItem ("Tools/Screenshot/Capture _F12")]				
//				#else
//				// For unity 5.0 to 5.2, CUSTOMIZE THIS
//				[MenuItem ("Tools/Screenshot/Capture #c")]
//				#endif
//				static void Capture ()
//				{
//						if (!ScreenshotWindow.IsOpen ()) {
//								ScreenshotWindow.Init ();
//						}
//						ScreenshotWindow.m_Window.Capture ();
//				}
//		}
//}