using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AlmostEngine.Preview
{
		/// <summary>
		/// Menu item for capturing a screenshot using the ScreenshotWindow.
		/// Customize the MenuItem symbols "#e" or "_F6" to set the hotkeys you want, depending on your Unity version.
		/// To create a hotkey you can use the following special characters: % (ctrl on Windows, cmd on macOS), # (shift), & (alt)
		/// Examples: 
		/// C " _c"
		/// shift+C " #c"
		/// alt+C " &c"
		/// F12 " _F12"
		/// Note that F1..12 hotkeys do not work on Unity 5.0 to 5.2
		/// For further details, please refer to https://docs.unity3d.com/ScriptReference/MenuItem.html
		/// </summary>
		public class ExportDevicePreviewMenuItem
		{
				#if UNITY_5_3_OR_NEWER
				// For unity 5.3 and later, CUSTOMIZE THIS
				[MenuItem ("Tools/Universal Device Preview/Export Preview(s) _F6")]								
				#else
				// For unity 5.0 to 5.2, CUSTOMIZE THIS
				[MenuItem ("Tools/Universal Device Preview/Export Preview(s) %#e")]
				#endif
				static void UpdateAll ()
				{
						if (ResolutionGalleryWindow.IsOpen ()) {
								ResolutionGalleryWindow.m_Window.Export ();
						} else if (ResolutionPreviewWindow.IsOpen ()) {
								ResolutionPreviewWindow.m_Window.Export ();
						}
				}
		}
}