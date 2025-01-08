#if GLEY_UVS_SUPPORT
using Unity.VisualScripting;
using UnityEngine;

namespace Gley.MobileAds.Internal
{
    [IncludeInSettings(true)]
    public static class MobileAdsUVS
    {
        private static GameObject _eventTarget;

        public static void Initialize()
        {
            Gley.MobileAds.API.Initialize(OnInitialized);
        }

        private static void OnInitialized()
        {
            EventBus.Trigger(EventNames.OnInitialized, 0);
        }

        public static bool IsInitialized()
        {
            return API.IsInitialized();
        }

        public static void ShowBanner(BannerPosition position, BannerType type)
        {
            Gley.MobileAds.API.ShowBanner(position, type);
        }

        public static void HideBanner()
        {
            Gley.MobileAds.API.HideBanner();
        }

        public static bool IsInterstitialAvailable()
        {
            return Gley.MobileAds.API.IsInterstitialAvailable();
        }

        public static void ShowInterstitial()
        {
            Gley.MobileAds.API.ShowInterstitial(InterstitialClosed);
        }

        private static void InterstitialClosed()
        {
            EventBus.Trigger(EventNames.OnInterstitialClosed, 0);
        }


        public static bool IsAppOpenAvailable()
        {
            return Gley.MobileAds.API.IsAppOpenAvailable();
        }

        public static void ShowAppOpen()
        {
            Gley.MobileAds.API.ShowAppOpen(AppOpenClosed);
        }

        private static void AppOpenClosed()
        {
            EventBus.Trigger(EventNames.OnAppOpenClosed, 0);
        }

        public static bool IsRewardedVideoAvailable()
        {
            return Gley.MobileAds.API.IsRewardedVideoAvailable();
        }

        public static void ShowRewardedVideo(GameObject eventTarget)
        {
            _eventTarget = eventTarget;
            Gley.MobileAds.API.ShowRewardedVideo(VideoComplete);
        }

        private static void VideoComplete(bool complete)
        {
            EventBus.Trigger(EventNames.OnRewardedVideoClosed, _eventTarget, complete);
        }

        public static bool IsRewardedInterstitialAvailable()
        {
            return Gley.MobileAds.API.IsRewardedInterstitialAvailable();
        }

        public static void ShowRewardedInterstitial(GameObject eventTarget)
        {
            _eventTarget = eventTarget;
            Gley.MobileAds.API.ShowRewardedInterstitial(RewardedInterstitialComplete);
        }

        private static void RewardedInterstitialComplete(bool complete)
        {
            EventBus.Trigger(EventNames.OnRewardedInterstitialClosed, _eventTarget, complete);
        }


        public static void ShowBuiltInConsentPopup()
        {
            Gley.MobileAds.API.ShowBuiltInConsentPopup(ConsentPopupClosed);
        }

        private static void ConsentPopupClosed()
        {
            EventBus.Trigger(EventNames.OnConsentPopupClosed, 0);
        }

        public static void RemoveAds()
        {
            Gley.MobileAds.API.RemoveAds(true);
        }

        public static void SetGDPRConsent(bool value)
        {
            Gley.MobileAds.API.SetGDPRConsent(value);
        }

        public static void SetCCPAConsent(bool value)
        {
            Gley.MobileAds.API.SetCCPAConsent(value);
        }

        public static bool GDPRConsentWasSet()
        {
            return Gley.MobileAds.API.GDPRConsentWasSet();
        }

        public static bool CCPAConsentWasSet()
        {
            return Gley.MobileAds.API.CCPAConsentWasSet();
        }
    }
}
#endif