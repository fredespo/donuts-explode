using System.Collections;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using System.Reflection;
#endif

#if UNITY_EDITOR
using AlmostEngine.Screenshot;
#endif

namespace AlmostEngine.Preview
{
    public class DetectOrientationChange : MonoBehaviour
    {

        public static UnityAction<bool> onOrientationChangedDelegate = (bool isPortrait) =>
        {
        };

        bool m_IsPortait;

        void Awake()
        {
			// We init the portrait value with the current portrait value
            m_IsPortait = DeviceInfo.IsPortrait();

			#if UNITY_EDITOR
				// In editor, we are in fact interested by the orientation of the device, not by the gameview resolution
				// So we try to get the first preview device and look at its orientation
				var firstDevice = GetFirstPreviewDevice();
				if (firstDevice != null)
				{
					m_IsPortait = firstDevice.m_Orientation == ScreenshotResolution.Orientation.PORTRAIT;
				}
			#endif

            Debug.Log("On orientation changed. Init portrait: " + m_IsPortait);
        }

        void Update()
        {
			#if UNITY_EDITOR
				// Update only if it is a simulated device
				if (DeviceInfo.GetSimulatedDevice() == null)
					return;
			#endif

            // Check if orientation changed
            bool portrait = DeviceInfo.IsPortrait();
            if (portrait != m_IsPortait)
            {
                m_IsPortait = portrait;
                Debug.Log("Orientation change detected. Is portrait: " + m_IsPortait);
                onOrientationChangedDelegate(m_IsPortait);
            }
        }


		#if UNITY_EDITOR
        ScreenshotResolution GetFirstPreviewDevice()
        {
            try
            {
                // We use reflection to access the Editor class that are not accessible from this assembly
                var settingsType = System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.FullName.Equals("AlmostEngine.Preview.ResolutionSettingsWindow"));
                var methodInfo = settingsType.GetMethod("GetConfig");
                var configAsset = methodInfo.Invoke(null, null);
                if (configAsset == null)
                    return null;
                var configAssetType = System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.FullName.Equals("AlmostEngine.Preview.PreviewConfigAsset"));
                var configField = configAssetType.GetField("m_Config");

                // Get the config asset from the preview settings
                ScreenshotConfig config = (ScreenshotConfig)configField.GetValue(configAsset);

                // Get the first active device
                return config.GetFirstActiveResolution();
            }
            catch
            {
                return null;
            }
        }
		#endif


    }
}