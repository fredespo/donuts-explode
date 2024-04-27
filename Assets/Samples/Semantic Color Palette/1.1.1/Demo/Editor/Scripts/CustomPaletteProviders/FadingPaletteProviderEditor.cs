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

using BinaryCharm.SemanticColorPalette;

namespace BinaryCharm.Samples.SemanticColorPalette.CustomProviders {

    [CustomEditor(typeof(FadingPaletteProvider)), CanEditMultipleObjects]
    public class FadingPaletteProviderEditor : SCP_PaletteProviderEditor {

        private SerializedProperty m_FadeDuration;

        protected new void OnEnable() {
            base.OnEnable();
            m_FadeDuration = serializedObject.FindProperty(
                nameof(FadingPaletteProvider.m_fFadeDuration)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(m_FadeDuration);
            serializedObject.ApplyModifiedProperties();
        }

    }

}
