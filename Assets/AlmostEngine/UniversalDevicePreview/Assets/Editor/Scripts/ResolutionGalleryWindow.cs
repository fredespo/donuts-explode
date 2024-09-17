#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

using AlmostEngine.Screenshot;

namespace AlmostEngine.Preview
{
    /// <summary>
    /// The Resolution Gallery Window makes possible to preview the game in different resolutions at a glance.
    /// </summary>
    public class ResolutionGalleryWindow : ResolutionWindowBase
    {

        [MenuItem("Window/Almost Engine/Universal Device Preview/Device Gallery")]
        public static void Init()
        {
            ResolutionGalleryWindow window = (ResolutionGalleryWindow)EditorWindow.GetWindow(typeof(ResolutionGalleryWindow), false, "Gallery");
            window.Show();
        }

        public static ResolutionGalleryWindow m_Window;


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

            // BackUpDevicePreview ();

        }

        protected override void HandleEditorEvents()
        {
            Event e = Event.current;

            if (e == null)
                return;

            // Zoom
            if (e.type == EventType.ScrollWheel && e.control)
            {
                m_ConfigAsset.m_PreviewGalleryZoom -= m_ConfigAsset.m_ZoomScrollSpeed * e.delta.y;
                e.Use();
            }
        }

        public override void Export()
        {
            m_ConfigAsset.m_Config.ExportToFiles(m_ConfigAsset.m_Config.GetActiveResolutions());
        }

        protected override void DrawToolBarGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

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

            // var exporttitle = IconsUtils.TryGetIcon("Texture2D Icon");
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
            var device = m_ConfigAsset.m_Config.GetFirstActiveResolution();
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
            EditorGUILayout.PropertyField(m_SerializedConfig.FindProperty("m_GalleryDisplayMode"), GUIContent.none, GUILayout.MaxWidth(70));

            // MODE
            EditorGUILayout.LabelField("Drawing mode", GUILayout.MaxWidth(85));
            var previousDrawing = m_ConfigAsset.m_DrawingMode;
            EditorGUILayout.PropertyField(m_SerializedConfig.FindProperty("m_DrawingMode"), GUIContent.none, GUILayout.MaxWidth(100));
            m_SerializedConfig.ApplyModifiedProperties();
            m_SerializedConfig.Update();
            if (previousDrawing != m_ConfigAsset.m_DrawingMode)
            {
                UpdateWindowResolutions();
            }


            // var settingstitle = IconsUtils.TryGetIcon("BuildSettings.iPhone.Small");
            var settingstitle = IconsUtils.TryGetIcon("SettingsIcon");
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
            float zoom = EditorGUILayout.Slider(m_ConfigAsset.m_PreviewGalleryZoom, 0.05f, 4f, GUILayout.ExpandWidth(true));
            if (zoom != m_ConfigAsset.m_PreviewGalleryZoom)
            {
                m_ConfigAsset.m_PreviewGalleryZoom = zoom;
                EditorUtility.SetDirty(m_ConfigAsset);
            }

            if (GUILayout.Button("1:1", EditorStyles.toolbarButton))
            {
                m_ConfigAsset.m_PreviewGalleryZoom = 1f;
                EditorUtility.SetDirty(m_ConfigAsset);
            }

            EditorGUILayout.EndHorizontal();


            // Device mask
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();
            DrawDevicesQuickSelect();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        protected void DrawDevicesQuickSelect()
        {
            // Init flags
            int flags = 0;
            for (int i = 0; i < m_ConfigAsset.m_Config.m_Resolutions.Count; ++i)
            {
                if (m_ConfigAsset.m_Config.m_Resolutions[i].m_Active)
                {
                    flags = flags | (1 << i);
                }
            }
            List<string> devices = m_ConfigAsset.m_Config.m_Resolutions.Select(x => x.ToString()).ToList();
            if (devices.Count != 0)
            {
                int newFlags = EditorGUILayout.MaskField("", flags, devices.ToArray(), GUILayout.MinWidth(400));
                if (newFlags != flags)
                {
                    flags = newFlags;
                    // Enable or disable the device depending on the device mask bit fields
                    for (int i = 0; i < m_ConfigAsset.m_Config.m_Resolutions.Count; ++i)
                    {
                        m_ConfigAsset.m_Config.m_Resolutions[i].m_Active = ((flags & (1 << i)) != 0);
                    }
                    // if (m_ConfigAsset.m_AutoGenerateEmptyPreview)
                    // {
                    //     UpdateAllRequiredResolutions();
                    // }
                }
            }
        }

        protected override void DrawPreviewGUI()
        {
            Rect pos = GUILayoutUtility.GetLastRect();
            pos = new Rect(m_ConfigAsset.m_MarginHorizontal, m_ConfigAsset.m_MarginVertical, m_WindowWidth, 1);

            // Start scroll area
            height = 0;
            scroll = EditorGUILayout.BeginScrollView(scroll);

            // Draw each resolution
            foreach (ScreenshotResolution resolution in m_ConfigAsset.m_Config.GetActiveResolutions())
            {
                pos = DrawResolutionPreview(pos, resolution);
            }

            // Make some space
            EditorGUILayout.LabelField("", GUILayout.MinHeight(height));
            EditorGUILayout.LabelField("", GUILayout.MinWidth(width));

            // End scroll
            EditorGUILayout.EndScrollView();
        }

        Rect DrawResolutionPreview(Rect pos, ScreenshotResolution resolution)
        {
            Vector2 size = GetRenderSize(resolution, m_ConfigAsset.m_PreviewGalleryZoom, m_ConfigAsset.m_GalleryDisplayMode);
            if (size == Vector2.zero)
                return pos;

            // If can not draw the rect in the current row, create a new row
            if (pos.x > m_ConfigAsset.m_MarginHorizontal && pos.x + size.x + m_ConfigAsset.m_MarginHorizontal > m_WindowWidth)
            {
                pos.x = m_ConfigAsset.m_MarginHorizontal;
                pos.y = height + m_ConfigAsset.m_GalleryPaddingVertical;
            }
            pos.width = size.x;
            pos.height = size.y;

            // Draw the white background
            Rect borderpos = pos;
            borderpos.x -= m_ConfigAsset.m_GalleryBorderSize;
            borderpos.y -= m_ConfigAsset.m_GalleryBorderSize;
            borderpos.width += 2 * m_ConfigAsset.m_GalleryBorderSize;
            borderpos.height += 2 * m_ConfigAsset.m_GalleryBorderSize + m_ConfigAsset.m_GalleryTextHeight;


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

            // Display the resolution name
            Rect labelpos = pos;
            labelpos.y = pos.y + size.y + 5;
            labelpos.height = m_ConfigAsset.m_GalleryTextHeight;
            string displayName = resolution.m_ResolutionName;
            if (m_ConfigAsset.m_ShowResolution || m_ConfigAsset.m_ShowPPI || m_ConfigAsset.m_ShowRatio)
            {
                displayName += "\n";
            }
            if (m_ConfigAsset.m_ShowResolution || displayName == "")
            {
                displayName += " " + resolution.m_Width + "x" + resolution.m_Height;
            }
            if (m_ConfigAsset.m_ShowPPI && resolution.m_PPI > 0)
            {
                displayName += " " + resolution.m_PPI + "ppi";
            }
            if (m_ConfigAsset.m_ShowRatio)
            {
                displayName += " " + resolution.m_Ratio;
            }
            EditorGUI.LabelField(labelpos, displayName, m_NameStyle);

            // Increment the box position
            pos.x += size.x + m_ConfigAsset.m_GalleryPaddingVertical;
            height = (int)Mathf.Max(height, pos.y + size.y + 5 + m_ConfigAsset.m_GalleryTextHeight);
            width = (int)Mathf.Max(width, size.x);

            return pos;

        }


    }
}

#endif