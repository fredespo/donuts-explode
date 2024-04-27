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

    [CustomEditor(typeof(ShadeImageColorer)), CanEditMultipleObjects]
    public class ShadeImageColorerEditor : SCP_AColorerEditor {

        private SerializedProperty m_ShadeAmount;

        protected new void OnEnable() {
            base.OnEnable();
            m_ShadeAmount = serializedObject.FindProperty(
                nameof(ShadeImageColorer.m_fShadeAmount)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_ShadeAmount, new GUIContent("Shade Amount"));
            serializedObject.ApplyModifiedProperties();
        }

    }

}
