using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AlmostEngine.Screenshot
{
    public class ScreenshotPresetDatabase 
    {

        public List<ScreenshotResolutionAsset> m_Presets = new List<ScreenshotResolutionAsset>();
        public List<PopularityPresetAsset> m_Popularities = new List<PopularityPresetAsset>();
        public List<PresetCollectionAsset> m_Collections = new List<PresetCollectionAsset>();

        public List<string> categories = new List<string>();
        public List<string> ratios = new List<string>();


        

        public void InitPresets()
        {
            EditorUtility.DisplayProgressBar("Loading presets", "", 0);

            // Load all presets
            m_Presets = AssetUtils.LoadAll<ScreenshotResolutionAsset>();


            EditorUtility.DisplayProgressBar("Loading collections", "", 0.9f);

            // Load all collections
            m_Popularities = AssetUtils.LoadAll<PopularityPresetAsset>();
            m_Collections = AssetUtils.LoadAll<PresetCollectionAsset>();

            // Update categories and ratios
            InitNamesList();
            InitCategoryList();
            InitRatioList();

            EditorUtility.ClearProgressBar();
        }

        
        void InitNamesList()
        {
            // Parse names
            foreach (var preset in m_Presets)
            {
                preset.m_Resolution.m_ResolutionName = preset.name;
            }
        }
        void InitCategoryList()
        {
            categories.Clear();
            Dictionary<string, bool> catTable = new Dictionary<string, bool>();


            // Parse and register category to table
            foreach (var preset in m_Presets)
            {
                preset.m_Resolution.m_Category = ScreenshotResolutionPresets.ParseCategory(preset);
                var c = preset.m_Resolution.m_Category;
                if (!c.Contains("Popularity") && !c.Contains("Collections"))
                {
                    for (int i = 0; i < c.Length; ++i)
                    {
                        if (c[i] == '/')
                        {
                            catTable[c.Substring(0, i) + "/All"] = true;
                        }
                    }
                }
                catTable[preset.m_Resolution.m_Category] = true;
            }

            // Popularity presets
            foreach (PopularityPresetAsset popularity in m_Popularities)
            {
                catTable["Popularity/" + popularity.name] = true;
            }

            // Collections presets
            foreach (PresetCollectionAsset collection in m_Collections)
            {
                catTable["Collections/" + collection.name] = true;
            }

            // Update list
            categories = catTable.Keys.ToList();
            categories.Insert(0, "All");
        }

        void InitRatioList()
        {
            ratios.Clear();
            Dictionary<string, bool> ratioTable = new Dictionary<string, bool>();

            // Parse and register ratio to table
            foreach (var preset in m_Presets)
            {

                preset.m_Resolution.UpdateRatio();
                ratioTable[preset.m_Resolution.m_Ratio] = true;
            }

            // Update list
            ratios = ratioTable.Keys.ToList().OrderBy(x => int.Parse(x.Split(':')[0])).ToList();
            ratios.Insert(0, "All");
        }



    }
}

