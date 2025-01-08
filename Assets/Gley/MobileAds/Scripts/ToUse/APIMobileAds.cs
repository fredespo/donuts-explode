using Gley.MobileAds.Internal;
using UnityEngine;
using UnityEngine.Events;

namespace Gley.MobileAds
{
    public static class API
    {
        #region Basic
        /// <summary>
        /// Initializes the plugin
        /// Should be called only once at the beginning of your app
        /// </summary>
        public static void Initialize(UnityAction completeMethod = null)
        {
            MobileAdsManager.Instance.Initialize(completeMethod);
        }


        /// <summary>
        /// Check if the advertiser was successfully initialized
        /// </summary>
        /// <returns>true if SDK is initialized</returns>
        public static bool IsInitialized()
        {
            return MobileAdsManager.Instance.IsInitialized();
        }


        /// <summary>
        /// Display a banner on screen
        /// </summary>
        /// <param name="position">Relative position to the screen.</param>
        /// <param name="bannerType">Type of banner to be displayed.</param>
        public static void ShowBanner(BannerPosition position, BannerType bannerType)
        {
            MobileAdsManager.Instance.ShowBanner(position, bannerType, new Vector2Int(), new Vector2Int());
        }


        /// <summary>
        /// Hides the active banner.
        /// </summary>
        public static void HideBanner()
        {
            MobileAdsManager.Instance.HideBanner();
        }


        /// <summary>
        /// Check if any interstitial is available to be displayed.
        /// </summary>
        /// <returns>true if interstitial is available</returns>
        public static bool IsInterstitialAvailable()
        {
            return MobileAdsManager.Instance.IsInterstitialAvailable();
        }


        /// <summary>
        /// Display an interstitial if available
        /// </summary>
        /// <param name="interstitialClosed">Callback called when interstitial is closed</param>
        public static void ShowInterstitial(UnityAction interstitialClosed = null)
        {
            MobileAdsManager.Instance.ShowInterstitial(interstitialClosed);
        }


        /// <summary>
        /// Check if any app open ad is available to be displayed
        /// </summary>
        /// <returns>true if app open ad is available</returns>
        public static bool IsAppOpenAvailable()
        {
            return MobileAdsManager.Instance.IsAppOpenAvailable();
        }


        /// <summary>
        /// Display an app open ad if available.
        /// </summary>
        /// <param name="appOpenClosed">Callback called when app open is closed.</param>
        public static void ShowAppOpen(UnityAction appOpenClosed = null)
        {
            MobileAdsManager.Instance.ShowAppOpen(appOpenClosed);
        }



        /// <summary>
        /// Check if any rewarded video is available to be displayed
        /// </summary>
        /// <returns>true is rewarded video is available</returns>
        public static bool IsRewardedVideoAvailable()
        {
            return MobileAdsManager.Instance.IsRewardedVideoAvailable();
        }


        /// <summary>
        /// Show a rewarded video ad if available 
        /// </summary>
        /// <param name="completeMethod">callback called when a rewarded video ad closes</param>
        public static void ShowRewardedVideo(UnityAction<bool> completeMethod)
        {
            MobileAdsManager.Instance.ShowRewardedVideo(completeMethod);
        }


        /// <summary>
        /// Check if any rewarded interstitial is available to be displayed.
        /// </summary>
        /// <returns>true if rewarded interstitial is available</returns>
        public static bool IsRewardedInterstitialAvailable()
        {
            return MobileAdsManager.Instance.IsRewardedInterstitialAvailable();
        }


        /// <summary>
        /// Display a rewarded interstitial ad if available.
        /// </summary>
        /// <param name="completeMethod">Callback called when rewarded interstitial is closed. Use it to reward the user after ad view.</param>
        public static void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            MobileAdsManager.Instance.ShowRewardedInterstitial(completeMethod);
        }


        /// <summary>
        /// Show a medium rectangle ad if available
        /// </summary>
        /// <param name="position">Relative position to the screen</param>
        public static void ShowMRec(BannerPosition position)
        {
            MobileAdsManager.Instance.ShowMRec(position, new Vector2Int());
        }


        /// <summary>
        /// Hide the active MRec
        /// </summary>
        public static void HideMRec()
        {
            MobileAdsManager.Instance.HideMRec();
        }


        /// <summary>
        /// Show a built in consent popup. if advertiser has one, it will be shown, otherwise the default popup available in the package will be shown
        /// </summary>
        /// <param name="consentPopupClosed">Callback called when consent popup is closed</param>
        public static void ShowBuiltInConsentPopup(UnityAction consentPopupClosed)
        {
            MobileAdsManager.Instance.ShowBuiltInConsentWindow(consentPopupClosed);
        }


        /// <summary>
        /// Check if the GDPR consent was previously set. Not required on iOS.
        /// </summary>
        /// <returns>true if consent was already set</returns>
        public static bool GDPRConsentWasSet()
        {
            return MobileAdsManager.Instance.GDPRConsentWasSet();
        }


        /// <summary>
        /// Pass the GDPR consent obtained from the user to the plugin. Not required on iOS
        /// </summary>
        /// <param name="accept">true - user accepted personalized ads</param>
        public static void SetGDPRConsent(bool accept)
        {
            MobileAdsManager.Instance.SetGDPRConsent(accept);
        }


        /// <summary>
        /// Check if the GDPR consent was previously set.
        /// </summary>
        /// <returns>true if consent was already set</returns>
        public static bool CCPAConsentWasSet()
        {
            return MobileAdsManager.Instance.CCPAConsentWasSet();
        }


        /// <summary>
        /// Pass the CCPA consent obtained from the user to the plugin.
        /// </summary>
        /// <param name="accept">true - user agreed to data selling</param>
        public static void SetCCPAConsent(bool accept)
        {
            MobileAdsManager.Instance.SetCCPAConsent(accept);
        }


        /// <summary>
        /// Open the Mediation debug window if supported by the selected advertiser.
        /// </summary>
        public static void OpenDebugWindow()
        {
            MobileAdsManager.Instance.OpenDebugWindow();
        }
        #endregion


        #region Advanced
        /// <summary>
        /// Get the complete config for the selected advertiser
        /// </summary>
        /// <returns>Advertiser config</returns>
        public static Advertiser GetSelectedAdvertiser()
        {
            return MobileAdsManager.Instance.GetSelectedAdvertiser();
        }


        /// <summary>
        /// Display a banner on screen
        /// </summary>
        /// <param name="bannerPositionX">Custom banner position on X axis.</param>
        /// <param name="bannerPositionY">Custom banner position on Y axis.</param>
        /// <param name="bannerSizeX">Custom banner size in X axis.</param>
        /// <param name="bannerSizeY">Custom banner size on Y axis.</param>
        public static void ShowBanner(int bannerPositionX, int bannerPositionY, int bannerSizeX, int bannerSizeY)
        {
            MobileAdsManager.Instance.ShowBanner(BannerPosition.Custom, BannerType.Custom, new Vector2Int(bannerPositionX, bannerPositionY), new Vector2Int(bannerSizeX, bannerSizeY));
        }


        /// <summary>
        /// Display a banner on screen
        /// </summary>
        /// <param name="bannerPositionX">Custom banner position on X axis.</param>
        /// <param name="bannerPositionY">Custom banner position on Y axis.</param>
        /// <param name="bannerType">Type of banner to be displayed.</param>
        public static void ShowBanner(int bannerPositionX, int bannerPositionY, BannerType bannerType)
        {
            MobileAdsManager.Instance.ShowBanner(BannerPosition.Custom, bannerType, new Vector2Int(bannerPositionX, bannerPositionY), new Vector2Int());
        }


        /// <summary>
        /// Display a banner on screen
        /// </summary>
        /// <param name="position">Relative position to the screen.</param>
        /// <param name="bannerSizeX">Custom banner size in X axis.</param>
        /// <param name="bannerSizeY">Custom banner size on Y axis.</param>
        public static void ShowBanner(BannerPosition position, int bannerSizeX, int bannerSizeY)
        {
            MobileAdsManager.Instance.ShowBanner(position, BannerType.Custom, new Vector2Int(), new Vector2Int(bannerSizeX, bannerSizeY));
        }


        /// <summary>
        /// Show a medium rectangle ad if available.
        /// </summary>
        /// <param name="bannerPositionX">Custom banner position on X axis.</param>
        /// <param name="bannerPositionY">Custom banner position on Y axis.</param>
        public static void ShowMRec(int bannerPositionX, int bannerPositionY)
        {
            MobileAdsManager.Instance.ShowMRec(BannerPosition.Custom, new Vector2Int(bannerPositionX, bannerPositionY));
        }


        /// <summary>
        /// Stop displaying interstitial and banner ads.
        /// </summary>
        /// <param name="remove">if true, do not display ads</param>
        public static void RemoveAds(bool remove)
        {
            MobileAdsManager.Instance.RemoveAds(remove);
        }


        /// <summary>
        /// Used to check if remove ads is active or not
        /// </summary>
        /// <returns>false if remove ads was bought</returns>
        public static bool CanShowAds()
        {
            return MobileAdsManager.Instance.CanShowAds();
        }

        public static void ShowATTPopup()
        {
            MobileAdsManager.Instance.ShowATTPopup();
        }
        #endregion
    }
}