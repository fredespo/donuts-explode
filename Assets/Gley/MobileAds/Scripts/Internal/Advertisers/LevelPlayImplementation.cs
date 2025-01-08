#if GLEY_LEVELPLAY
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gley.MobileAds.Internal
{
    public class LevelPlayImplementation : MonoBehaviour, IAdProvider
    {
        private const float reloadTime = 30;
        private readonly int maxRetryCount = 10;

        private UnityAction onInterstitialClosed;
        private UnityAction onInitialized;
        private UnityAction<bool> onRewardedVideoClosed;
        private Events events;
        private string appKey;
        private string bannerAdUnit;
        private string mrecAdUnit;
        private string interstitialAdUnit;
        private string rewardedVideoAdUnit;
        private int currentRetryInterstitial;
        private bool rewardedWatched;
        private bool directedForChildren;
        private bool initialized;


        #region Initialize
        #region InterfaceImplementation
        public void SetDirectedForChildren(bool active)
        {
            directedForChildren = active;
        }

        /// <summary>
        /// Initializing IronSource
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            if (initialized == false)
            {
                events = new Events();
                PlatformSettings settings = platformSettings;
                this.onInitialized = onInitialized;

                //apply settings
                appKey = settings.appId.id;
                bannerAdUnit = settings.idBanner.id;
                interstitialAdUnit = settings.idInterstitial.id;
                rewardedVideoAdUnit = settings.idRewarded.id;
                mrecAdUnit = settings.idMRec.id;

                //verify settings
                GleyLogger.AddLog($"{settings.appId.displayName} : {settings.appId.id}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerAdUnit}");
                GleyLogger.AddLog($"{settings.idMRec.displayName} : {mrecAdUnit}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {interstitialAdUnit}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedVideoAdUnit}");
                GleyLogger.AddLog($"Directed for children: {directedForChildren}");


                if (directedForChildren)
                {
                    IronSource.Agent.setMetaData("is_child_directed", "true");
                }
                else
                {
                    IronSource.Agent.setMetaData("is_child_directed", "false");
                }

                IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

                if (GleyLogger.IsInitialized())
                {
                    IronSource.Agent.validateIntegration();
                }

                if (!string.IsNullOrEmpty(bannerAdUnit)||!string.IsNullOrEmpty(mrecAdUnit))
                {
                    IronSourceBannerEvents.onAdLoadedEvent += BannerAdLoadedEvent;
                    IronSourceBannerEvents.onAdLoadFailedEvent += BannerAdLoadFailedEvent;
                    IronSourceBannerEvents.onAdClickedEvent += BannerAdClickedEvent;
                    IronSourceBannerEvents.onAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
                    IronSourceBannerEvents.onAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
                    IronSourceBannerEvents.onAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
                    IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
                }

                if (!string.IsNullOrEmpty(interstitialAdUnit))
                {
                    IronSourceInterstitialEvents.onAdReadyEvent += InterstitialAdReadyEvent;
                    IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
                    IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
                    IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialAdShowFailedEvent;
                    IronSourceInterstitialEvents.onAdClickedEvent += InterstitialAdClickedEvent;
                    IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialAdOpenedEvent;
                    IronSourceInterstitialEvents.onAdClosedEvent += InterstitialAdClosedEvent;
                    IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
                    LoadInterstitial();
                }

                if (!string.IsNullOrEmpty(rewardedVideoAdUnit))
                {
                    IronSourceRewardedVideoEvents.onAdReadyEvent += RewardedVideoAdReadyEvent;
                    IronSourceRewardedVideoEvents.onAdLoadFailedEvent += RewardedVideoAdLoadFailedEvent;
                    IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoAdOpenedEvent;
                    IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoAdClosedEvent;
                    IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoAdAvailableEvent;
                    IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoAdUnavailableEvent;
                    IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoAdClickedEvent;
                    IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoAdRewardedEvent;
                    IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
                    IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
                }


            }
        }

        #endregion
        private void SdkInitializationCompletedEvent()
        {
            initialized = true;
            GleyLogger.AddLog($"Initialization Complete");
            onInitialized?.Invoke();
          
        }
        #endregion


        #region Banner
        #region InterfaceImplementation
        /// <summary>
        /// Check if IronSource banner is available
        /// </summary>
        /// <returns>true if a banner is available</returns>
        public bool IsBannerAvailable()
        {
            return true;
        }


        /// <summary>
        /// Show IronSource banner
        /// </summary>
        /// <param name="position"> can be TOP or BOTTOM</param>
        ///  /// <param name="bannerType"> can be Banner or SmartBanner</param>
        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            ShowBanner(bannerAdUnit, position, bannerType, customSize, customPosition);
        }

        /// <summary>
        /// Hides IronSource banner
        /// </summary>
        public void HideBanner()
        {
            IronSource.Agent.destroyBanner();
        }
        #endregion
        public void ShowBanner(string bannerID, BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            HideBanner();
            IronSourceBannerPosition adPosition = IronSourceBannerPosition.TOP;
            IronSourceBannerSize adSize = IronSourceBannerSize.BANNER;
            adSize.SetAdaptive(false);
            switch (bannerType)
            {
                case BannerType.Banner:
                    adSize = IronSourceBannerSize.BANNER;
                    break;
                case BannerType.Large:
                    adSize = IronSourceBannerSize.LARGE;
                    break;
                case BannerType.MediumRectangle:
                    adSize = IronSourceBannerSize.RECTANGLE;
                    break;
                case BannerType.Smart:
                    adSize = IronSourceBannerSize.SMART;
                    break;
                case BannerType.Adaptive:
                    adSize.SetAdaptive(true);
                    break;
                case BannerType.Custom:
                    break;
                default:
                    GleyLogger.AddLog($"Banner Type: {bannerType} not supported by IronSource, BannerType.Banner will be used");
                    break;

            }

            switch (position)
            {
                case BannerPosition.Top:
                    adPosition = IronSourceBannerPosition.TOP;
                    break;
                case BannerPosition.Bottom:
                    adPosition = IronSourceBannerPosition.BOTTOM;
                    break;
                default:
                    GleyLogger.AddLog($"Banner Position: {position} not supported by IronSource, BannerPosition.Top will be used");
                    break;
            }

            if (bannerType == BannerType.Custom)
            {
                IronSource.Agent.loadBanner(new IronSourceBannerSize(customSize.x, customSize.y), adPosition, bannerID);
            }
            else
            {
                IronSource.Agent.loadBanner(adSize, adPosition, bannerID);
            }
        }



        //Invoked once the banner has loaded
        void BannerAdLoadedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Banner Ad Loaded Event {adInfo}");
            events.TriggerBannerLoadSucces();
        }

        //Invoked when the banner loading process has failed.
        //@param description - string - contains information about the failure.
        void BannerAdLoadFailedEvent(IronSourceError error)
        {
            GleyLogger.AddLog($"Banner Ad Load Failed Event {error}");
            events.TriggerBannerLoadFailed(error.ToString());
        }

        // Invoked when end user clicks on the banner ad
        void BannerAdClickedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Banner Ad Clicked Event {adInfo.adNetwork}");
            events.TriggerBannerClicked();
        }

        //Notifies the presentation of a full screen content following user click
        void BannerAdScreenPresentedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Banner Ad Screen Presented Event {adInfo.adNetwork}");
        }

        //Notifies the presented screen has been dismissed
        void BannerAdScreenDismissedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Banner Ad Screen Dismissed Event {adInfo.adNetwork}");
        }

        //Invoked when the user leaves the app
        void BannerAdLeftApplicationEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Banner Ad Left Application Event {adInfo.adNetwork}");
        }
        #endregion


        #region Interstitial
        #region InterfaceImplementation
        /// <summary>
        /// Check if IronSource interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            return IronSource.Agent.isInterstitialReady();
        }


        /// <summary>
        /// Show IronSource interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (IsInterstitialAvailable())
            {
                onInterstitialClosed = InterstitialClosed;
                IronSource.Agent.showInterstitial(interstitialAdUnit);
            }
        }
        #endregion


        /// <summary>
        /// Loads IronSource interstitial
        /// </summary>
        private void LoadInterstitial()
        {
            GleyLogger.AddLog(this + " Start Loading Interstitial");

            IronSource.Agent.loadInterstitial();
        }

        /// <summary>
        /// Coroutine to reload an interstitial after a fail
        /// </summary>
        /// <param name="reloadTime">time to wait</param>
        /// <returns></returns>
        private IEnumerator ReloadInterstitial(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);
            LoadInterstitial();
        }

        //Invoked when the initialization process has failed.
        //@param description - string - contains information about the failure.
        void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            GleyLogger.AddLog($"Interstitial Failed To Load {error}");

            if (currentRetryInterstitial < maxRetryCount)
            {
                currentRetryInterstitial++;

                GleyLogger.AddLog("Retry " + currentRetryInterstitial);

                StartCoroutine(ReloadInterstitial(reloadTime));
            }
            events.TriggerInterstitialLoadFailed(error.ToString());
        }

        //Invoked right before the Interstitial screen is about to open.
        void InterstitialAdShowSucceededEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Interstitial Show Success {adInfo.adNetwork}");
            currentRetryInterstitial = 0;
        }

        //Invoked when the ad fails to show.
        //@param description - string - contains information about the failure.
        void InterstitialAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Interstitial Failed To Show {error} {adInfo}");
            StartCoroutine(ReloadInterstitial(reloadTime));
        }

        // Invoked when end user clicked on the interstitial ad
        void InterstitialAdClickedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Interstitial Ad Clicked Event {adInfo.adNetwork}");
            events.TriggerInterstitialClicked();
        }

        //Invoked when the interstitial ad closed and the user goes back to the application screen.
        void InterstitialAdClosedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog("Reload Interstitial");

            //reload interstitial
            LoadInterstitial();

            //trigger complete event
            if (onInterstitialClosed != null)
            {
                onInterstitialClosed();
                onInterstitialClosed = null;
            }
        }

        //Invoked when the Interstitial is Ready to shown after load function is called
        void InterstitialAdReadyEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Interstitial Ad Ready Event {adInfo}");
            events.TriggerInterstitialLoadSucces();
        }

        //Invoked when the Interstitial Ad Unit has opened
        void InterstitialAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Interstitial Ad Opened Event {adInfo.adNetwork}");
        }

        #endregion


        #region Rewarded
        #region InterfaceImplementation
        /// <summary>
        /// Check if IronSource rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            return IronSource.Agent.isRewardedVideoAvailable();
        }


        /// <summary>
        /// Show IronSource rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (IsRewardedVideoAvailable())
            {
                onRewardedVideoClosed = CompleteMethod;
                IronSource.Agent.showRewardedVideo(rewardedVideoAdUnit);
            }
        }
        #endregion


        //Invoked when the RewardedVideo ad view has opened.
        //Your Activity will lose focus. Please avoid performing heavy 
        //tasks till the video ad will be closed.
        void RewardedVideoAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Opened Event {adInfo.adNetwork}");
        }


        //Invoked when the RewardedVideo ad view is about to be closed.
        //Your activity will now regain its focus.
        void RewardedVideoAdClosedEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Closed Event {adInfo.adNetwork}");

            if (onRewardedVideoClosed != null)
            {
                onRewardedVideoClosed(rewardedWatched);
                onRewardedVideoClosed = null;
            }

            rewardedWatched = false;
        }


        //Invoked when the user completed the video and should be rewarded. 
        //If using server-to-server callbacks you may ignore this events and wait for the callback from the  ironSource server.
        //
        //@param - placement - placement object which contains the reward data
        //
        void RewardedVideoAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Rewarded Event {placement} {adInfo.adNetwork}");
            rewardedWatched = true;
        }


        //Invoked when the Rewarded Video failed to show
        //@param description - string - contains information about the failure.
        void RewardedVideoAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Show Failed Event {error} {adInfo}");
        }


        private void RewardedVideoAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Clicked Event {placement} {adInfo.adNetwork}");
            events.TriggerRewardedVideoClicked();
        }

        private void RewardedVideoAdUnavailableEvent()
        {
            GleyLogger.AddLog("Rewarded Video Ad Unavailable Event");
        }

        private void RewardedVideoAdAvailableEvent(IronSourceAdInfo adInfo)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Available Event {adInfo}");
        }


        private void RewardedVideoAdLoadFailedEvent(IronSourceError error)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Load Failed Event {error}");
            events.TriggerRewardedVideoLoadFailed(error.ToString());
        }

        private void RewardedVideoAdReadyEvent(IronSourceAdInfo info)
        {
            GleyLogger.AddLog($"Rewarded Video Ad Ready Event {info}");
            events.TriggerRewardedVideoLoadSucces();
        }
        #endregion


        #region MRec
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            ShowBanner(mrecAdUnit, position, BannerType.MediumRectangle, new Vector2Int(), customPosition);
        }

        public void HideMRec()
        {
            HideBanner();
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {
            if (IsInterstitialAvailable() == false)
            {
                if (currentRetryInterstitial == maxRetryCount)
                {
                    LoadInterstitial();
                }
            }
        }
        #endregion


        #region Auto Consent
        public bool HasBuiltInConsentWindow()
        {
            return false;
        }

        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            GleyLogger.AddLog($"ShowBuiltInConsentWindow Not supported on {SupportedAdvertisers.LevelPlay}");
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

            IronSource.Agent.setConsent(accept);
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
                IronSource.Agent.setMetaData("do_not_sell", "false");
            }
            else
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Deny);
                IronSource.Agent.setMetaData("do_not_sell", "true");
            }
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
            GleyLogger.AddLog($"OpenDebugWindow Not supported on {SupportedAdvertisers.LevelPlay}");
        }
        #endregion



        void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }

        #region NotSupported

        public bool IsAppOpenAvailable()
        {
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            GleyLogger.AddLog($"ShowAppOpen Not supported on {SupportedAdvertisers.LevelPlay}");
        }

        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            GleyLogger.AddLog($"ShowRewardedInterstitial Not supported on {SupportedAdvertisers.LevelPlay}");
        }
        #endregion
    }
}
#endif