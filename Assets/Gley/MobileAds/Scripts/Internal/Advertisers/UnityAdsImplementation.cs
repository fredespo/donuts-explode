#if GLEY_UNITYADS
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Advertisements;
using BannerPosition = Gley.MobileAds.BannerPosition;

namespace Gley.MobileAds.Internal
{
    public class UnityAdsImplementation : MonoBehaviour, IAdProvider, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        const int reloadInterval = 20;
        const int maxRetryCount = 10;

        private UnityAction<bool> OnRewardedVideoClosed;
        private UnityAction OnInterstitialClosed;
        private UnityAction onInitialized;
        private Events events;
        private string unityAdsId;
        private string bannerPlacement;
        private string videoAdPlacement;
        private string rewardedVideoAdPlacement;
        private int retryNumberInterstitial;
        private int retryNumberRewarded;
        private bool interstitialAvailable;
        private bool rewardedAvailable;
        private bool directedForChildren;
        private bool initialized;


        #region Initialize
        #region Interface Implementation
        public void SetDirectedForChildren(bool active)
        {
            directedForChildren = active;
        }

        /// <summary>
        /// Initializing Unity Ads
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            if (initialized == false)
            {
                events = new Events();

                //get settings
                PlatformSettings settings = platformSettings;
                this.onInitialized = onInitialized;

                //apply settings
                unityAdsId = settings.appId.id;
                bannerPlacement = settings.idBanner.id;
                videoAdPlacement = settings.idInterstitial.id;
                rewardedVideoAdPlacement = settings.idRewarded.id;

                //verify settings
                GleyLogger.AddLog($"{settings.appId.displayName} : {settings.appId.id}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerPlacement}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {bannerPlacement}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedVideoAdPlacement}");
                GleyLogger.AddLog($"Directed for children: {directedForChildren}");


                if (directedForChildren)
                {
                    MetaData userMetaData = new MetaData("user");
                    MetaData metaData = new MetaData("privacy");
                    userMetaData.Set("nonbehavioral", "true");
                    metaData.Set("mode", "app");
                    Advertisement.SetMetaData(userMetaData);
                    Advertisement.SetMetaData(metaData);
                }

                Advertisement.Initialize(unityAdsId, settings.testMode, this);
            }
        }
        #endregion

        public void OnInitializationComplete()
        {
            GleyLogger.AddLog("Initialization complete.");
            if (!string.IsNullOrEmpty(videoAdPlacement))
            {
                LoadInterstitialAd();
            }
            if(!string.IsNullOrEmpty(rewardedVideoAdPlacement))
            {
                LoadRewardedVideo();
            }
            
            initialized = true;
            onInitialized?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            GleyLogger.AddLog($"Unity Ads Initialization Failed: {error} - {message}");
        }
        #endregion


        #region Banner
        #region Interface Implementation
        public bool IsBannerAvailable()
        {
            return true;
        }

        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            HideBanner();
            if (IsBannerAvailable())
            {
                UnityEngine.Advertisements.BannerPosition adPosition = UnityEngine.Advertisements.BannerPosition.TOP_CENTER;

                switch (position)
                {
                    case BannerPosition.Top:
                        adPosition = UnityEngine.Advertisements.BannerPosition.TOP_CENTER;
                        break;
                    case BannerPosition.Bottom:
                        adPosition = UnityEngine.Advertisements.BannerPosition.BOTTOM_CENTER;
                        break;
                    case BannerPosition.TopLeft:
                        adPosition = UnityEngine.Advertisements.BannerPosition.TOP_LEFT;
                        break;
                    case BannerPosition.TopRight:
                        adPosition = UnityEngine.Advertisements.BannerPosition.TOP_RIGHT;
                        break;
                    case BannerPosition.BottomLeft:
                        adPosition = UnityEngine.Advertisements.BannerPosition.BOTTOM_LEFT;
                        break;
                    case BannerPosition.BottomRight:
                        adPosition = UnityEngine.Advertisements.BannerPosition.BOTTOM_RIGHT;
                        break;
                    case BannerPosition.Center:
                        adPosition = UnityEngine.Advertisements.BannerPosition.CENTER;
                        break;
                    default:
                        GleyLogger.AddLog($"Banner Position: {position} not supported by Unity Ads, BannerPosition.Top will be used");
                        break;
                }

                Advertisement.Banner.SetPosition(adPosition);

                BannerLoadOptions options = new BannerLoadOptions
                {
                    errorCallback = BannerLoadFailed,
                    loadCallback = BannerLoadSuccess
                };
                GleyLogger.AddLog($"Start Loading Placement: {bannerPlacement}");

                Advertisement.Banner.Load(bannerPlacement, options);
            }
        }

        public void HideBanner()
        {
            GleyLogger.AddLog("Hide Banner");
            Advertisement.Banner.Hide(true);
        }
        #endregion


        private void BannerLoadSuccess()
        {
            GleyLogger.AddLog($"Banner Load Success");

            BannerOptions options = new BannerOptions
            {
                showCallback = BanerDisplayed,
                hideCallback = BannerHidded,
                clickCallback = BannerClicked
            };
            events.TriggerBannerLoadSucces();
            Advertisement.Banner.Show(bannerPlacement, options);
        }


        private void BannerClicked()
        {
            GleyLogger.AddLog("Banner Clicked");
            events.TriggerBannerClicked();
        }


        private void BannerLoadFailed(string message)
        {
            GleyLogger.AddLog($"Banner Load Failed {message}");
            HideBanner();
            events.TriggerBannerLoadFailed(message);
        }


        private void BanerDisplayed()
        {
            GleyLogger.AddLog("Banner Displayed");
        }


        private void BannerHidded()
        {
            GleyLogger.AddLog("Banner Hidden");
        }
        #endregion


        #region Interstitial
        #region Interface Implementation
        /// <summary>
        /// Check if Unity Ads interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            return interstitialAvailable;
        }

        /// <summary>
        /// Show Unity Ads interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (IsInterstitialAvailable())
            {
                OnInterstitialClosed = InterstitialClosed;
                Advertisement.Show(videoAdPlacement, this);
            }
        }
        #endregion

        private void LoadInterstitialAd()
        {
            interstitialAvailable = false;
            GleyLogger.AddLog($"Loading Interstitial Ad: {videoAdPlacement}");
            Advertisement.Load(videoAdPlacement, this);
        }

        #endregion


        #region Rewarded
        #region Interface Implementation
        /// <summary>
        /// Check if Unity Ads rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            return rewardedAvailable;
        }


        /// <summary>
        /// Show Unity Ads rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true, video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (IsRewardedVideoAvailable())
            {
                OnRewardedVideoClosed = CompleteMethod;
                Advertisement.Show(rewardedVideoAdPlacement, this);
            }
        }
        #endregion


        private void LoadRewardedVideo()
        {
            rewardedAvailable = false;
            GleyLogger.AddLog($"Loading Rewarded Video Ad: {rewardedVideoAdPlacement}");

            Advertisement.Load(rewardedVideoAdPlacement, this);
        }
        #endregion


        #region MRec
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            ShowBanner(position, BannerType.MediumRectangle, new Vector2Int(), customPosition);
        }

        public void HideMRec()
        {
            HideBanner();
        }
        #endregion


        #region Events
        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId == videoAdPlacement)
            {
                interstitialAvailable = true;
                GleyLogger.AddLog($"Interstitial Ad Loaded: {placementId}");
                events.TriggerInterstitialLoadSucces();
            }

            if (placementId == rewardedVideoAdPlacement)
            {
                rewardedAvailable = true;
                GleyLogger.AddLog($"Rewarded Video Ad Loaded: {placementId}");
                events.TriggerRewardedVideoLoadSucces();
            }
        }


        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            GleyLogger.AddLog($"Error loading Ad Unit: {placementId} - {error} - {message}");

            if (placementId == videoAdPlacement)
            {
                retryNumberInterstitial++;
                if (retryNumberInterstitial < maxRetryCount)
                {
                    Invoke("LoadInterstitialAd", reloadInterval);
                }
                events.TriggerInterstitialLoadFailed($"{placementId} - {error} - {message}");
            }
            if (placementId == rewardedVideoAdPlacement)
            {
                retryNumberRewarded++;
                if (retryNumberRewarded < maxRetryCount)
                {
                    Invoke("LoadRewardedVideo", reloadInterval);
                }
                events.TriggerRewardedVideoLoadFailed($"{placementId} - {error} - {message}");
            }
        }


        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            GleyLogger.AddLog($"Error showing Ad Unit {placementId}: {error} - {message}");

            if (placementId == videoAdPlacement)
            {
                LoadInterstitialAd();
            }

            if (placementId == rewardedVideoAdPlacement)
            {
                LoadRewardedVideo();
            }
        }


        public void OnUnityAdsShowStart(string placementId)
        {
            GleyLogger.AddLog($"Ad Shown: {placementId}");

            if (placementId == videoAdPlacement)
            {
                retryNumberInterstitial = 0;
            }

            if (placementId == rewardedVideoAdPlacement)
            {
                retryNumberRewarded = 0;
            }
        }


        public void OnUnityAdsShowClick(string placementId)
        {
            GleyLogger.AddLog($"Ad Clicked: {placementId}");
            if (placementId == videoAdPlacement)
            {
                events.TriggerInterstitialClicked();
            }

            if (placementId == rewardedVideoAdPlacement)
            {
                events.TriggerRewardedVideoClicked();
            }
        }


        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            GleyLogger.AddLog($"OnUnityAdsShowComplete {placementId}");
            if (placementId == videoAdPlacement)
            {
                GleyLogger.AddLog($"Interstitial result: {showCompletionState}");

                if (OnInterstitialClosed != null)
                {
                    OnInterstitialClosed();
                    OnInterstitialClosed = null;
                }

                LoadInterstitialAd();
            }

            if (placementId == rewardedVideoAdPlacement)
            {
                GleyLogger.AddLog($"Complete method result: {showCompletionState}");
                if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                {
                    if (OnRewardedVideoClosed != null)
                    {
                        OnRewardedVideoClosed(true);
                        OnRewardedVideoClosed = null;
                    }
                }
                else
                {
                    if (OnRewardedVideoClosed != null)
                    {
                        OnRewardedVideoClosed(false);
                        OnRewardedVideoClosed = null;
                    }
                }
                LoadRewardedVideo();
            }
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {

        }
        #endregion


        #region Auto Consent
        public bool HasBuiltInConsentWindow()
        {
            return false;
        }

        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            GleyLogger.AddLog($"ShowBuiltInConsentWindow Not supported on {SupportedAdvertisers.UnityLegacy}");
        }
        #endregion


        #region ManualConsent
        #region InterfaceImplementation
        public bool GDPRConsentWasSet()
        {
            return ConsentWasSet(Constants.GDPR_KEY);
        }


        /// <summary>
        /// Used to set user consent that will be later forwarded to each advertiser SDK
        /// Should be set before initializing the SDK
        /// </summary>
        /// <param name="accept">if true -> show personalized ads, if false show unpersonalized ads</param>
        public void SetGDPRConsent(bool accept)
        {
            MetaData gdprMetaData = new MetaData("gdpr");
            if (accept == true)
            {
                gdprMetaData.Set("consent", "true");
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Accept);
            }
            else
            {
                gdprMetaData.Set("consent", "false");
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Deny);
            }
            Advertisement.SetMetaData(gdprMetaData);
        }


        public UserConsent GetGDPRConsent()
        {
            return GetConsent(Constants.GDPR_KEY);
        }


        public bool CCPAConsentWasSet()
        {
            return ConsentWasSet(Constants.CCPA_KEY);
        }


        /// <summary>
        /// Used to set user consent that will be later forwarded to each advertiser SDK
        /// Should be set before initializing the SDK
        /// </summary>
        /// <param name="accept">if true -> show personalized ads, if false show unpersonalized ads</param>
        public void SetCCPAConsent(bool accept)
        {
            MetaData privacyMetaData = new MetaData("privacy");
            if (accept == true)
            {
                privacyMetaData.Set("consent", "true");
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Accept);
            }
            else
            {
                privacyMetaData.Set("consent", "false");
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Deny);
            }
            Advertisement.SetMetaData(privacyMetaData);
        }


        public UserConsent GetCCPAConsent()
        {
            return GetConsent(Constants.CCPA_KEY);
        }
        #endregion


        private UserConsent GetConsent(string fileName)
        {
            if (!ConsentWasSet(fileName))
                return UserConsent.Unset;
            return (UserConsent)PlayerPrefs.GetInt(fileName);
        }


        private bool ConsentWasSet(string fileName)
        {
            return PlayerPrefs.HasKey(fileName);
        }
        #endregion


        #region Debug
        public void OpenDebugWindow()
        {
            GleyLogger.AddLog($"OpenDebugWindow Not supported on {SupportedAdvertisers.UnityLegacy}");
        }
        #endregion


        #region NotSupported
        public bool IsAppOpenAvailable()
        {
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            GleyLogger.AddLog($"ShowAppOpen Not supported on {SupportedAdvertisers.UnityLegacy}");
        }

        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            GleyLogger.AddLog($"ShowRewardedInterstitial Not supported on {SupportedAdvertisers.UnityLegacy}");
        }
        #endregion
    }
}
#endif
