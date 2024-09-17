using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AlmostEngine.Preview;

namespace AlmostEngine.Examples.Preview
{
	public class DebugValuesUI : MonoBehaviour
	{
		public Text m_DeviceName;
		public Text m_Orientation;
		public Text m_Screen;
		public Text m_SafeaArea;
		public Text m_DPI;

		void Update ()
		{
			m_DeviceName.text = "Device name: " + DeviceInfo.GetName ();
			m_Orientation.text = DeviceInfo.IsPortrait () ? "PORTRAIT" : "LANDSCAPE";
			m_Screen.text = "Screen: " + DeviceInfo.GetResolution ();
			m_SafeaArea.text = "Safe Area: " + DeviceInfo.GetSafeArea ();
			#if UNITY_ANDROID && ! UNITY_EDITOR
			m_SafeaArea.text += " (on Android devices vertical values may be inverted.)";
			#endif

			m_DPI.text = "PPI: " + DeviceInfo.GetDPI () + " (note that Unity often returns wrong PPI value; prefer using the official PPI value from constructor websites)"; 
		}
	}
}
