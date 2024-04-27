/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2022 Binary Charm - All Rights Reserved
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@                        @@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@                           @@@@@@@@@@@@@@@@@@
             @@@@@@@@@   @@@@@@@@@@@  @@@@@        @@@@@@@@@@@@@@@
             @@@@@@@@@@@  @@@@@@@@@  @@@@@@@@@@       (@@@@@@@@@@@
             @@@@@@@@@@@@  @@@@@@@@& @@@@@@@@@@ @@@@     @@@@@@@@@
             @@@@@@@@@@@@@ @@@@@@@@@@ *@@@@@@@ @@@@@@@@@*   @@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*/

using System;

using UnityEngine;
using UnityEditor;

namespace BinaryCharm.Common.UI {

    public static class EditorGUIEx {
        public static Rect s_inspectorRect = Rect.zero;

        public static void DrawScriptField(SerializedObject rSerializedObject) {
            ReadonlyPropertyField(rSerializedObject.FindProperty("m_Script"));
        }

        public static void DrawSeparator() {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        public static void ReadonlyPropertyField(SerializedProperty rSerializedProperty) {
            using (new EditorGUI.DisabledScope(true)) {
                EditorGUILayout.PropertyField(rSerializedProperty);
            }
        }

        public static void DrawPossiblyInvalidPropertyField(
                bool bIsInvalid,
                Rect rect,
                SerializedProperty rSerializedProperty, 
                GUIContent label) {
            var guiColor = GUI.color;
            if (bIsInvalid) GUI.color = Color.red;
            try {
                EditorGUI.PropertyField(rect, rSerializedProperty, label);
            }
            catch (ExitGUIException rEx) {
                throw rEx;
            }
            catch (Exception rEx) { 
                Debug.LogError(rEx);
            }
            finally {
                GUI.color = guiColor;
            }
        }

        public static void DrawPossiblyInvalidPropertyField(
                bool bIsInvalid,
                SerializedProperty property,
                GUIContent label,
                params GUILayoutOption[] options) {
            var guiColor = GUI.color;
            if (bIsInvalid) GUI.color = Color.red;
            try {
                EditorGUILayout.PropertyField(property, label, options);
            } catch (ExitGUIException rEx) {
                throw rEx;
            } catch (Exception rEx) {
                Debug.LogError(rEx);
            } finally {
                GUI.color = guiColor;
            }
        }

        public static void DrawReadonlyColorField(Rect r, Color c) {
            float fAlphaIndicatorHeight = r.height * 0.2f;
            float fTopHeight = r.height - fAlphaIndicatorHeight;
            float fBottomHeight = r.height - fTopHeight;

            float fAlphaWhiteWidth = r.width * c.a;
            float fAlphaBlackWidth = r.width - fAlphaWhiteWidth;

            Rect top = new Rect(
                r.x, r.y,
                r.width, fTopHeight);
            Rect bottom_white = new Rect(
                r.x, r.y + fTopHeight, 
                fAlphaWhiteWidth, fBottomHeight);
            Rect bottom_black = new Rect(
                r.x + fAlphaWhiteWidth, r.y + fTopHeight, 
                fAlphaBlackWidth, fBottomHeight);

            EditorGUI.DrawRect(top, new Color(c.r, c.g, c.b));
            EditorGUI.DrawRect(bottom_white, Color.white);
            EditorGUI.DrawRect(bottom_black, Color.black);
        }

        public static void DrawWarningLabel(string sTitle, string sContent) {
            ColoredLabel(sTitle, Color.yellow);
            MultilineLabel(sContent);
        }

        public static void DrawErrorLabel(string sTitle, string sContent) {
            ColoredLabel(sTitle, Color.red);
            MultilineLabel(sContent);
        }

        public static void ColoredLabel(string sText, Color rColor) {
            Color prevColor = EditorStyles.label.normal.textColor;
            try {
                EditorStyles.label.normal.textColor = rColor;
                EditorGUILayout.LabelField(sText);
            }
            catch (ExitGUIException rEx) {
                throw rEx;
            }
            catch (Exception rEx) {
                Debug.LogError(rEx);
            }
            finally {
                EditorStyles.label.normal.textColor = prevColor;
            }
        }

        public static void MultilineLabel(string sText) {
            GUILayout.Label(sText, EditorStyles.wordWrappedLabel);
            //bool bPrev = EditorStyles.label.wordWrap;
            //EditorStyles.label.wordWrap = true;
            //EditorGUILayout.LabelField(sText);
            //EditorStyles.label.wordWrap = bPrev;
        }

    }

}
