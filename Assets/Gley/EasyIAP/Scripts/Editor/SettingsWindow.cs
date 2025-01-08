#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#define GleyIAPEnabled
#endif

using Gley.Common;
using Gley.EasyIAP.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace Gley.EasyIAP.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;
        private static string rootWithoutAssets;

        private List<StoreProduct> localShopProducts;
        private EasyIAPData easyIAPData;
        private Vector2 scrollPosition;
        private string errorText = "";
        private bool useForGooglePlay;
        private bool useForAmazon;
        private bool useForIos;
        private bool useForMac;
        private bool useForWindows;
        private bool debug;
        private bool useReceiptValidation;
        private bool usePlaymaker;
        private bool useUVS;


        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder, out rootWithoutAssets);
        }


        private void OnEnable()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties(), out rootWithoutAssets);
            }


            easyIAPData = EditorUtilities.LoadOrCreateDataAsset<EasyIAPData>(rootFolder, Internal.Constants.RESOURCES_FOLDER, Internal.Constants.DATA_NAME_RUNTIME);

            debug = easyIAPData.debug;
            useReceiptValidation = easyIAPData.useReceiptValidation;
            usePlaymaker = easyIAPData.usePlaymaker;
            useUVS = easyIAPData.useUVS;
            useForGooglePlay = easyIAPData.useForGooglePlay;
            useForAmazon = easyIAPData.useForAmazon;
            useForIos = easyIAPData.useForIos;
            useForMac = easyIAPData.useForMac;
            useForWindows = easyIAPData.useForWindows;

            localShopProducts = new List<StoreProduct>();
            for (int i = 0; i < easyIAPData.shopProducts.Count; i++)
            {
                localShopProducts.Add(easyIAPData.shopProducts[i]);
            }
        }


        private void SaveSettings()
        {
            if (useForGooglePlay)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_GOOGLEPLAY, false, BuildTargetGroup.Android);
#if GleyIAPEnabled
                try
                {
                    UnityEditor.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AppStore.GooglePlay);
                }
                catch
                {
                    Debug.LogError("Enable In-App Purchases from Services");
                }
#endif
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_GOOGLEPLAY, true, BuildTargetGroup.Android);
            }

            if (useForAmazon)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_AMAZON, false, BuildTargetGroup.Android);
#if GleyIAPEnabled
                try
                {
                    UnityEditor.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AppStore.AmazonAppStore);
                }
                catch
                {
                    Debug.LogError("Enable In-App Purchases from Services");
                }
#endif
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_AMAZON, true, BuildTargetGroup.Android);
            }

            if (useForIos)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_IOS, false, BuildTargetGroup.iOS);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_IOS, true, BuildTargetGroup.iOS);
            }

            if (useForMac)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_MACOS, false, BuildTargetGroup.Standalone);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_MACOS, true, BuildTargetGroup.Standalone);
            }

            if (useForWindows)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_WINDOWS, false, BuildTargetGroup.WSA);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_WINDOWS, true, BuildTargetGroup.WSA);
            }


            if (useReceiptValidation)
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_VALIDATION, false, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_VALIDATION, false, BuildTargetGroup.iOS);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_VALIDATION, true, BuildTargetGroup.Android);
                Gley.Common.PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_IAP_VALIDATION, true, BuildTargetGroup.iOS);
            }

            if (usePlaymaker)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_PLAYMAKER_SUPPORT, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_PLAYMAKER_SUPPORT, true);
            }

            if (useUVS)
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_UVS_SUPPORT, false);
            }
            else
            {
                Gley.Common.PreprocessorDirective.AddToCurrent(Gley.Common.Constants.GLEY_UVS_SUPPORT, true);
            }

            easyIAPData.debug = debug;
            easyIAPData.useReceiptValidation = useReceiptValidation;
            easyIAPData.usePlaymaker = usePlaymaker;
            easyIAPData.useUVS = useUVS;
            easyIAPData.useForGooglePlay = useForGooglePlay;
            easyIAPData.useForIos = useForIos;
            easyIAPData.useForAmazon = useForAmazon;
            easyIAPData.useForMac = useForMac;
            easyIAPData.useForWindows = useForWindows;

            easyIAPData.shopProducts = new List<StoreProduct>();
            for (int i = 0; i < localShopProducts.Count; i++)
            {
                easyIAPData.shopProducts.Add(localShopProducts[i]);
            }

            CreateEnumFile();

            EditorUtility.SetDirty(easyIAPData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));
            EditorGUILayout.Space();
            debug = EditorGUILayout.Toggle("Debug", debug);
            useReceiptValidation = EditorGUILayout.Toggle("Use Receipt Validation", useReceiptValidation);
            if (useReceiptValidation)
            {
                GUILayout.Label("Go to Window > Unity IAP > IAP Receipt Validation Obfuscator,\nand paste your GooglePlay public key and click Obfuscate.");
            }
            EditorGUILayout.Space();
            GUILayout.Label("Enable Visual Scripting Tool:", EditorStyles.boldLabel);
            usePlaymaker = EditorGUILayout.Toggle("Playmaker", usePlaymaker);
            useUVS = EditorGUILayout.Toggle("Unity Visual Scripting", useUVS);
            EditorGUILayout.Space();
            GUILayout.Label("Select your platforms:", EditorStyles.boldLabel);
            useForGooglePlay = EditorGUILayout.Toggle("Google Play", useForGooglePlay);
            if (useForGooglePlay == true)
            {
                useForAmazon = false;
            }
            useForAmazon = EditorGUILayout.Toggle("Amazon", useForAmazon);
            if (useForAmazon)
            {
                useForGooglePlay = false;
            }
            useForIos = EditorGUILayout.Toggle("iOS", useForIos);
            useForMac = EditorGUILayout.Toggle("MacOS", useForMac);
            useForWindows = EditorGUILayout.Toggle("Windows Store", useForWindows);
            EditorGUILayout.Space();

            if (GUILayout.Button("Import Unity IAP SDK"))
            {
                Gley.Common.ImportRequiredPackages.ImportPackage("com.unity.purchasing", CompleteMethod);
            }
            EditorGUILayout.Space();

            if (useForGooglePlay || useForIos || useForAmazon || useForMac || useForWindows)
            {
                GUILayout.Label("In App Products Setup", EditorStyles.boldLabel);

                for (int i = 0; i < localShopProducts.Count; i++)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    localShopProducts[i].productName = EditorGUILayout.TextField("Product Name:", localShopProducts[i].productName);
                    localShopProducts[i].productName = Regex.Replace(localShopProducts[i].productName, @"^[\d-]*\s*", "");
                    localShopProducts[i].productName = localShopProducts[i].productName.Replace(" ", "");
                    localShopProducts[i].productName = localShopProducts[i].productName.Trim();
                    localShopProducts[i].productType = (ProductType)EditorGUILayout.EnumPopup("Product Type:", localShopProducts[i].productType);
                    localShopProducts[i].value = EditorGUILayout.IntField("Reward Value:", localShopProducts[i].value);

                    if (useForGooglePlay)
                    {
                        localShopProducts[i].idGooglePlay = EditorGUILayout.TextField("Google Play ID:", localShopProducts[i].idGooglePlay);
                    }

                    if (useForAmazon)
                    {
                        localShopProducts[i].idAmazon = EditorGUILayout.TextField("Amazon SKU:", localShopProducts[i].idAmazon);
                    }

                    if (useForIos)
                    {
                        localShopProducts[i].idIOS = EditorGUILayout.TextField("App Store (iOS) ID:", localShopProducts[i].idIOS);
                    }

                    if (useForMac)
                    {
                        localShopProducts[i].idMac = EditorGUILayout.TextField("Mac Store ID:", localShopProducts[i].idMac);
                    }

                    if (useForWindows)
                    {
                        localShopProducts[i].idWindows = EditorGUILayout.TextField("Windows Store ID:", localShopProducts[i].idWindows);
                    }

                    if (GUILayout.Button("Remove Product"))
                    {
                        localShopProducts.RemoveAt(i);
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                }
                EditorGUILayout.Space();

                if (GUILayout.Button("Add new product"))
                {
                    localShopProducts.Add(new StoreProduct());
                }
            }

            GUILayout.Label(errorText);
            if (GUILayout.Button("Save"))
            {
                if (CheckForNull() == false)
                {
                    SaveSettings();
                    errorText = "Save Success";
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Example Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.exampleScene}");
            }

            if (GUILayout.Button("Open Test Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.testScene}");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }
            EditorGUILayout.Space();

            GUILayout.EndScrollView();
        }


        private void CompleteMethod(string message)
        {
            Debug.Log(message);
        }


        private bool CheckForNull()
        {
            for (int i = 0; i < localShopProducts.Count; i++)
            {
                if (String.IsNullOrEmpty(localShopProducts[i].productName))
                {
                    errorText = "Product name cannot be empty! Please fill all of them";
                    return true;
                }

                if (useForGooglePlay)
                {
                    if (String.IsNullOrEmpty(localShopProducts[i].idGooglePlay))
                    {
                        errorText = "Google Play ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }

                if (useForAmazon)
                {
                    if (String.IsNullOrEmpty(localShopProducts[i].idAmazon))
                    {
                        errorText = "Amazon SKU cannot be empty! Please fill all of them";
                        return true;
                    }
                }

                if (useForIos)
                {
                    if (String.IsNullOrEmpty(localShopProducts[i].idIOS))
                    {
                        errorText = "App Store ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }

                if (useForMac)
                {
                    if (String.IsNullOrEmpty(localShopProducts[i].idMac))
                    {
                        errorText = "Mac Store ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }

                if (useForWindows)
                {
                    if (String.IsNullOrEmpty(localShopProducts[i].idWindows))
                    {
                        errorText = "Windows Store ID cannot be empty! Please fill all of them";
                        return true;
                    }
                }
            }
            return false;
        }


        private void CreateEnumFile()
        {
            string text =
            "//Automatically generated\n" +
            "#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS\n" +
            "#define GleyIAPEnabled\n" +
            "#endif\n" +
            "#if GleyIAPEnabled\n" +
            "namespace Gley.EasyIAP\n" +
            "{\n" +
            "\tpublic enum ShopProductNames\n" +
            "\t{\n";
            for (int i = 0; i < localShopProducts.Count; i++)
            {
                text += $"\t\t{localShopProducts[i].productName},\n";
            }
            text += "\t}\n";
            text += "}\n";
            text += "#endif";
            File.WriteAllText(Application.dataPath + $"/{rootWithoutAssets}/{Internal.Constants.PRODUCT_NAMES_FILE}", text);
        }
    }
}
