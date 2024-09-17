using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AlmostEngine.Preview;

namespace AlmostEngine.Examples.Preview
{
    [ExecuteInEditMode]
    public class DeviceCustomBorder : MonoBehaviour
    {
        public Vector2 m_PortraitAnchorMin;
        public Vector2 m_PortraitAnchorMax = Vector2.one;
        public Vector2 m_LandscapeAnchorMin;
        public Vector2 m_LandscapeAnchorMax = Vector2.one;

		[Tooltip ("Le list of devices to which the border constraints will be applied. Carefully check the hardware device name, which is often different from the device name, to make this component work properly in build. For instance, the iPhone 11 hardware name is iPhone12,1.")]
        public List<string> m_DevicesNames = new List<string>{
		// iPhone X, XR, XS, XSMax, XR
		"iPhone X", "iPhone10,3", "iPhone10,6", "iPhone11,2", "iPhone11,4", "iPhone11,6", "iPhone11,8",
        
		// iPhone 11, 11Pro, 11ProMax
		"iPhone 11", "iPhone12,1", "iPhone12,3", "iPhone12,5"
        };		
		
		[Tooltip ("When NOT is enabled, the constraints will be applied to every devices except those listed in the device list.")]
        public bool m_NOT = false;
		

        void Update()
        {
            bool isDevice = IsDevice();
            if (!m_NOT && isDevice || m_NOT && !isDevice)
            {
                if (DeviceInfo.IsPortrait())
                {
                    GetComponent<RectTransform>().anchorMin = m_PortraitAnchorMin;
                    GetComponent<RectTransform>().anchorMax = m_PortraitAnchorMax;
                }
                else
                {
                    GetComponent<RectTransform>().anchorMin = m_LandscapeAnchorMin;
                    GetComponent<RectTransform>().anchorMax = m_LandscapeAnchorMax;
                }
            }
            else
            {
                GetComponent<RectTransform>().anchorMin = Vector2.zero;
                GetComponent<RectTransform>().anchorMax = Vector2.one;
            }
        }

        bool IsDevice()
        {
            foreach (var device in m_DevicesNames)
            {
                if (DeviceInfo.GetName().Contains(device))
                    return true;
            }
            return false;
        }

    }
}
