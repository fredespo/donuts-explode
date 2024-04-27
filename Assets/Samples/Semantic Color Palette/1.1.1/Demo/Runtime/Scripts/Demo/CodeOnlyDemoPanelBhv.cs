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

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using BinaryCharm.SemanticColorPalette;
using BinaryCharm.SemanticColorPalette.Colorers;
using BinaryCharm.SemanticColorPalette.Colorers.Renderers;
using BinaryCharm.SemanticColorPalette.Colorers.TMPro;

namespace BinaryCharm.Samples.SemanticColorPalette.Demo {

    public class CodeOnlyDemoPanelBhv : DemoPanelBhv {

        [SerializeField] private Transform m_rDynamicContentRootTr;
        [SerializeField] private Camera m_rCamera;
        [SerializeField] private Transform m_rProvidersFatherTr;

        private GameObject m_rGroundGO;
        private SCP_PaletteProvider m_rPaletteProvider;

        private Dictionary<PrimitiveType, SCP_ColorId> m_primitiveTypeToColorId =
            new Dictionary<PrimitiveType, SCP_ColorId>();

        void Start() {

            const string sSPHERE_COLOR_NAME = "sphere";
            const string sCAPSULE_COLOR_NAME = "capsule";
            const string sCUBE_COLOR_NAME = "cube";
            const string sCYLINDER_COLOR_NAME = "cylinder";
            const string sGROUNDPLANE_COLOR_NAME = "ground";
            const string sTEXT_COLOR_NAME = "text";

            // create main palette
            SCP_Palette rMainPalette = SCP_Palette.CreateMain(
                "dynpal_main", 
                new Dictionary<string, Color>() {
                    { sSPHERE_COLOR_NAME, new Color32(0x8D, 0x0F, 0x0F,0xFF)},
                    { sCAPSULE_COLOR_NAME, new Color32(0xBC, 0x4D, 0xA4, 0xFF) },
                    { sCUBE_COLOR_NAME, new Color32(0x37,0x93, 0xA8, 0xFF)},
                    { sCYLINDER_COLOR_NAME, new Color32(0xFF, 0xA6, 0x04, 0xFF) },
                    { sGROUNDPLANE_COLOR_NAME, Color.grey },
                    { sTEXT_COLOR_NAME, new Color32(0xB2, 0x48, 0x48, 0xFF )},
                }
            );

            // color names should only be used
            // 1) when defining a palette (the Dictionary keys)
            // 2) to retrieve the color ids (once, after creating the palette)
            // after that, you should work with color ids (faster and less error prone!)

            SCP_ColorId sphereColorId = rMainPalette.GetColorIdByName(sSPHERE_COLOR_NAME);
            SCP_ColorId capsuleColorId = rMainPalette.GetColorIdByName(sCAPSULE_COLOR_NAME);
            SCP_ColorId cubeColorId = rMainPalette.GetColorIdByName(sCUBE_COLOR_NAME);
            SCP_ColorId cylinderColorId = rMainPalette.GetColorIdByName(sCYLINDER_COLOR_NAME);
            SCP_ColorId groundColorId = rMainPalette.GetColorIdByName(sGROUNDPLANE_COLOR_NAME);
            SCP_ColorId textColorId = rMainPalette.GetColorIdByName(sTEXT_COLOR_NAME);

            // create palette variant
            // we only change the primitive colors, so the others will stay the same
            SCP_Palette rPaletteVariant = SCP_Palette.CreateVariant("dynpal_variant", rMainPalette);
            rPaletteVariant.SetColor(sphereColorId, new Color32(0x0E, 0x59, 0x8C, 0xFF));
            rPaletteVariant.SetColor(capsuleColorId, new Color32(0x51, 0x2E, 0x1D, 0xFF));
            rPaletteVariant.SetColor(cubeColorId, new Color32(0x94, 0x28, 0x57, 0xFF));
            rPaletteVariant.SetColor(cylinderColorId, new Color32(0x37, 0xA3, 0xBF, 0xFF));

            // create and setup provider
            GameObject rPaletteProviderHolder = new GameObject("spawnedProvider");
            rPaletteProviderHolder.transform.parent = m_rProvidersFatherTr;

            m_rPaletteProvider = rPaletteProviderHolder.AddComponent<SCP_PaletteProvider>();
            m_rNeededProviders.Add(m_rPaletteProvider);

            m_rPaletteProvider.AddPalette(rMainPalette);
            m_rPaletteProvider.AddPalette(rPaletteVariant);
            m_rPaletteProvider.SetActivePaletteIndex(0);

            // store primitive -> color id mapping
            m_primitiveTypeToColorId.Add(PrimitiveType.Sphere, sphereColorId);
            m_primitiveTypeToColorId.Add(PrimitiveType.Capsule, capsuleColorId);
            m_primitiveTypeToColorId.Add(PrimitiveType.Cube, cubeColorId);
            m_primitiveTypeToColorId.Add(PrimitiveType.Cylinder, cylinderColorId);

            // setup startup scene
            m_rGroundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            m_rGroundGO.transform.parent = m_rDynamicContentRootTr;
            m_rGroundGO.transform.localScale = Vector3.one * 40f;
            m_rGroundGO.transform.localPosition = Vector3.zero;
            setupPrimitiveColorer(m_rGroundGO, groundColorId);

            Vector3 vCubePos = m_rDynamicContentRootTr.TransformPoint(new Vector3(-0.85f, 0.5f, -2f));
            GameObject rCubeGO = spawnPrimitive(PrimitiveType.Cube, vCubePos);
            setupPrimitiveColorer(rCubeGO, cubeColorId);

            Vector3 vSpherePos = m_rDynamicContentRootTr.TransformPoint(new Vector3(0.4f, 0.5f, -2.2f ));
            GameObject rSphereGO = spawnPrimitive(PrimitiveType.Sphere, vSpherePos);
            setupPrimitiveColorer(rSphereGO, sphereColorId);

            Vector3 vCylinderPos = m_rDynamicContentRootTr.TransformPoint(new Vector3(0.15f, 1f, -1f));
            GameObject rCylinderGO = spawnPrimitive(PrimitiveType.Cylinder, vCylinderPos);
            setupPrimitiveColorer(rCylinderGO, cylinderColorId);

            Vector3 vCapsulePos = m_rDynamicContentRootTr.TransformPoint(new Vector3(-1.25f, 1f, -0.7f));
            GameObject rCapsuleGO = spawnPrimitive(PrimitiveType.Capsule, vCapsulePos);
            setupPrimitiveColorer(rCapsuleGO, capsuleColorId);

            GameObject rTMP_TextGO = new GameObject("dynText");
            rTMP_TextGO.transform.parent = m_rDynamicContentRootTr;
            rTMP_TextGO.transform.localPosition = new Vector3(0f, 3f, 10f);
            TextMeshPro rTMP_Text = rTMP_TextGO.AddComponent<TextMeshPro>();
            rTMP_Text.fontSize = 24;
            rTMP_Text.text = "Click on the ground!";

            SCP_TMP_TextColorer rTMP_TextColorer =
                rTMP_TextGO.AddComponent<SCP_TMP_TextColorer>();
            rTMP_TextColorer.SetPaletteProvider(m_rPaletteProvider);
            rTMP_TextColorer.SetColorIds(new SCP_TMP_TextColorsDef() {
                vertexColor = textColorId
            });
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = m_rCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    StartCoroutine(popRandomPrimitive(hit.point));
                }
            }
        }

        private GameObject spawnPrimitive(PrimitiveType primitiveType, Vector3 vPos) {
            GameObject rPrimitiveGO = GameObject.CreatePrimitive(primitiveType);
            rPrimitiveGO.transform.parent = m_rDynamicContentRootTr;
            rPrimitiveGO.transform.localScale = Vector3.one;
            rPrimitiveGO.transform.position = vPos;
            rPrimitiveGO.AddComponent<Rigidbody>();
            return rPrimitiveGO;
        }

        private IEnumerator popRandomPrimitive(Vector3 planeHitPoint) {
            PrimitiveType primitiveType = (PrimitiveType)Random.Range(0, 4);
            SCP_ColorId colorId = m_primitiveTypeToColorId[primitiveType];
            GameObject rNewGO = spawnPrimitive(primitiveType, planeHitPoint + new Vector3(0f, 5f, 0f));
            setupPrimitiveColorer(rNewGO, colorId);
            
            const float fPOP_SCALE_BEGIN = 0.01f;
            const float fPOP_SCALE_END = 1f;
            const float fPOP_DURATION_SECS = 0.15f;

            rNewGO.transform.localScale = Vector3.one * fPOP_SCALE_BEGIN;
            float fPopTime = Time.time;
            while (Time.time < fPopTime + fPOP_DURATION_SECS) {
                float fElapsed = Time.time - fPopTime;
                float fPopAmount01 = fElapsed / fPOP_DURATION_SECS;
                float fScale = Mathf.Lerp(fPOP_SCALE_BEGIN, fPOP_SCALE_END, fPopAmount01 * fPopAmount01);
                if (fScale > 1f) {
                    fScale = 1f;
                }
                rNewGO.transform.localScale = Vector3.one * fScale;
                yield return null;
            }
            rNewGO.transform.localScale = Vector3.one * fPOP_SCALE_END;
        }

        private void setupPrimitiveColorer(GameObject rGO, SCP_ColorId colorId) {
            SCP_RendererMaterialColorer rColorer = rGO.AddComponent<SCP_RendererMaterialColorer>();

            rColorer.SetPaletteProvider(m_rPaletteProvider);
            rColorer.SetMaterialIndex(0);
            rColorer.SetUsingMaterialInstance(true);
            rColorer.SetColorIds(new SCP_MaterialColorDef[] {
                new SCP_MaterialColorDef("_Color", colorId),
                new SCP_MaterialColorDef("_EmissionColor", SCP_ColorId.DO_NOT_APPLY)
            });
        }

    }

}
