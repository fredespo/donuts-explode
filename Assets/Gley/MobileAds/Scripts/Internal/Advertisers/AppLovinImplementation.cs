#if GLEY_APPLOVIN
using UnityEngine;
using UnityEngine.Events;

namespace Gley.MobileAds.Internal
{
    public class AppLovinImplementation : MonoBehaviour, IAdProvider
    {
        const int reloadInterval = 20;
        const int maxRetryCount = 10;

        private UnityAction onInterstitialClosed;
        private UnityAction onAppOpenClosed;
        private UnityAction<bool> onRewardedVideoClosed;
        private UnityAction onInitialized;
        private Events events;
        private UserConsent gdprConsent;
        private UserConsent ccpaConsent;
        private string bannerId;
        private string interstitialId;
        private string appOpenId;
        private string rewardedVideoId;
        private string mRecId;
        private int retryNumberInterstitial;
        private int retryNumberAppOpen;
        private int retryNumberRewarded;
        private bool initialized;
        private bool rewardedVideoCompleted;


        #region Initialize
        #region InterfaceImplementation
        public void SetDirectedForChildren(bool active)
        {
            
        }

        /// <summary>
        /// Initializing AppLovin
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            this.onInitialized = onInitialized;
            this.gdprConsent = GetConsent(Constants.GDPR_KEY);
            this.ccpaConsent = GetConsent(Constants.CCPA_KEY);
            if (initialized == false)
            {
                events = new Events();
                if (GleyLogger.IsInitialized())
                {
                    MaxSdk.SetVerboseLogging(true);
                }
                //get settings
                PlatformSettings settings = platformSettings;


                //preparing AppLovin SDK for initialization
                interstitialId = settings.idInterstitial.id;
                bannerId = settings.idBanner.id;
                rewardedVideoId = settings.idRewarded.id;
                mRecId = settings.idMRec.id;
                appOpenId = settings.idOpenApp.id;
                MaxSdkCallbacks.OnSdkInitializedEvent += ApplovinInitialized;


                //Initialize the SDK
                //MaxSdk.SetSdkKey(settings.appId.id.ToString());
                MaxSdk.InitializeSdk();

                //verify settings
                GleyLogger.AddLog($"{settings.appId.displayName} : {settings.appId.id}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerId}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {interstitialId}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedVideoId}");
                GleyLogger.AddLog($"{settings.idMRec.displayName} : {mRecId}");
                GleyLogger.AddLog($"{settings.idOpenApp.displayName} : {appOpenId}");
            }
        }
        #endregion


        private void ApplovinInitialized(MaxSdk.SdkConfiguration sdkConfiguration)
        {

            GleyLogger.AddLog($"Initialization Successful: {sdkConfiguration.IsSuccessfullyInitialized}");
            if (gdprConsent == UserConsent.Accept || gdprConsent == UserConsent.Unset)
            {
                MaxSdk.SetHasUserConsent(true);
            }
            else
            {
                MaxSdk.SetHasUserConsent(false);
            }

            if (ccpaConsent == UserConsent.Accept || ccpaConsent == UserConsent.Unset)
            {
                MaxSdk.SetDoNotSell(false);
            }
            else
            {
                MaxSdk.SetDoNotSell(true);
            }

            if (!string.IsNullOrEmpty(bannerId))
            {
                MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
                MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
                MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
                MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
            }

            if (!string.IsNullOrEmpty(interstitialId))
            {
                // Attach callbacks
                MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
                MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
                MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
                MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
                MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;
                MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;

                LoadInterstitial();
            }

            if (!string.IsNullOrEmpty(rewardedVideoId))
            {
                // Attach callbacks
                MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
                MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
                MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
                MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
                MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
                MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
                MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
                MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

                //start loading ads
                LoadRewardedVideo();
            }

            if (!string.IsNullOrEmpty(mRecId))
            {
                GleyLogger.AddLog("ADD EVENTS");
                MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
                MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdLoadFailedEvent;
                MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
                MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;
                MaxSdkCallbacks.MRec.OnAdExpandedEvent += OnMRecAdExpandedEvent;
                MaxSdkCallbacks.MRec.OnAdCollapsedEvent += OnMRecAdCollapsedEvent;
            }

            if (!string.IsNullOrEmpty(appOpenId))
            {
                MaxSdkCallbacks.AppOpen.OnAdClickedEvent += OnAppOpenAdClickedEvent;
                MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += OnAppOpenAdDisplayedEvent;
                MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += OnAppOpenAdDisplayFailedEvent;
                MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += OnAppOpenAdLoadedEvent;
                MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAppOpenAdRevenuePaidEvent;
                MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += OnAppOpenAdLoadFailedEvent;
                MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenAdHiddenEvent;
                LoadAppOpen();
            }

            initialized = true;
            onInitialized?.Invoke();
        }

        #endregion


        #region Banner
        #region InterfaceImplementation
        /// <summary>
        /// Check if AppLovin banner is available
        /// </summary>
        /// <returns>always returns true, AppLovin does not have such a method for banners</returns>
        public bool IsBannerAvailable()
        {
            return true;
        }


        /// <summary>
        /// Show AppLovin banner
        /// </summary>
        /// <param name="position">can be TOP of BOTTOM</param>
        /// <param name="bannerType">it is not used in AppLovin, this param is used just in Admob implementation</param>
        /// 
        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            if (string.IsNullOrEmpty(bannerId))
                return;
            LoadBanner(position, bannerType, customPosition, customSize);
            MaxSdk.ShowBanner(bannerId);
        }


        /// <summary>
        /// Hides AppLovin banner
        /// </summary>
        public void HideBanner()
        {
            if (string.IsNullOrEmpty(bannerId))
                return;
            MaxSdk.HideBanner(bannerId);
        }
        #endregion


        private void LoadBanner(BannerPosition position, BannerType bannerType, Vector2Int customPosition, Vector2Int customSize)
        {
            if (string.IsNullOrEmpty(bannerId))
                return;
            MaxSdk.DestroyBanner(bannerId);

            MaxSdkBase.BannerPosition adPosition = MaxSdkBase.BannerPosition.TopCenter;

            switch (position)
            {
                case BannerPosition.Top:
                    adPosition = MaxSdkBase.BannerPosition.TopCenter;
                    break;
                case BannerPosition.Bottom:
                    adPosition = MaxSdkBase.BannerPosition.BottomCenter;
                    break;
                case BannerPosition.TopLeft:
                    adPosition = MaxSdkBase.BannerPosition.TopLeft;
                    break;
                case BannerPosition.TopRight:
                    adPosition = MaxSdkBase.BannerPosition.TopRight;
                    break;
                case BannerPosition.BottomLeft:
                    adPosition = MaxSdkBase.BannerPosition.BottomLeft;
                    break;
                case BannerPosition.BottomRight:
                    adPosition = MaxSdkBase.BannerPosition.BottomRight;
                    break;
                case BannerPosition.Center:
                    adPosition = MaxSdkBase.BannerPosition.Centered;
                    break;
                case BannerPosition.CenterLeft:
                    adPosition = MaxSdkBase.BannerPosition.CenterLeft;
                    break;
                case BannerPosition.CenterRight:
                    adPosition = MaxSdkBase.BannerPosition.CenterRight;
                    break;
                case BannerPosition.Custom:
                    break;
                default:
                    GleyLogger.AddLog($"Banner Position: {position} not supported by AppLovin, BannerPosition.Top will be used");
                    break;
            }

            switch (bannerType)
            {
                case BannerType.Banner:
                    MaxSdk.SetBannerExtraParameter(bannerId, "adaptive_banner", "false");
                    break;
                case BannerType.Adaptive:
                    MaxSdk.SetBannerExtraParameter(bannerId, "adaptive_banner", "true");
                    break;
                case BannerType.Custom:
                    MaxSdk.SetBannerWidth(bannerId, customSize.x);
                    break;
                default:
                    GleyLogger.AddLog($"Banner Type: {bannerType} not supported by AppLovin, Default banner behavior will be used");
                    MaxSdk.SetBannerExtraParameter(bannerId, "adaptive_banner", "true");
                    break;
            }



            if (position == BannerPosition.Custom)
            {
                MaxSdk.CreateBanner(bannerId, customPosition.x, customPosition.y);
            }
            else
            {
                MaxSdk.CreateBanner(bannerId, adPosition);
            }

            // Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(bannerId, new Color(1, 1, 1, 0));
        }


        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Banner ad loaded");
            events.TriggerBannerLoadSucces();
        }


        private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            GleyLogger.AddLog($"Banner ad failed to load {errorInfo.Code} {errorInfo.Message}");
            events.TriggerBannerLoadFailed(errorInfo.ToString());
        }


        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Banner ad clicked");
            events.TriggerBannerClicked();
        }


        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Banner ad revenue paid");
        }
        #endregion


        #region Interstitial
        #region InterfaceImplementation
        /// <summary>
        /// Check if AppLovin interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            if (!initialized)
                return false;
            if (string.IsNullOrEmpty(interstitialId))
                return false;
            return MaxSdk.IsInterstitialReady(interstitialId);
        }


        /// <summary>
        /// Show AppLovin interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (IsInterstitialAvailable())
            {
                onInterstitialClosed = InterstitialClosed;
                MaxSdk.ShowInterstitial(interstitialId);
            }
        }
        #endregion


        /// <summary>
        /// Preload an interstitial ad before showing
        /// if it fails for maxRetryCount times do not try anymore
        /// </summary>
        private void LoadInterstitial()
        {
            if (retryNumberInterstitial < maxRetryCount)
            {
                GleyLogger.AddLog("Load Interstitial");
                retryNumberInterstitial++;
                MaxSdk.LoadInterstitial(interstitialId);
            }
        }


        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Interstitial ad was loaded " + adInfo.NetworkName);
            retryNumberInterstitial = 0;
            events.TriggerInterstitialLoadSucces();
        }


        private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            GleyLogger.AddLog($"Interstitial ad failed to load {errorInfo.Code}");
            GleyLogger.AddLog($"Reloading {retryNumberInterstitial} in {reloadInterval}  sec");

            //wait and load another
            Invoke("LoadInterstitial", reloadInterval);
            events.TriggerInterstitialLoadFailed(errorInfo.ToString());
        }


        private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog(this + " interstitial ad failed to display " + errorInfo.Code);
            LoadInterstitial();
        }


        private void OnInterstitialDismissedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {

            GleyLogger.AddLog("Interstitial ad was closed");

            //trigger closed callback
            if (onInterstitialClosed != null)
            {
                onInterstitialClosed();
                onInterstitialClosed = null;
            }

            //load another ad
            LoadInterstitial();
        }

        private void OnInterstitialClickedEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog($"OnInterstitialClickedEvent {info}");
            events.TriggerInterstitialClicked();
        }

        private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog($"OnInterstitialRevenuePaidEvent {adInfo}");
        }
        #endregion


        #region AppOpen
        #region InterfaceImplementation
        public bool IsAppOpenAvailable()
        {
            if (!initialized)
                return false;
            if (string.IsNullOrEmpty(appOpenId))
                return false;
            return MaxSdk.IsAppOpenAdReady(appOpenId);
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            if (IsAppOpenAvailable())
            {
                onAppOpenClosed = appOpenClosed;
                MaxSdk.ShowAppOpenAd(appOpenId);
            }
        }
        #endregion
        private void LoadAppOpen()
        {
            if (retryNumberAppOpen < maxRetryCount)
            {
                GleyLogger.AddLog("Load App Open");
                retryNumberAppOpen++;
                MaxSdk.LoadAppOpenAd(appOpenId);
            }
        }

        private void OnAppOpenAdHiddenEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog("App Open ad was closed");

            //trigger closed callback
            if (onAppOpenClosed != null)
            {
                onAppOpenClosed();
                onAppOpenClosed = null;
            }

            //load another ad
            LoadAppOpen();
        }

        private void OnAppOpenAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo info)
        {
            GleyLogger.AddLog($"Open App ad failed to load {info}");

            //wait and load another
            Invoke("LoadAppOpen", reloadInterval);
            events.TriggerAppOpenLoadFailed(info.ToString());
        }

        private void OnAppOpenAdRevenuePaidEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog($"App Open revenue paid {info}");
        }

        private void OnAppOpenAdLoadedEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog($"App Open ad was loaded {info}");
            retryNumberAppOpen = 0;
            events.TriggerAppOpenLoadSucces();
        }

        private void OnAppOpenAdDisplayFailedEvent(string arg1, MaxSdkBase.ErrorInfo info1, MaxSdkBase.AdInfo info2)
        {
            GleyLogger.AddLog($" App Open ad failed to display {info1} {info2}");
            LoadAppOpen();
        }

        private void OnAppOpenAdDisplayedEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog($"App Open ad was displayed {info}");
        }

        private void OnAppOpenAdClickedEvent(string arg1, MaxSdkBase.AdInfo info)
        {
            GleyLogger.AddLog($"App Open ad was clicked {info}");
            events.TriggerAppOpenClicked();
        }
        #endregion


        #region RewardedVideo
        #region InterfaceImplementation
        /// <summary>
        /// Check if AppLovin rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            if (!initialized)
                return false;
            if (string.IsNullOrEmpty(rewardedVideoId))
                return false;
            return MaxSdk.IsRewardedAdReady(rewardedVideoId);
        }


        /// <summary>
        /// Show AppLovin rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true, video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (IsRewardedVideoAvailable())
            {
                onRewardedVideoClosed = CompleteMethod;
                rewardedVideoCompleted = false;
                MaxSdk.ShowRewardedAd(rewardedVideoId);
            }
        }
        #endregion


        /// <summary>
        /// Preload a rewarded video ad before showing
        /// if it fails for maxRetryCount times do not try anymore
        /// </summary>
        private void LoadRewardedVideo()
        {
            if (retryNumberRewarded < maxRetryCount)
            {
                GleyLogger.AddLog("Load Rewarded Video");

                retryNumberRewarded++;
                MaxSdk.LoadRewardedAd(rewardedVideoId);
            }
        }


        private void OnRewardedAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            GleyLogger.AddLog("Rewarded video was successfully loaded");

            retryNumberRewarded = 0;
            events.TriggerRewardedVideoLoadSucces();
        }


        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {

            GleyLogger.AddLog($"Rewarded video failed to load {errorInfo.Code} {errorInfo.Message}");

            GleyLogger.AddLog($"Reloading {retryNumberRewarded} in {reloadInterval} sec");

            //wait and load another
            Invoke("LoadRewardedVideo", reloadInterval);
            events.TriggerRewardedVideoLoadFailed(errorInfo.ToString());
        }


        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded video failed to display {errorInfo.Code} {errorInfo.Message}");

            LoadRewardedVideo();
        }


        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Rewarded video displayed");
        }


        private void OnRewardedAdClickedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            GleyLogger.AddLog("Rewarded video clicked");
            events.TriggerRewardedVideoClicked();
        }


        private void OnRewardedAdDismissedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            GleyLogger.AddLog("Rewarded video was closed");

            //trigger rewarded video completed callback method
            if (onRewardedVideoClosed != null)
            {
                onRewardedVideoClosed(rewardedVideoCompleted);
                onRewardedVideoClosed = null;
            }

            //load another rewarded video
            LoadRewardedVideo();
        }


        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("Rewarded video was completed");
            rewardedVideoCompleted = true;
        }


        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Ad revenue
            double revenue = adInfo.Revenue;

            // Miscellaneous data
            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD" in most cases!
            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            string placement = adInfo.Placement; // The placement this ad's post-backs are tied to

            GleyLogger.AddLog($"Revenue {revenue} countryCode {countryCode} networkName {networkName} adUnitIdentifier {adUnitIdentifier} placement {placement}");

        }
        #endregion


        #region MRec
        #region Interface Implementation
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            GleyLogger.AddLog("Show MRec");
            LoadMRec(position, customPosition);
        }

        public void HideMRec()
        {
            if (string.IsNullOrEmpty(mRecId))
                return;
            MaxSdk.DestroyMRec(mRecId);
        }
        #endregion
        private void LoadMRec(BannerPosition position, Vector2Int customPosition)
        {
            if (string.IsNullOrEmpty(mRecId))
                return;
            MaxSdk.DestroyMRec(mRecId);
            MaxSdkBase.AdViewPosition adPosition = MaxSdkBase.AdViewPosition.TopCenter;

            switch (position)
            {
                case BannerPosition.Top:
                    adPosition = MaxSdkBase.AdViewPosition.TopCenter;
                    break;
                case BannerPosition.Bottom:
                    adPosition = MaxSdkBase.AdViewPosition.BottomCenter;
                    break;
                case BannerPosition.TopLeft:
                    adPosition = MaxSdkBase.AdViewPosition.TopLeft;
                    break;
                case BannerPosition.TopRight:
                    adPosition = MaxSdkBase.AdViewPosition.TopRight;
                    break;
                case BannerPosition.BottomLeft:
                    adPosition = MaxSdkBase.AdViewPosition.BottomLeft;
                    break;
                case BannerPosition.BottomRight:
                    adPosition = MaxSdkBase.AdViewPosition.BottomRight;
                    break;
                case BannerPosition.Center:
                    adPosition = MaxSdkBase.AdViewPosition.Centered;
                    break;
                case BannerPosition.CenterLeft:
                    adPosition = MaxSdkBase.AdViewPosition.CenterLeft;
                    break;
                case BannerPosition.CenterRight:
                    adPosition = MaxSdkBase.AdViewPosition.CenterRight;
                    break;
                case BannerPosition.Custom:
                    break;
                default:
                    GleyLogger.AddLog($"MRec Position: {position} not supported by AppLovin, AdViewPosition.Top will be used");
                    break;
            }

            if (position == BannerPosition.Custom)
            {
                MaxSdk.CreateMRec(mRecId, customPosition.x, customPosition.y);
            }
            else
            {
                MaxSdk.CreateMRec(mRecId, adPosition);
            }
        }


        void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("MRec Ad Loaded");
            MaxSdk.ShowMRec(mRecId);
        }

        void OnMRecAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo error)
        {
            GleyLogger.AddLog($"MRec Ad Load Failed {error}");
        }

        void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("MRec Ad Clicked Event");
        }

        void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("MRec Ad Revenue Paid");
        }

        void OnMRecAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("MRec Ad Expanded");
        }

        void OnMRecAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            GleyLogger.AddLog("MRec Ad Collapsed");
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {
            if (IsInterstitialAvailable() == false)
            {
                if (retryNumberInterstitial == maxRetryCount)
                {
                    retryNumberInterstitial--;
                    LoadInterstitial();
                }
            }

            if (IsRewardedVideoAvailable() == false)
            {
                if (retryNumberRewarded == maxRetryCount)
                {
                    retryNumberRewarded--;
                    LoadRewardedVideo();
                }
            }

            if (IsAppOpenAvailable() == false)
            {
                if (retryNumberAppOpen == maxRetryCount)
                {
                    retryNumberAppOpen--;
                    LoadAppOpen();
                }
            }
        }
        #endregion


        #region AutoConsent
        public bool HasBuiltInConsentWindow()
        {
            return false;
        }


        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            GleyLogger.AddLog($"ShowBuiltInConsentWindow Not supported on {SupportedAdvertisers.AppLovin}");
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
            if (accept == true)
            {
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Accept);

            }
            else
            {
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Deny);
            }
            MaxSdk.SetHasUserConsent(accept);
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
            if (accept == true)
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Accept);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Deny);
            }
            MaxSdk.SetDoNotSell(!accept);
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
            MaxSdk.ShowMediationDebugger();
        }
        #endregion


        #region NotSupported


        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            GleyLogger.AddLog($"ShowRewardedInterstitial Not supported on {SupportedAdvertisers.AppLovin}");
        }
        #endregion
    }
}
#endif