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

using System.IO;
using System.Collections.Generic;

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

using BinaryCharm.Common.UI;

namespace BinaryCharm.SemanticColorPalette {

    [CustomEditor(typeof(SCP_Palette))]
    public class SCP_PaletteEditor : Editor {

        private SerializedProperty PaletteName;
        private SerializedProperty PaletteType;
        private SerializedProperty MainPalette;
        private SerializedProperty PaletteElements;

        private PaletteEditorCache m_rCache;
        private ReorderableList ColorDefsList;

        private const string sNEW_COLOR_NAME = "new color";
        private static readonly Color NEW_COLOR_VALUE = Color.magenta;
        
        private const bool bSHOW_IDs = false;

        private void OnEnable() {
            MainPalette = serializedObject.FindProperty(
                nameof(SCP_Palette.MainPalette)
            );
            PaletteType = serializedObject.FindProperty(
                nameof(SCP_Palette.Type)
            );
            PaletteName = serializedObject.FindProperty(
                nameof(SCP_Palette.Name)
            );
            PaletteElements = serializedObject.FindProperty(
                nameof(SCP_Palette.Elements)
            );

            ColorDefsList = setupColorDefsList();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // show readonly properties ---------------------------------------
            EditorGUIEx.DrawScriptField(serializedObject);

            // the palette name is always derived from the asset file name
            string sAssetPath = AssetDatabase.GetAssetPath(target);
            PaletteName.stringValue = Path.GetFileNameWithoutExtension(sAssetPath);
            EditorGUIEx.ReadonlyPropertyField(PaletteName);
            // ----------------------------------------------------------------

            // update cache (used in ColorDefsList callbacks) -----------------
            m_rCache = new PaletteEditorCache(target as SCP_Palette);
            // ----------------------------------------------------------------

            // show editable properties ---------------------------------------
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(PaletteType);
            if (EditorGUI.EndChangeCheck()) { // if changed palette type, rebuild ColorDefsList
                ColorDefsList = setupColorDefsList();
            }

            if (!isMainPalette()) {
                EditorGUILayout.PropertyField(MainPalette);
                if (needsUpdateFromMainPalette()) {
                    updateFromMainPalette();
                }
            }
            ColorDefsList.DoLayoutList();
            // ----------------------------------------------------------------

            // show error/warning if needed -----------------------------------
            eConfigState configState = getConfigState();
            switch (configState) {
                case eConfigState.invalid:
                    displayInvalidConfigurationError();
                    break;
                case eConfigState.incomplete:
                    // too annoying maybe, let's comment it out for now
                    // displayIncompleteConfigurationWarning();
                    break;
            }
            // ----------------------------------------------------------------

            serializedObject.ApplyModifiedProperties();
        }

        private bool isMainPalette() {
            SCP_Palette.eType configuredPaletteType = ((SCP_Palette.eType)PaletteType.enumValueIndex);
            return configuredPaletteType == SCP_Palette.eType.main;
        }

        private bool needsUpdateFromMainPalette() {
            SCP_Palette rMainPalette = ((SCP_Palette)MainPalette.objectReferenceValue);
            
            int iMainPaletteNumElems = rMainPalette == null ? 0 : rMainPalette.GetNumElems();
            int iNumElems = PaletteElements.arraySize;

            if (iNumElems != iMainPaletteNumElems) return true; // detected different number of entries

            for (int i = 0; i < iNumElems; ++i) {
                var rElem = PaletteElements.GetArrayElementAtIndex(i);

                var rElem_Id = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Id));
                int iElemId = rElem_Id.intValue;
                int iMainPaletteElemId = rMainPalette.Elements[i].Id;
                if (iElemId != iMainPaletteElemId) return true; // detected different entries (id)

                var rElem_Name = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Name));
                string sElemName = rElem_Name.stringValue;
                string sMainPaletteElemName = rMainPalette.Elements[i].Name;
                if (sElemName != sMainPaletteElemName) return true;  // detected different entries (name)
            }
            return false;
        }

        private void updateFromMainPalette() {
            SCP_Palette rMainPalette = ((SCP_Palette)MainPalette.objectReferenceValue);
            if (rMainPalette == null) return;

            int iNumElems = PaletteElements.arraySize;
            Dictionary<int, Color> rCurrValues = new Dictionary<int, Color>();
            for (int i = 0; i < iNumElems; ++i) {
                var rElem = PaletteElements.GetArrayElementAtIndex(i);
                var rElem_Id = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Id));
                var rElem_Color = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Color));
                rCurrValues[rElem_Id.intValue] = rElem_Color.colorValue;
            }

            int iMainPaletteNumElems = rMainPalette.GetNumElems();
            PaletteElements.arraySize = iMainPaletteNumElems;
            for (int i = 0; i < iMainPaletteNumElems; ++i) {
                var rElem = PaletteElements.GetArrayElementAtIndex(i);
                var rElem_Id = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Id));
                var rElem_Name = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Name));
                var rElem_Color = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Color));

                rElem_Id.intValue = rMainPalette.Elements[i].Id;
                rElem_Name.stringValue = rMainPalette.Elements[i].Name;

                Color color;
                if (!rCurrValues.TryGetValue(rElem_Id.intValue, out color)) {
                    color = NEW_COLOR_VALUE;
                }
                rElem_Color.colorValue = color;
            }
        }

        private enum eConfigState {
            valid,
            invalid,
            incomplete
        };

        private eConfigState getConfigState() {
            if (m_rCache.hasAnyInvalidNames()) return eConfigState.invalid;
            if (m_rCache.hasAnyDefaultColors()) return eConfigState.incomplete;
            return eConfigState.valid;
        }

        private void displayInvalidConfigurationError() {
            EditorGUIEx.DrawErrorLabel(
                "Invalid configuration!",
                "To be valid, a configuration must have:\n"
                + " - NO duplicate color names\n"
                + " - NO empty color names\n"
            );
        }

        private void displayIncompleteConfigurationWarning() {
            EditorGUIEx.DrawWarningLabel("Warning!",
                "Some colors are set to the default value " +
                "(the classic \"something is broken\" magenta), " +
                "did you forget to configure them?\n" +
                "Ignore this warning if you actually want a magenta entry."
            );
        }

        private ReorderableList setupColorDefsList() {
            ReorderableList rColorDefsList = new ReorderableList(serializedObject, PaletteElements) { 
                displayAdd = isMainPalette(),
                displayRemove = isMainPalette(),
                draggable = isMainPalette(),
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, PaletteElements.displayName),
                drawElementCallback = (rect, iIndex, bFocused, bActive) => {
                    var rElem = PaletteElements.GetArrayElementAtIndex(iIndex);
                    
                    var rElem_id = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Id));
                    var rElem_color = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Color));
                    var rElem_name = rElem.FindPropertyRelative(nameof(SCP_Palette.Element.Name));

                    float fFieldHeight = EditorGUIUtility.singleLineHeight;
                    float fFieldWidth = rect.width / (bSHOW_IDs ? 3f : 2f);
                    float fHorizOffsetAcc = 0f;

                    // index and colorId column -------------------------------
                    #pragma warning disable 0162
                    if (bSHOW_IDs) {
                        EditorGUI.LabelField(
                            new Rect(rect.x + fHorizOffsetAcc, rect.y, fFieldWidth, fFieldHeight),
                            iIndex.ToString() + " (colorId: " + rElem_id.intValue.ToString() + ")"
                        );
                        fHorizOffsetAcc += fFieldWidth;
                    }
                    #pragma warning restore 0162
                    // --------------------------------------------------------

                    // color name column  -------------------------------------
                    Rect colorNameRect = new Rect(rect.x + fHorizOffsetAcc, rect.y, fFieldWidth, fFieldHeight);
                    if (isMainPalette()) {
                        EditorGUIEx.DrawPossiblyInvalidPropertyField(
                            m_rCache.isNameInvalid(iIndex),
                            colorNameRect,
                            rElem_name,
                            GUIContent.none
                        );
                    } else {
                        EditorGUI.LabelField(
                            colorNameRect,
                            rElem_name.stringValue
                        );
                    }
                    fHorizOffsetAcc += fFieldWidth;
                    // --------------------------------------------------------

                    // color value column -------------------------------------
                    EditorGUI.PropertyField(
                        new Rect(rect.x + fHorizOffsetAcc, rect.y, fFieldWidth, fFieldHeight),
                        rElem_color, 
                        GUIContent.none
                    );
                    // --------------------------------------------------------

                },
                elementHeightCallback = iIndex => EditorGUIUtility.singleLineHeight,
                onAddCallback = rList => {
                    if (!isMainPalette()) return;
                    rList.serializedProperty.arraySize++;

                    int iNewElemIndex = rList.serializedProperty.arraySize - 1;

                    var rNewElem = rList.serializedProperty.GetArrayElementAtIndex(iNewElemIndex);
                    var rNewElem_Id = rNewElem.FindPropertyRelative(nameof(SCP_Palette.Element.Id));
                    var rNewElem_Name = rNewElem.FindPropertyRelative(nameof(SCP_Palette.Element.Name));
                    var rNewElem_Color = rNewElem.FindPropertyRelative(nameof(SCP_Palette.Element.Color));

                    rNewElem_Id.intValue = m_rCache.getFirstAvailableElemId();
                    rNewElem_Name.stringValue = sNEW_COLOR_NAME;
                    rNewElem_Color.colorValue = NEW_COLOR_VALUE;
                }
            };
            return rColorDefsList;
        }


        #region PaletteEditorCache private utility class ----------------------

        private class PaletteEditorCache {

            private readonly bool[] m_bInvalidNames;
            private readonly bool[] m_bDefaultColors;
            private readonly int m_iFirstAvailableElemId;

            private readonly bool m_bHasAnyInvalidNames;
            private readonly bool m_bHasAnyDefaultColors;

            public PaletteEditorCache(SCP_Palette rPalette) {

                m_bHasAnyInvalidNames = false;
                m_bHasAnyDefaultColors = false;

                int iNumElems = rPalette.GetNumElems();

                m_bInvalidNames = new bool[iNumElems];
                m_bDefaultColors = new bool[iNumElems];

                HashSet<int> rUsedInts = new HashSet<int>();
                Dictionary<string, int> rUsedNamesToIndex = new Dictionary<string, int>(iNumElems);

                for (int i = 0; i < iNumElems; ++i) {

                    // keep track of used ids
                    SCP_ColorId colorId = rPalette.GetColorIdByIndex(i);
                    rUsedInts.Add(colorId - 1);

                    // check names (empty? duplicate?)
                    string sElemName = rPalette.GetColorNameByIndex(i);

                    m_bInvalidNames[i] = string.IsNullOrEmpty(sElemName);
                    int iPreviouslyUsedIndex;
                    bool isDuplicate = rUsedNamesToIndex.TryGetValue(sElemName, out iPreviouslyUsedIndex);
                    if (isDuplicate) {
                        m_bInvalidNames[i] = true;
                        m_bInvalidNames[iPreviouslyUsedIndex] = true;
                    }
                    rUsedNamesToIndex[sElemName] = i;

                    // check Color values (default?)
                    Color elemColor = rPalette.GetColorByIndex(i); ;
                    m_bDefaultColors[i] = elemColor == NEW_COLOR_VALUE;

                    // update flags
                    if (m_bInvalidNames[i]) m_bHasAnyInvalidNames = true;
                    if (m_bDefaultColors[i]) m_bHasAnyDefaultColors = true;
                }

                // find first available elem id
                int iAvailableElemIdCandidate = 0;
                while (rUsedInts.Contains(iAvailableElemIdCandidate)) {
                    ++iAvailableElemIdCandidate;
                }
                m_iFirstAvailableElemId = iAvailableElemIdCandidate;
            }

            public bool isNameInvalid(int iIndex) {
                return m_bInvalidNames[iIndex];
            }

            public bool isColorDefault(int iIndex) {
                return m_bDefaultColors[iIndex];
            }

            public int getFirstAvailableElemId() {
                return m_iFirstAvailableElemId;
            }

            public bool hasAnyInvalidNames() {
                return m_bHasAnyInvalidNames;
            }

            public bool hasAnyDefaultColors() {
                return m_bHasAnyDefaultColors;
            }
        }

        #endregion ------------------------------------------------------------

    }

}
