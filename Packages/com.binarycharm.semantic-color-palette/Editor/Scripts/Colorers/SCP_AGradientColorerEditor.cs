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

    public abstract class SCP_AGradientColorerEditor : SCP_AColorerBaseEditor {

        private SerializedProperty m_ColorsArray;

        private string[] m_rGradientColorsLabels;

        protected abstract string getComponentName();

        protected new void OnEnable() {
            base.OnEnable();

            m_ColorsArray = serializedObject.FindProperty(
                nameof(SCP_AGradientColorer.m_gradientColors)
            );

            m_rGradientColorsLabels = new string[m_ColorsArray.arraySize];
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUIEx.MultilineLabel(
                "Define some dummy color keys by using the Gradient editor " +
                "in the " + getComponentName() + " inspector, " +
                "and the number of palette color identifiers here will be " +
                "consequently adjusted."
            );
            
            updateProps();
            drawGradientColorProperties();

            serializedObject.ApplyModifiedProperties();

            EditorGUIEx.DrawWarningLabel("Warning!",
                "After you select the desired palette color identifiers, " +
                "the color gradient preview in the " + getComponentName() +
                " component will not update until you click on it..."
            );
        }

        private void updateProps() {
            SCP_AGradientColorer rTarget = (SCP_AGradientColorer)target;

            Gradient rCurrGrad = rTarget.getCurrGradient();
            int iPrevSize = m_ColorsArray.arraySize;
            int iNewSize = rCurrGrad.colorKeys.Length;

            Func<float, string> getLabel = (float v) => {
                return string.Format("Color at {0:0.#} %", (v * 100f));
            };

            if (iNewSize != iPrevSize || iNewSize != m_rGradientColorsLabels.Length) {
                Dictionary<string, SCP_ColorId> rPrevSetup = new Dictionary<string, SCP_ColorId>();
                for(int i = 0; i < iPrevSize; ++i) {
                    SerializedProperty rProp = m_ColorsArray.GetArrayElementAtIndex(i);
                    var rColorIdProp = rProp.FindPropertyRelative(nameof(SCP_ColorId.m_id));
                    rPrevSetup.Add(m_rGradientColorsLabels[i], rColorIdProp.intValue);
                }

                m_ColorsArray.arraySize = iNewSize;
                m_rGradientColorsLabels = new string[iNewSize];

                for (int i = 0; i < iNewSize; ++i) {
                    float fKeyTime = rCurrGrad.colorKeys[i].time;
                    SCP_ColorId newColorId;
                    if (!rPrevSetup.TryGetValue(getLabel(fKeyTime), out newColorId)) {
                        newColorId = SCP_ColorId.DO_NOT_APPLY;
                    }
                    SerializedProperty rProp = m_ColorsArray.GetArrayElementAtIndex(i);
                    var rNewColorIdProp = rProp.FindPropertyRelative(nameof(SCP_ColorId.m_id));
                    rNewColorIdProp.intValue = newColorId;
                }
            }
            for (int i = 0; i < rCurrGrad.colorKeys.Length; ++i) {
                m_rGradientColorsLabels[i] = getLabel(rCurrGrad.colorKeys[i].time);
            }
        }

        private void drawGradientColorProperties() {
            ColorerBaseEditorCache rCache = getCache();
            float fFieldWidth = getColorSelectorWidth();

            for (int i = 0; i < m_ColorsArray.arraySize; ++i) {
                var rColor = m_ColorsArray.GetArrayElementAtIndex(i);
                var sDisplayName = m_rGradientColorsLabels[i];
                drawColorSelectorContent(sDisplayName, rColor, rCache, fFieldWidth);
            }
        }

    }

}
