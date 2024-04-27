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

using BinaryCharm.Common.UI;

namespace BinaryCharm.SemanticColorPalette.Colorers.Renderers {

    [CustomEditor(typeof(SCP_RendererMaterialColorer)), CanEditMultipleObjects]
    public class SCP_RendererMaterialColorerEditor : SCP_AMaterialColorerEditor {

        private SerializedProperty m_MaterialIndex;

        private new void OnEnable() {
            base.OnEnable();

            m_MaterialIndex = serializedObject.FindProperty(
                nameof(SCP_RendererMaterialColorer.m_iMaterialId)
            );
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();

            drawMaterialSelector();
            // 0 index is reserved for "<NOT SELECTED>"
            int iMatIndex = m_MaterialIndex.intValue;
            if (iMatIndex != 0) {
                instanceSetup();
            }

            updateAndDisplayColorProperties();

            serializedObject.ApplyModifiedProperties();
        }

        private void drawMaterialSelector() {
            SCP_RendererMaterialColorer rMC = (SCP_RendererMaterialColorer)target;
            SCP_RendererMaterialColorer[] rMCs = rMC.GetComponents<SCP_RendererMaterialColorer>();
            Renderer rRenderer = rMC.gameObject.GetComponent<Renderer>();
            Material[] rMats = rRenderer.sharedMaterials;

            if (rMCs.Length > rMats.Length) {
                displayError();
                return;
            }

            HashSet<int> rAlreadyConfiguredMats = new HashSet<int>();
            foreach(SCP_RendererMaterialColorer x in rMCs) {
                if (x == rMC || x.m_iMaterialId == 0) continue;
                rAlreadyConfiguredMats.Add(x.m_iMaterialId);
            }
            int iNumAvailableMats = rMats.Length - rAlreadyConfiguredMats.Count;
            int[] iMatIds = new int[iNumAvailableMats+1];
            string[] rMatNames = new string[iNumAvailableMats+1];
            int iAvailableMatCount = 0;
            iMatIds[0] = 0;
            rMatNames[0] = "<NOT SELECTED>";

            int iCurrMatSelection = m_MaterialIndex.intValue;
            bool bCurrMatSelectionValid = false;
            for (int i = 0; i < rMats.Length; ++i) {
                int iInternalMatId = i + 1;
                if (rAlreadyConfiguredMats.Contains(iInternalMatId)) continue;
                iMatIds[iAvailableMatCount+1] = iInternalMatId;
                rMatNames[iAvailableMatCount+1] = rMats[i] != null ? rMats[i].name : "";
                if (iInternalMatId == iCurrMatSelection) bCurrMatSelectionValid = true;
                ++iAvailableMatCount;
            }

            if (!bCurrMatSelectionValid) iCurrMatSelection = iMatIds[0];

            bool bUsingInstance = m_UseMaterialInstance.boolValue;
            using (new EditorGUI.DisabledScope(bUsingInstance)) {
                int iPopupSelection = EditorGUILayout.IntPopup("Material", iCurrMatSelection, rMatNames, iMatIds);
                m_MaterialIndex.intValue = iPopupSelection;
            }
            if (bUsingInstance) {
                displayHelp();
            }
        }

        private void displayError() {
            EditorGUIEx.DrawErrorLabel("Invalid configuration!",
                "The number of attached SCP_RendererMaterialColorer components " +
                "must be lower than the number of Materials."
            );
        }

        private void displayHelp() {
            EditorGUIEx.MultilineLabel(
                "To change material, disable \"use material instance\" first " +
                "(which reverts to the original material).\n" +
                "To apply coloring to multiple materials on the same object, " +
                "attach multiple SCP_MaterialColorer components.\n"
            );
        }
    }
}
