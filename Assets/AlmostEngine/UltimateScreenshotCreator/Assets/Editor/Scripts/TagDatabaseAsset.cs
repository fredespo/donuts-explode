using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
    [System.Serializable]
    public class TagDatabaseAsset : ScriptableObject
    {
        [System.Serializable]
        public class TagData
        {
            public List<ScreenshotResolutionAsset> m_Data = new List<ScreenshotResolutionAsset>();
        }
        [System.Serializable]
        public class TagDatabaseData : SerializableDictionary<string, TagData> { }

        public TagDatabaseData m_Database = new TagDatabaseData();

        public static TagDatabaseAsset m_TagAsset;

        public static TagDatabaseAsset GetDatabase()
        {
            if (m_TagAsset == null)
            {
                m_TagAsset = AssetUtils.GetFirstOrCreate<TagDatabaseAsset>("TagDatabase");
            }
            return m_TagAsset;
        }


        public static List<string> GetAllTags()
        {
            return new List<string>(GetDatabase().m_Database.keys);
        }
        public static List<string> GetTags(ScreenshotResolutionAsset preset)
        {
            return GetDatabase().m_Database.keys.Where(x => GetDatabase().m_Database.ContainsKey(x) && GetDatabase().m_Database[x].m_Data.Contains(preset)).ToList();
        }

        public static string GetTagString(ScreenshotResolutionAsset preset)
        {
            var tags = GetTags(preset);
            var s = "";
            for (int i = 0; i < tags.Count; ++i)
            {
                s += tags[i];
                if (i != tags.Count - 1) { s += ", "; }

            }
            return s;
        }

        public static List<string> GetTags(List<ScreenshotResolutionAsset> presets)
        {
            List<string> tags = new List<string>();
            foreach (var preset in presets)
            {
                var ts = GetTags(preset);
                tags.AddRange(ts.Where(x => !tags.Contains(x)));
            }
            return tags;
        }

        public static bool HasTag(List<ScreenshotResolutionAsset> presets, string tag)
        {
            foreach (var preset in presets)
            {
                if (!(GetDatabase().m_Database.ContainsKey(tag) && GetDatabase().m_Database[tag].m_Data.Contains(preset)))
                {
                    return false;
                }
            }
            return true;
        }

        public static void AddTag(ScreenshotResolutionAsset preset, string tag)
        {
            // Create empty list for a new tag
            if (!GetDatabase().m_Database.ContainsKey(tag))
            {
                GetDatabase().m_Database.Add(tag, new TagData());
            }
            // Add tag to list
            GetDatabase().m_Database[tag].m_Data.Add(preset);
            // Remove tag if no preset have it
            GetDatabase().RemoveEmptyTags();
            // Update database
            EditorUtility.SetDirty(GetDatabase());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void RemoveEmptyTags()
        {
            foreach (var tag in m_Database.keys)
            {
                GetDatabase().m_Database[tag].m_Data.RemoveAll(x => x == null);
                if (GetDatabase().m_Database[tag].m_Data.Count == 0)
                {
                    GetDatabase().m_Database.Remove(tag);
                }
            }
        }

        public static void RemoveTag(ScreenshotResolutionAsset preset, string tag)
        {
            if (GetDatabase().m_Database.ContainsKey(tag))
            {
                // Remove tag from list
                GetDatabase().m_Database[tag].m_Data.Remove(preset);
                // Remove tag if no preset have it
                GetDatabase().RemoveEmptyTags();
                // Update database
                EditorUtility.SetDirty(GetDatabase());
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

    }
}

