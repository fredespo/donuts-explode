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

#if TEXTMESHPRO_PRESENT

using UnityEditor;
using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Colorers.TMPro {

    [CustomEditor(typeof(SCP_TMP_RichTextColorer))]
    public class SCP_TMP_RichTextColorerEditor : SCP_AColorerBaseEditor {

        private SerializedProperty m_UnprocessedText;

        protected override void OnEnable() {
            base.OnEnable();
            m_UnprocessedText = serializedObject.FindProperty(
                nameof(SCP_TMP_RichTextColorer.m_sUnprocessedText)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Text");

            GUIStyle style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;
            m_UnprocessedText.stringValue = EditorGUILayout.TextArea(
                m_UnprocessedText.stringValue, style, GUILayout.Height(80)
            );

            serializedObject.ApplyModifiedProperties();
        }

    }

}

#endif
