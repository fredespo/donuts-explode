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

using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    public abstract class SCP_AMaterialColorerEditor<T> : SCP_AMaterialColorerBaseEditor where T : struct {

        private SerializedProperty m_Colors;

        protected new void OnEnable() {
            base.OnEnable();

            m_Colors = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorer<T>.m_color)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            drawColorStructProperties(m_Colors);

            serializedObject.ApplyModifiedProperties();
        }

    }

    public abstract class SCP_AMaterialColorerEditor : SCP_AMaterialColorerBaseEditor {

        private SerializedProperty m_Colors;

        protected new void OnEnable() {
            base.OnEnable();

            m_Colors = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorer.m_colors)
            );
        }

        protected void updateAndDisplayColorProperties() {
            updateMaterialColorPropertiesIds();
            drawMaterialColorProperties();
        }

        private void updateMaterialColorPropertiesIds() {
            SCP_AMaterialColorerBase rMC = (SCP_AMaterialColorerBase)target;

            List<string> rColorPropsNamesList = new List<string>();
            Material rCurrMat = rMC.getSharedMaterialImpl();
            if (rCurrMat != null) {
                Shader rShader = rCurrMat.shader;
                int iNumProps = ShaderUtil.GetPropertyCount(rShader);
                for (int i = 0; i < iNumProps; ++i) {
                    ShaderUtil.ShaderPropertyType propType = ShaderUtil.GetPropertyType(rShader, i);
                    if (propType == ShaderUtil.ShaderPropertyType.Color) {
                        string sPropName = ShaderUtil.GetPropertyName(rShader, i);
                        rColorPropsNamesList.Add(sPropName);
                    }
                }
            }

            if (m_Colors.arraySize != rColorPropsNamesList.Count) {
                m_Colors.arraySize = rColorPropsNamesList.Count;
            }
            for (int i = 0; i < rColorPropsNamesList.Count; ++i) {
                SerializedProperty rElem = m_Colors.GetArrayElementAtIndex(i);
                SerializedProperty rElemName = rElem.FindPropertyRelative(
                    nameof(SCP_MaterialColorDef.m_sShaderPropName)
                );
                SerializedProperty rElemValue = rElem.FindPropertyRelative(
                    nameof(SCP_MaterialColorDef.m_colorId)
                );
                if (rElemName.stringValue != rColorPropsNamesList[i]) {
                    rElemName.stringValue = rColorPropsNamesList[i];
                }
            }
        }

        private void drawMaterialColorProperties() {
            ColorerBaseEditorCache rCache = getCache();
            float fFieldWidth = getColorSelectorWidth();

            for (int i = 0; i < m_Colors.arraySize; ++i) {
                var rElem = m_Colors.GetArrayElementAtIndex(i);

                SerializedProperty rElemName = rElem.FindPropertyRelative(
                    nameof(SCP_MaterialColorDef.m_sShaderPropName)
                );
                SerializedProperty rElemValue = rElem.FindPropertyRelative(
                    nameof(SCP_MaterialColorDef.m_colorId)
                );

                drawColorSelectorContent(rElemName.stringValue, rElemValue, rCache, fFieldWidth);
            }
        }

    }

}
