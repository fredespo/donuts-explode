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

namespace BinaryCharm.SemanticColorPalette.Colorers {

    public abstract class SCP_AColorerEditor<T> : SCP_AColorerBaseEditor where T : struct {

        private SerializedProperty m_Colors;

        protected new void OnEnable() {
            base.OnEnable();
            
            m_Colors = serializedObject.FindProperty(
                nameof(SCP_AColorer<T>.m_colors)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            drawColorStructProperties(m_Colors);

            serializedObject.ApplyModifiedProperties();
        }

    }

    public abstract class SCP_AColorerEditor : SCP_AColorerBaseEditor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            drawColorProperties();
            serializedObject.ApplyModifiedProperties();
        }

    }

}
