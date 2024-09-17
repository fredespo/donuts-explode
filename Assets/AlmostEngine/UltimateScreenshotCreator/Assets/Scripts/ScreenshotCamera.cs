using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace AlmostEngine.Screenshot
{
    [System.Serializable]
    /// <summary>
    /// Screenshot cameras are used to control the cameras used for rendering the screenshot.
    /// </summary>
    public class ScreenshotCamera
    {
        public enum CustomSettings
        {
            KEEP_CAMERA_SETTINGS,
            CUSTOM
        }
        ;

        // Camera
        public bool m_Active = true;
        public Camera m_Camera = null;

        // Clear
        public CustomSettings m_ClearSettings;
        public CameraClearFlags m_ClearFlags = CameraClearFlags.Skybox;
        public Color m_BackgroundColor = Color.white;

        // Culling
        public CustomSettings m_CullingSettings;
        public int m_CullingMask = ~0;

        // FOV
        public CustomSettings m_FOVSettings;
        public float m_FOV = 70;


        public class Settings
        {
            public bool m_Enabled;
            public bool m_GameObjectEnabled;

            public CameraClearFlags m_ClearFlags;
            public Color m_BackgroundColor;
            public int m_CullingMask;
            public float m_FOV;

            public Color m_HDClearColor;

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            public HDAdditionalCameraData.ClearColorMode m_HDClear;
#endif

            public Settings(Camera camera)
            {
                m_Enabled = camera.enabled;
                m_GameObjectEnabled = camera.gameObject;

                m_ClearFlags = camera.clearFlags;
                m_BackgroundColor = camera.backgroundColor;
                m_CullingMask = camera.cullingMask;
                m_FOV = camera.fieldOfView;

#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
                if (camera.GetComponent<HDAdditionalCameraData>() != null)
                {
                m_HDClearColor = camera.GetComponent<HDAdditionalCameraData>().backgroundColorHDR;
                m_HDClear = camera.GetComponent<HDAdditionalCameraData>().clearColorMode;
                }
#endif
            }
        };

        public Stack<Settings> m_SettingStack = new Stack<Settings>();


        public ScreenshotCamera()
        {
        }

        public ScreenshotCamera(Camera cam)
        {
            m_Camera = cam;
        }

        /// <summary>
        /// Apply the custom settings to the camera.
        /// </summary>
        public void ApplySettings(bool isOffscreen = false)
        {
            if (m_Camera == null)
                return;

            // Save current settings
            m_SettingStack.Push(new Settings(m_Camera));

            // Apply settings
            if (m_Camera.enabled == false && isOffscreen)
            {
                // If the camera does not need to be set active, and the camera component was disabled, 
                // we disable the gameobject to prevent rendering the camera when its component is enabled.
                m_Camera.gameObject.SetActive(false);
            }
            else
            {
                m_Camera.gameObject.SetActive(true);
            }
            m_Camera.enabled = true;

            if (m_ClearSettings == CustomSettings.CUSTOM)
            {
                m_Camera.clearFlags = m_ClearFlags;
                m_Camera.backgroundColor = m_BackgroundColor;
#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
                if (m_Camera.GetComponent<HDAdditionalCameraData>() != null)
                {
                m_Camera.GetComponent<HDAdditionalCameraData>().clearColorMode = m_ClearFlags == CameraClearFlags.SolidColor ? HDAdditionalCameraData.ClearColorMode.Color : HDAdditionalCameraData.ClearColorMode.Sky;
                m_Camera.GetComponent<HDAdditionalCameraData>().backgroundColorHDR = m_BackgroundColor;
                }
#endif
            }
            if (m_CullingSettings == CustomSettings.CUSTOM)
            {
                m_Camera.cullingMask = m_CullingMask;
            }
            if (m_FOVSettings == CustomSettings.CUSTOM)
            {
                m_Camera.fieldOfView = m_FOV;
            }
        }

        public void Disable()
        {
            if (m_Camera == null)
                return;

            // Save current settings
            m_SettingStack.Push(new Settings(m_Camera));

            // Apply settings
            m_Camera.enabled = false;

        }

        /// <summary>
        /// Restaure the original camera settings.
        /// </summary>
        public void RestoreSettings()
        {
            if (m_Camera == null)
                return;

            if (m_SettingStack.Count <= 0)
                return;

            Settings s = m_SettingStack.Pop();
            m_Camera.enabled = s.m_Enabled;
            m_Camera.gameObject.SetActive(s.m_GameObjectEnabled);
            m_Camera.clearFlags = s.m_ClearFlags;
            m_Camera.backgroundColor = s.m_BackgroundColor;
            m_Camera.cullingMask = s.m_CullingMask;
            m_Camera.fieldOfView = s.m_FOV;
#if USC_UNITY_PIPELINE_HDRP && USC_HDRP
            if (m_Camera.GetComponent<HDAdditionalCameraData>() != null)
            {
            m_Camera.GetComponent<HDAdditionalCameraData>().backgroundColorHDR = s.m_HDClearColor;
            m_Camera.GetComponent<HDAdditionalCameraData>().clearColorMode = s.m_HDClear;
            }
#endif
        }


    }
}
