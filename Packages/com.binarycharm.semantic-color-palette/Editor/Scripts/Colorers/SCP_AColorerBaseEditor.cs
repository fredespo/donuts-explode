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
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using BinaryCharm.Common.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    public class SCP_AColorerBaseEditor : Editor {

        private SerializedProperty m_ColorPaletteProvider;
        private SerializedProperty m_ForcePaletteVariant;
        private SerializedProperty m_ForcedPaletteVariantIndex;

        private const string sLABEL_PaletteProvider = "Palette Provider";
        private const string sLABEL_ForcePaletteVariant = "Force Palette Variant";

        private const string sSINGLE_PALETTE_WARNING =
            "Single palette currently available, variant selection greyed out.";

        private const string sINVALID_CONFIG_ERROR_LABEL =
            "Invalid configuration!";

        private const string sINVALID_CONFIG_ERROR_TEXT =
            "You have to select a properly configured Palette Provider.";

        private const string sDO_NOT_APPLY = "<DO NOT APPLY>";

        protected virtual void OnEnable() {
            m_ColorPaletteProvider = serializedObject.FindProperty(
                nameof(SCP_AColorerBase.m_rPaletteProvider)
            );
            m_ForcePaletteVariant = serializedObject.FindProperty(
                nameof(SCP_AColorerBase.m_bIsActivePaletteIndexOverridden)
            );
            m_ForcedPaletteVariantIndex = serializedObject.FindProperty(
                nameof(SCP_AColorerBase.m_iActivePaletteIndexOverride)
            );
        }

        public override void OnInspectorGUI() {
            EditorGUIEx.DrawScriptField(serializedObject);

            serializedObject.Update();

            //EditorGUILayout.PropertyField(m_ColorPaletteProvider, new GUIContent(sLABEL_PaletteProvider));

            EditorGUIEx.DrawPossiblyInvalidPropertyField(
                m_ColorPaletteProvider.objectReferenceValue == null,
                m_ColorPaletteProvider,
                new GUIContent(sLABEL_PaletteProvider)
            );

            if (!((SCP_AColorerBase)target).isConfigurationValid()) {
                EditorGUIEx.DrawErrorLabel(sINVALID_CONFIG_ERROR_LABEL, sINVALID_CONFIG_ERROR_TEXT);
            } else {
                drawPaletteVariantSelector();
            }

            serializedObject.ApplyModifiedProperties();
            
            EditorGUIEx.DrawSeparator();
        }

        // --------------------------------------------------------------------
        // hacky way to fetch the custom inspector width
        private Rect m_rInspectorRect;
        private float getInspectorWidth() {
            GUILayout.Label("", GUILayout.MaxHeight(0));
            if (Event.current.type == EventType.Repaint) {
                m_rInspectorRect = GUILayoutUtility.GetLastRect();
            }
            return m_rInspectorRect.width;
        }

        protected float getColorSelectorWidth() {
            float fFieldWidth = 0.5f * (
                getInspectorWidth() - EditorGUIUtility.labelWidth
            );
            return fFieldWidth;
        }
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        protected ColorerBaseEditorCache getCache() {
            SCP_PaletteProvider rProvider = (SCP_PaletteProvider)m_ColorPaletteProvider.objectReferenceValue;
            ColorerBaseEditorCache rCache = null;

            if (rProvider != null && rProvider.GetNumPalettes() > 0) {
                int iPaletteIdx = m_ForcePaletteVariant.boolValue ?
                    m_ForcedPaletteVariantIndex.intValue :
                    rProvider.GetActivePaletteIndex();
                iPaletteIdx = Mathf.Clamp(iPaletteIdx, 0, rProvider.GetNumPalettes() - 1);
                m_ForcedPaletteVariantIndex.intValue = iPaletteIdx;
                SCP_Palette rPalette = rProvider.GetPaletteByIndex(iPaletteIdx);
                if (rPalette != null) {
                    rCache = new ColorerBaseEditorCache(rPalette);
                }
            }
            return rCache;
        }
        // --------------------------------------------------------------------

        private void drawPaletteVariantSelector() {
            SCP_PaletteProvider rProvider = (SCP_PaletteProvider)m_ColorPaletteProvider.objectReferenceValue;
            if (rProvider == null) return;

            int iNumPalettes = rProvider.GetNumPalettes();
            string[] rPaletteOptions = new string[iNumPalettes];
            for (int i = 0; i < iNumPalettes; ++i) {
                SCP_Palette rPalette = rProvider.GetPaletteByIndex(i);
                rPaletteOptions[i] = rPalette != null ? rPalette.Name : "";
            }

            // ----------------------------------------------------------------
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_ForcePaletteVariant, new GUIContent(sLABEL_ForcePaletteVariant));

            int iPopupSelection = m_ForcePaletteVariant.boolValue ?
                m_ForcedPaletteVariantIndex.intValue :
                rProvider.GetActivePaletteIndex();

            bool bVariantSelectionDisabled = 
                !m_ForcePaletteVariant.boolValue || rPaletteOptions.Length == 1;

            using (new EditorGUI.DisabledScope(bVariantSelectionDisabled)) {
                int iSelectedVariant = EditorGUILayout.Popup(
                    iPopupSelection,
                    rPaletteOptions,
                    GUILayout.ExpandWidth(true)
                );

                if (m_ForcePaletteVariant.boolValue) {
                    m_ForcedPaletteVariantIndex.intValue = iSelectedVariant;
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            // ----------------------------------------------------------------

            if (m_ForcePaletteVariant.boolValue && rPaletteOptions.Length == 1) {
                TextAnchor prev = EditorStyles.label.alignment;
                EditorStyles.label.alignment = TextAnchor.MiddleRight;
                EditorGUIEx.MultilineLabel(sSINGLE_PALETTE_WARNING);
                EditorStyles.label.alignment = prev;
            }
        }

        private List<SerializedProperty> getColorProperties() {
            List<SerializedProperty> rProps = new List<SerializedProperty>();
            var rIt = serializedObject.GetIterator();
            bool bFirstLevel = true;
            while (rIt.Next(bFirstLevel)) {
                if (rIt.type == nameof(SCP_ColorId)) {
                    rProps.Add(rIt.Copy());
                }
                bFirstLevel = false;
            }
            return rProps;
        }

        internal void drawColorProperties() {
            ColorerBaseEditorCache rCache = getCache();
            float fFieldWidth = getColorSelectorWidth();

            List<SerializedProperty> rProps = getColorProperties();
            foreach (SerializedProperty rProp in rProps) {
                drawColorSelectorContent(rProp.displayName, rProp, rCache, fFieldWidth);
            }
        }

        internal void drawColorStructProperties(SerializedProperty rStructProp) {
            ColorerBaseEditorCache rCache = getCache();
            float fFieldWidth = getColorSelectorWidth();

            List<SerializedProperty> rColorPropsArray = new List<SerializedProperty>();
            Dictionary<SerializedProperty, string> rColorPropToLabelMap = new Dictionary<SerializedProperty, string>();
            
            var rStructPropCopy = rStructProp.Copy();
            int iStructDepth = rStructPropCopy.depth;
            rStructPropCopy.NextVisible(true); // enter struct
            string sStructName = rStructProp.propertyPath;
            int iLastDepth = iStructDepth;
            Stack<string> sLabelFragmentsStack = new Stack<string>();
            do {
                if (rStructPropCopy.depth > iStructDepth) {
                    if (rStructPropCopy.depth > iLastDepth) {
                        sLabelFragmentsStack.Push(rStructPropCopy.displayName);
                        iLastDepth = rStructPropCopy.depth;
                    } else if (rStructPropCopy.depth < iLastDepth) {
                        while (iLastDepth > rStructPropCopy.depth) {
                            sLabelFragmentsStack.Pop();
                            --iLastDepth;
                        }
                    } 
                    if (sLabelFragmentsStack.Peek() != rStructPropCopy.displayName) {
                        sLabelFragmentsStack.Pop();
                        sLabelFragmentsStack.Push(rStructPropCopy.displayName);
                    }
                }
                if (rStructPropCopy.type == nameof(SCP_ColorId)) {
                    SerializedProperty rPropCopy = rStructPropCopy.Copy();
                    rColorPropsArray.Add(rPropCopy);
                    string[] sStackReverse = sLabelFragmentsStack.ToArray();
                    Array.Reverse(sStackReverse);
                    rColorPropToLabelMap.Add(rPropCopy, string.Join(".", sStackReverse));
                }
            } while (rStructPropCopy.NextVisible(rStructPropCopy.depth > iStructDepth));

            //string sLabel = bShowStructName ? rStructProp.displayName + "." : "";
            string sLabel = "";
            for (int i = 0; i < rColorPropsArray.Count; ++i) {
                var rColorProp = rColorPropsArray[i];
                string sDisplayName = sLabel + rColorPropToLabelMap[rColorProp];
                drawColorSelectorContent(sDisplayName, rColorProp, rCache, fFieldWidth);
            }
        }

        protected void drawColorSelectorContent(
                string sDisplayName,
                SerializedProperty rProp,
                ColorerBaseEditorCache rCache,
                float fFieldWidth) {
            EditorGUILayout.BeginHorizontal();

            SerializedProperty rColorProp = rProp.FindPropertyRelative(nameof(SCP_ColorId.m_id));
            int iCurrOptionIndex = 0;
            string[] rAvailableOptions = new string[] { };
            if (rCache != null && rColorProp != null) {
                iCurrOptionIndex =  rCache.getOptionIndexByColorId(rColorProp.intValue);
                rAvailableOptions = rCache.getColorSelectorOptions();
            }

            int iSelected = EditorGUILayout.Popup(
                new GUIContent(sDisplayName),
                iCurrOptionIndex,
                rAvailableOptions,
                GUILayout.Width(fFieldWidth + EditorGUIUtility.labelWidth)
            );

            if (rCache != null && rColorProp != null) {
                SCP_ColorId colorId = rCache.getColorIdByOptionIndex(iSelected);
                rColorProp.intValue = colorId;

                if (colorId != SCP_ColorId.DO_NOT_APPLY) {
                    Color selectedColor = rCache.getColor(colorId);

                    //EditorGUILayout.ColorField(rColor, GUILayout.Width(fFieldWidth)); 
                    // ...but we don't want the color to be editable :(
                    // so, as a workaround...

                    Rect colorPopupRect = GUILayoutUtility.GetLastRect();
                    float fSwatchX = colorPopupRect.x + colorPopupRect.width;
                    float fSwatchY = colorPopupRect.y + 1; // +1: looks better
                    float fSwatchW = fFieldWidth;
                    float fSwatchH = EditorGUIUtility.singleLineHeight - 2; // -2: looks better

                    Rect colorSwatchRect = new Rect(fSwatchX, fSwatchY, fSwatchW, fSwatchH);
                    EditorGUIEx.DrawReadonlyColorField(colorSwatchRect, selectedColor);
                }
            }

            EditorGUILayout.EndHorizontal();
        }


        #region ColorerBaseEditorCache private utility class ------------------

        protected class ColorerBaseEditorCache {
            private readonly string[] m_rIndexToString;
            private readonly int[] m_rIndexToColorId;
            private readonly int[] m_rColorIdToIndex;

            private SCP_Palette m_rPalette;

            public ColorerBaseEditorCache(SCP_Palette rPalette) {
                m_rPalette = rPalette;

                int iNumElems = rPalette.GetNumElems();
                int iMaxColorId = rPalette.getMaxId();

                m_rIndexToString = new string[iNumElems + 1];
                m_rIndexToColorId = new int[iNumElems + 1];
                m_rColorIdToIndex = new int[iMaxColorId + 2];

                m_rIndexToString[0] = sDO_NOT_APPLY;
                m_rIndexToColorId[0] = SCP_ColorId.DO_NOT_APPLY;
                m_rColorIdToIndex[0] = 0;

                for(int i = 0; i < iNumElems; ++i) {
                    int iIndex = i + 1;

                    SCP_ColorId colorId = rPalette.GetColorIdByIndex(i);
                    m_rIndexToString[iIndex] = rPalette.GetColorNameByIndex(i);
                    m_rIndexToColorId[iIndex] = colorId;
                    m_rColorIdToIndex[colorId] = iIndex;
                }

            }

            internal SCP_ColorId getColorIdByOptionIndex(int iIndex) {
                return m_rIndexToColorId[iIndex];
            }

            internal int getOptionIndexByColorId(SCP_ColorId colorId) {
                if (colorId < 0 || colorId > m_rColorIdToIndex.Length - 1) return 0;
                return m_rColorIdToIndex[colorId];
            }

            internal string[] getColorSelectorOptions() {
                return m_rIndexToString;
            }
            internal Color getColor(SCP_ColorId colorId) {
                return m_rPalette.GetColor(colorId);
            }

        }
        #endregion ------------------------------------------------------------

    }

}
