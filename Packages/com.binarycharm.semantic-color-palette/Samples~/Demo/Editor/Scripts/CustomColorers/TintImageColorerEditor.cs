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

using UnityEditor;
using UnityEngine;

using BinaryCharm.SemanticColorPalette.Colorers;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomColorers {

    [CustomEditor(typeof(TintImageColorer)), CanEditMultipleObjects]
    public class TintImageColorerEditor : SCP_AColorerEditor {

        private SerializedProperty m_TintAmount;

        protected new void OnEnable() {
            base.OnEnable();
            m_TintAmount = serializedObject.FindProperty(
                nameof(TintImageColorer.m_fTintAmount)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_TintAmount, new GUIContent("Tint Amount"));
            serializedObject.ApplyModifiedProperties();
        }

    }

}
