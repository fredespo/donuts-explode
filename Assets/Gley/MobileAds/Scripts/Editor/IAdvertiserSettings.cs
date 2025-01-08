using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public interface IAdvertiserSettings
    {
        SupportedAdvertisers advertiser { get; }
        string preprocessorDirective { get; }
        string sdkLink { get; }
        SupportedPlatforms[] supportedPlatforms { get; }
        string appIdDisplayName{get;}
        string bannerDisplayName { get; }
        string interstitialDisplayName { get; }
        string rewardedVideoDisplayName { get; }
        string rewardedInterstitialDisplayName { get; }
        string mRecDisplayName { get; }
        string nativeAdsDisplayName { get; }
        string appOpenAdsDisplayName { get; }
        bool directedForChildrenRequired { get; }
        bool testModeRequired { get; }
        bool testDeviceRequired { get; }
        bool consentPopupRequired { get; }
    }
}
