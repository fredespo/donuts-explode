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
using UnityEngine;

using BinaryCharm.Common.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers {

    public abstract class SCP_AMaterialColorerBaseEditor : SCP_AColorerBaseEditor {

        protected SerializedProperty m_UseMaterialInstance;
        protected SerializedProperty m_OriginalMaterial;
        protected SerializedProperty m_InstancedMaterial;
        protected SerializedProperty m_SavedOriginalMaterial;
        protected SerializedProperty m_ObjectId;

        private const string sLABEL_UseMaterialInstance =
            "Use Instance Of Material";

        protected new void OnEnable() {
            base.OnEnable();

            m_UseMaterialInstance = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorerBase.m_bUseMaterialInstance)
            );
            m_OriginalMaterial = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorerBase.m_rOriginalMaterial)
            );
            m_InstancedMaterial = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorerBase.m_rInstancedMaterial)
            );
            m_SavedOriginalMaterial = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorerBase.m_bSavedOriginalMaterial)
            );
            m_ObjectId = serializedObject.FindProperty(
                nameof(SCP_AMaterialColorerBase.m_sObjectId)
            );
        }

        protected void instanceSetup() {
            EditorGUILayout.PropertyField(m_UseMaterialInstance, new GUIContent(sLABEL_UseMaterialInstance));
            EditorGUIEx.DrawSeparator();

            SCP_AMaterialColorerBase rT = (SCP_AMaterialColorerBase)target;
            if (m_UseMaterialInstance.boolValue) {
                if (m_OriginalMaterial.objectReferenceValue == null) {
                    Material rOriginal = rT.getSharedMaterialImpl();
                    m_OriginalMaterial.objectReferenceValue = rOriginal;
                    m_SavedOriginalMaterial.boolValue = true;

                    Material rInstanced = new Material(rOriginal);
                    rInstanced.name = rOriginal.name + " (SCP Instance)";
                    m_InstancedMaterial.objectReferenceValue = rInstanced;
                    m_ObjectId.stringValue = GlobalObjectId.GetGlobalObjectIdSlow(rT).ToString();
                }
                else if (m_ObjectId.stringValue != GlobalObjectId.GetGlobalObjectIdSlow(rT).ToString()) {
                    Material rOriginal = (Material)m_OriginalMaterial.objectReferenceValue;
                    Material rInstanced = new Material(rOriginal);
                    rInstanced.name = rOriginal.name + " (SCP Instance)";
                    m_InstancedMaterial.objectReferenceValue = rInstanced;
                    m_ObjectId.stringValue = GlobalObjectId.GetGlobalObjectIdSlow(rT).ToString();
                }
            }
        }

    }

}
