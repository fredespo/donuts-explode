using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class LevelPlaySettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.LevelPlay;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_LEVELPLAY;

        public string sdkLink => "com.unity.services.levelplay";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };


        public string appIdDisplayName => "App Key";

        public string bannerDisplayName => "Banner Placement";

        public string interstitialDisplayName => "Interstitial Placement";

        public string rewardedVideoDisplayName => "Rewarded Placement";

        public string rewardedInterstitialDisplayName => "";

        public string nativeAdsDisplayName => "";

        public string appOpenAdsDisplayName => "";

        public bool directedForChildrenRequired => true;

        public bool testModeRequired => false;

        public bool consentPopupRequired => true;

        public string mRecDisplayName =>"MRec Placement";

        public bool testDeviceRequired => false;
    }
}
