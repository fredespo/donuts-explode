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
using UnityEditorInternal;
using UnityEngine;

using BinaryCharm.Common.UI;

namespace BinaryCharm.SemanticColorPalette {

    [CustomEditor(typeof(SCP_PaletteProvider)), CanEditMultipleObjects]
    public class SCP_PaletteProviderEditor : Editor {

        private SerializedProperty m_Palettes;
        private SerializedProperty m_ActivePaletteIndex;

        private ReorderableList m_PalettesList;

        private const string sINVALID_CONFIG_ERROR_LABEL =
            "Invalid configuration!";

        private const string sINVALID_CONFIG_ERROR_TEXT =
            "To be valid, a configuration must have:\n"
          + " - at least one palette reference (press \"+\") to add one\n"
          + " - NO empty palette references (shown as \"None (SCP_Palette)\")\n"
          + " - NO more than one \"main\" palette\n"
          + " - NO variants of different \"main\" palettes\n"
        ;

        protected void OnEnable() {
            m_Palettes = serializedObject.FindProperty(
                nameof(SCP_PaletteProvider.m_rPalettes)
            );
            m_ActivePaletteIndex = serializedObject.FindProperty(
                nameof(SCP_PaletteProvider.m_iActivePaletteIndex)
            );

            m_PalettesList = setupPalettesList();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUIEx.DrawScriptField(serializedObject);

            m_ActivePaletteIndex.intValue =
                m_Palettes.arraySize == 0 ? 
                -1 :
                Mathf.Clamp(m_ActivePaletteIndex.intValue, 0, m_Palettes.arraySize - 1);

            m_PalettesList.DoLayoutList();

            if (!((SCP_PaletteProvider)target).isConfigurationValid()) {
                EditorGUIEx.DrawErrorLabel(sINVALID_CONFIG_ERROR_LABEL, sINVALID_CONFIG_ERROR_TEXT);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private ReorderableList setupPalettesList() {
            ReorderableList rPaletteList = new ReorderableList(serializedObject, m_Palettes) {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Handled Color Palettes"),
                drawElementCallback = (rect, iIndex, bFocused, bActive) => {
                    var rPaletteProp = m_Palettes.GetArrayElementAtIndex(iIndex);
                    SCP_Palette rPalette = rPaletteProp.objectReferenceValue as SCP_Palette;

                    const float fIdFieldWidth = 28f;
                    const float fRadioFieldWidth = 28f;
                    
                    const float fFixedFieldWidthTotal = fIdFieldWidth + fRadioFieldWidth;

                    float fFieldHeight = EditorGUIUtility.singleLineHeight;
                    float fVariableFieldWidth = (rect.width - fFixedFieldWidthTotal) / 2f;
                    float fHorizOffsetAcc = 0f;
                    
                    EditorGUI.LabelField(
                        new Rect(rect.x + fHorizOffsetAcc, rect.y, fIdFieldWidth, fFieldHeight),
                        string.Format("{0,3}", iIndex)
                    );
                    fHorizOffsetAcc += fIdFieldWidth;

                    bool bIsActivePalette = iIndex == m_ActivePaletteIndex.intValue;
                    if (GUI.Toggle(
                            new Rect(rect.x + fHorizOffsetAcc, rect.y, fRadioFieldWidth, fFieldHeight),
                            bIsActivePalette, "", (GUIStyle)"Radio")) {
                        m_ActivePaletteIndex.intValue = iIndex;
                        serializedObject.ApplyModifiedProperties();
                    }
                    fHorizOffsetAcc += fRadioFieldWidth;

                    bool bIsPaletteSet = rPalette != null;
                    EditorGUIEx.DrawPossiblyInvalidPropertyField(
                        !bIsPaletteSet,
                        new Rect(rect.x + fHorizOffsetAcc, rect.y, fVariableFieldWidth, fFieldHeight),
                        rPaletteProp,
                        GUIContent.none
                    );
                    fHorizOffsetAcc += fVariableFieldWidth;

                    if (bIsPaletteSet) {
                        string sPaletteInfo = " <- " + rPalette.Type.ToString();
                        if (rPalette.Type == SCP_Palette.eType.variant) {
                            SCP_Palette rMainPalette = rPalette.GetMainPalette();
                            sPaletteInfo += " of " + rMainPalette.Name;
                        }
                        EditorGUI.LabelField(
                            new Rect(rect.x + fHorizOffsetAcc, rect.y, fVariableFieldWidth, fFieldHeight),
                            sPaletteInfo
                        );
                    }
                },
                elementHeightCallback = iIndex => EditorGUIUtility.singleLineHeight,
                onAddCallback = rList => {
                    rList.serializedProperty.arraySize++;

                    int iNewElemIndex = rList.serializedProperty.arraySize - 1;
                    var rNewElem = rList.serializedProperty.GetArrayElementAtIndex(iNewElemIndex);
                    rNewElem.objectReferenceValue = null;
                },
                onRemoveCallback = rList => {
                    for (int i = rList.index; i < rList.serializedProperty.arraySize - 1; ++i) {
                        var rElem = rList.serializedProperty.GetArrayElementAtIndex(i);
                        var rNextElem = rList.serializedProperty.GetArrayElementAtIndex(i+1);
                        rElem.objectReferenceValue = rNextElem.objectReferenceValue;
                    }
                    rList.serializedProperty.arraySize--;
                    if (m_ActivePaletteIndex.intValue >= rList.index) {
                        --m_ActivePaletteIndex.intValue;
                    }
                }
            };
            return rPaletteList;
        }

    }

}
