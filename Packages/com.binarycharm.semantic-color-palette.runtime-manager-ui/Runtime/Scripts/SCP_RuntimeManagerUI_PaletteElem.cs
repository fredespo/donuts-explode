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

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using BinaryCharm.SemanticColorPalette.Colorers.UI;

namespace BinaryCharm.SemanticColorPalette.RuntimeManagement {

    internal class SCP_RuntimeManagerUI_PaletteElem : MonoBehaviour {

        [SerializeField] private Image m_rBorderImage;
        [SerializeField] private SCP_ImageColorer m_rColorImage;
        [SerializeField] private Button m_rBtn;
        [SerializeField] private TextMeshProUGUI m_rTxt;

        internal event Action<int> OnSelected;

        private static readonly Color SELECTED_COLOR = Color.black;
        private static readonly Color UNSELECTED_COLOR = Color.white;

        private int m_iElementIndex;

        private void Awake() {
            m_rBtn.onClick.AddListener(onSelected);
        }

        internal void setup(
                int iElementIndex,
                SCP_PaletteProvider rProvider, 
                int iPaletteIndex, 
                bool bSelected
            ) {

            m_iElementIndex = iElementIndex;

            SCP_Palette rPalette = rProvider.GetPaletteByIndex(iPaletteIndex);

            string sColorName = rPalette.GetColorNameByIndex(iElementIndex);
            SCP_ColorId colorId = rPalette.GetColorIdByIndex(iElementIndex);

            m_rTxt.text = sColorName;
            m_rColorImage.SetColorId(colorId);
            m_rColorImage.SetPaletteProvider(rProvider, iPaletteIndex);

            setSelected(bSelected);
        }

        private void onSelected() {
            if (OnSelected != null) {
                OnSelected(m_iElementIndex);
            }
        }

        internal void setSelected(bool bSelected) {
            m_rBorderImage.color = bSelected ?
                SELECTED_COLOR : UNSELECTED_COLOR;
        }

        internal float getHeight() {
            return GetComponent<RectTransform>().sizeDelta.y;
        }

    }

}
