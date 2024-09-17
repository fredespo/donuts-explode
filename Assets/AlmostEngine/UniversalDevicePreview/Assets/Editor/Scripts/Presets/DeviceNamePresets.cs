using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


namespace AlmostEngine.Screenshot
{
	[InitializeOnLoad]
	public class DeviceNamePresets
	{

		static DeviceNamePresets ()
		{
			Init ();
		}

		public static void Init ()
		{
			ScreenshotNamePresets.m_NamePresets.Add (new ScreenshotNamePresets.NamePreset ("{width}x{height}-{scale}-{name} {orientation} {ppi}ppi", "Resolution detailed infos"));
		}
	}
}