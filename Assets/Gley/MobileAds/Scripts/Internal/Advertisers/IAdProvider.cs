using UnityEngine;
using UnityEngine.Events;

namespace Gley.MobileAds.Internal
{
    /// <summary>
    /// interface implemented by all supported advertisers
    /// </summary>
    public interface IAdProvider
    {
        //initialize
        void SetDirectedForChildren(bool active);
        void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized);

        //banner
        void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition);
        void HideBanner();

        //interstitial
        bool IsInterstitialAvailable();
        void ShowInterstitial(UnityAction interstitialClosed);

        //app open
        bool IsAppOpenAvailable();
        void ShowAppOpen(UnityAction appOpenClosed);

        //rewarded
        bool IsRewardedVideoAvailable();
        void ShowRewardedVideo(UnityAction<bool> completeMethod);

        //rewarded interstitial
        bool IsRewardedInterstitialAvailable();
        void ShowRewardedInterstitial(UnityAction<bool> completeMethod);

        //mrec
        void ShowMRec(BannerPosition position, Vector2Int customPosition);
        void HideMRec();

        //resume
        void LoadAdsOnResume();

        //auto consent
        bool HasBuiltInConsentWindow();
        void ShowBuiltInConsentWindow(UnityAction consentPopupClosed);

        //manual consent
        bool GDPRConsentWasSet();
        void SetGDPRConsent(bool accept);
        UserConsent GetGDPRConsent();
        bool CCPAConsentWasSet();
        void SetCCPAConsent(bool accept);
        UserConsent GetCCPAConsent();

        //debug
        void OpenDebugWindow();
    }
}