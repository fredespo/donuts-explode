#if UNITY_EDITOR

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Preview
{
    public class ResolutionPreviewWindow : ResolutionWindowBase
    {

        [MenuItem("Window/Almost Engine/Universal Device Preview/Device Preview")]
        public static void Init()
        {
            ResolutionPreviewWindow window = (ResolutionPreviewWindow)EditorWindow.GetWindow(typeof(ResolutionPreviewWindow), false, "Preview");
            window.Show();
        }

        public static ResolutionPreviewWindow m_Window;

        public static bool IsOpen()
        {
            return m_Window != null;
        }

        protected override void OnEnable()
        {
            m_Window = this;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            m_Window = null;
            base.OnDisable();
        }

        protected override float GetZoom()
        {
            return m_ConfigAsset.m_PreviewZoom;
        }
        protected override PreviewConfigAsset.GalleryDisplayMode GetDisplayMode()
        {
            return m_ConfigAsset.m_PreviewDisplayMode;
        }

        public override void Export()
        {
            if (m_ConfigAsset.m_Selected < m_ConfigAsset.m_Config.GetActiveResolutions().Count)
            {
                m_ConfigAsset.m_Config.ExportToFiles(new List<ScreenshotResolution> { m_ConfigAsset.m_Config.m_Resolutions[m_ConfigAsset.m_Selected] });
            }
        }

        protected override void HandleEditorEvents()
        {
            Event e = Event.current;

            if (e == null)
                return;

            // Zoom
            if (e.type == EventType.ScrollWheel && e.control)
            {
                m_ConfigAsset.m_PreviewZoom -= m_ConfigAsset.m_ZoomScrollSpeed * e.delta.y;
                e.Use();
            }
        }

        ScreenshotResolution m_SelectedResolution;

        protected override void DrawToolBarGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);


            // Safe selection
            if (m_ConfigAsset.m_Selected >= m_ConfigAsset.m_Config.m_Resolutions.Count)
            {
                m_ConfigAsset.m_Selected = 0;
            }


            // BUTTONS
            var col = GUI.color;
            GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
            // var updatetitle = IconsUtils.TryGetIcon("RotateTool");
            var updatetitle = IconsUtils.TryGetIcon("Refresh");
            updatetitle.text = " Update Previews";
            if (GUILayout.Button(updatetitle, EditorStyles.toolbarButton))
            // if (GUILayout.Button("  Update Previews ", EditorStyles.toolbarButton))
            {
                UpdateWindowResolutions();
            }
            GUI.color = col;

#if UNITY_2019_4_OR_NEWER
            var exporttitle = IconsUtils.TryGetIcon("Image Icon");
#else
            var exporttitle = IconsUtils.TryGetIcon("BuildSettings.Standalone.Small");
#endif
            exporttitle.text = " Save to file(s)";
            if (GUILayout.Button(exporttitle, EditorStyles.toolbarButton))
            // if (GUILayout.Button("Export to file(s)", EditorStyles.toolbarButton))
            {
                Export();
            }

            // SPACE
            GUILayout.FlexibleSpace();




            // SAFE AREA
            if (m_ConfigAsset.m_DrawSafeArea)
            {
                if (GUILayout.Button("Safe Area (ON)  ", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_DrawSafeArea = false;
                    EditorUtility.SetDirty(m_ConfigAsset);
                    UpdateWindowResolutions();
                }
            }
            else
            {
                if (GUILayout.Button("Safe Area (OFF)", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_DrawSafeArea = true;
                    EditorUtility.SetDirty(m_ConfigAsset);
                    UpdateWindowResolutions();
                }
            }

            // ORIENTATION          
            var previousOrientation = ScreenshotResolution.Orientation.LANDSCAPE;
            ScreenshotResolution device = null;
            if (m_ConfigAsset.m_Config.m_Resolutions.Count > m_ConfigAsset.m_Selected)
            {
                device = m_ConfigAsset.m_Config.m_Resolutions[m_ConfigAsset.m_Selected];
            }
            if (device != null)
            {
                previousOrientation = device.m_Orientation;
                var newOrientation = (ScreenshotResolution.Orientation)EditorGUILayout.EnumPopup("", m_ConfigAsset.m_Config.GetFirstActiveResolution().m_Orientation);
                if (newOrientation != previousOrientation)
                {
                    if (newOrientation == ScreenshotResolution.Orientation.LANDSCAPE)
                    {
                        m_ConfigAsset.m_Config.SetAllLandscape();
                    }
                    else if (newOrientation == ScreenshotResolution.Orientation.LANDSCAPE_RIGHT)
                    {
                        m_ConfigAsset.m_Config.SetAllLandscapeRight();
                    }
                    else
                    {
                        m_ConfigAsset.m_Config.SetAllPortait();
                    }
                    EditorUtility.SetDirty(m_ConfigAsset);
                    UpdateWindowResolutions();
                }
            }

            // MODE
            EditorGUILayout.LabelField("Display mode", GUILayout.MaxWidth(85));
            EditorGUILayout.PropertyField(m_SerializedConfig.FindProperty("m_PreviewDisplayMode"), GUIContent.none, GUILayout.MaxWidth(70));


            // MODE
            EditorGUILayout.LabelField("Drawing mode", GUILayout.MaxWidth(85));
            var previousDrawing = m_ConfigAsset.m_DrawingMode;
            EditorGUILayout.PropertyField(m_SerializedConfig.FindProperty("m_DrawingMode"), GUIContent.none, GUILayout.MaxWidth(100));
            m_SerializedConfig.ApplyModifiedProperties();
            if (previousDrawing != m_ConfigAsset.m_DrawingMode)
            {
                UpdateWindowResolutions();
            }


            var settingstitle = IconsUtils.TryGetIcon("SettingsIcon");
            // var settingstitle = IconsUtils.TryGetIcon("BuildSettings.iPhone.Small");
            settingstitle.text = " Settings";
            // if (GUILayout.Button("Settings", EditorStyles.toolbarButton))
            if (GUILayout.Button(settingstitle, EditorStyles.toolbarButton))
            {
                ResolutionSettingsWindow.Init();
            }

            // ABOUT
            var abouttitle = IconsUtils.TryGetIcon("UnityEditor.InspectorWindow");
            abouttitle.text = " About";
            if (GUILayout.Button(abouttitle, EditorStyles.toolbarButton))
            // if (GUILayout.Button("About", EditorStyles.toolbarButton))
            {
                UniversalDevicePreview.About();
            }

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);


            // AUTO REFRESH
            if (m_ConfigAsset.m_AutoRefresh)
            {
                if (GUILayout.Button("Auto refresh (ON)", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_AutoRefresh = false;
                    EditorUtility.SetDirty(m_ConfigAsset);
                }
            }
            else
            {
                if (GUILayout.Button("Auto refresh (OFF)", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_AutoRefresh = true;
                    EditorUtility.SetDirty(m_ConfigAsset);
                }
            }
            EditorGUILayout.LabelField("Refresh delay (s)", GUILayout.MaxWidth(110));
            float delay = EditorGUILayout.Slider(m_ConfigAsset.m_RefreshDelay, 0.01f, 10f, GUILayout.MaxWidth(200));
            if (delay != m_ConfigAsset.m_RefreshDelay)
            {
                m_ConfigAsset.m_RefreshDelay = delay;
                EditorUtility.SetDirty(m_ConfigAsset);
            }



            // SPACE
            GUILayout.FlexibleSpace();


            // OPTIMIZE
            if (m_ConfigAsset.m_OptimizePreviewResolution)
            {
                var guicol = GUI.color;
                GUI.color = Color.yellow;
                if (GUILayout.Button("Optimize Preview Resolution (ON)", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_OptimizePreviewResolution = false;
                    foreach (var res in m_ConfigAsset.m_Config.m_Resolutions)
                    {
                        res.m_Scale = 1f;
                    }
                    EditorUtility.SetDirty(m_ConfigAsset);
                    UpdateWindowResolutions();
                }
                GUI.color = guicol;
            }
            else
            {
                if (GUILayout.Button("Optimize Preview Resolution (OFF)", EditorStyles.toolbarButton))
                {
                    m_ConfigAsset.m_OptimizePreviewResolution = true;
                    EditorUtility.SetDirty(m_ConfigAsset);
                    UpdateWindowResolutions();
                }
            }



            // ZOOM
            EditorGUILayout.LabelField("Zoom", GUILayout.MaxWidth(40));
            float zoom = EditorGUILayout.Slider(m_ConfigAsset.m_PreviewZoom, 0.05f, 4f);
            if (zoom != m_ConfigAsset.m_PreviewZoom)
            {
                m_ConfigAsset.m_PreviewZoom = zoom;
                EditorUtility.SetDirty(m_ConfigAsset);
            }

            if (GUILayout.Button("1:1", EditorStyles.toolbarButton))
            {
                m_ConfigAsset.m_PreviewZoom = 1f;
                EditorUtility.SetDirty(m_ConfigAsset);
            }

            EditorGUILayout.EndHorizontal();



            // DEVICES
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();

            // SELECTION
            List<string> names = m_ConfigAsset.m_Config.m_Resolutions.Select(x => x.ToString()).ToList();
            int selected = EditorGUILayout.Popup(m_ConfigAsset.m_Selected, names.ToArray(), GUILayout.MinWidth(400));

            if (m_ConfigAsset.m_Config.m_Resolutions.Count > 0)
            {
                m_SelectedResolution = m_ConfigAsset.m_Config.m_Resolutions[selected];
            }
            else
            {
                m_SelectedResolution = null;
            }


            // Call update if selected changed
            if (selected != m_ConfigAsset.m_Selected)
            {
                m_ConfigAsset.m_Selected = selected;
                EditorUtility.SetDirty(m_ConfigAsset);
                if (m_ConfigAsset.m_AutoGenerateEmptyPreview)
                {
                    UpdateWindowResolutions();
                }
            }


            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();




        }

        protected override void DrawPreviewGUI()
        {
            if (m_SelectedResolution == null)
                return;

            Rect pos = GUILayoutUtility.GetLastRect();
            pos = new Rect(m_ConfigAsset.m_MarginHorizontal, m_ConfigAsset.m_MarginVertical, m_WindowWidth, 1);

            // Start scroll area
            height = 0;
            width = 0;
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // Draw the selected resolution
            DrawResolutionPreview(pos, m_SelectedResolution);

            // Make some space
            EditorGUILayout.LabelField("", GUILayout.MinHeight(height));
            EditorGUILayout.LabelField("", GUILayout.MinWidth(width));

            // End scroll
            EditorGUILayout.EndScrollView();
        }

        void DrawResolutionPreview(Rect pos, ScreenshotResolution resolution)
        {
            Vector2 size = GetRenderSize(resolution, m_ConfigAsset.m_PreviewZoom, m_ConfigAsset.m_PreviewDisplayMode);
            if (size == Vector2.zero)
                return;

            pos.width = size.x;
            pos.height = size.y;

            // Center device
            if (pos.width < m_WindowWidth)
            {
                pos.x = (m_WindowWidth - pos.width) / 2;
            }

            // Draw the resolution texture
            if (resolution.m_Texture != null)
            {
                EditorGUI.DrawTextureTransparent(pos, resolution.m_Texture);
            }
            else
            {
                EditorGUI.DrawTextureTransparent(pos, m_BackgroundTexture);
                EditorGUI.LabelField(pos, "Needs to be updated.", m_NameStyle);
            }

            height = (int)Mathf.Max(height, pos.y + size.y);
            width = (int)Mathf.Max(width, size.x);

        }

        protected override void UpdateWindowResolutions()
        {
            InitTempManager();
            m_ScreenshotTaker.StartCoroutine(UpdateCoroutine(new List<ScreenshotResolution> { m_SelectedResolution }));
        }

    }
}

#endif