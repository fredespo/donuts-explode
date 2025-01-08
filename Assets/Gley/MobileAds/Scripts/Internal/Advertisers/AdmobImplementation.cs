#if GLEY_ADMOB
namespace Gley.MobileAds.Internal
{
    using UnityEngine.Events;
    using UnityEngine;
    using GoogleMobileAds.Api;
    using System.Collections.Generic;
    using System.Collections;
    using GoogleMobileAds.Ump.Api;
    using System;

    public class AdmobImplementation : MonoBehaviour, IAdProvider
    {
        private readonly float reloadTime = 30;
        private readonly int maxRetryCount = 10;
        private readonly float bufferTime = 1;
        private readonly float skipAppOpenTime = 5;

        private UnityAction<bool> onRewardedVideoClosed;
        private UnityAction<bool> onRewardedInterstitialClosed;
        private UnityAction onInterstitialClosed;
        private UnityAction onAppOpenClosed;
        private UnityAction onConsentPopupClosed;
        private UnityAction onInitialized;
        private InterstitialAd interstitial;
        private AppOpenAd appOpen;
        private ConsentForm consentForm;
        private BannerView banner;
        private RewardedAd rewardedVideo;
        private RewardedInterstitialAd rewardedInterstitial;
        private Events events;
        private DateTime appOpenExpireTime;
        private BannerPosition position;
        private BannerType bannerType;
        private DebugGeography debugGeography;
        private string rewardedVideoId;
        private string interstitialId;
        private string appOpenId;
        private string rewardedInterstitialId;
        private string bannerId;
        private string mrecId;
        private string designedForFamilies;
        private string testDeviceID;
        private float callbackTime;
        private bool resetCallbackTime;
        private float adShowTIme;
        private int currentRetryRewardedVideo;
        private int currentRetryRewardedInterstitial;
        private int currentRetryInterstitial;
        private int currentRetryAppOpen;
        private bool initialized;
        private bool rewardedVideoWatched;
        private bool rewardedVideoClosed;
        private bool interstitialClosed;
        private bool rewardedInterstitialWatched;
        private bool bannerLoaded;
        private bool interstitialFailedToLoad;
        private bool rewardedVideoFailedToLoad;
        private bool rewardedInterstitialFailedToLoad;
        private bool appOpenFailedToLoad;
        private bool directedForChildren;
        private bool adIsActive;
        private bool resetTimer;



        #region Initialize
        #region InterfaceImplementation
        public void SetDirectedForChildren(bool active)
        {
            directedForChildren = active;
        }

        /// <summary>
        /// Initializing Admob
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            if (initialized == false)
            {
                GleyLogger.AddLog("Start Initialization");
                events = new Events();

                this.onInitialized = onInitialized;
                //get settings
                PlatformSettings settings = platformSettings;

                if (settings.testMode)
                {
#if UNITY_ANDROID
                    bannerId = "ca-app-pub-3940256099942544/6300978111";
                    mrecId = bannerId;
                    interstitialId = "ca-app-pub-3940256099942544/1033173712";
                    rewardedVideoId = "ca-app-pub-3940256099942544/5224354917";
                    rewardedInterstitialId = "ca-app-pub-3940256099942544/5354046379";
                    appOpenId = "ca-app-pub-3940256099942544/9257395921";
#else
                    bannerId = "ca-app-pub-3940256099942544/2934735716";
                    mrecId = bannerId;
                    interstitialId = "ca-app-pub-3940256099942544/4411468910";
                    rewardedVideoId = "ca-app-pub-3940256099942544/1712485313";
                    rewardedInterstitialId = "ca-app-pub-3940256099942544/6978759866";
                    appOpenId = "ca-app-pub-3940256099942544/5575463023";
#endif
                }
                else
                {
                    //apply settings
                    interstitialId = settings.idInterstitial.id;
                    bannerId = settings.idBanner.id;
                    mrecId = settings.idMRec.id;
                    rewardedVideoId = settings.idRewarded.id;
                    rewardedInterstitialId = settings.idRewardedInterstitial.id;
                    appOpenId = settings.idOpenApp.id;
                }
                TagForChildDirectedTreatment tagFororChildren;
                TagForUnderAgeOfConsent tagForUnderAge;
                MaxAdContentRating contentRating;

                if (directedForChildren == true)
                {
                    designedForFamilies = "true";
                    tagFororChildren = TagForChildDirectedTreatment.True;
                    tagForUnderAge = TagForUnderAgeOfConsent.True;
                    contentRating = MaxAdContentRating.G;
                }
                else
                {
                    designedForFamilies = "false";
                    tagFororChildren = TagForChildDirectedTreatment.Unspecified;
                    tagForUnderAge = TagForUnderAgeOfConsent.Unspecified;
                    contentRating = MaxAdContentRating.Unspecified;
                }


                RequestConfiguration requestConfiguration = new RequestConfiguration
                {
                    TagForChildDirectedTreatment = tagFororChildren,
                    MaxAdContentRating = contentRating,
                    TagForUnderAgeOfConsent = tagForUnderAge,
                    TestDeviceIds = new List<string> { settings.testDevice }
                };

                if (!string.IsNullOrEmpty(testDeviceID))
                {
                    requestConfiguration.TestDeviceIds.Add(settings.testDevice);
                }

                MobileAds.SetRequestConfiguration(requestConfiguration);

                //verify settings

                GleyLogger.AddLog($"Test mode: " + settings.testMode);
                GleyLogger.AddLog($"{settings.appId.displayName} : {settings.appId.id}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerId}");
                GleyLogger.AddLog($"{settings.idMRec.displayName} : {mrecId}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {interstitialId}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedVideoId}");
                GleyLogger.AddLog($"{settings.idRewardedInterstitial.displayName} : {rewardedInterstitialId}");
                GleyLogger.AddLog($"{settings.idOpenApp.displayName} : {appOpenId}");
                GleyLogger.AddLog($"Directed for children: {directedForChildren}");

                //preparing Admob SDK for initialization

                MobileAds.RaiseAdEventsOnUnityMainThread = true;
                MobileAds.SetiOSAppPauseOnBackground(true);

                MobileAds.Initialize(InitComplete);
            }
        }
        #endregion


        private void InitComplete(InitializationStatus status)
        {
            GleyLogger.AddLog("Initialization complete: ");
            Dictionary<string, AdapterStatus> adapterState = status.getAdapterStatusMap();
            GleyLogger.AddLog("Adapter status: ");
            foreach (var adapter in adapterState)
            {
                GleyLogger.AddLog(adapter.Key + " " + adapter.Value.InitializationState + " " + adapter.Value.Description);
            }
            if (!string.IsNullOrEmpty(rewardedVideoId))
            {
                LoadRewardedVideo();
            }
            if (!string.IsNullOrEmpty(interstitialId))
            {
                LoadInterstitial();
            }
            if (!string.IsNullOrEmpty(rewardedInterstitialId))
            {
                LoadRewardedInterstitial();
            }

            if (!string.IsNullOrEmpty(appOpenId))
            {
                LoadAppOpen();
            }

            initialized = true;

            onInitialized?.Invoke();
        }
        #endregion


        #region Banner
        #region InterfaceImplementation
        /// <summary>
        /// Show Admob banner
        /// </summary>
        /// <param name="position"> can be TOP or BOTTOM</param>
        ///  /// <param name="bannerType"> can be Banner or SmartBanner</param>
        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            ShowBanner(bannerId, position, bannerType, customSize, customPosition, false);
        }


        /// <summary>
        /// Hides Admob banner
        /// </summary>
        public void HideBanner()
        {
            GleyLogger.AddLog("Hide banner");

            if (banner != null)
            {
                if (bannerLoaded == false)
                {
                    DestroyBanner();
                }
                else
                {
                    //hide the banner -> will be available later without loading
                    banner.Hide();
                }
            }
        }

        #endregion

        public void ShowBanner(string bannerID, BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition, bool collapsable)
        {
            bannerLoaded = false;
            //this.DisplayResult = DisplayResult;
            if (banner != null)
            {
                if (this.position == position && this.bannerType == bannerType)
                {
                    GleyLogger.AddLog("Show Banner");
                    bannerLoaded = true;
                    banner.Show();
                }
                else
                {
                    LoadBanner(bannerID, position, bannerType, customSize, customPosition, collapsable);
                }
            }
            else
            {
                LoadBanner(bannerID, position, bannerType, customSize, customPosition, collapsable);
            }
        }
        /// <summary>
        /// Loads an Admob banner
        /// </summary>
        /// <param name="position">display position</param>
        /// <param name="bannerType">can be normal banner or smart banner</param>
        private void LoadBanner(string bannerID, BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition, bool collapsable)
        {
            GleyLogger.AddLog("Start Loading Banner");
            //setup banner

            DestroyBanner();

            this.position = position;
            this.bannerType = bannerType;

            AdSize adSize;
            switch (bannerType)
            {
                case BannerType.Banner:
                    adSize = AdSize.Banner;
                    break;
                case BannerType.IABBanner:
                    adSize = AdSize.IABBanner;
                    break;
                case BannerType.Leaderboard:
                    adSize = AdSize.Leaderboard;
                    break;
                case BannerType.MediumRectangle:
                    adSize = AdSize.MediumRectangle;
                    break;
                case BannerType.Adaptive:
                    adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
                    break;
                case BannerType.Custom:
                    adSize = new AdSize(customSize.x, customSize.y);
                    break;
                default:
                    GleyLogger.AddLog($"Banner Type: {bannerType} not supported by Admob, BannerType.Banner will be used");
                    adSize = AdSize.Banner;
                    break;
            }

            AdPosition adPosition = AdPosition.Top;
            // create an instance of a banner view first.
            switch (position)
            {
                case BannerPosition.Bottom:
                    adPosition = AdPosition.Bottom;
                    break;
                case BannerPosition.BottomLeft:
                    adPosition = AdPosition.BottomLeft;
                    break;
                case BannerPosition.BottomRight:
                    adPosition = AdPosition.BottomRight;
                    break;
                case BannerPosition.Center:
                    adPosition = AdPosition.Center;
                    break;
                case BannerPosition.Custom:
                    break;
                case BannerPosition.Top:
                    adPosition = AdPosition.Top;
                    break;
                case BannerPosition.TopLeft:
                    adPosition = AdPosition.TopLeft;
                    break;
                case BannerPosition.TopRight:
                    adPosition = AdPosition.TopRight;
                    break;
                default:
                    GleyLogger.AddLog($"Banner Position: {position} not supported by Admob, BannerPosition.Top will be used");
                    break;
            }

            if (position == BannerPosition.Custom)
            {
                banner = new BannerView(bannerID, adSize, customPosition.x, customPosition.y);
            }
            else
            {
                banner = new BannerView(bannerID, adSize, adPosition);
            }

            // listen to events the banner may raise.
            banner.OnBannerAdLoaded += BannerLoadSucces;
            banner.OnBannerAdLoadFailed += BannerLoadFailed;
            banner.OnAdPaid += BannerAdPaied;
            banner.OnAdImpressionRecorded += BannerImpressionRecorded;
            banner.OnAdClicked += BannerClicked;
            banner.OnAdFullScreenContentOpened += BannerFullScreenOpened;
            banner.OnAdFullScreenContentClosed += BannerFullScreenClose;

            //request and load banner
            var request = CreateRequest();
            if (collapsable)
            {

                if (position.ToString().Contains("Top"))
                {
                    GleyLogger.AddLog("Collapsible banner top");
                    request.Extras.Add("collapsible", "top");
                }
                else
                {
                    GleyLogger.AddLog("Collapsible banner bottom");
                    request.Extras.Add("collapsible", "bottom");
                }
                request.Extras.Add("collapsible_request_id", Guid.NewGuid().ToString());
            }

            banner.LoadAd(request);
        }

        private void DestroyBanner()
        {
            if (banner != null)
            {
                banner.OnBannerAdLoaded -= BannerLoadSucces;
                banner.OnBannerAdLoadFailed -= BannerLoadFailed;
                banner.OnAdPaid -= BannerAdPaied;
                banner.OnAdImpressionRecorded -= BannerImpressionRecorded;
                banner.OnAdClicked -= BannerClicked;
                banner.OnAdFullScreenContentOpened -= BannerFullScreenOpened;
                banner.OnAdFullScreenContentClosed -= BannerFullScreenClose;
                banner.Destroy();
            }
            banner = null;
        }

        /// <summary>
        /// Admob specific event triggered after banner was loaded into the banner view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BannerLoadSucces()
        {
            GleyLogger.AddLog("Banner Loaded");
            bannerLoaded = true;
            events.TriggerBannerLoadSucces();
        }


        /// <summary>
        /// Admob specific event triggered after banner failed to load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="loadAdError"></param>
        private void BannerLoadFailed(LoadAdError loadAdError)
        {
            GleyLogger.AddLog("Admob Banner Failed To Load ");
            LogErrorMessage(loadAdError);
            DestroyBanner();
            bannerLoaded = false;
            events.TriggerBannerLoadFailed(loadAdError.ToString());
        }


        /// <summary>
        /// Raised when the ad is estimated to have earned money.
        /// </summary>
        /// <param name="adValue"></param>
        private void BannerAdPaied(AdValue adValue)
        {
            GleyLogger.AddLog($"Banner view paid {adValue.Value} {adValue.CurrencyCode}.");
            Events.OnBannerPaid?.Invoke(adValue, banner.GetResponseInfo());
        }


        /// <summary>
        /// Raised when an impression is recorded for an ad.
        /// </summary>
        private void BannerImpressionRecorded()
        {
            GleyLogger.AddLog("Banner view recorded an impression.");
        }


        /// <summary>
        /// Raised when a click is recorded for an ad.
        /// </summary>
        private void BannerClicked()
        {
            GleyLogger.AddLog("Banner view was clicked.");
            events.TriggerBannerClicked();
        }


        /// <summary>
        /// Raised when an ad opened full screen content.
        /// </summary>
        private void BannerFullScreenOpened()
        {
            GleyLogger.AddLog("Banner view full screen content opened.");
        }


        /// <summary>
        /// Raised when the ad closed full screen content.
        /// </summary>
        private void BannerFullScreenClose()
        {
            GleyLogger.AddLog("Banner view full screen content closed.");
        }
        #endregion


        #region Interstitial
        #region InterfaceImplementation
        /// <summary>
        /// Check if Admob interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            if (interstitial != null)
            {
                return interstitial.CanShowAd();
            }
            return false;
        }


        /// <summary>
        /// Show Admob interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial </param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (IsInterstitialAvailable())
            {
                adIsActive = true;
                resetTimer = true;
                interstitialClosed = false;
                onInterstitialClosed = InterstitialClosed;
                interstitial.Show();
            }
            else
            {
                GleyLogger.AddLog("Interstitial ad cannot be shown.");
            }
        }
        #endregion


        /// <summary>
        /// Loads an Admob interstitial
        /// </summary>
        private void LoadInterstitial()
        {
            GleyLogger.AddLog("Start Loading Interstitial");

            // Clean up the old ad before loading a new one.
            if (interstitial != null)
            {
                interstitial.OnAdPaid -= InterstitialAdPaied;
                interstitial.OnAdImpressionRecorded -= ImpressionRecorded;
                interstitial.OnAdClicked -= AdClicked;
                interstitial.OnAdFullScreenContentOpened -= AdFullScreenOpened;
                interstitial.OnAdFullScreenContentClosed -= AdFullScreenClosed;
                interstitial.OnAdFullScreenContentFailed -= AdFullScreenFailed;
                interstitial.Destroy();
                interstitial = null;
            }

            // create our request used to load the ad.
            var adRequest = CreateRequest();

            // Load an interstitial ad
            InterstitialAd.Load(interstitialId, adRequest, InterstitialLoadCallback);
        }


        private void InterstitialLoadCallback(InterstitialAd ad, LoadAdError loadAdError)
        {
            // if error is not null, the load request failed.
            if (loadAdError != null || ad == null)
            {
                InterstitialFailed(loadAdError);
                return;
            }
            else
            {
                InterstitialLoaded(ad);
            }
        }


        /// <summary>
        /// Admob specific event triggered after an interstitial was loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterstitialLoaded(InterstitialAd ad)
        {
            interstitial = ad;

            GleyLogger.AddLog("Interstitial Loaded");

            interstitial.OnAdPaid += InterstitialAdPaied;
            interstitial.OnAdImpressionRecorded += ImpressionRecorded;
            interstitial.OnAdClicked += AdClicked;
            interstitial.OnAdFullScreenContentOpened += AdFullScreenOpened;
            interstitial.OnAdFullScreenContentClosed += AdFullScreenClosed;
            interstitial.OnAdFullScreenContentFailed += AdFullScreenFailed;

            currentRetryInterstitial = 0;
            events.TriggerInterstitialLoadSucces();
        }


        /// <summary>
        /// Admob specific event triggered if an interstitial failed to load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterstitialFailed(LoadAdError e)
        {
            // Gets the domain from which the error came.
            //string domain = loadAdError.GetDomain();

            // Gets the error code. See
            // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
            // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
            // for a list of possible codes.
            //int code = loadAdError.GetCode();

            // Gets an error message.
            // For example "Account not approved yet". See
            // https://support.google.com/admob/answer/9905175 for explanations of
            // common errors.
            //string message = loadAdError.GetMessage();

            // Gets the cause of the error, if available.
            //AdError underlyingError = loadAdError.GetCause();



            // Get response information, which may include results of mediation requests.
            ResponseInfo responseInfo = e.GetResponseInfo();


            GleyLogger.AddLog("Interstitial Failed To Load ");
            GleyLogger.AddLog($"Interstitial -> Load error string: {e}");
            GleyLogger.AddLog($"Interstitial -> Response info: {responseInfo}");


            //try again to load a rewarded video
            if (currentRetryInterstitial < maxRetryCount)
            {
                currentRetryInterstitial++;
                GleyLogger.AddLog("Retry " + currentRetryInterstitial);

                interstitialFailedToLoad = true;
            }
            events.TriggerInterstitialLoadFailed(e.ToString());
        }


        /// <summary>
        /// Admob specific event triggered after an interstitial was closed
        /// </summary>
        private void InterstitialClosed()
        {
            adIsActive = false;
            resetTimer = true;
            GleyLogger.AddLog("Reload Interstitial");

            interstitialClosed = true;

            //reload interstitial
            LoadInterstitial();
        }


        /// <summary>
        ///  Because Admob has some problems when used in multi threading applications with Unity a frame needs to be skipped before returning to application
        /// </summary>
        /// <returns></returns>
        private void CompleteMethodInterstitial()
        {
            interstitialClosed = false;
            if (onInterstitialClosed != null)
            {
                onInterstitialClosed();
                onInterstitialClosed = null;
            }
        }


        /// <summary>
        /// Raised when the ad is estimated to have earned money.
        /// </summary>
        /// <param name="adValue"></param>
        private void InterstitialAdPaied(AdValue adValue)
        {
            GleyLogger.AddLog($"Interstitial ad paid {adValue.Value} {adValue.CurrencyCode}");
            Events.OnInterstitialPaid?.Invoke(adValue, interstitial.GetResponseInfo());
        }


        /// <summary>
        /// Raised when an impression is recorded for an ad.
        /// </summary>        
        private void ImpressionRecorded()
        {
            GleyLogger.AddLog("Interstitial ad recorded an impression.");
        }


        /// <summary>
        ///Raised when a click is recorded for an ad. 
        /// </summary>
        private void AdClicked()
        {
            GleyLogger.AddLog("Interstitial ad was clicked.");
            events.TriggerInterstitialClicked();
        }


        /// <summary>
        /// Raised when an ad opened full screen content.
        /// </summary>
        private void AdFullScreenOpened()
        {
            GleyLogger.AddLog("Interstitial ad full screen content opened.");
        }


        /// <summary>
        ///Raised when the ad closed full screen content. 
        /// </summary>
        private void AdFullScreenClosed()
        {
            GleyLogger.AddLog("Interstitial ad full screen content closed.");
            InterstitialClosed();
        }


        /// <summary>
        ///Raised when the ad failed to open full screen content. 
        /// </summary>
        /// <param name="error"></param>
        private void AdFullScreenFailed(AdError error)
        {
            GleyLogger.AddLog($"Interstitial ad failed to open full screen content. Error: {error}");
            InterstitialClosed();
        }
        #endregion


        #region AppOpen
        #region InterfaceImplementation
        public bool IsAppOpenAvailable()
        {
            if (appOpen != null)
            {
                return appOpen.CanShowAd() && DateTime.Now < appOpenExpireTime;
            }
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed = null)
        {
            if (IsAppOpenAvailable())
            {
                // Don't show AppOpen after another full screen ad was shown. 
                if (Time.realtimeSinceStartup - adShowTIme > skipAppOpenTime && adIsActive == false && resetTimer == false)
                {
                    onAppOpenClosed = appOpenClosed;
                    appOpen.Show();
                }
            }
            else
            {
                GleyLogger.AddLog("AppOpen ad cannot be shown.");
                if (initialized == true)
                {
                    if (DateTime.Now > appOpenExpireTime)
                    {
                        LoadAppOpen();
                    }
                }
            }
        }
        #endregion

        private void LoadAppOpen()
        {
            GleyLogger.AddLog("Start Loading App Open");

            // Clean up the old ad before loading a new one.
            if (appOpen != null)
            {
                appOpen.OnAdPaid -= AppOpenAdPaied;
                appOpen.OnAdImpressionRecorded -= AppOpenImpressionRecorded;
                appOpen.OnAdClicked -= AppOpenAdClicked;
                appOpen.OnAdFullScreenContentOpened -= AppOpenAdFullScreenOpened;
                appOpen.OnAdFullScreenContentClosed -= AppOpenAdFullScreenClosed;
                appOpen.OnAdFullScreenContentFailed -= AppOpenAdFullScreenFailed;
                appOpen.Destroy();
                appOpen = null;
            }

            // create our request used to load the ad.
            var adRequest = CreateRequest();

            // Load an interstitial ad
            AppOpenAd.Load(appOpenId, adRequest, AppOpenLoadCallback);
        }

        private void AppOpenAdFullScreenFailed(AdError error)
        {
            GleyLogger.AddLog($"App open ad failed to open full screen content. Error: {error}");
            AppOpenClosed();
        }

        private void AppOpenAdFullScreenClosed()
        {
            GleyLogger.AddLog("App open ad full screen content closed.");
            AppOpenClosed();
        }

        private void AppOpenClosed()
        {
            GleyLogger.AddLog("Reload App Open");

            //trigger complete event
            StartCoroutine(CompleteMethodAppOpen());

            //reload app open
            LoadAppOpen();
        }

        IEnumerator CompleteMethodAppOpen()
        {
            yield return null;
            if (onAppOpenClosed != null)
            {
                onAppOpenClosed();
                onAppOpenClosed = null;
            }
        }

        private void AppOpenAdFullScreenOpened()
        {
            GleyLogger.AddLog("Open app ad full screen content opened.");
        }

        private void AppOpenAdClicked()
        {
            GleyLogger.AddLog("Open app ad was clicked.");
            events.TriggerAppOpenClicked();
        }

        private void AppOpenImpressionRecorded()
        {
            GleyLogger.AddLog("Open app ad recorded an impression.");
        }

        private void AppOpenAdPaied(AdValue adValue)
        {
            GleyLogger.AddLog($"Open app ad paid {adValue.Value} {adValue.CurrencyCode}");
            Events.OnAppOpenPaid?.Invoke(adValue, appOpen.GetResponseInfo());
        }

        private void AppOpenLoadCallback(AppOpenAd ad, LoadAdError loadAdError)
        {
            // if error is not null, the load request failed.
            if (loadAdError != null || ad == null)
            {
                AppOpenFailed(loadAdError);
                return;
            }
            else
            {
                AppOpenLoaded(ad);
            };
        }

        private void AppOpenLoaded(AppOpenAd ad)
        {
            appOpenExpireTime = DateTime.Now + TimeSpan.FromHours(4);
            appOpen = ad;

            GleyLogger.AddLog("App Open Loaded");

            appOpen.OnAdPaid += AppOpenAdPaied;
            appOpen.OnAdImpressionRecorded += AppOpenImpressionRecorded;
            appOpen.OnAdClicked += AppOpenAdClicked;
            appOpen.OnAdFullScreenContentOpened += AppOpenAdFullScreenOpened;
            appOpen.OnAdFullScreenContentClosed += AppOpenAdFullScreenClosed;
            appOpen.OnAdFullScreenContentFailed += AppOpenAdFullScreenFailed;

            currentRetryAppOpen = 0;
            events.TriggerAppOpenLoadSucces();
        }

        private void AppOpenFailed(LoadAdError loadAdError)
        {
            GleyLogger.AddLog($"App open ad failed to load error :{loadAdError}");

            if (currentRetryAppOpen < maxRetryCount)
            {
                currentRetryAppOpen++;
                GleyLogger.AddLog("Retry " + currentRetryAppOpen);

                appOpenFailedToLoad = true;
            }
            events.TriggerAppOpenLoadFailed(loadAdError.ToString());
        }



        #endregion


        #region Rewarded
        #region InterfaceImplementation
        /// <summary>
        /// Check if Admob rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            if (rewardedVideo != null)
            {
                return rewardedVideo.CanShowAd();
            }
            return false;
        }


        /// <summary>
        /// Show Admob rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (IsRewardedVideoAvailable())
            {
                adIsActive = true;
                resetTimer = true;
                onRewardedVideoClosed = CompleteMethod;
                rewardedVideoWatched = false;
                rewardedVideoClosed = false;
                resetCallbackTime = false;
                rewardedVideo.Show(RewardedVideoWatched);
            }
            else
            {
                GleyLogger.AddLog("Rewarded video cannot be shown.");
            }
        }
        #endregion


        /// <summary>
        /// Loads an Admob rewarded video
        /// </summary>
        private void LoadRewardedVideo()
        {
            GleyLogger.AddLog("Start Loading Rewarded Video");


            // Clean up the old ad before loading a new one.
            if (rewardedVideo != null)
            {
                rewardedVideo.OnAdPaid -= RewardedPaid;
                rewardedVideo.OnAdImpressionRecorded -= RewardedImpressionRec;
                rewardedVideo.OnAdClicked -= RewardedAdClicked;
                rewardedVideo.OnAdFullScreenContentOpened -= RewardedFullScreenOpened;
                rewardedVideo.OnAdFullScreenContentClosed -= RewardedFullScreenClosed;
                rewardedVideo.OnAdFullScreenContentFailed -= RewardedFullScreenFailed;
                rewardedVideo.Destroy();
                rewardedVideo = null;
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedAd.Load(rewardedVideoId, adRequest, LoadRewardedVideoCallback);
        }


        private void LoadRewardedVideoCallback(RewardedAd ad, LoadAdError loadAdError)
        {
            // if error is not null, the load request failed.
            if (loadAdError != null || ad == null)
            {
                RewardedVideoFailed(loadAdError);
                return;
            }
            else
            {
                RewardedVideoLoaded(ad);
            }
        }


        /// <summary>
        /// Admob specific event triggered after a rewarded video is loaded and ready to be watched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RewardedVideoLoaded(RewardedAd ad)
        {
            rewardedVideo = ad;
            GleyLogger.AddLog("Rewarded Video Loaded.");

            rewardedVideo.OnAdPaid += RewardedPaid;
            rewardedVideo.OnAdImpressionRecorded += RewardedImpressionRec;
            rewardedVideo.OnAdClicked += RewardedAdClicked;
            rewardedVideo.OnAdFullScreenContentOpened += RewardedFullScreenOpened;
            rewardedVideo.OnAdFullScreenContentClosed += RewardedFullScreenClosed;
            rewardedVideo.OnAdFullScreenContentFailed += RewardedFullScreenFailed;

            currentRetryRewardedVideo = 0;
            events.TriggerRewardedVideoLoadSucces();
        }


        /// <summary>
        /// Admob specific event triggered if a rewarded video failed to load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RewardedVideoFailed(LoadAdError e)
        {
            LoadAdError loadAdError = e;

            // Gets the domain from which the error came.
            //string domain = loadAdError.GetDomain();

            // Gets the error code. See
            // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
            // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
            // for a list of possible codes.
            //int code = loadAdError.GetCode();

            // Gets an error message.
            // For example "Account not approved yet". See
            // https://support.google.com/admob/answer/9905175 for explanations of
            // common errors.
            //string message = loadAdError.GetMessage();

            // Gets the cause of the error, if available.
            //AdError underlyingError = loadAdError.GetCause();


            // Get response information, which may include results of mediation requests.
            ResponseInfo responseInfo = loadAdError.GetResponseInfo();

            //GleyLogger.AddLog($"Rewarded Video Failed: {loadAdError.}");
            //GleyLogger.AddLog($"Rewarded Video Response info: {responseInfo}");


            //try again to load a rewarded video
            if (currentRetryRewardedVideo < maxRetryCount)
            {
                currentRetryRewardedVideo++;

                GleyLogger.AddLog("Retry " + currentRetryRewardedVideo);

                rewardedVideoFailedToLoad = true;
            }
            events.TriggerRewardedVideoLoadFailed(e.ToString());
        }


        private void RewardedVideoWatched(Reward reward)
        {
            GleyLogger.AddLog($"Rewarded Video Watched -> Reward amount: {reward.Amount} Reward type: {reward.Type}");
            rewardedVideoWatched = true;
        }


        /// <summary>
        /// Admob specific event triggered when a rewarded video was skipped
        /// </summary>
        private void RewardedAdClosed()
        {
            adIsActive = false;
            resetTimer = true;
            GleyLogger.AddLog("Rewarded Ad Closed");
            rewardedVideoClosed = true;
            resetCallbackTime = true;
        }


        /// <summary>
        /// Because Admob has some problems when used in multi-threading applications with Unity a frame needs to be skipped before returning to application
        /// </summary>
        /// <returns></returns>
        private void CompleteMethodRewardedVideo(bool val)
        {
            adIsActive = false;
            resetTimer = true;
            rewardedVideoClosed = false;
            resetCallbackTime = false;
            if (onRewardedVideoClosed != null)
            {
                onRewardedVideoClosed(val);
                onRewardedVideoClosed = null;
            }

            //reload
            LoadRewardedVideo();
        }


        /// <summary>
        ///Raised when an impression is recorded for an ad.
        /// </summary>
        private void RewardedImpressionRec()
        {
            GleyLogger.AddLog("Rewarded ad recorded an impression.");
        }


        /// <summary>
        /// Raised when the ad is estimated to have earned money.
        /// </summary>
        /// <param name="adValue"></param>
        private void RewardedPaid(AdValue adValue)
        {
            GleyLogger.AddLog($"Rewarded ad paid {adValue.Value} {adValue.CurrencyCode}");
            Events.OnRewardedVideoPaid?.Invoke(adValue, rewardedVideo.GetResponseInfo());
        }


        /// <summary>
        ///Raised when a click is recorded for an ad.
        /// </summary>
        private void RewardedAdClicked()
        {
            GleyLogger.AddLog("Rewarded ad was clicked.");
            events.TriggerRewardedVideoClicked();
        }


        /// <summary>
        ///Raised when an ad opened full screen content. 
        /// </summary>
        private void RewardedFullScreenOpened()
        {
            GleyLogger.AddLog("Rewarded ad full screen content opened.");
        }


        /// <summary>
        ///Raised when the ad closed full screen content.
        /// </summary>
        private void RewardedFullScreenClosed()
        {
            GleyLogger.AddLog("Rewarded ad full screen content closed.");
            RewardedAdClosed();
        }


        /// <summary>
        ///Raised when the ad failed to open full screen content.
        /// </summary>
        /// <param name="error"></param>
        private void RewardedFullScreenFailed(AdError error)
        {
            GleyLogger.AddLog($"Rewarded ad failed to open full screen content. Error: {error}");

            //Reload the rewarded ad so that we can show another one as soon as possible
            RewardedAdClosed();
        }
        #endregion


        #region RewardedInterstitial
        #region InterfaceImplementation
        public bool IsRewardedInterstitialAvailable()
        {
            if (rewardedInterstitial != null)
            {
                return rewardedInterstitial.CanShowAd();
            }
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            if (IsRewardedInterstitialAvailable())
            {
                adIsActive = true;
                resetTimer = true;
                onRewardedInterstitialClosed = completeMethod;
                rewardedInterstitialWatched = false;
                rewardedInterstitial.Show(RewardedInterstitialWatched);
            }
            else
            {
                GleyLogger.AddLog("Rewarded interstitial cannot be shown.");
            }
        }
        #endregion
        private void RewardedInterstitialWatched(Reward reward)
        {
            adIsActive = false;
            resetTimer = true;
            GleyLogger.AddLog($"Rewarded Interstitial Watched -> Reward amount: {reward.Amount} Reward type: {reward.Type}");
            rewardedInterstitialWatched = true;
#if UNITY_EDITOR
            RewardedInterstitialClosed();
#endif
        }

        private void RewardedInterstitialClosed()
        {
            adIsActive = false;
            resetTimer = true;
            GleyLogger.AddLog("Rewarded Interstitial Closed");
            //trigger complete method
            StartCoroutine(CompleteMethodRewardedInterstitial(rewardedInterstitialWatched));
        }

        IEnumerator CompleteMethodRewardedInterstitial(bool rewardedVideoWatched)
        {
            yield return null;
            if (onRewardedInterstitialClosed != null)
            {
                onRewardedInterstitialClosed(rewardedVideoWatched);
                onRewardedInterstitialClosed = null;
            }

            //reload
            LoadRewardedInterstitial();
        }

        private void LoadRewardedInterstitial()
        {
            GleyLogger.AddLog("Start Loading Rewarded Interstitial");


            // Clean up the old ad before loading a new one.
            if (rewardedInterstitial != null)
            {
                rewardedInterstitial.OnAdPaid -= RewardedInterstitialPaid;
                rewardedInterstitial.OnAdImpressionRecorded -= RewardedInterstitialImpressionRec;
                rewardedInterstitial.OnAdClicked -= RewardedInterstitialAdClicked;
                rewardedInterstitial.OnAdFullScreenContentOpened -= RewardedInterstitialFullScreenOpened;
                rewardedInterstitial.OnAdFullScreenContentClosed -= RewardedInterstitialFullScreenClosed;
                rewardedInterstitial.OnAdFullScreenContentFailed -= RewardedInterstitialFullScreenFailed;
                rewardedInterstitial.Destroy();
                rewardedInterstitial = null;
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedInterstitialAd.Load(rewardedInterstitialId, adRequest, LoadRewardedInterstitialCallback);
        }

        private void LoadRewardedInterstitialCallback(RewardedInterstitialAd ad, LoadAdError loadAdError)
        {
            // if error is not null, the load request failed.
            if (loadAdError != null || ad == null)
            {
                RewardedInterstitialFailed(loadAdError);
                return;
            }
            else
            {
                RewardedInterstitialLoaded(ad);
            }
        }

        private void RewardedInterstitialLoaded(RewardedInterstitialAd ad)
        {
            rewardedInterstitial = ad;
            GleyLogger.AddLog("Rewarded Interstitial Loaded.");

            rewardedInterstitial.OnAdPaid += RewardedInterstitialPaid;
            rewardedInterstitial.OnAdImpressionRecorded += RewardedInterstitialImpressionRec;
            rewardedInterstitial.OnAdClicked += RewardedInterstitialAdClicked;
            rewardedInterstitial.OnAdFullScreenContentOpened += RewardedInterstitialFullScreenOpened;
            rewardedInterstitial.OnAdFullScreenContentClosed += RewardedInterstitialFullScreenClosed;
            rewardedInterstitial.OnAdFullScreenContentFailed += RewardedInterstitialFullScreenFailed;

            currentRetryRewardedInterstitial = 0;
            events.TriggerRewardedInterstitialLoadSucces();
        }

        private void RewardedInterstitialFailed(LoadAdError e)
        {
            GleyLogger.AddLog($"Rewarded interstitial ad failed to load an ad with error : {e}");


            //try again to load a rewarded video
            if (currentRetryRewardedInterstitial < maxRetryCount)
            {
                currentRetryRewardedInterstitial++;

                GleyLogger.AddLog("Retry " + currentRetryRewardedInterstitial);

                rewardedInterstitialFailedToLoad = true;
            }
            events.TriggerRewardedInterstitialLoadFailed(e.ToString());
        }

        private void RewardedInterstitialFullScreenFailed(AdError error)
        {

            GleyLogger.AddLog($"Rewarded Interstitial ad failed to open full screen content. Error: {error}");

            //Reload the rewarded ad so that we can show another one as soon as possible
            RewardedInterstitialClosed();
        }

        private void RewardedInterstitialFullScreenClosed()
        {
            GleyLogger.AddLog("Rewarded Interstitial ad full screen content closed.");
#if !UNITY_EDITOR
            RewardedInterstitialClosed();
#endif
        }

        private void RewardedInterstitialFullScreenOpened()
        {
            GleyLogger.AddLog("Rewarded Interstitial ad full screen content opened.");
        }

        private void RewardedInterstitialAdClicked()
        {
            GleyLogger.AddLog("Rewarded Interstitial ad was clicked.");
            events.TriggerRewardedInterstitialClicked();
        }

        private void RewardedInterstitialImpressionRec()
        {
            GleyLogger.AddLog("Rewarded Interstitial ad recorded an impression.");
        }

        private void RewardedInterstitialPaid(AdValue adValue)
        {
            GleyLogger.AddLog($"Rewarded Interstitial ad paid {adValue.Value} {adValue.CurrencyCode}");
            Events.OnRewardedInterstitialPaid?.Invoke(adValue, rewardedInterstitial.GetResponseInfo());
        }


        #endregion


        #region MRec
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            ShowBanner(mrecId, position, BannerType.MediumRectangle, new Vector2Int(), customPosition, false);
        }

        public void HideMRec()
        {
            HideBanner();
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {
            if (initialized == true)
            {
                if (IsInterstitialAvailable() == false)
                {
                    if (currentRetryInterstitial == maxRetryCount)
                    {
                        LoadInterstitial();
                    }
                }

                if (IsRewardedVideoAvailable() == false)
                {
                    if (currentRetryRewardedVideo == maxRetryCount)
                    {
                        LoadRewardedVideo();
                    }
                }

                if (IsRewardedInterstitialAvailable() == false)
                {
                    if (currentRetryRewardedInterstitial == maxRetryCount)
                    {
                        LoadRewardedInterstitial();
                    }
                }

                if (IsAppOpenAvailable() == false)
                {
                    if (currentRetryAppOpen == maxRetryCount)
                    {
                        LoadAppOpen();
                    }
                }
            }
        }
        #endregion


        #region AutoConsent
        #region InterfaceImplementation
        public bool HasBuiltInConsentWindow()
        {
            //ConsentInformation.Reset();
            return true;
        }

        public void InitializaConsentWindow(DebugGeography debugGeography, string testDevice)
        {
            testDeviceID = testDevice;
            this.debugGeography = debugGeography;
        }

        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            onConsentPopupClosed = consentPopupClosed;
            if (GDPRConsentWasSet() == false)
            {
                SetInitialConsent(directedForChildren);
            }
            else
            {
                ShowForm();
            }
        }
        #endregion

        private void SetInitialConsent(bool directedForChildren)
        {
            var debugSettings = new ConsentDebugSettings
            {
                // Geography appears as in EEA for debug devices.
                DebugGeography = debugGeography,
            };

            if (!string.IsNullOrEmpty(testDeviceID))
            {
                debugSettings.TestDeviceHashedIds.Add(testDeviceID);
            }

            ConsentRequestParameters request = new ConsentRequestParameters
            {
                TagForUnderAgeOfConsent = directedForChildren,
                ConsentDebugSettings = debugSettings,
            };
            // Check the current consent information status.
            ConsentInformation.Update(request, OnConsentInfoUpdated);
            ConsentInformation.CanRequestAds();
        }

        private void OnConsentInfoUpdated(FormError error)
        {
            if (error != null)
            {
                OnConsentPopupClosed(error);
                return;
            }
            // If the error is null, the consent information state was updated.
            // You are now ready to check if a form is available.

            LoadForm();
        }


        private void LoadForm()
        {
            if (ConsentInformation.IsConsentFormAvailable())
            {
                // Loads a consent form.
                ConsentForm.Load(OnLoadConsentForm);
            }
            else
            {
                GleyLogger.AddLog("Consent form not available");
                OnConsentPopupClosed(null);
            }
        }

        private void OnLoadConsentForm(ConsentForm consentForm, FormError error)
        {
            if (error != null)
            {
                // Handle the error.
                OnConsentPopupClosed(error);
                return;
            }
            GleyLogger.AddLog($"Consent Status: {ConsentInformation.ConsentStatus}");
            // The consent form was loaded.
            // Save the consent form for future requests.
            this.consentForm = consentForm;

            if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
            {
                ShowForm();
            }
            else
            {
                GleyLogger.AddLog("Consent form not required");
                OnConsentPopupClosed(null);
            }
        }


        private void ShowForm()
        {
            if (consentForm != null)
            {
                consentForm.Show(OnShowForm);
            }
            else
            {
                LoadForm();
            }
        }


        private void OnShowForm(FormError error)
        {
            if (error != null)
            {
                // Handle the error.
                OnConsentPopupClosed(error);
                return;
            }

            // Handle dismissal by reloading form.
            GleyLogger.AddLog("Form Closed");
            OnConsentPopupClosed(null);
            LoadForm();
        }

        void OnConsentPopupClosed(FormError error)
        {
            if (error != null)
            {
                GleyLogger.AddLog($"{error.ErrorCode} {error.Message}");
            }
            onConsentPopupClosed?.Invoke();
            onConsentPopupClosed = null;
        }
        #endregion


        #region ManualConsent
        public bool GDPRConsentWasSet()
        {
            if (ConsentInformation.ConsentStatus == ConsentStatus.Obtained || ConsentInformation.ConsentStatus == ConsentStatus.NotRequired)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetGDPRConsent(bool accept)
        {
            GleyLogger.AddLog($"Admob does not support SetGDPRConsent. Use build in consent popup for settings");
        }

        public UserConsent GetGDPRConsent()
        {
            switch (ConsentInformation.ConsentStatus)
            {
                case ConsentStatus.Obtained:
                case ConsentStatus.NotRequired:
                    return UserConsent.Accept;

                default:
                    return UserConsent.Unset;
            }
        }

        public bool CCPAConsentWasSet()
        {
            return GDPRConsentWasSet();
        }

        public void SetCCPAConsent(bool accept)
        {
            GleyLogger.AddLog($"Admob does not support SetCCPAConsent. Use build in consent popup for settings");
        }

        public UserConsent GetCCPAConsent()
        {
            return GetGDPRConsent();
        }
        #endregion


        #region Debug
        public void OpenDebugWindow()
        {
            MobileAds.OpenAdInspector((AdInspectorError error) =>
            {
                // If the operation failed, an error is returned.
                if (error != null)
                {
                    GleyLogger.AddLog($"Ad Inspector failed to open with error: {error}");
                    return;
                }

                Debug.Log("Ad Inspector opened successfully.");
            });
        }
        #endregion


        AdRequest CreateRequest()
        {
            AdRequest request = new AdRequest();
            request.Extras.Add("is_designed_for_families", designedForFamilies);
            return request;
        }


        void LogErrorMessage(LoadAdError loadAdError)
        {
            // Gets the error code. See
            // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
            // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
            // for a list of possible codes.

            // Gets an error message.
            // For example "Account not approved yet". See
            // https://support.google.com/admob/answer/9905175 for explanations of
            // common errors.
            GleyLogger.AddLog(loadAdError);
            var responseInfo = loadAdError.GetResponseInfo();
            if (responseInfo != null)
            {
                GleyLogger.AddLog(responseInfo.ToString());

                foreach (var item in responseInfo.GetAdapterResponses())
                {
                    GleyLogger.AddLog($"{item.AdapterClassName} {item.AdError} {item.AdError.GetMessage()} {item.AdError.GetCode()}");
                }
            }
        }


        /// <summary>
        /// Used to delay the admob events for multi-threading applications
        /// </summary>
        private void Update()
        {
            if (resetTimer)
            {
                adShowTIme = Time.realtimeSinceStartup;
                resetTimer = false;
            }

            if(interstitialClosed)
            {
                CompleteMethodInterstitial();
            }

            if (rewardedVideoClosed)
            {
                if (resetCallbackTime)
                {
                    callbackTime = Time.realtimeSinceStartup;
                    resetCallbackTime = false;
                }
                if (Time.realtimeSinceStartup - callbackTime > bufferTime || rewardedVideoWatched)
                {
                    CompleteMethodRewardedVideo(rewardedVideoWatched);
                }
            }

            if (interstitialFailedToLoad)
            {
                interstitialFailedToLoad = false;
                Invoke("LoadInterstitial", reloadTime);
            }

            if (rewardedVideoFailedToLoad)
            {
                rewardedVideoFailedToLoad = false;
                Invoke("LoadRewardedVideo", reloadTime);
            }

            if (rewardedInterstitialFailedToLoad)
            {
                rewardedInterstitialFailedToLoad = false;
                Invoke("LoadRewardedInterstitial", reloadTime);
            }

            if (appOpenFailedToLoad)
            {
                appOpenFailedToLoad = false;
                Invoke("LoadAppOpen", reloadTime);
            }
        }



        //private void Awake()
        //{
        //    // Use the AppStateEventNotifier to listen to application open/close events.
        //    // This is used to launch the loaded ad when we open the app.
        //    AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        //}

        //private void OnDestroy()
        //{
        //    // Always unlisten to events when complete.
        //    AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
        //}

        //private void OnAppStateChanged(AppState state)
        //{
        //    Debug.Log("App State changed to : " + state);

        //    // if the app is Foregrounded and the ad is available, show it.
        //    if (state == AppState.Foreground)
        //    {

        //        StartCoroutine(OpenApp());
        //    }
        //}

        //IEnumerator OpenApp()
        //{
        //    yield return new WaitForSeconds(0.2f);
        //    ShowAppOpen();
        //}
    }
}
#endif