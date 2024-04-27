/*
             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
             Copyright (C) 2020 Binary Charm - All Rights Reserved
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

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace BinaryCharm.Samples.UI
{
    public class VarDisplayBhv : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField]
        private Slider m_rSlider;

        [SerializeField]
        private TextMeshProUGUI m_rVarName;

        [SerializeField]
        private TextMeshProUGUI m_rVarValue;
        #pragma warning restore 649

        private Action<float> m_rOnValueChange;

        public void setup(string sVarName, bool wholeNumbers, 
                float fMin, float fMax, float fDefaultValue, 
                Action<float> onValueChange) {
            m_rVarName.text = sVarName;
            m_rSlider.wholeNumbers = wholeNumbers;
            m_rSlider.minValue = fMin;
            m_rSlider.maxValue = fMax;

            m_rOnValueChange = onValueChange;

            setValue(fDefaultValue);
            onValueChange(fDefaultValue);

            m_rSlider.onValueChanged.AddListener((float f) => {
                onValueChange(f);
                updateValueText();
            });
        }

        public void setValue(float fValue) {
            m_rSlider.value = fValue;
            updateValueText();
        }

        private void updateValueText() {
            float f = m_rSlider.value;
            m_rVarValue.text = m_rSlider.wholeNumbers ?
                f.ToString() : f.ToString("0.000");
        }
    }
}