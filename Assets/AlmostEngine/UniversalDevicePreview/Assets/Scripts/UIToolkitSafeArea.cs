using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from https://github.com/PregOfficial/UI-Toolkit-SafeArea

#if UNITY_2021_3_OR_NEWER

using UnityEngine.UIElements;

namespace AlmostEngine.Preview
{
    public class UIToolkitSafeArea : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIToolkitSafeArea, VisualElement.UxmlTraits> { }


        public UIToolkitSafeArea()
        {
            // Init anchor behaviour
            style.flexGrow = 1;
            style.flexShrink = 1;

            // Register to screen geometry changes
            RegisterCallback<GeometryChangedEvent>(LayoutChanged);
        }

        private void LayoutChanged(GeometryChangedEvent evnt)
        {
            // Get current device 
            var safeArea = DeviceInfo.GetSafeArea();

            try
            {
                // Compute new corners
                var leftTopCorner = RuntimePanelUtils.ScreenToPanel(panel, new Vector2(safeArea.xMin, Screen.height - safeArea.yMax));
                var rightBottomCorner = RuntimePanelUtils.ScreenToPanel(panel, new Vector2(Screen.width - safeArea.xMax, safeArea.yMin));

                // Set margin
                style.marginLeft = leftTopCorner.x;
                style.marginTop = leftTopCorner.y;
                style.marginRight = rightBottomCorner.x;
                style.marginBottom = rightBottomCorner.y;
            }
            catch { }
        }
    }
}

#endif