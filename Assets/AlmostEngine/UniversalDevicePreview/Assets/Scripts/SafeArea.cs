using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using AlmostEngine.Screenshot;
using UnityEditor;
#endif

#if UNITY_EDITOR && (UNITY_2019_4_OR_NEWER || UNITY_2020_1_OR_NEWER)
using System.IO;
#endif

namespace AlmostEngine.Preview
{
    [ExecuteInEditMode]
    public class SafeArea : MonoBehaviour
    {
        [HideInInspector]
        public RectTransform m_Panel;

        #region Constraints

        public enum HorizontalConstraint
        {
            LEFT,
            RIGHT,
            LEFT_AND_RIGHT
        }
        ;

        public enum VerticalConstraint
        {
            TOP,
            BOTTOM,
            TOP_AND_BOTTOM
        }
        ;


        public enum Constraint
        {
            NONE,
            SNAP,
            PUSH,
            ENLARGE
        }
        ;

        public Constraint m_HorizontalConstraintType = Constraint.NONE;
        public HorizontalConstraint m_HorizontalConstraint = HorizontalConstraint.LEFT_AND_RIGHT;

        public Constraint m_VerticalConstraintType = Constraint.NONE;
        public VerticalConstraint m_VerticalConstraint = VerticalConstraint.TOP_AND_BOTTOM;

        #endregion



        #region Safe Area & constraints

        public Vector2 m_DefaultAnchorMin = new Vector2(-99f, -99f);
        public Vector2 m_DefaultAnchorMax = new Vector2(-99f, -99f);

        public void Restore()
        {
            // Restore default anchor
            // Debug.Log("Restore");
            if (m_DefaultAnchorMin != new Vector2(-99f, -99f))
            {
                m_Panel.anchorMin = m_DefaultAnchorMin;
                m_Panel.anchorMax = m_DefaultAnchorMax;
            }
        }

        public void ApplySafeArea()
        {
            Rect safeArea = DeviceInfo.GetSafeArea();
            ApplySafeArea(safeArea);
        }

        [System.NonSerialized]
        Rect m_LastSafeArea = new Rect(-1, -1, -1, -1);
        ScreenOrientation m_LastScreenOrientation = ScreenOrientation.LandscapeLeft;
        Vector2 m_LastResolution = new Vector2(-1, -1);

        public void ApplySafeAreaIfNeeded()
        {
            // We only apply safe area when detecting a change
            Rect safeArea = DeviceInfo.GetSafeArea();
            if (DeviceInfo.GetSafeArea() != m_LastSafeArea || DeviceInfo.GetOrientation() != m_LastScreenOrientation || DeviceInfo.GetResolution() != m_LastResolution)
            {
                ApplySafeArea(safeArea);
            }
        }


        public void ApplySafeArea(Rect safeArea)
        {
            // Ignore invalid safe area
            if (safeArea.width <= 0 || safeArea.height <= 0)
                return;

            // Safe current safe area to apply the modification only when changed
            m_LastSafeArea = safeArea;
            m_LastScreenOrientation = DeviceInfo.GetOrientation();
            m_LastResolution = DeviceInfo.GetResolution();

            // Init panel
            if (m_Panel == null || Application.isEditor)
            {
                m_Panel = GetComponent<RectTransform>();
            }
            // Init the original anchor values if required
            if (m_DefaultAnchorMin == new Vector2(-99f, -99f))
            {
                m_DefaultAnchorMin = m_Panel.anchorMin;
                m_DefaultAnchorMax = m_Panel.anchorMax;
            }
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                // Save current values to enable UNDO if something goes wrong
                Undo.RecordObject(m_Panel, "Safe Area");
            }
#endif

            // Init default anchor change
            m_Panel.anchorMin = m_DefaultAnchorMin;
            m_Panel.anchorMax = m_DefaultAnchorMax;
            Vector2 deviceScreenSize = DeviceInfo.GetResolution();

            // HORIZONTAL
            if (m_HorizontalConstraintType == Constraint.SNAP)
            {
                if (m_HorizontalConstraint == HorizontalConstraint.LEFT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.x = safeArea.position.x / deviceScreenSize.x;
                    m_Panel.anchorMin = anchorMin;
                }
                if (m_HorizontalConstraint == HorizontalConstraint.RIGHT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.x = (safeArea.position.x + safeArea.size.x) / deviceScreenSize.x;
                    m_Panel.anchorMax = anchorMax;
                }
            }

            if (m_HorizontalConstraintType == Constraint.ENLARGE)
            {
                if (m_HorizontalConstraint == HorizontalConstraint.LEFT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.x = anchorMax.x + safeArea.position.x / deviceScreenSize.x;
                    m_Panel.anchorMax = anchorMax;
                }
                if (m_HorizontalConstraint == HorizontalConstraint.RIGHT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.x = anchorMin.x - (deviceScreenSize.x - safeArea.width - safeArea.position.x) / deviceScreenSize.x;
                    m_Panel.anchorMin = anchorMin;
                }
            }


            if (m_HorizontalConstraintType == Constraint.PUSH)
            {
                if (m_HorizontalConstraint == HorizontalConstraint.LEFT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.x = anchorMin.x + safeArea.position.x / deviceScreenSize.x;
                    m_Panel.anchorMin = anchorMin;
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.x = anchorMax.x + safeArea.position.x / deviceScreenSize.x;
                    m_Panel.anchorMax = anchorMax;
                }
                if (m_HorizontalConstraint == HorizontalConstraint.RIGHT || m_HorizontalConstraint == HorizontalConstraint.LEFT_AND_RIGHT)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.x = anchorMin.x - (deviceScreenSize.x - safeArea.width - safeArea.position.x) / deviceScreenSize.x;
                    m_Panel.anchorMin = anchorMin;
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.x = anchorMax.x - (deviceScreenSize.x - safeArea.width - safeArea.position.x) / deviceScreenSize.x;
                    m_Panel.anchorMax = anchorMax;
                }
            }


            // VERTICAL

            if (m_VerticalConstraintType == Constraint.SNAP)
            {
                if (m_VerticalConstraint == VerticalConstraint.BOTTOM || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.y = safeArea.position.y / deviceScreenSize.y;
                    m_Panel.anchorMin = anchorMin;
                }
                if (m_VerticalConstraint == VerticalConstraint.TOP || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.y = (safeArea.position.y + safeArea.size.y) / deviceScreenSize.y;
                    m_Panel.anchorMax = anchorMax;
                }
            }

            if (m_VerticalConstraintType == Constraint.ENLARGE)
            {
                if (m_VerticalConstraint == VerticalConstraint.TOP || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.y = anchorMin.y - (deviceScreenSize.y - safeArea.height - safeArea.position.y) / deviceScreenSize.y;
                    m_Panel.anchorMin = anchorMin;
                }
                if (m_VerticalConstraint == VerticalConstraint.BOTTOM || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.y = anchorMax.y + safeArea.position.y / deviceScreenSize.y;
                    m_Panel.anchorMax = anchorMax;
                }
            }

            if (m_VerticalConstraintType == Constraint.PUSH)
            {
                if (m_VerticalConstraint == VerticalConstraint.TOP || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.y = anchorMin.y - (deviceScreenSize.y - safeArea.height - safeArea.position.y) / deviceScreenSize.y;
                    m_Panel.anchorMin = anchorMin;
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.y = anchorMax.y - (deviceScreenSize.y - safeArea.height - safeArea.position.y) / deviceScreenSize.y;
                    m_Panel.anchorMax = anchorMax;
                }
                if (m_VerticalConstraint == VerticalConstraint.BOTTOM || m_VerticalConstraint == VerticalConstraint.TOP_AND_BOTTOM)
                {
                    Vector2 anchorMin = m_Panel.anchorMin;
                    anchorMin.y = anchorMin.y + safeArea.position.y / deviceScreenSize.y;
                    m_Panel.anchorMin = anchorMin;
                    Vector2 anchorMax = m_Panel.anchorMax;
                    anchorMax.y = anchorMax.y + safeArea.position.y / deviceScreenSize.y;
                    m_Panel.anchorMax = anchorMax;
                }
            }

        }

        #endregion


#if !UNITY_EDITOR
        #region IN BUILD LOGIC
        void OnEnable()
        {
            // Apply safe area at startup
            ApplySafeArea();
        }
        void Update()
        {
            // We constantly check if the safe area changed, for instance if the screen is rotated            
            ApplySafeAreaIfNeeded();
        }
        #endregion
#endif


#if UNITY_EDITOR && (UNITY_2019_4_OR_NEWER || UNITY_2020_1_OR_NEWER)
        #region DEVICE SIMULATOR COMPATIBILITY

        static int deviceSimulatorEnabled = -1;
        public static bool IsDeviceSimulatorEnabled()
        {
            if (deviceSimulatorEnabled < 0)
            {
                string manifestPath = Application.dataPath + "/../Packages/manifest.json";  
                if (!System.IO.File.Exists(manifestPath))
                {
                    // Debug.Log("Package manigest not found " + manifestPath);
                    deviceSimulatorEnabled = 0;
                }
                else
                {
                    string manifest = System.IO.File.ReadAllText(manifestPath);
                    if (manifest.Contains("device-simulator"))
                    {
                        // Debug.Log("device-simulator is enabled");
                        deviceSimulatorEnabled = 1;
                    }
                    else
                    {
                        // Debug.Log("device-simulator is disabled");
                        deviceSimulatorEnabled = 0;
                    }
                }
            }
            return deviceSimulatorEnabled == 1;
        }
        
        void Start()
        {
            // Apply safe area at startup
            ApplySafeArea();
        }

        void Update()
        {
            // We prevent to automatically update the safe area if a device simulation is in process to prevent any conflict
            if (m_IsSimulatingDevice)
            {
                return;
            }
            // Auto update only needed when Device Simulator is enabled 
            if (!IsDeviceSimulatorEnabled())
            {
                return;
            }
            ApplySafeAreaIfNeeded();
        }

        #endregion
#endif


#if UNITY_EDITOR
        #region SIMULATION LOGIC

        [System.NonSerialized]
        public bool m_IsSimulatingDevice = false;

        void OnEnable()
        {
            // Register to simulation events
            ScreenshotTaker.onResolutionUpdateStartDelegate += DeviceBeforeCapture;
            ScreenshotTaker.onResolutionUpdateEndDelegate += DeviceAfterCapture;
            ScreenshotTaker.onResolutionScreenResizedDelegate += DeviceResized;

            // Apply safe area
            ApplySafeAreaIfNeeded();
        }


        void OnDisable()
        {
            // Unregister simulation events
            ScreenshotTaker.onResolutionUpdateStartDelegate -= DeviceBeforeCapture;
            ScreenshotTaker.onResolutionUpdateEndDelegate -= DeviceAfterCapture;
            ScreenshotTaker.onResolutionScreenResizedDelegate -= DeviceResized;
        }

        void DeviceBeforeCapture(ScreenshotResolution device)
        {
        }

        void DeviceResized(ScreenshotResolution device)
        {
            // In live preview, we ignore the resize event for non device,
            // to prevent resizing to the device full preview with border 
            if (Application.isPlaying && device.m_ResolutionName == "" && DeviceInfo.m_IsLivePreview)
            {
                return;
            }

            m_IsSimulatingDevice = true;
            ApplySafeArea();
        }

        void DeviceAfterCapture(ScreenshotResolution device)
        {
            // We restore the safe area except if it is a live preview to prevent flickering
            if (DeviceInfo.m_IsLivePreview)
            {
                return;
            }
            Restore();
            m_IsSimulatingDevice = false;
        }

        #endregion
#endif


    }
}
