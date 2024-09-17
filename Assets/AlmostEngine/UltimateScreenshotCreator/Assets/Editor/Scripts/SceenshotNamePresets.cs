using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace AlmostEngine.Screenshot
{
    [InitializeOnLoad]
    public class ScreenshotNamePresets
    {
        public class NamePreset
        {
            public string m_Path;
            public string m_Description;

            public NamePreset(string path = "", string description = "")
            {
                m_Path = path;
                m_Description = description;
            }
        };

        public static List<NamePreset> m_NamePresets = new List<NamePreset>();

        static ScreenshotNamePresets()
        {
            Init();
        }

        public static void Init()
        {
            m_NamePresets.Clear();

            m_NamePresets.Add(new NamePreset("{width}x{height}-screenshot", "Default"));
            m_NamePresets.Add(new NamePreset("{width}x{height}/screenshot", "Screenshots grouped by resolutions in separate folders"));
            m_NamePresets.Add(new NamePreset("{ratio}/screenshot", "Screenshots grouped by ratio in separate folders"));
            m_NamePresets.Add(new NamePreset("{category}/{name}", "Screenshots grouped by categories in separate folders"));
            m_NamePresets.Add(new NamePreset("{name}", "Resolution name"));
            m_NamePresets.Add(new NamePreset("{width}x{height}-{scale} {name} {orientation}", "Resolution infos"));
            m_NamePresets.Add(new NamePreset("{year}-{month}-{day}_{hour}h{minute}_{second}", "Current time"));
            m_NamePresets.Add(new NamePreset("{batch}/{composer}-{width}x{height}", "Composition"));
            m_NamePresets.Add(new NamePreset("{batch}/{width}x{height}", "Batch"));
        }
        public static string m_Presets = "{width}" + ", "
                        + "{height}" + ", "
                        + "{ratio}" + ", "
                        + "{orientation}" + ", "
                        + "{scale}" + ", "
                        + "{name}" + ", "
                        + "{category}" + ", " + "\n"
                        + "{year}, {month}, {day}, {hour}, {minute}, {second}" + ", " + "\n"
                        + "{time}, {unscaledTime}, {deltaTime}, {unscaledDeltaTime}, {frameCount} " + ", " + "\n"
                        + "{burstFrameCount}" + ", "
                        + "{batch}, {composer}" + ", " + "\n"
                        + "{scene}, {activeObject}, {gameObjects}" + ", " + "\n"
                        + "{companyName}, {productName}, {version}";
    }
}
