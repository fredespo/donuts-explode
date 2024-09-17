using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using AlmostEngine.Screenshot;
#endif

namespace AlmostEngine.Preview
{
    public class DeviceInfo
    {
#if UNITY_EDITOR

        #region Device Simulation
        static ScreenshotResolution m_CurrentDevice;
        public static bool m_IsLivePreview = false;
        public static UnityAction<ScreenshotResolution> onSimulationStart = (ScreenshotResolution device) =>
        {
        };
        public static UnityAction<ScreenshotResolution> onSimulationEnd = (ScreenshotResolution device) =>
        {
        };

        public static void SetSimulatedDevice(ScreenshotResolution device)
        {
            if (m_CurrentDevice == device)
                return;
            if (m_CurrentDevice != null)
            {
                onSimulationEnd(m_CurrentDevice);
            }
            m_CurrentDevice = device;
            if (m_CurrentDevice != null)
            {
                onSimulationStart(m_CurrentDevice);
            }
        }

        public static ScreenshotResolution GetSimulatedDevice()
        {
            return m_CurrentDevice;
        }

        #endregion

#endif

        #region Device info

        public static string GetName()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_ResolutionName;
            }
#endif

            return SystemInfo.deviceModel;
        }

        #endregion


        #region Screen info

        public static ScreenOrientation GetOrientation()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                if (m_CurrentDevice.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE)
                {
                    return ScreenOrientation.LandscapeLeft;
                }
                else if (m_CurrentDevice.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE_RIGHT)
                {
                    return ScreenOrientation.LandscapeRight;
                }
                else
                {
                    return ScreenOrientation.Portrait;
                }
            }
#endif
            return Screen.orientation;
        }

        public static Vector2 GetResolution()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return new Vector2(m_CurrentDevice.ComputeTargetWidth(), m_CurrentDevice.ComputeTargetHeight());
            }
#endif

            return new Vector2(Screen.width, Screen.height);
        }

        public static int GetDPI()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_PPI;
            }
#endif

            return (int)Screen.dpi;
        }

        public static bool IsPortrait()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_Orientation == ScreenshotResolution.Orientation.PORTRAIT;
            }
#endif

            return Screen.height > Screen.width;
        }

        #endregion

        #region Safe Area

#if UNITY_EDITOR

        public static bool HasNotch()
        {
            var safearea = GetSafeArea();
            if (IsPortrait() && safearea.height != GetResolution().y)
            {
                return true;
            }
            else if (!IsPortrait() && safearea.width != GetResolution().x)
            {
                return true;
            }
            return false;
        }

        public static bool HasSimulatedSafeAreaValue()
        {
            return GetSimulatedSafeArea() != new Rect();
        }

        public static Rect GetSimulatedSafeArea()
        {
            Rect safeArea = new Rect();
            if (m_CurrentDevice != null && !m_CurrentDevice.m_DisableSafeArea)
            {
                // Get current device safe area
                if (m_CurrentDevice.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE)
                {
                    safeArea = m_CurrentDevice.m_SafeAreaLandscapeLeft;
                }
                else if (m_CurrentDevice.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE_RIGHT && m_CurrentDevice.m_SafeAreaLandscapeLeft != Rect.zero)
                {
                    safeArea.x = m_CurrentDevice.m_Width - m_CurrentDevice.m_SafeAreaLandscapeLeft.width - m_CurrentDevice.m_SafeAreaLandscapeLeft.x;
                    safeArea.width = m_CurrentDevice.m_SafeAreaLandscapeLeft.width;
                    safeArea.y = m_CurrentDevice.m_SafeAreaLandscapeLeft.y;
                    safeArea.height = m_CurrentDevice.m_SafeAreaLandscapeLeft.height;
                }
                else
                {
                    safeArea = m_CurrentDevice.m_SafeAreaPortrait;
                }
                // Apply resolution scale
                if (m_CurrentDevice.m_Scale != 1f)
                {
                    float scale = m_CurrentDevice.m_Scale;
                    safeArea = new Rect(safeArea.x * scale, safeArea.y * scale, safeArea.width * scale, safeArea.height * scale);
                }
            }
            return safeArea;
        }

#endif

        public static Rect GetSafeArea()
        {
            Rect safeArea = Screen.safeArea;

#if UNITY_EDITOR
            if (HasSimulatedSafeAreaValue())
            {
                safeArea = GetSimulatedSafeArea();
            }
            else if (m_CurrentDevice != null)
            {
                safeArea = new Rect(0, 0, GetResolution().x, GetResolution().y);
            }
#endif
            return safeArea;
        }

        #endregion

        #region Platforms

        public static bool IsIOS()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_Platform.Contains("iOS");
            }
#endif

#if UNITY_IOS
			return true;
#else
            return false;
#endif
        }

        public static bool IsAndroid()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_Platform.Contains("Android");
            }
#endif

#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }

        public static bool IsStandalone()
        {
#if UNITY_EDITOR
            if (m_CurrentDevice != null)
            {
                return m_CurrentDevice.m_Platform.Contains("Standalone");
            }
#endif

#if UNITY_STANDALONE
            return true;
#else
            return false;
#endif
        }

        #endregion
    }
}

