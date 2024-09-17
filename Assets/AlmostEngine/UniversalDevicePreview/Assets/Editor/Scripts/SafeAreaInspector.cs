using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AlmostEngine.Preview
{
    [CustomEditor(typeof(SafeArea))]
    [CanEditMultipleObjects]
    public class SafeAreaInspector : Editor
    {
        protected SafeArea m_SafeArea;


        void OnEnable()
        {
            m_SafeArea = (SafeArea)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (m_SafeArea.GetComponent<RectTransform>() == null || m_SafeArea.transform.parent == null)
            {
                EditorGUILayout.HelpBox("Safe area must be placed on a valid UI object.", MessageType.Error);
                return;
            }

            m_SafeArea.m_Panel = m_SafeArea.GetComponent<RectTransform>();


            EditorGUILayout.HelpBox("Most common use case: add a SafeArea component to a fully stretched panel, with SNAP horizontal and vertical, LEFT_AND_RIGHT and TOP_AND_BOTTOM, and put all your UI buttons inside that panel. For a more advanced use, please see the documentation.", MessageType.Info);

            if (m_SafeArea.gameObject.activeInHierarchy
                && m_SafeArea.m_HorizontalConstraintType != SafeArea.Constraint.NONE
                && m_SafeArea.m_Panel.parent.GetComponent<Canvas>() == null
                && (m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMin.x != 0f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMax.x != 1f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMin.x != 0f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMax.x != 0f))
            {

                EditorGUILayout.HelpBox("Horizontal Safe Area must be placed within fully horizontally stretched parent to be scaled properly. " +
                "\nParent anchor min.x: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMin.x + " should be 0." +
                "\nParent anchor max.x: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMax.x + " should be 1." +
                "\nParent anchor offsetMin.x: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMin.x + " should be 0." +
                "\nParent anchor offsetMax.x: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMax.x + " should be 0.",
                    MessageType.Error);

            }
            if (m_SafeArea.gameObject.activeInHierarchy
                && m_SafeArea.m_VerticalConstraintType != SafeArea.Constraint.NONE
                && m_SafeArea.m_Panel.parent.GetComponent<Canvas>() == null
                && (m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMin.y != 0f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMax.y != 1f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMin.y != 0f
                || m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMax.y != 0f))
            {


                EditorGUILayout.HelpBox("Vertical Safe Area must be placed within fully vertical stretched parent to be scaled properly. " +
                "\nParent anchor min.y: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMin.y + " should be 0." +
                "\nParent anchor max.y: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().anchorMax.y + " should be 1." +
                "\nParent anchor offsetMin.y: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMin.y + " should be 0." +
                "\nParent anchor offsetMax.y: " + m_SafeArea.m_Panel.parent.GetComponentInParent<RectTransform>().offsetMax.y + " should be 0.",
                    MessageType.Error);
            }




            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_HorizontalConstraintType"));
            if (m_SafeArea.m_HorizontalConstraintType != SafeArea.Constraint.NONE)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_HorizontalConstraint"));
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_VerticalConstraintType"));
            if (m_SafeArea.m_VerticalConstraintType != SafeArea.Constraint.NONE)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_VerticalConstraint"));
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();



            // DEFAULT ANCHORS
            EditorGUILayout.LabelField("Default anchors", EditorStyles.boldLabel);
#if (UNITY_2019_4_OR_NEWER || UNITY_2020_1_OR_NEWER)
            // Because we can not detect a change when device simulator is enabled, we need to manually ask to the user to update the anchors
            if (SafeArea.IsDeviceSimulatorEnabled())
            {
                DefaultAnchorButtonGUI();
            }
            else
            {
                DefaultAnchorGUI();
            }
#else
            // We simply update the anchors when they change
            DefaultAnchorGUI();
#endif



            serializedObject.ApplyModifiedProperties();
        }

        void DefaultAnchorGUI()
        {
            EditorGUILayout.LabelField("Default Anchors Min (" + m_SafeArea.m_DefaultAnchorMin.x + ", " + m_SafeArea.m_DefaultAnchorMin.y + ")");
            EditorGUILayout.LabelField("Default Anchors Max (" + m_SafeArea.m_DefaultAnchorMax.x + ", " + m_SafeArea.m_DefaultAnchorMax.y + ")");
            // We automatically update the default Anchors if no device simulation is running
            if (!Application.isPlaying && !m_SafeArea.m_IsSimulatingDevice
             && (DeviceInfo.GetSafeArea().x == 0 && DeviceInfo.GetSafeArea().y == 0 && DeviceInfo.GetSafeArea().width == DeviceInfo.GetResolution().x && DeviceInfo.GetSafeArea().height == DeviceInfo.GetResolution().y))
            {
                m_SafeArea.m_DefaultAnchorMin = m_SafeArea.m_Panel.anchorMin;
                m_SafeArea.m_DefaultAnchorMax = m_SafeArea.m_Panel.anchorMax;

            }
        }

#if (UNITY_2019_4_OR_NEWER || UNITY_2020_1_OR_NEWER)
        void DefaultAnchorButtonGUI()
        {
            bool anchorWasEdited = (m_SafeArea.m_Panel.anchorMin != m_SafeArea.m_DefaultAnchorMin) || (m_SafeArea.m_Panel.anchorMax != m_SafeArea.m_DefaultAnchorMax);
            EditorGUILayout.LabelField("Default Anchors Min (" + m_SafeArea.m_DefaultAnchorMin.x + ", " + m_SafeArea.m_DefaultAnchorMin.y + ")"
                + "   Current Anchors Min (" + m_SafeArea.m_Panel.anchorMin.x + ", " + m_SafeArea.m_Panel.anchorMin.y + ")");
            EditorGUILayout.LabelField("Default Anchors Max (" + m_SafeArea.m_DefaultAnchorMax.x + ", " + m_SafeArea.m_DefaultAnchorMax.y + ")"
                + "   Current Anchors Max (" + m_SafeArea.m_Panel.anchorMax.x + ", " + m_SafeArea.m_Panel.anchorMax.y + ")");
            if (!Application.isPlaying && !m_SafeArea.m_IsSimulatingDevice)
            {
                EditorGUILayout.HelpBox("When the Unity DeviceSimulator package is enabled, default Anchors values need to be saved manually after being edited.", MessageType.Info);
                var previousColor = GUI.color;
                if (anchorWasEdited)
                {
                    GUI.color = Color.yellow;
                    EditorGUILayout.HelpBox("Anchors have been changed." +
                        "\n\nIf you edited the Anchors, you need to save their new values using the button below. " +
                        "\n\nIf you didn't edited them, it means they are temporarily changed by the device simulation, and you can ignore that message.", MessageType.Warning);
                    if (DeviceInfo.GetSafeArea().x != 0 || DeviceInfo.GetSafeArea().y != 0 || DeviceInfo.GetSafeArea().width != DeviceInfo.GetResolution().x || DeviceInfo.GetSafeArea().height != DeviceInfo.GetResolution().y)
                    {
                        EditorGUILayout.HelpBox("A device with notches is currently being simulated by the DeviceSimulator. It is recommended to edit and save the SafeArea Anchors only after switching from Simulator to GameView mode, or after selecting a device without notches.", MessageType.Warning);
                    }
                }
                if (GUILayout.Button("Save current Anchors as default"))
                {
                    m_SafeArea.m_DefaultAnchorMin = m_SafeArea.m_Panel.anchorMin;
                    m_SafeArea.m_DefaultAnchorMax = m_SafeArea.m_Panel.anchorMax;
                }
                if (anchorWasEdited)
                {
                    GUI.color = previousColor;
                }
            }
        }
#endif
    }
}