using UnityEngine;
using UnityEngine.Events;

namespace Gley.MobileAds.Internal
{
    public class DefaultImplementation : MonoBehaviour, IAdProvider
    {
        public void HideBanner()
        {
            
        }

        public bool IsBannerAvailable()
        {
            return false;
        }

        public bool IsInterstitialAvailable()
        {
            return false;
        }

        public bool IsRewardedVideoAvailable()
        {
            return false;
        }

        public void OpenDebugWindow()
        {
            
        }

        public void ShowBanner(BannerPosition position, BannerType bannerType, Vector2Int customSize, Vector2Int customPosition)
        {
           
        }

        public void ShowInterstitial(UnityAction InterstitialClosed)
        {
                
        }

        public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
        {
                
        }

        public void LoadAdsOnResume()
        {
            
        }

        public bool HasBuiltInConsentWindow()
        {
            return false;
        }

        public void InitializeAds(PlatformSettings platformSettings, UnityAction onInitialized)
        {
            
        }

        public void ShowBuiltInConsentWindow(UnityAction consentPopupClosed)
        {
            
        }

        public bool GDPRConsentWasSet()
        {
            return false;
        }

        public void SetGDPRConsent(bool accept)
        {
            
        }

        public UserConsent GetGDPRConsent()
        {
            return UserConsent.Unset;
        }

        public bool CCPAConsentWasSet()
        {
            return false;
        }

        public void SetCCPAConsent(bool accept)
        {
            
        }

        public UserConsent GetCCPAConsent()
        {
            return UserConsent.Unset;
        }

        public void SetDirectedForChildren(bool active)
        {
            
        }

        public void ShowMRec(BannerPosition position, Vector2Int customPosition)
        {
            
        }

        public void HideMRec()
        {
            
        }

        public bool IsRewardedInterstitialAvailable()
        {
            return false;
        }

        public void ShowRewardedInterstitial(UnityAction<bool> completeMethod)
        {
            
        }

        public bool IsAppOpenAvailable()
        {
            return false;
        }

        public void ShowAppOpen(UnityAction appOpenClosed)
        {
            
        }
    }
}