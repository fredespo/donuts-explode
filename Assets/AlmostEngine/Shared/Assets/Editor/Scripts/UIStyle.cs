using UnityEngine;
using UnityEditor;

namespace AlmostEngine
{
    public class UIStyle
    {

        static GUIStyle m_CenteredGreyTextStyle;

        public static GUIStyle centeredGreyTextStyle
        {
            get
            {
                if (m_CenteredGreyTextStyle == null)
                {
                    m_CenteredGreyTextStyle = new GUIStyle();
                    m_CenteredGreyTextStyle.wordWrap = true;
                    m_CenteredGreyTextStyle.alignment = TextAnchor.MiddleCenter;
                    m_CenteredGreyTextStyle.fontSize = 10;
                    m_CenteredGreyTextStyle.normal.textColor = Color.gray;
                }
                return m_CenteredGreyTextStyle;
            }
        }

        public static void DrawUILine(int thickness = 1, int padding = 10)
        {
            Color col = new Color(0, 0, 0, 0.5f);
            DrawUILine(col, thickness, padding);
        }

        public static void DrawUILine(Color color, int thickness = 1, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

    }
}

