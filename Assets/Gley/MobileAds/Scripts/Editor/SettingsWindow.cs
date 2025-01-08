using Gley.Common;
using Gley.MobileAds.Internal;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gley.MobileAds.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private static string rootFolder;

        private List<AdvertiserSettings> advertiserSettings;
        private MobileAdsSettingsData mobileAdsSettingsData;
        private AdvertiserSettings androidAdvertiserSettings;
        private AdvertiserSettings iOSAdvertiserSettings;
        private Vector2 scrollPosition = Vector2.zero;
        private SupportedAdvertisers selectedAndroidAdvertiser;
        private SupportedAdvertisers selectediOSAdvertiser;
        private string nativePopupText;
        private int step;
        private bool debugMode;
        private bool usePlaymaker;
        private bool useUnityVisualScripting;
        private bool enableATT;
        private bool saving;




        [MenuItem(SettingsWindowProperties.menuItem, false)]
        private static void Init()
        {
            WindowLoader.LoadWindow<SettingsWindow>(new SettingsWindowProperties(), out rootFolder);
        }


        private void OnEnable()
        {
            if (rootFolder == null)
            {
                rootFolder = WindowLoader.GetRootFolder(new SettingsWindowProperties());
            }

            mobileAdsSettingsData = EditorUtilities.LoadOrCreateDataAsset<MobileAdsSettingsData>(rootFolder, Internal.Constants.DATA_PATH_EDITOR, Internal.Constants.DATA_NAME_EDITOR);

            advertiserSettings = new List<AdvertiserSettings>();
            for (int i = 0; i < mobileAdsSettingsData.advertiserSettings.Count; i++)
            {
                advertiserSettings.Add(mobileAdsSettingsData.advertiserSettings[i]);
            }
            UpdateAdvertiserSettings();

            selectedAndroidAdvertiser = mobileAdsSettingsData.selectedAndroidAdvertiser;
            selectediOSAdvertiser = mobileAdsSettingsData.selectediOSAdvertiser;

            androidAdvertiserSettings = GetAdvertiser(selectedAndroidAdvertiser, SupportedPlatforms.Android);
            iOSAdvertiserSettings = GetAdvertiser(selectediOSAdvertiser, SupportedPlatforms.iOS);

            debugMode = mobileAdsSettingsData.debugMode;
            usePlaymaker = mobileAdsSettingsData.usePlaymaker;
            useUnityVisualScripting = mobileAdsSettingsData.useUnityVisualScripting;
            enableATT = mobileAdsSettingsData.enableATT;
            nativePopupText = mobileAdsSettingsData.nativePopupText;
        }

        private void OnInspectorUpdate()
        {
            if (saving)
            {
                if (EditorApplication.isCompiling == false)
                {
                    SaveSettings();
                }
            }
        }


        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;
            EditorStyles.textField.wordWrap = true;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height));
            GUILayout.Label("Advertisement Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            debugMode = EditorGUILayout.Toggle("Debug Mode", debugMode);
            EditorGUILayout.Space();


            //visual scripting
            GUILayout.Label("Enable visual scripting support:", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            usePlaymaker = EditorGUILayout.Toggle("Playmaker ", usePlaymaker);
            useUnityVisualScripting = EditorGUILayout.Toggle("Unity Visual Scripting", useUnityVisualScripting);
            EditorGUILayout.Space();

            GUILayout.Label("Select the ad providers you want to enable for each platform:", EditorStyles.boldLabel);


            //android
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.changed = false;
            selectedAndroidAdvertiser = (SupportedAdvertisers)EditorGUILayout.EnumPopup("Android", selectedAndroidAdvertiser);
            if (GUI.changed)
            {
                androidAdvertiserSettings = GetAdvertiser(selectedAndroidAdvertiser, SupportedPlatforms.Android);
            }

            DisplayAdvertiser(androidAdvertiserSettings, SupportedPlatforms.Android);
            GUILayout.EndVertical();
            EditorGUILayout.Space();


            //iOS
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.changed = false;
            selectediOSAdvertiser = (SupportedAdvertisers)EditorGUILayout.EnumPopup("iOS", selectediOSAdvertiser);
            if (GUI.changed)
            {
                iOSAdvertiserSettings = GetAdvertiser(selectediOSAdvertiser, SupportedPlatforms.iOS);
            }
            DisplayAdvertiser(iOSAdvertiserSettings, SupportedPlatforms.iOS);
            if (iOSAdvertiserSettings != null && iOSAdvertiserSettings.advertiser != SupportedAdvertisers.None)
            {
                //iOS ATT
                if (selectediOSAdvertiser != SupportedAdvertisers.Admob)
                {
                    ShowATTSettings();
                }
                else
                {
                    ShowAdmobSettings();
                }
            }
            GUILayout.EndVertical();
            EditorGUILayout.Space();


            //save settings
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                step = 0;
                SaveSettings();
            }
            EditorGUILayout.Space();


            //docs
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Test Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.testScene}");
            }

            if (GUILayout.Button("Example Scene"))
            {
                EditorSceneManager.OpenScene($"{rootFolder}/{SettingsWindowProperties.exampleScene}");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(SettingsWindowProperties.documentation);
            }


            GUILayout.EndScrollView();
        }

        void ShowAdmobSettings()
        {
            EditorGUILayout.LabelField("iOS 14.5 and above requires publishers to obtain permission to track the user's device across applications. This device setting is called App Tracking Transparency, or ATT.");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("This text will be presented to the user in the confirmation popup that will be automatically displayed:");
            nativePopupText = EditorGUILayout.TextArea(nativePopupText);
        }

        void ShowATTSettings()
        {
            enableATT = EditorGUILayout.Toggle("Enable iOS Tracking", enableATT);
            if (enableATT)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("iOS 14.5 and above requires publishers to obtain permission to track the user's device across applications. This device setting is called App Tracking Transparency, or ATT.\n" +
                    "\nHit import required packages to enable this functionality.");
                if (GUILayout.Button("Import Required iOS Packages"))
                {
                    Debug.Log("Installation started. Please wait");
                    ImportRequiredPackages.ImportPackage("com.unity.ads.ios-support", CompleteMethod);
                }
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("This text will be presented to the user in the confirmation popup that will be automatically displayed:");
                nativePopupText = EditorGUILayout.TextArea(nativePopupText);
            }
            EditorGUILayout.Space();
        }


        //display settings for advertiser
        void DisplayAdvertiser(AdvertiserSettings advertiser, SupportedPlatforms platform)
        {
            if (advertiser != null)
            {
                PlatformSettings platformSettings = advertiser.platformSettings[(int)platform];

                EditorGUILayout.Space();
                if (advertiser.sdkLink.StartsWith("http"))
                {
                    if (GUILayout.Button("Download " + advertiser.advertiser + " SDK"))
                    {
                        Application.OpenURL(advertiser.sdkLink);
                    }
                }
                else
                {
                    string sdkName;
                    switch (advertiser.advertiser)
                    {
                        case SupportedAdvertisers.UnityLegacy:
                            sdkName = "Advertisements Legacy";
                            break;
                        default:
                            sdkName = advertiser.advertiser.ToString();
                            break;

                    }
                    if (GUILayout.Button($"Import {sdkName} SDK"))
                    {
                        ImportRequiredPackages.ImportPackage(advertiser.sdkLink, CompleteMethod);
                    }
                }
                EditorGUILayout.Space();

                if (advertiser.testModeRequired == true)
                {
                    platformSettings.testMode = EditorGUILayout.Toggle("Test Mode", platformSettings.testMode);
                }

                if (advertiser.testDeviceRequired == true)
                {
                    platformSettings.testDevice = EditorGUILayout.TextField("Test Device ID", platformSettings.testDevice);
                }
#if GLEY_ADMOB
                if (!string.IsNullOrEmpty(platformSettings.testDevice))
                {
                    platformSettings.debugGeography = (GoogleMobileAds.Ump.Api.DebugGeography)EditorGUILayout.EnumPopup("Force Geography", platformSettings.debugGeography);
                }
#endif

                if (platformSettings.appId.required == true)
                {
                    platformSettings.appId.id = EditorGUILayout.TextField(platformSettings.appId.displayName, platformSettings.appId.id);
                }

                if (platformSettings.idBanner.required == true)
                {
                    platformSettings.idBanner.id = EditorGUILayout.TextField(platformSettings.idBanner.displayName, platformSettings.idBanner.id);
                }

                if (platformSettings.idMRec.required == true)
                {
                    platformSettings.idMRec.id = EditorGUILayout.TextField(platformSettings.idMRec.displayName, platformSettings.idMRec.id);
                }

                if (platformSettings.idInterstitial.required == true)
                {
                    platformSettings.idInterstitial.id = EditorGUILayout.TextField(platformSettings.idInterstitial.displayName, platformSettings.idInterstitial.id);
                }

                if (platformSettings.idRewarded.required == true)
                {
                    platformSettings.idRewarded.id = EditorGUILayout.TextField(platformSettings.idRewarded.displayName, platformSettings.idRewarded.id);
                }

                if (platformSettings.idRewardedInterstitial.required == true)
                {
                    platformSettings.idRewardedInterstitial.id = EditorGUILayout.TextField(platformSettings.idRewardedInterstitial.displayName, platformSettings.idRewardedInterstitial.id);
                }

                if (platformSettings.idOpenApp.required == true)
                {
                    platformSettings.idOpenApp.id = EditorGUILayout.TextField(platformSettings.idOpenApp.displayName, platformSettings.idOpenApp.id);
                }

                if (advertiser.directedForChildrenRequired == true)
                {
                    advertiser.directedForChildren = EditorGUILayout.Toggle("Directed for children", advertiser.directedForChildren);
                }

                if (platform == SupportedPlatforms.Android)
                {
                    if (advertiser.consentPopupRequired)
                    {
                        advertiser.showConsentPopup = EditorGUILayout.Toggle("Show consent popup", advertiser.showConsentPopup);
                        if (advertiser.showConsentPopup == true)
                        {
                            EditorGUILayout.LabelField("This text will be presented to the user in the popup that will be automatically displayed:");
                            nativePopupText = EditorGUILayout.TextArea(nativePopupText);
                        }
                    }
                }
                EditorGUILayout.Space();
            }
        }


        private AdvertiserSettings GetAdvertiser(SupportedAdvertisers selectedAdvertiser, SupportedPlatforms platform)
        {
            //disable other SDKs
            for (int i = 0; i < advertiserSettings.Count; i++)
            {
                if (advertiserSettings[i].useSDK == true)
                {
                    bool didableSdk = true;
                    foreach (PlatformSettings entry in advertiserSettings[i].platformSettings)
                    {
                        if (entry.platform == platform)
                        {
                            entry.enabled = false;
                        }
                        else
                        {
                            if (entry.enabled == true)
                            {
                                didableSdk = false;
                            }
                        }
                    }
                    if (didableSdk)
                    {
                        advertiserSettings[i].useSDK = false;
                    }
                }
            }

            //activate current SDK
            AdvertiserSettings advertiser = advertiserSettings.FirstOrDefault(cond => cond.advertiser == selectedAdvertiser);
            if (advertiser != null)
            {
                advertiser.useSDK = true;
                advertiser.platformSettings[(int)platform].enabled = true;
            }
            return advertiser;
        }


        //called when packages are imported
        private void CompleteMethod(string message)
        {
            if (message != "InProgress")
            {
                Debug.Log(message);
            }
        }


        private void SaveSettings()
        {
            Debug.Log($"Saving {step + 1}/7");
            saving = false;
            switch (step)
            {
                case 0:
                    mobileAdsSettingsData.debugMode = debugMode;
                    mobileAdsSettingsData.usePlaymaker = usePlaymaker;
                    mobileAdsSettingsData.useUnityVisualScripting = useUnityVisualScripting;
                    mobileAdsSettingsData.enableATT = enableATT;
                    mobileAdsSettingsData.nativePopupText = nativePopupText;
                    mobileAdsSettingsData.selectedAndroidAdvertiser = selectedAndroidAdvertiser;
                    mobileAdsSettingsData.selectediOSAdvertiser = selectediOSAdvertiser;
                    mobileAdsSettingsData.advertiserSettings = new List<AdvertiserSettings>();
                    for (int i = 0; i < advertiserSettings.Count; i++)
                    {
                        mobileAdsSettingsData.advertiserSettings.Add(advertiserSettings[i]);
                    }
                    EditorUtility.SetDirty(mobileAdsSettingsData);

                    GameObject canvas = null;
                    GameObject popup = null;
                    if (androidAdvertiserSettings != null)
                    {
                        if (androidAdvertiserSettings.showConsentPopup)
                        {
                            canvas = AssetDatabase.LoadAssetAtPath($"{rootFolder}/Prefabs/ConsentPopupCanvas.prefab", typeof(GameObject)) as GameObject;
                            popup = AssetDatabase.LoadAssetAtPath($"{rootFolder}/Prefabs/ConsentPopup.prefab", typeof(GameObject)) as GameObject;
                        }
                    }
                    MobileAdsData mobileAdsData = ScriptableObject.CreateInstance("MobileAdsData") as MobileAdsData;
                    mobileAdsData.Init(
                        (androidAdvertiserSettings != null) ? new Advertiser(androidAdvertiserSettings.advertiser, androidAdvertiserSettings.platformSettings[(int)SupportedPlatforms.Android], androidAdvertiserSettings.directedForChildren) : null,
                        (iOSAdvertiserSettings != null) ? new Advertiser(iOSAdvertiserSettings.advertiser, iOSAdvertiserSettings.platformSettings[(int)SupportedPlatforms.iOS], iOSAdvertiserSettings.directedForChildren) : null,
                        mobileAdsSettingsData.debugMode,
                        mobileAdsSettingsData.enableATT,
                        mobileAdsSettingsData.nativePopupText,
                        canvas,
                        popup
                        );

                    EditorUtilities.CreateDataAsset(mobileAdsData, rootFolder, "Resources", Internal.Constants.DATA_NAME_RUNTIME);
                    mobileAdsData = Resources.Load<MobileAdsData>(Internal.Constants.DATA_NAME_RUNTIME);
                    EditorUtility.SetDirty(mobileAdsData);
                    AssetDatabase.Refresh();
                    saving = true;
                    step++;
                    break;
                case 1:
                    if (selectedAndroidAdvertiser == SupportedAdvertisers.Admob || selectediOSAdvertiser == SupportedAdvertisers.Admob)
                    {
                        InstallAdmobPatch();
                    }
                    saving = true;
                    step++;
                    break;
                case 2:
                    SetPreprocessorDirectives();
                    saving = true;
                    step++;
                    break;
                case 3:
                    AddAdmobPatchDirective();
                    saving = true;
                    step++;
                    break;
                case 4:
                    SetAdmobAppID();
                    saving = true;
                    step++;
                    break;
                case 5:
                    AddApplovinSDKkey();
                    saving = true;
                    step++;
                    break;
                default:
                    Debug.Log("Save Done");
                    break;
            }
        }


        void AddAdmobPatchDirective()
        {
#if GLEY_ADMOB
            if (selectedAndroidAdvertiser == SupportedAdvertisers.Admob)
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, false, BuildTargetGroup.Android);
            }
            if (selectediOSAdvertiser == SupportedAdvertisers.Admob)
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, false, BuildTargetGroup.iOS);
            }
#endif
        }

        void SetAdmobAppID()
        {
#if GLEY_PATCH_ADMOB
            if (selectedAndroidAdvertiser == SupportedAdvertisers.Admob || selectediOSAdvertiser == SupportedAdvertisers.Admob)
            {
                string andoidAppID = null;
                string iosAppID = null;
                string nativePopupText = null;
                if (selectedAndroidAdvertiser == SupportedAdvertisers.Admob)
                {
                    andoidAppID = androidAdvertiserSettings.platformSettings[(int)SupportedPlatforms.Android].appId.id;
                }

                if (selectediOSAdvertiser == SupportedAdvertisers.Admob)
                {
                    iosAppID = iOSAdvertiserSettings.platformSettings[(int)SupportedPlatforms.iOS].appId.id;
                    nativePopupText = mobileAdsSettingsData.nativePopupText;
                }

                GoogleMobileAds.Editor.GleyAdmobPatch.SetAdmobAppID(andoidAppID, iosAppID, nativePopupText);

                AssetDatabase.SaveAssets();
            }
#endif
        }


        private void InstallAdmobPatch()
        {
            if (!File.Exists(Application.dataPath + "/GoogleMobileAds/Editor/GleyAdmobPatch.cs"))
            {
                AssetDatabase.ImportPackage($"{Application.dataPath}/{SettingsWindowProperties.admobPatch}", false);
            }
        }

        private void AddApplovinSDKkey()
        {
#if GLEY_APPLOVIN
            if (selectedAndroidAdvertiser == SupportedAdvertisers.AppLovin)
            {
                AppLovinSettings.Instance.SdkKey = androidAdvertiserSettings.platformSettings[(int)SupportedPlatforms.Android].appId.id;
            }
            if (selectediOSAdvertiser == SupportedAdvertisers.AppLovin)
            {
                AppLovinSettings.Instance.SdkKey = iOSAdvertiserSettings.platformSettings[(int)SupportedPlatforms.iOS].appId.id;
            }
            EditorUtility.SetDirty(AppLovinSettings.Instance);
#endif
        }


        //this function should be changed when new advertiser is added
        private void UpdateAdvertiserSettings()
        {
            //Admob
            AdvertiserSettings advertiser = advertiserSettings.Find(cond => cond.advertiser == SupportedAdvertisers.Admob);
            if (advertiser == null)
            {
                advertiserSettings.Add(new AdvertiserSettings(new AdmobSettings()));
            }
            else
            {
                advertiser?.UpdateSettings(new AdmobSettings());
            }


            //AppLovin
            advertiser = advertiserSettings.Find(cond => cond.advertiser == SupportedAdvertisers.AppLovin);
            if (advertiser == null)
            {
                advertiserSettings.Add(new AdvertiserSettings(new ApplovinSettings()));
            }
            else
            {
                advertiser?.UpdateSettings(new ApplovinSettings());
            }

            //Vungle
            advertiser = advertiserSettings.Find(cond => cond.advertiser == SupportedAdvertisers.Vungle);
            if (advertiser == null)
            {
                advertiserSettings.Add(new AdvertiserSettings(new VungleSettings()));
            }
            else
            {
                advertiser.UpdateSettings(new VungleSettings());
            }

            //ironSource
            advertiser = advertiserSettings.Find(cond => cond.advertiser == SupportedAdvertisers.LevelPlay);
            if (advertiser == null)
            {
                advertiserSettings.Add(new AdvertiserSettings(new LevelPlaySettings()));
            }
            else
            {
                advertiser.UpdateSettings(new LevelPlaySettings());
            }

            //Unity Ads
            advertiser = advertiserSettings.Find(cond => cond.advertiser == SupportedAdvertisers.UnityLegacy);
            if (advertiser == null)
            {
                advertiserSettings.Add(new AdvertiserSettings(new UnitySettings()));
            }
            else
            {
                advertiser.UpdateSettings(new UnitySettings());
            }
        }


        private void SetPreprocessorDirectives()
        {
            if (usePlaymaker)
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.iOS);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, false, BuildTargetGroup.WSA);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.iOS);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_PLAYMAKER_SUPPORT, true, BuildTargetGroup.WSA);
            }

            if (useUnityVisualScripting)
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.iOS);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, false, BuildTargetGroup.WSA);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.Android);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.iOS);
                PreprocessorDirective.AddToPlatform(Common.Constants.GLEY_UVS_SUPPORT, true, BuildTargetGroup.WSA);
            }

            Debug.Log("ENABLE ATT "+enableATT);
            if (enableATT)
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_ATT, false, BuildTargetGroup.iOS);
            }
            else
            {
                PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_ATT, true, BuildTargetGroup.iOS);
            }


            for (int i = 0; i < advertiserSettings.Count; i++)
            {
                if (advertiserSettings[i].useSDK == true)
                {
                    for (int j = 0; j < advertiserSettings[i].platformSettings.Count; j++)
                    {
                        if (advertiserSettings[i].platformSettings[j].enabled == true)
                        {
                            if (advertiserSettings[i].platformSettings[j].platform == SupportedPlatforms.Android)
                            {
                                PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, false, BuildTargetGroup.Android);
                            }
                            if (advertiserSettings[i].platformSettings[j].platform == SupportedPlatforms.iOS)
                            {
                                PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, false, BuildTargetGroup.iOS);
                            }
                        }
                        else
                        {
                            if (advertiserSettings[i].platformSettings[j].platform == SupportedPlatforms.Android)
                            {
                                if (advertiserSettings[i].advertiser == SupportedAdvertisers.Admob)
                                {
                                    PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, true, BuildTargetGroup.Android);
                                }
                                PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, true, BuildTargetGroup.Android);
                            }
                            if (advertiserSettings[i].platformSettings[j].platform == SupportedPlatforms.iOS)
                            {
                                if (advertiserSettings[i].advertiser == SupportedAdvertisers.Admob)
                                {
                                    PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, true, BuildTargetGroup.iOS);
                                }
                                PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, true, BuildTargetGroup.iOS);
                            }
                        }
                    }
                }
                else
                {
                    if (advertiserSettings[i].advertiser == SupportedAdvertisers.Admob)
                    {
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, true, BuildTargetGroup.Android);
                        PreprocessorDirective.AddToPlatform(SettingsWindowProperties.GLEY_PATCH_ADMOB, true, BuildTargetGroup.iOS);
                    }
                    PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, true, BuildTargetGroup.Android);
                    PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, true, BuildTargetGroup.iOS);
                    PreprocessorDirective.AddToPlatform(advertiserSettings[i].preprocessorDirective, true, BuildTargetGroup.WSA);
                }
            }
        }



    }
}