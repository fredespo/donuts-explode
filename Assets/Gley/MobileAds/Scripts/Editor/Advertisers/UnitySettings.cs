using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class UnitySettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.UnityLegacy;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_UNITYADS;

        public string sdkLink => "com.unity.ads";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };


        public string appIdDisplayName => "Game ID";

        public string bannerDisplayName => "Banner Placement ID";

        public string interstitialDisplayName => "Interstitial Placement ID";

        public string rewardedVideoDisplayName => "Rewarded Placement ID";

        public string rewardedInterstitialDisplayName => "";

        public string nativeAdsDisplayName => "";

        public string appOpenAdsDisplayName => "";

        public bool directedForChildrenRequired => true;

        public bool testModeRequired => true;

        public bool consentPopupRequired => false;

        public string mRecDisplayName => "";

        public bool testDeviceRequired => false;
    }
}
