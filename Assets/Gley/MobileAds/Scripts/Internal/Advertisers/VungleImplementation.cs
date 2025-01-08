#if GLEY_VUNGLE
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Gley.MobileAds.Internal
{
    public class VungleImplementation : MonoBehaviour, IAdProvider
    {
        private UnityAction<bool> onRewardedVideoClosed;
        private UnityAction onInterstitialClosed;
        private UnityAction onInitialized;
        private string appID = "";
        private string rewardedPlacementId = "";
        private string interstitialPlacementID = "";
        private string bannerPlacementID = "";
        private string mRecPlacementID = "";
        private bool initComplete;
        private bool reinitialize;
        private bool initialized;


        #region Initialize
        #region Interface Implementation
        public void SetDirectedForChildren(bool active)
        {
            Vungle.updateCoppaStatus(active);
        }

        /// <summary>
        /// Initializing Vungle
        /// </summary>
        /// <param name="consent">user consent -> if true show personalized ads</param>
        /// <param name="platformSettings">contains all required settings for this publisher</param>
        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            if (initialized == false)
            {
                //get settings
                PlatformSettings settings = platformSettings;
                this.onInitialized = onInitialized;

                //apply settings
                appID = settings.appId.id;
                rewardedPlacementId = settings.idRewarded.id;
                interstitialPlacementID = settings.idInterstitial.id;
                bannerPlacementID = settings.idBanner.id;
                mRecPlacementID = settings.idMRec.id;

                //verify settings

                GleyLogger.AddLog($"{settings.appId.displayName} : {appID}");
                GleyLogger.AddLog($"{settings.idBanner.displayName} : {bannerPlacementID}");
                GleyLogger.AddLog($"{settings.idMRec.displayName} : {mRecPlacementID}");
                GleyLogger.AddLog($"{settings.idInterstitial.displayName} : {interstitialPlacementID}");
                GleyLogger.AddLog($"{settings.idRewarded.displayName} : {rewardedPlacementId}");


                //preparing Vungle SDK for initialization
                Dictionary<string, bool> placements = new Dictionary<string, bool>
                {
                    { rewardedPlacementId, false },
                    { interstitialPlacementID, false }
                };

                string[] array = new string[placements.Keys.Count];
                placements.Keys.CopyTo(array, 0);
                Vungle.onInitializeEvent += InitComplete;
                Vungle.onAdStartedEvent += AdStarted;
                Vungle.onLogEvent += VungleLog;
                Vungle.onAdEndEvent = OnAdEnd;
                Vungle.onAdRewardedEvent += OnAdRewarded;
                Vungle.onErrorEvent += OnErrorEvent;
                Vungle.onPlacementPreparedEvent += OnPlacementPreparedEvent;
                Vungle.adPlayableEvent += AdPlayableEvent;

                GleyLogger.AddLog($"Start Initialization");
                Vungle.init(appID);
            }
        }
        #endregion

        /// <summary>
        /// Vungle specific event triggered after initialization is done
        /// </summary>
        private void InitComplete()
        {
            initComplete = true;
            reinitialize = false;
            Vungle.onInitializeEvent -= InitComplete;
            GleyLogger.AddLog("Initialization Complete");

            //load ads
            if (!string.IsNullOrEmpty(interstitialPlacementID))
            {
                Vungle.loadAd(interstitialPlacementID);
            }
            if (!string.IsNullOrEmpty(rewardedPlacementId))
            {
                Vungle.loadAd(rewardedPlacementId);
            }
            initialized = true;
            onInitialized?.Invoke();
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
            ShowBanner(bannerPlacementID, position, bannerType, customSize, customPosition);
        }
        public void HideBanner()
        {
            HideBanner(bannerPlacementID);
        }
        #endregion

        public void HideBanner(string bannerID)
        {
            StopAllCoroutines();
            Vungle.closeBanner(bannerID);
        }

        public void ShowBanner(string bannerID, BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
            HideBanner(bannerID);
            Vungle.VungleBannerSize adSize = Vungle.VungleBannerSize.VungleAdSizeBanner;
            Vungle.VungleBannerPosition adPosition = Vungle.VungleBannerPosition.TopCenter;

            switch (bannerType)
            {
                case BannerType.Banner:
                    adSize = Vungle.VungleBannerSize.VungleAdSizeBanner;
                    break;
                case BannerType.BannerShort:
                    adSize = Vungle.VungleBannerSize.VungleAdSizeBannerShort;
                    break;
                case BannerType.MediumRectangle:
                    adSize = Vungle.VungleBannerSize.VungleAdSizeBannerMedium;
                    break;
                case BannerType.Leaderboard:
                    adSize = Vungle.VungleBannerSize.VungleAdSizeBannerLeaderboard;
                    break;
                default:
                    GleyLogger.AddLog($"Banner Type: {bannerType} not supported by Vungle, BannerType.Banner will be used");
                    break;
            }

            switch (position)
            {
                case BannerPosition.Top:
                    adPosition = Vungle.VungleBannerPosition.TopCenter;
                    break;
                case BannerPosition.Bottom:
                    adPosition = Vungle.VungleBannerPosition.BottomCenter;
                    break;
                case BannerPosition.Center:
                    adPosition = Vungle.VungleBannerPosition.Centered;
                    break;
                case BannerPosition.TopLeft:
                    adPosition = Vungle.VungleBannerPosition.TopLeft;
                    break;
                case BannerPosition.TopRight:
                    adPosition = Vungle.VungleBannerPosition.TopRight;
                    break;
                case BannerPosition.BottomLeft:
                    adPosition = Vungle.VungleBannerPosition.BottomLeft;
                    break;
                case BannerPosition.BottomRight:
                    adPosition = Vungle.VungleBannerPosition.BottomRight;
                    break;
                case BannerPosition.Custom:
                    break;
                default:
                    GleyLogger.AddLog($"Banner Position: {position} not supported by Vungle, BannerPosition.Top will be used");
                    break;
            }

            Vungle.loadBanner(bannerID, adSize, adPosition);



            StartCoroutine(WaitForBanner(bannerID, position, customPosition, adSize));
        }


        IEnumerator WaitForBanner(string bannerID, BannerPosition position, Vector2Int customPosition, Vungle.VungleBannerSize adSize)
        {
            while (Vungle.isAdvertAvailable(bannerID, adSize) == false)
            {
                yield return new WaitForSeconds(0.5f);
            }
            GleyLogger.AddLog("Show banner");
            Vungle.showBanner(bannerID);
            if (position == BannerPosition.Custom)
            {
                Vungle.setBannerOffset(bannerID, customPosition.x, customPosition.y);
            }
        }
        #endregion


        #region Interstitial
        #region Interface Implementation
        /// <summary>
        /// Check if Vungle interstitial is available
        /// </summary>
        /// <returns>true if an interstitial is available</returns>
        public bool IsInterstitialAvailable()
        {
            if (!initComplete)
                return false;
            return Vungle.isAdvertAvailable(interstitialPlacementID);
        }


        /// <summary>
        /// Show Vungle interstitial
        /// </summary>
        /// <param name="InterstitialClosed">callback called when user closes interstitial</param>
        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
            if (IsInterstitialAvailable())
            {
                onInterstitialClosed = InterstitialClosed;
                Vungle.playAd(interstitialPlacementID);
            }
        }
        #endregion


        #endregion


        #region Rewarded
        #region Interface Implementation
        /// <summary>
        /// Check if Vungle rewarded video is available
        /// </summary>
        /// <returns>true if a rewarded video is available</returns>
        public bool IsRewardedVideoAvailable()
        {
            if (!initComplete)
                return false;
            return Vungle.isAdvertAvailable(rewardedPlacementId);
        }


        /// <summary>
        /// Show Vungle rewarded video
        /// </summary>
        /// <param name="CompleteMethod">callback called when user closes the rewarded video -> if true video was not skipped</param>
        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
            if (IsRewardedVideoAvailable())
            {
                onRewardedVideoClosed = CompleteMethod;
                Vungle.playAd(rewardedPlacementId);
            }
        }
        #endregion

        #endregion


        #region MRec
        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            ShowBanner(mRecPlacementID, position, BannerType.MediumRectangle, new Vector2Int(), customPosition);
        }

        public void HideMRec()
        {
            HideBanner(mRecPlacementID);
        }
        #endregion


        #region Events
        private void OnAdRewarded(string placementID)
        {
            GleyLogger.AddLog(this + " OnAdRewarded " + placementID);

            if (placementID == rewardedPlacementId)
            {
                if (onRewardedVideoClosed != null)
                {
                    onRewardedVideoClosed(true);
                    onRewardedVideoClosed = null;
                }
            }
        }


        private void OnAdEnd(string placementID)
        {

            GleyLogger.AddLog($"OnAdEnd {placementID}");


            if (placementID == rewardedPlacementId)
            {
                if (onRewardedVideoClosed != null)
                {
                    onRewardedVideoClosed(false);
                    onRewardedVideoClosed = null;
                }
                GleyLogger.AddLog($"Load another ad {placementID}");

                Vungle.loadAd(rewardedPlacementId);
            }

            if (placementID == interstitialPlacementID)
            {
                if (onInterstitialClosed != null)
                {
                    onInterstitialClosed();
                    onInterstitialClosed = null;
                }

                GleyLogger.AddLog($"Load another ad {placementID}");


                Vungle.loadAd(interstitialPlacementID);
            }
        }

        private void AdPlayableEvent(string placementID, bool adPlayable)
        {
            GleyLogger.AddLog($"Ad's playable state has been changed! placementID {placementID}.Now: {adPlayable}");
        }


        private void OnPlacementPreparedEvent(string arg1, string arg2)
        {
            GleyLogger.AddLog($"OnPlacementPreparedEvent {arg1} {arg2}");
        }


        private void OnErrorEvent(string message)
        {
            GleyLogger.AddLog($"OnErrorEvent: {message}");
            if (message.Contains("Initialization"))
            {
                reinitialize = true;
            }
            if (message.Contains(bannerPlacementID) || message.Contains(mRecPlacementID))
            {
                StopAllCoroutines();
            }

        }


        private void AdStarted(string placementID)
        {
            GleyLogger.AddLog($"Ad Started: {placementID}");
        }


        /// <summary>
        /// VUngle specific log event
        /// </summary>
        /// <param name="obj"></param>
        private void VungleLog(string obj)
        {
            GleyLogger.AddLog(obj);
        }
        #endregion


        #region Resume
        public void LoadAdsOnResume()
        {
            if (reinitialize == true)
            {
                Vungle.init(appID);
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
            GleyLogger.AddLog($"ShowBuiltInConsentWindow Not supported on {SupportedAdvertisers.Vungle}");
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
                Vungle.updateConsentStatus(Vungle.Consent.Accepted);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.GDPR_KEY, (int)UserConsent.Deny);
                Vungle.updateConsentStatus(Vungle.Consent.Denied);
            }
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
                Vungle.updateCCPAStatus(Vungle.Consent.Accepted);
            }
            else
            {
                PlayerPrefs.SetInt(Constants.CCPA_KEY, (int)UserConsent.Deny);
                Vungle.updateCCPAStatus(Vungle.Consent.Denied);
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
            GleyLogger.AddLog($"OpenDebugWindow Not supported on {SupportedAdvertisers.Vungle}");
        }
        #endregion

        #region NotSupported
        public bool IsAppOpenAvailable()
        {
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            GleyLogger.AddLog($"ShowAppOpen Not supported on {SupportedAdvertisers.Vungle}");
        }

        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            GleyLogger.AddLog($"ShowRewardedInterstitial Not supported on {SupportedAdvertisers.Vungle}");
        }
        #endregion
    }
}
#endif