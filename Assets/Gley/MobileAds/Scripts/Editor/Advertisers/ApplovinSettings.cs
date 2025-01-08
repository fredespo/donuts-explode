using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class ApplovinSettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.AppLovin;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_APPLOVIN;

        public string sdkLink => "https://dash.applovin.com/documentation/mediation/unity/getting-started/integration";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };


        public string appIdDisplayName => "SDK key";

        public string bannerDisplayName => "Banner ID";

        public string interstitialDisplayName => "Interstitial ID";

        public string rewardedVideoDisplayName => "Rewarded ID";

        public string rewardedInterstitialDisplayName => "";

        public string nativeAdsDisplayName => "";

        public string appOpenAdsDisplayName => "App Open ID";

        public bool directedForChildrenRequired => false;

        public bool testModeRequired => false;

        public bool consentPopupRequired => true;

        public string mRecDisplayName => "MRec ID";

        public bool testDeviceRequired => false;
    }
}
