#if GLEY_ADMOB
using GoogleMobileAds.Ump.Api;
#endif
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gley.MobileAds.Internal
{
    public class MobileAdsManager : MonoBehaviour
    {
        //static instance of the class to be used anywhere in code
        private static MobileAdsManager instance;
        public static MobileAdsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "MobieAdsScripts";
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<MobileAdsManager>();
                }
                return instance;
            }
        }

        private Advertiser selectedAdvertiser;
        private Events events;
        private UnityAction onInitialized;
        //stores plugin all settings
        private MobileAdsData adSettings;
        private bool initialized;
        private bool continueInitialization;

        /// <summary>
        /// automatically disables banner and interstitial ads
        /// </summary>
        /// <param name="remove">if true, no banner and interstitials will be shown in your app</param>
        public void RemoveAds(bool remove)
        {
            if (remove == true)
            {
                PlayerPrefs.SetInt(Constants.REMOVE_ADS_KEY, 1);
                //if banner is active and user bought remove ads the banner will automatically hide
                HideBanner();
            }
            else
            {
                PlayerPrefs.SetInt(Constants.REMOVE_ADS_KEY, 0);
            }
        }


        /// <summary>
        /// check if ads are not disabled by user
        /// </summary>
        /// <returns>true if ads should be displayed</returns>
        public bool CanShowAds()
        {
            if (!PlayerPrefs.HasKey(Constants.REMOVE_ADS_KEY))
            {
                return true;
            }
            else
            {
                if (PlayerPrefs.GetInt(Constants.REMOVE_ADS_KEY) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Initializes all the advertisers from the plugin
        /// Should be called only once at the beginning of your app
        /// </summary>
        public void Initialize(UnityAction completeMethod)
        {
            if (initialized == false)
            {
                events = new Events();
                onInitialized = completeMethod;
                adSettings = Resources.Load<MobileAdsData>(Constants.DATA_NAME_RUNTIME);
                if (adSettings == null)
                {
                    Debug.LogError("Gley Ads Plugin is not properly configured. Go to Tools->Gley->Mobile Ads to set up the plugin. See the documentation [LINK]");
                    return;
                }
                if (adSettings.debugMode)
                {
                    GleyLogger.Initialize();
                }


#if UNITY_ANDROID
                selectedAdvertiser = GetRequiredAdvertiser(adSettings.androidAdvertiser);
#elif UNITY_IOS
                selectedAdvertiser = GetRequiredAdvertiser(adSettings.iOSAdvertiser);
#else
                Debug.LogWarning("Current platform not supported");
#endif
                GleyLogger.AddLog($"Selected advertiser: {selectedAdvertiser.advertiser}");

                selectedAdvertiser.advertiserScript.SetDirectedForChildren(selectedAdvertiser.directedForChildren);
#if UNITY_ANDROID
#if GLEY_ADMOB
                ((AdmobImplementation)selectedAdvertiser.advertiserScript).InitializaConsentWindow(adSettings.androidAdvertiser.platformSettings.debugGeography, adSettings.androidAdvertiser.platformSettings.testDevice);
#endif
                if (selectedAdvertiser.advertiserScript.HasBuiltInConsentWindow())
                {
                    //ConsentInformation.Reset();
                    if (!selectedAdvertiser.advertiserScript.GDPRConsentWasSet())
                    {
                        selectedAdvertiser.advertiserScript.ShowBuiltInConsentWindow(ConsentPopupClosed);
                        return;
                    }
                }
                else
                {
                    //PlayerPrefs.DeleteAll();
                    if (adSettings.consentCanvas != null && adSettings.consentPopup != null)
                    {
                        if (!selectedAdvertiser.advertiserScript.GDPRConsentWasSet())
                        {
                            LoadPopup(adSettings.consentCanvas, adSettings.consentPopup, ConsentPopupClosed);
                            return;
                        }
                    }
                }
#endif
#if UNITY_IOS
                //Admob iOS
                //ConsentInformation.Reset();
                if (selectedAdvertiser.advertiser == SupportedAdvertisers.Admob)
                {
                    if (!selectedAdvertiser.advertiserScript.GDPRConsentWasSet())
                    {
                        selectedAdvertiser.advertiserScript.ShowBuiltInConsentWindow(ContinueInitialization);     
                    }
                    else
                    {
                        ContinueInitialization();
                    }
                    return;
                }

#endif
#if UNITY_IOS
#if GLEY_ATT
                //Rest of the advertisers
                //App Tracking Transparency
                var status = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

                if (status == Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    Unity.Advertisement.IosSupport.ATTrackingStatusBinding.RequestAuthorizationTracking();
                }
#endif
#endif
                StartCoroutine(WaitForConsent(ContinueInitialization));
            }
        }



#if UNITY_ANDROID
        void ConsentPopupClosed()
        {
            ContinueInitialization();
        }
#endif

        internal void ShowATTPopup()
        {
            StartCoroutine(ShowATTPopupCoroutine());
        }

        internal IEnumerator ShowATTPopupCoroutine()
        {
#if GLEY_ATT
            string version = UnityEngine.iOS.Device.systemVersion;
            if (string.IsNullOrEmpty(version))
            {
                Debug.Log("ATT popup only works on a real iOS device");
                yield break;
            }

            System.Version ver = System.Version.Parse(UnityEngine.iOS.Device.systemVersion);
            if (ver.Major >= 14)
            {
                Unity.Advertisement.IosSupport.ATTrackingStatusBinding.RequestAuthorizationTracking();
                yield return null;
                var status = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                while (status == Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    status = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                    yield return null;
                }
                if (selectedAdvertiser == null)
                    yield break;

                if (selectedAdvertiser.advertiser != SupportedAdvertisers.Admob &&
                    selectedAdvertiser.advertiser != SupportedAdvertisers.UnityLegacy &&
                    selectedAdvertiser.advertiser != SupportedAdvertisers.Vungle)
                {
                    switch (status)
                    {
                        case Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED:
                            SetGDPRConsent(true);
                            break;
                        case Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED:
                            SetGDPRConsent(false);
                            break;
                        default:
                            SetGDPRConsent(true);
                            break;
                    }
                }
            }
#endif
            yield return null;
        }

        private void LoadPopup(GameObject canvas, GameObject popup, UnityAction consentPopupClosed)
        {
            Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            int max = 1;
            if (allCanvases.Length > 0)
            {
                max = allCanvases.Max(cond => cond.sortingOrder);
            }
            Transform consentCanvas = Instantiate(canvas).transform;
            consentCanvas.GetComponent<Canvas>().sortingOrder = IncreaseSortingOrder(max);
            if (Screen.width > Screen.height)
            {
                consentCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            }
            else
            {
                consentCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
            }

            Transform consentPopup = Instantiate(popup).transform;
            consentPopup.SetParent(consentCanvas, false);
            consentPopup.GetComponent<ConsentPopup>().Initialize(adSettings.nativePopupText, consentPopupClosed);
        }

        private int IncreaseSortingOrder(int oldLayer)
        {
            int result = oldLayer + 1;
            if (result > short.MaxValue)
            {
                result = short.MaxValue;
            }
            return result;
        }

        private IEnumerator WaitForConsent(UnityAction Continue)
        {
#if UNITY_IOS && !UNITY_EDITOR
#if GLEY_ATT
            System.Version ver = System.Version.Parse(UnityEngine.iOS.Device.systemVersion);
            if (ver.Major >= 14)
            {
                var status = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                while (status == Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    status = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                    yield return null;
                }

                if (selectedAdvertiser.advertiser != SupportedAdvertisers.Admob &&
                    selectedAdvertiser.advertiser != SupportedAdvertisers.UnityLegacy &&
                    selectedAdvertiser.advertiser != SupportedAdvertisers.Vungle)
                {
                    switch (status)
                    {
                        case Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED:
                            SetGDPRConsent(true);
                            break;
                        case Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED:
                            SetGDPRConsent(false);
                            break;
                        default:
                            SetGDPRConsent(true);
                            break;
                    }
                }
            }
#endif
#endif

            yield return null;
            if (Continue != null)
            {
                Continue();
            }
            yield return null;
        }


        private void ContinueInitialization()
        {
            continueInitialization = true;
        }

        void InitializeAdvertiser()
        {
            selectedAdvertiser.advertiserScript.InitializeAds(selectedAdvertiser.platformSettings, OnInitialized);
#if UNITY_ANDROID
            GleyLogger.AddLog($"User GDPR consent is set to: {selectedAdvertiser.advertiserScript.GetGDPRConsent()}");
            GleyLogger.AddLog($"User CCPA consent is set to: {selectedAdvertiser.advertiserScript.GetCCPAConsent()}");
#endif
        }

        void OnInitialized()
        {
            initialized = true;
            onInitialized?.Invoke();
            events.TriggerOnInitialized();
        }

        public bool IsInitialized()
        {
            return initialized;
        }

        private void Update()
        {
            if (continueInitialization)
            {
                continueInitialization = false;
                InitializeAdvertiser();
            }
        }

        private Advertiser GetRequiredAdvertiser(Advertiser advertiser)
        {
            if (advertiser != null)
            {
                switch (advertiser.advertiser)
                {
#if GLEY_ADMOB
                    case SupportedAdvertisers.Admob:
                        return new Advertiser(SupportedAdvertisers.Admob, gameObject.AddComponent<AdmobImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
#endif
#if GLEY_ADCOLONY && !UNITY_EDITOR
                    case SupportedAdvertisers.AdColony:
                        return new Advertiser(SupportedAdvertisers.AdColony, gameObject.AddComponent<AdColonyImplementation>(), advertiser.platformSettings, advertiser.directedForChildren); 
#endif
#if GLEY_APPLOVIN
                    case SupportedAdvertisers.AppLovin:
                        return new Advertiser(SupportedAdvertisers.AppLovin, gameObject.AddComponent<AppLovinImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
#endif
#if GLEY_VUNGLE
                    case SupportedAdvertisers.Vungle:
                        return new Advertiser(SupportedAdvertisers.Vungle, gameObject.AddComponent<VungleImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
#endif
#if GLEY_LEVELPLAY
                    case SupportedAdvertisers.LevelPlay:
                        return new Advertiser(SupportedAdvertisers.LevelPlay, gameObject.AddComponent<LevelPlayImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
#endif

#if GLEY_UNITYADS
                    case SupportedAdvertisers.UnityLegacy:
                        return new Advertiser(SupportedAdvertisers.UnityLegacy, gameObject.AddComponent<UnityAdsImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
#endif
                    default:
                        return new Advertiser(SupportedAdvertisers.None, gameObject.AddComponent<DefaultImplementation>(), advertiser.platformSettings, advertiser.directedForChildren);
                }
            }
            //default advertiser
            return null;
        }


        public Advertiser GetSelectedAdvertiser()
        {
            return selectedAdvertiser;
        }


        /// <summary>
        /// Displays an interstitial from the requested advertiser, if the requested advertiser is not available, another interstitial will be displayed based on your mediation settings
        /// </summary>
        /// <param name="advertiser">advertiser from which ad will be displayed if available</param>
        /// <param name="InterstitialClosed">callback triggered when interstitial video is closed</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (!IsInitialized())
            {
                return;
            }
            //if ads are disabled by user -> do nothing
            if (CanShowAds() == false)
            {
                return;
            }

            if (selectedAdvertiser.advertiserScript.IsInterstitialAvailable())
            {
                GleyLogger.AddLog($"Interstitial from {selectedAdvertiser.advertiser} is available");
                selectedAdvertiser.advertiserScript.ShowInterstitial(InterstitialClosed);
            }
            else
            {
                GleyLogger.AddLog($"Interstitial from {selectedAdvertiser.advertiser} is NOT available");
            }
        }


        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            if (!IsInitialized())
            {
                return;
            }

            //if ads are disabled by user -> do nothing
            if (CanShowAds() == false)
            {
                return;
            }

            if (selectedAdvertiser.advertiserScript.IsAppOpenAvailable())
            {
                GleyLogger.AddLog($"App Open from {selectedAdvertiser.advertiser} is available");
                selectedAdvertiser.advertiserScript.ShowAppOpen(appOpenClosed);
            }
            else
            {
                GleyLogger.AddLog($"App Open from {selectedAdvertiser.advertiser} is NOT available");
            }
        }


        /// <summary>
        /// Displays a rewarded video based on advertiser sent as parameter, if the requested advertiser is not available selected mediation settings are used
        /// </summary>
        /// <param name="advertiser">the advertiser from which you want to display the rewarded video</param>
        /// <param name="CompleteMethod">callback triggered when video reward finished - if bool param is true => video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (!IsInitialized())
            {
                return;
            }

            if (selectedAdvertiser.advertiserScript.IsRewardedVideoAvailable())
            {
                GleyLogger.AddLog($"Rewarded video from {selectedAdvertiser.advertiser} is available");
                selectedAdvertiser.advertiserScript.ShowRewardedVideo(CompleteMethod);
            }
            else
            {
                GleyLogger.AddLog($"Rewarded video from {selectedAdvertiser.advertiser} is NOT available");
            }
        }


        public void ShowRewardedInterstitial(UnityAction<bool> CompleteMethod)
        {
            if (!IsInitialized())
            {
                return;
            }

            if (selectedAdvertiser.advertiserScript.IsRewardedInterstitialAvailable())
            {
                GleyLogger.AddLog($"Rewarded Interstitial from {selectedAdvertiser.advertiser} is available");
                selectedAdvertiser.advertiserScript.ShowRewardedInterstitial(CompleteMethod);
            }
            else
            {
                GleyLogger.AddLog($"Rewarded Interstitial from {selectedAdvertiser.advertiser} is NOT available");
            }
        }


        /// <summary>
        /// Displays a banner from advertiser used as parameter
        /// </summary>
        /// <param name="advertiser">Advertiser to show banner from</param>
        /// <param name="position">Top or Bottom</param>

        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customPosition, Vector2Int customSize)
        {
            if (!IsInitialized())
            {
                return;
            }

            if (CanShowAds() == false)
            {
                return;
            }
            GleyLogger.AddLog($"Banner loaded from {selectedAdvertiser.advertiser}");
            selectedAdvertiser.advertiserScript.ShowBanner(position, bannerType, customSize, customPosition);
        }


        /// <summary>
        /// Hides the active banner
        /// </summary>
        public void HideBanner()
        {
            if (!IsInitialized())
            {
                return;
            }

            GleyLogger.AddLog($"Hide banner {selectedAdvertiser.advertiser}");
            selectedAdvertiser.advertiserScript.HideBanner();
        }


        /// <summary>
        /// Check if any rewarded video is available to be played
        /// </summary>
        /// <returns>true if at least one rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            if (!IsInitialized())
            {
                return false;
            }
            return selectedAdvertiser.advertiserScript.IsRewardedVideoAvailable();
        }


        public bool IsRewardedInterstitialAvailable()
        {
            if (!IsInitialized())
            {
                return false;
            }
            return selectedAdvertiser.advertiserScript.IsRewardedInterstitialAvailable();
        }


        public bool IsAppOpenAvailable()
        {
            if (!IsInitialized())
            {
                return false;
            }
            return selectedAdvertiser.advertiserScript.IsAppOpenAvailable();
        }

        /// <summary>
        /// Check if any interstitial is available
        /// </summary>
        /// <returns>true if at least one interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            if (!IsInitialized())
            {
                return false;
            }
            //if ads are disabled by user -> interstitial is not available
            if (CanShowAds() == false)
            {
                return false;
            }

            return selectedAdvertiser.advertiserScript.IsInterstitialAvailable();
        }

        internal void OpenDebugWindow()
        {
            selectedAdvertiser.advertiserScript.OpenDebugWindow();
        }

        internal void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            if (selectedAdvertiser.advertiserScript.HasBuiltInConsentWindow())
            {
                selectedAdvertiser.advertiserScript.ShowBuiltInConsentWindow(consentPopupClosed);
                return;
            }
            else
            {
                if (adSettings.consentCanvas != null && adSettings.consentPopup != null)
                {
                    LoadPopup(adSettings.consentCanvas, adSettings.consentPopup, consentPopupClosed);
                    return;
                }
            }
            GleyLogger.AddLog("No built in consent popup found");
        }

        internal bool GDPRConsentWasSet()
        {
            return selectedAdvertiser.advertiserScript.GDPRConsentWasSet();
        }

        internal void SetGDPRConsent(bool accept)
        {
            selectedAdvertiser.advertiserScript.SetGDPRConsent(accept);
        }

        internal bool CCPAConsentWasSet()
        {
            return selectedAdvertiser.advertiserScript.CCPAConsentWasSet();
        }

        internal void SetCCPAConsent(bool accept)
        {
            selectedAdvertiser.advertiserScript.SetCCPAConsent(accept);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                if (selectedAdvertiser != null)
                {
                    selectedAdvertiser.advertiserScript.LoadAdsOnResume();
                }
            }
        }

        internal void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            if (!IsInitialized())
            {
                return;
            }

            selectedAdvertiser.advertiserScript.ShowMRec(position, customPosition);
        }

        internal void HideMRec()
        {
            if (!IsInitialized())
            {
                return;
            }

            selectedAdvertiser.advertiserScript.HideMRec();
        }
    }
}
