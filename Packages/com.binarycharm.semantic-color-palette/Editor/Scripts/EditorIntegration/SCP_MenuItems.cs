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
using System.IO;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if TEXTMESHPRO_PRESENT
using TMPro;
using BinaryCharm.SemanticColorPalette.Colorers.TMPro;
#endif

using BinaryCharm.SemanticColorPalette.Utils;
using BinaryCharm.SemanticColorPalette.Colorers.Others;
using BinaryCharm.SemanticColorPalette.Colorers.Renderers;
using BinaryCharm.SemanticColorPalette.Colorers.UI;

namespace BinaryCharm.SemanticColorPalette.EditorIntegration {

    public class SCP_MenuItems {

        #region Tools MenuItems -----------------------------------------------

        [MenuItem("Tools/Semantic Color Palette/Create new palette", priority = -20)]
        private static void CreatePalette() {
            SCP_Palette rPalette = SCP_Palette.CreateEmpty();
            string sDesiredPath = "Assets/pal_new.asset";
            string sPath = AssetDatabase.GenerateUniqueAssetPath(sDesiredPath);
            ProjectWindowUtil.CreateAsset(rPalette, sPath);
        }

        [MenuItem("Tools/Semantic Color Palette/Import palette from JSON", priority = -19)]
        private static void ImportJsonPalette() {
            importPalette("json", SCP_Utils.LoadPalette);
        }

        [MenuItem("Tools/Semantic Color Palette/Add palette provider to scene", priority = -18)]
        private static void AddPaletteProviderToScene() {
            createPaletteProvider(Selection.activeGameObject);
        }

        [MenuItem("Tools/Semantic Color Palette/Import SVG palette")]
        private static void ImportSvgPalette() {
            importPalette("svg", SCP_ImportUtils.ImportSvgPalette);
        }

        [MenuItem("Tools/Semantic Color Palette/Import Photoshop Color Swatches palette palette (.aco)")]
        private static void ImportAcoPalette() {
            importPalette("aco", SCP_ImportUtils.ImportAcoPalette);
        }

        [MenuItem("Tools/Semantic Color Palette/Import Adobe Swatch Exchange palette (.ase)")]
        private static void ImportAsePalette() {
            importPalette("ase", SCP_ImportUtils.ImportAsePalette);
        }

        [MenuItem("Tools/Semantic Color Palette/Import GIMP palette (.gpl)")]
        private static void ImportGimpPalette() {
            importPalette("gpl", SCP_ImportUtils.ImportGimpPalette);
        }
        
        [MenuItem("Tools/Semantic Color Palette/Import Krita palette (.kpl)")]
        private static void ImportKritaPalette() {
            importPalette("kpl", SCP_ImportUtils.ImportKritaPalette);
        }

        [MenuItem("Tools/Semantic Color Palette/Import Android palette (.xml)")]
        private static void ImportXmlPalette() {
            importPalette("xml", SCP_ImportUtils.ImportAndroidPalette);
        }

        #endregion ------------------------------------------------------------


        #region Assets MenuItems ----------------------------------------------

        [MenuItem("Assets/Semantic Color Palette/Export palette as JSON")]
        private static void ExportPalette() {
            string sDefaultName = Selection.activeObject.name;
            string sPath = EditorUtility.SaveFilePanel(
                "Export palette as JSON", "", sDefaultName, "json"
            );
            if (string.IsNullOrEmpty(sPath)) return;
            Debug.Log("export palette");
            SCP_Utils.SavePalette(Selection.activeObject as SCP_Palette, sPath);
        }

        [MenuItem("Assets/Semantic Color Palette/Export palette as JSON", true)]
        private static bool ExportPaletteValidation() {
            return Selection.activeObject is SCP_Palette;
        }

        [MenuItem("Assets/Semantic Color Palette/Create palette variant")]
        private static void CreatePaletteVariant() {
            SCP_Palette rMainPalette = Selection.activeObject as SCP_Palette;
            string sMainPalettePath = AssetDatabase.GetAssetPath(Selection.activeObject);
            createPaletteVariant(rMainPalette, sMainPalettePath);
        }

        [MenuItem("Assets/Semantic Color Palette/Create palette variant", true)]
        private static bool CreatePaletteVariantValidation() {
            return Selection.activeObject is SCP_Palette &&
                (Selection.activeObject as SCP_Palette).Type == SCP_Palette.eType.main
            ;
        }

        #endregion ------------------------------------------------------------


        #region GameObject MenuItems ------------------------------------------

        [MenuItem("GameObject/Semantic Color Palette/Create new Palette Provider", false, 10)]
        static void CreatePaletteProviderGameObject(MenuCommand menuCommand) {
            GameObject rGO = new GameObject("SCP_PaletteProvider");
            SCP_PaletteProvider rPP = rGO.AddComponent<SCP_PaletteProvider>();
            rPP.AddPalette(null); // prepare a palette slot too

            GameObjectUtility.SetParentAndAlign(rGO, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(rGO, "Create " + rGO.name);
            Selection.activeObject = rGO;
        }

        [MenuItem("GameObject/Semantic Color Palette/Add appropriate colorer", false, 10)]
        static void AddColorerToGameObject(MenuCommand menuCommand) {
            GameObject rSelectedGO = Selection.activeGameObject;
            Type rType = lookUpSuitableColorer(rSelectedGO);
            Undo.AddComponent(rSelectedGO, rType);
        }

        [MenuItem("GameObject/Semantic Color Palette/Add appropriate colorer", true, 10)]
        static bool AddColorerToGameObjectValidation(MenuCommand menuCommand) {
            GameObject rSelectedGO = Selection.activeGameObject;
            if (rSelectedGO == null) return false;
            Type rType = lookUpSuitableColorer(rSelectedGO);
            return rType != null;
        }

        [MenuItem("GameObject/Semantic Color Palette/Add appropriate material colorer", false, 10)]
        static void AddMaterialColorerToGameObject(MenuCommand menuCommand) {
            GameObject rSelectedGO = Selection.activeGameObject;
            Type rType = lookUpSuitableMaterialColorer(rSelectedGO);
            Undo.AddComponent(rSelectedGO, rType);
        }

        [MenuItem("GameObject/Semantic Color Palette/Add appropriate material colorer", true, 10)]
        static bool AddMaterialColorerToGameObjectValidation(MenuCommand menuCommand) {
            GameObject rSelectedGO = Selection.activeGameObject;
            if (rSelectedGO == null) return false;
            Type rType = lookUpSuitableMaterialColorer(rSelectedGO);
            return rType != null;
        }

#if TEXTMESHPRO_PRESENT
        [MenuItem("GameObject/Semantic Color Palette/Add TextMeshPro rich text colorer", false, 10)]
        static void AddRichTextColorerToGameObject(MenuCommand menuCommand) {
            GameObject selectedGO = Selection.activeGameObject;
            Undo.AddComponent(selectedGO, typeof(SCP_TMP_RichTextColorer));
        }

        [MenuItem("GameObject/Semantic Color Palette/Add TextMeshPro rich text colorer", true, 10)]
        static bool AddRichTextColorerToGameObjectValidation(MenuCommand menuCommand) {
            GameObject rSelectedGO = Selection.activeGameObject;
            if (rSelectedGO == null) return false;
            bool bHasTMPText = rSelectedGO.GetComponent<TMP_Text>();
            bool bHasTMPTextRTC = rSelectedGO.GetComponent<SCP_TMP_RichTextColorer>();
            return bHasTMPText && !bHasTMPTextRTC;
        }
#endif

        #endregion ------------------------------------------------------------


        #region Private utility methods ---------------------------------------

        private static readonly Dictionary<Type, Type> s_simpleColorerMappings = new Dictionary<Type, Type>() {
            { typeof(Light), typeof(SCP_LightColorer) },
            { typeof(TextMesh), typeof(SCP_TextMeshColorer) },

            { typeof(LineRenderer), typeof(SCP_LineRendererColorer) },
            { typeof(SpriteRenderer), typeof(SCP_SpriteRendererColorer) },
            { typeof(TrailRenderer), typeof(SCP_TrailRendererColorer) },

#if TEXTMESHPRO_PRESENT
            { typeof(TMP_Dropdown), typeof(SCP_TMP_DropdownColorer) },
            { typeof(TMP_InputField), typeof(SCP_TMP_InputFieldColorer) },
            { typeof(TMP_Text) , typeof(SCP_TMP_TextColorer) },
            { typeof(TextMeshPro), typeof(SCP_TMP_TextColorer) },
            { typeof(TextMeshProUGUI), typeof(SCP_TMP_TextColorer) },
            //{ typeof(Button), typeof(SCP_TMP_ButtonColorer) }, // no: would be a duplicate key,
            // we map to Button to SCP_ButtonColorer and change later to
            // SCP_TMP_TextColorer if a TextMeshPro label is detected.
#endif

            { typeof(Dropdown), typeof(SCP_DropdownColorer) },
            //{ typeof(Image), typeof(SCP_ImageColorer) }, // no: Image component is often secondary
            { typeof(Button), typeof(SCP_ButtonColorer) }, // disambiguation needed
            { typeof(InputField), typeof(SCP_InputFieldColorer) },
            { typeof(RawImage), typeof(SCP_RawImageColorer) },
            { typeof(Scrollbar), typeof(SCP_ScrollbarColorer) },
            { typeof(Slider), typeof(SCP_SliderColorer) },
            { typeof(Text), typeof(SCP_TextColorer) },
            { typeof(Toggle), typeof(SCP_ToggleColorer) },
        };

        private static readonly Dictionary<Type, Type> s_materialColorerMappings = new Dictionary<Type, Type>() {
            { typeof(Renderer) , typeof(SCP_RendererMaterialColorer) },
            //{ typeof(MeshRenderer) , typeof(SCP_RendererMaterialColorer) }, // no: MeshRenderer component can be secondary
            { typeof(SkinnedMeshRenderer) , typeof(SCP_RendererMaterialColorer) },
            { typeof(Graphic) , typeof(SCP_GraphicMaterialColorer) },
#if TEXTMESHPRO_PRESENT
            { typeof(TMP_Text) , typeof(SCP_TMP_FontMaterialColorer) },
            { typeof(TextMeshPro), typeof(SCP_TMP_FontMaterialColorer) },
            { typeof(TextMeshProUGUI), typeof(SCP_TMP_FontMaterialColorer) },
#endif
        };

        private static Type lookUpSuitableColorer(GameObject go) {
            Type ret = null;
            Component[] rComponents = go.GetComponents<Component>();
            foreach (Component rC in rComponents) {
                if (s_simpleColorerMappings.TryGetValue(rC.GetType(), out ret)) {
                    //return ret;
                    break;
                }
            }
#if TEXTMESHPRO_PRESENT
            // special handling needed: "Button" or "Button - TextMeshPro"?
            if (ret == typeof(SCP_ButtonColorer)) { 
                if (go.transform.childCount > 0) {
                    Transform rLabelTr = go.transform.GetChild(0);
                    if (rLabelTr.GetComponent<TextMeshProUGUI>() != null) {
                        ret = typeof(SCP_TMP_ButtonColorer);
                    }
                }
            }
#endif
            if (ret == null) {
                // check for Image explicitly
                if (go.GetComponent<Image>() != null) {
                    ret = typeof(SCP_ImageColorer);
                }
            }

            if (ret == null || go.GetComponent(ret) != null) return null;
            return ret;
        }

        private static Type lookUpSuitableMaterialColorer(GameObject go) {
            Type ret = null;
            Component[] rComponents = go.GetComponents<Component>();
            foreach (Component rC in rComponents) {
                if (s_materialColorerMappings.TryGetValue(rC.GetType(), out ret)) {
                    break;
                }
            }
            if (ret == null) {
                // check for MeshRenderer explicitly
                if (go.GetComponent<MeshRenderer>() != null) {
                    ret = typeof(SCP_RendererMaterialColorer);
                }
            }
            if (ret == null || go.GetComponent(ret) != null) return null;
            return ret;
        }

        private static void createAndSelect(UnityEngine.Object rAsset, string sPath) {
            AssetDatabase.CreateAsset(rAsset, sPath);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = rAsset;
        }

        private static void importPalette(string sExtension, Func<string, SCP_Palette> dImportFunc) {
            string path = EditorUtility.OpenFilePanel("Select file", "", sExtension);
            if (path.Length != 0) {
                SCP_Palette rAsset = dImportFunc(path);
                string sBaseName = Path.GetFileNameWithoutExtension(path);
                string sDesiredPath = "Assets/pal_" + sBaseName + ".asset";
                string sImportPath = AssetDatabase.GenerateUniqueAssetPath(sDesiredPath);
                ProjectWindowUtil.CreateAsset(rAsset, sImportPath);
            }
        }

        private static void createPaletteVariant(SCP_Palette mainPalette, string sMainPalettePath) {

            string sMainPaletteDir = Path.GetDirectoryName(sMainPalettePath);
            string sMainPaletteFilename = Path.GetFileNameWithoutExtension(sMainPalettePath);
            string sVariantPaletteDefaultName = sMainPaletteFilename + "_variant";
            string sVariantPaletteDesiredPath = sMainPaletteDir + "/" + sVariantPaletteDefaultName + ".asset";
            string sVariantPalettePath = AssetDatabase.GenerateUniqueAssetPath(sVariantPaletteDesiredPath);

            SCP_Palette rVariantPalette = SCP_Palette.CreateVariant(sVariantPaletteDefaultName, mainPalette);
            ProjectWindowUtil.CreateAsset(rVariantPalette, sVariantPalettePath);
        }

        private static void createPaletteProvider(GameObject rParent) {
            GameObject rGO = new GameObject("SCP_PaletteProvider");
            SCP_PaletteProvider rPP = rGO.AddComponent<SCP_PaletteProvider>();
            rPP.AddPalette(null); // prepare a palette slot too

            GameObjectUtility.SetParentAndAlign(rGO, rParent);
            Undo.RegisterCreatedObjectUndo(rGO, "Create " + rGO.name);
            Selection.activeObject = rGO;
        }
        #endregion ------------------------------------------------------------

    }

}
