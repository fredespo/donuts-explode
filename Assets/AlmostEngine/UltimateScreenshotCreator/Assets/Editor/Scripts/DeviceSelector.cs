using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

namespace AlmostEngine.Screenshot
{
    public class DeviceSelector
    {

        [Header("Data")]
        public ScreenshotConfig m_Config;

        public ScreenshotPresetDatabase m_Database = new ScreenshotPresetDatabase();

        public bool m_ShowDetailedDevice = false;



        public List<ScreenshotResolutionAsset> m_FilteredPresets = new List<ScreenshotResolutionAsset>();


        [Header("Filter")]
        public string m_TextFilter = "";
        string m_CategoryFilter = "";
        PresetCollectionAsset m_SelectedCollection = null;
        PopularityPresetAsset m_SelectedPopularityCollection = null;
        string m_RatioFilter = "";
        string m_TagFilter = "";
        public string m_ResolutionFilter = "";
        public string m_PPIFilter = "";
        bool m_HasSafeAreaOnly = false;
        bool m_HasImageOnly = false;


        [Header("UI")]
        int currentPage = 0;




        #region DATA

        public void Init(ScreenshotConfig config, bool showDevice = false)
        {
            m_Config = config;
            m_ShowDetailedDevice = showDevice;
            // InitPresets();
            UpdateDeviceSelectorList();
        }

        void UpdateDeviceSelectorList()
        {
            // Filter preset based on user filter
            FilterDeviceList();

            // If popularity, sort by popularity
            if (m_SelectedPopularityCollection != null)
            {
                m_FilteredPresets.Sort((x, y) => m_SelectedPopularityCollection.GetPopularity(y).CompareTo(m_SelectedPopularityCollection.GetPopularity(x)));
            }
        }

        void AddDevice(ScreenshotResolutionAsset preset)
        {
            // Create a new instance of the preset resolution
            m_Config.m_Resolutions.Add(new ScreenshotResolution(preset.m_Resolution));
        }

        //// Add all asset of the current page
        void AddAll()
        {
            for (int i = currentPage * m_Config.m_ItemsPerPage; i < ((currentPage + 1) * m_Config.m_ItemsPerPage) && i < m_FilteredPresets.Count; ++i)
            {
                AddDevice(m_FilteredPresets[i]);
            }
        }


        #endregion

        #region FILTER

        void FilterDeviceList()
        {
            // Reset to page 0
            currentPage = 0;

            // Init with all presets
            m_FilteredPresets.Clear();
            m_FilteredPresets.AddRange(m_Database.m_Presets);

            // Filter with the text filter
            if (m_TextFilter != "")
            {
                var splits = m_TextFilter.ToLower().Split(' ');
                foreach (var s in splits)
                {
                    m_FilteredPresets = m_FilteredPresets.Where(x => (x.name + " "
                    + x.m_Resolution.m_Category + " "
                    + x.m_Resolution.m_Platform + " "
                    + x.m_Resolution.m_Ratio + " "
                    + TagDatabaseAsset.GetTagString(x) + " "
                    + x.m_Resolution.m_Width + "x" + x.m_Resolution.m_Height).ToLower().Contains(s)).ToList();
                }
            }
            // Filter by category, collection or popularity
            if (m_CategoryFilter != "")
            {
                if (m_SelectedCollection != null)
                {
                    m_FilteredPresets = m_FilteredPresets.Where(x => m_SelectedCollection.m_Presets.Contains(x)).ToList();
                }
                else if (m_SelectedPopularityCollection != null)
                {
                    m_FilteredPresets = m_FilteredPresets.Where(x => m_SelectedPopularityCollection.Contains(x)).ToList();
                }
                else
                {
                    m_FilteredPresets = m_FilteredPresets.Where(x => ("" + x.m_Resolution.m_Category).Contains(m_CategoryFilter)).ToList();
                }
            }
            // Ratio
            if (m_RatioFilter != "")
            {
                m_FilteredPresets = m_FilteredPresets.Where(x => ("" + x.m_Resolution.m_Ratio) == (m_RatioFilter)).ToList();
            }
            // Tags
            if (m_TagFilter != "")
            {
                m_FilteredPresets = m_FilteredPresets.Where(x => ("" + TagDatabaseAsset.GetTagString(x)).Contains(m_TagFilter)).ToList();
            }
            // PPI
            if (m_PPIFilter != "")
            {
                m_FilteredPresets = m_FilteredPresets.Where(x => ("" + x.m_Resolution.m_PPI).Contains(m_PPIFilter)).ToList();
            }
            // Safe Area
            if (m_HasSafeAreaOnly)
            {
                m_FilteredPresets = m_FilteredPresets.Where(x => x.m_Resolution.m_SafeAreaPortrait != Rect.zero).ToList();
            }
            // Canvas
            if (m_HasImageOnly)
            {
                m_FilteredPresets = m_FilteredPresets.Where(x => x.m_Resolution.m_DeviceCanvas != null).ToList();
            }
        }




        #endregion

        #region GUI


        Vector2 scrollPos;


        bool m_TogglePresets = false;

        public void OnGUI()
        {
            if (!m_TogglePresets)
            {
                // Color c = GUI.color;
                // GUI.color = new Color(0.6f, 1f, 0.6f, 1.0f);
                EditorGUILayout.Separator();
                EditorGUILayout.HelpBox("Load the presets to search and add new devices to your selection.", MessageType.Info);
                var colorBK = GUI.color;
                GUI.color = new Color(1.0f, 0.9f, 0.7f);
                if (GUILayout.Button("Load all presets", GUILayout.Height(50)))
                {
                    m_TogglePresets = true;
                }
                GUI.color = colorBK;
                EditorGUILayout.Separator();
                // GUI.color =  c;
                return;
            }

            EditorGUILayout.LabelField("Search for presets", EditorStyles.boldLabel);

            // Draw filters
            DrawFilterInput(ref m_TextFilter, "Text filter");
            DrawCategoryPopup();
            DrawTagsPopup();
            DrawRatiosPopup();

            EditorGUILayout.BeginHorizontal();
            if (m_ShowDetailedDevice)
            {
                DrawFilterToggle(ref m_HasImageOnly, "Image");
                DrawFilterToggle(ref m_HasSafeAreaOnly, "SafeArea data");
            }
            // Items per page
            int nb = EditorGUILayout.IntField("Items per page", m_Config.m_ItemsPerPage);
            if (nb != m_Config.m_ItemsPerPage && nb > 0)
            {
                m_Config.m_ItemsPerPage = nb;
                currentPage = 0;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            // Draw devices selector
            int nbPages = ((m_FilteredPresets.Count - 1) / m_Config.m_ItemsPerPage) + 1;
            if (currentPage >= nbPages)
            {
                currentPage = nbPages - 1;
            }
            DrawDeviceTitles();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            for (int i = currentPage * m_Config.m_ItemsPerPage; i < ((currentPage + 1) * m_Config.m_ItemsPerPage); ++i)
            {
                if (i < m_FilteredPresets.Count)
                {
                    DrawDeviceItem(m_FilteredPresets[i]);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(20));
                    EditorGUILayout.LabelField("");
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();

            // Draw page selection
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pages ", EditorStyles.miniLabel, GUILayout.MaxWidth(40));
            for (int p = 0; p < nbPages; ++p)
            {
                EditorGUI.BeginDisabledGroup(p == currentPage);
                if (GUILayout.Button(p.ToString()))
                {
                    currentPage = p;
                }
                EditorGUI.EndDisabledGroup();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // Update
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            // var colorBK = GUI.color;
            // GUI.color = new Color(1.0f, 0.9f, 0.7f);
            if (GUILayout.Button("Update preset database", GUILayout.MaxWidth(300), GUILayout.Height(30)) || m_Database.m_Presets.Count == 0)
            {
                m_Database.InitPresets();
                UpdateDeviceSelectorList();
            }
            // GUI.color = colorBK;
            EditorGUILayout.EndHorizontal();
        }

        void DrawDeviceTitles()
        {
            var style = EditorStyles.miniLabel;
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add all", GUILayout.Width(75)))
            {
                AddAll();
            }
            EditorGUILayout.LabelField("", style, GUILayout.Width(10));
            if (m_SelectedPopularityCollection != null)
            {
                EditorGUILayout.LabelField("", GUILayout.Width(50));
            }
            EditorGUILayout.LabelField("Name", style, GUILayout.Width(200));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField("Platform", style, GUILayout.Width(70));
            }
            EditorGUILayout.LabelField("Resolution", style, GUILayout.Width(75));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField("ppi", style, GUILayout.Width(30));
            }
            EditorGUILayout.LabelField("ratio", style, GUILayout.Width(50));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField("SafeArea", style, GUILayout.Width(50));
                EditorGUILayout.LabelField("Image", style, GUILayout.Width(35));
            }
            EditorGUILayout.LabelField("Tags", style, GUILayout.MinWidth(50));
            EditorGUILayout.LabelField("Category", style, GUILayout.MinWidth(70));

            EditorGUILayout.EndHorizontal();
        }

        void DrawDeviceItem(ScreenshotResolutionAsset preset)
        {
            if (preset == null)
                return;

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

            // BUTTONS
            if (GUILayout.Button("Add", GUILayout.Width(35)))
            {
                AddDevice(preset);
            }
            if (GUILayout.Button("Select", GUILayout.Width(45)))
            {
                Selection.activeObject = preset;
            }

            // Bold if already in list
            var style = EditorStyles.label;
            if (m_Config.m_Resolutions.Where(x => preset.name == x.m_ResolutionName).ToList().Count > 0)
            {
                style = EditorStyles.boldLabel;
            }

            // Popularity if popularity selection 
            if (m_SelectedPopularityCollection != null)
            {
                float frequency = m_SelectedPopularityCollection.GetPopularity(preset);
                string fs = "";
                if (frequency > 0f)
                {
                    fs = frequency.ToString() + (m_SelectedPopularityCollection.m_Type == PopularityPresetAsset.Type.Percent ? "%" : "m");
                }
                EditorGUILayout.LabelField(fs, style, GUILayout.Width(50));
            }

            // Device name
            EditorGUILayout.LabelField(preset.name, style, GUILayout.Width(200));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField(preset.m_Resolution.m_Platform, style, GUILayout.Width(70));
            }
            EditorGUILayout.LabelField("" + preset.m_Resolution.m_Width + "x" + preset.m_Resolution.m_Height, style, GUILayout.Width(75));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField(preset.m_Resolution.m_PPI > 0 ? preset.m_Resolution.m_PPI.ToString() : "", style, GUILayout.Width(30));
            }
            EditorGUILayout.LabelField(preset.m_Resolution.m_Ratio, style, GUILayout.Width(50));
            if (m_ShowDetailedDevice)
            {
                EditorGUILayout.LabelField(preset.m_Resolution.m_SafeAreaPortrait == Rect.zero ? " " : "■", GUILayout.Width(50));
                EditorGUILayout.LabelField(preset.m_Resolution.m_DeviceCanvas == null ? " " : "■", GUILayout.Width(35));
            }
            EditorGUILayout.LabelField(TagDatabaseAsset.GetTagString(preset), style, GUILayout.MinWidth(50));
            EditorGUILayout.LabelField(preset.m_Resolution.m_Category, style, GUILayout.MinWidth(70));


            EditorGUILayout.EndHorizontal();
        }

        void DrawFilterInput(ref string value, string label)
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.MaxWidth(145));
            var tmp = GUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();
            if (tmp != value)
            {
                value = tmp;
                FilterDeviceList();
            }
        }

        void DrawFilterToggle(ref bool value, string label)
        {
            EditorGUILayout.BeginHorizontal();
            var tmp = GUILayout.Toggle(value, "");
            if (tmp != value)
            {
                value = tmp;
                FilterDeviceList();
            }
            EditorGUILayout.LabelField(label, GUILayout.MaxWidth(122));
            EditorGUILayout.EndHorizontal();
        }


        int categoryId = 0;
        void DrawCategoryPopup()
        {
            int selectedId = EditorGUILayout.Popup("Category", categoryId, m_Database.categories.ToArray());
            if (categoryId != selectedId)
            {
                categoryId = selectedId;

                // Reset category filters
                m_CategoryFilter = "";
                m_SelectedPopularityCollection = null;
                m_SelectedCollection = null;

                // Update filter
                if (categoryId != 0)
                {
                    m_CategoryFilter = m_Database.categories[categoryId];

                    if (m_CategoryFilter.Contains("Collections/"))
                    {
                        m_CategoryFilter = m_CategoryFilter.Replace("/All", "");
                        m_CategoryFilter = m_CategoryFilter.Replace("Collections/", "");
                        m_SelectedCollection = m_Database.m_Collections.Find(x => x.name == m_CategoryFilter);
                    }
                    else if (m_CategoryFilter.Contains("Popularity"))
                    {
                        m_CategoryFilter = m_CategoryFilter.Replace("/All", "");
                        m_CategoryFilter = m_CategoryFilter.Replace("Popularity/", "");
                        m_SelectedPopularityCollection = m_Database.m_Popularities.Find(x => x.name == m_CategoryFilter);
                    }
                    else
                    {
                        m_CategoryFilter = m_CategoryFilter.Replace("/All", "");
                        m_CategoryFilter = m_CategoryFilter.Replace("All", "");
                    }
                }

                UpdateDeviceSelectorList();
            }

        }

        int ratiosId = 0;
        void DrawRatiosPopup()
        {
            int selectedRatio = EditorGUILayout.Popup("Ratio", ratiosId, m_Database.ratios.ToArray());//, GUILayout.MaxWidth(100));
            if (ratiosId != selectedRatio)
            {
                ratiosId = selectedRatio;
                if (ratiosId != 0)
                {
                    m_RatioFilter = m_Database.ratios[ratiosId];
                }
                else
                {
                    m_RatioFilter = "";
                }
                UpdateDeviceSelectorList();
            }
        }

        int tagsId = 0;
        List<string> tags = new List<string>();
        void DrawTagsPopup()
        {
            tags.Clear();
            tags = TagDatabaseAsset.GetAllTags();
            tags.Insert(0, "All");
            int selectedTag = EditorGUILayout.Popup("Tags", tagsId, tags.ToArray());
            if (tagsId != selectedTag)
            {
                tagsId = selectedTag;
                if (tagsId != 0)
                {
                    m_TagFilter = tags[tagsId];
                }
                else
                {
                    m_TagFilter = "";
                }
                UpdateDeviceSelectorList();
            }
        }

        #endregion
    }
}
