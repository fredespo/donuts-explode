using Gley.MobileAds.Internal;

namespace Gley.MobileAds.Editor
{
    public class AdmobSettings : IAdvertiserSettings
    {
        public SupportedAdvertisers advertiser => SupportedAdvertisers.Admob;

        public string preprocessorDirective => SettingsWindowProperties.GLEY_ADMOB;

        public string sdkLink => "https://github.com/googleads/googleads-mobile-unity/releases";

        public SupportedPlatforms[] supportedPlatforms => new SupportedPlatforms[] { SupportedPlatforms.Android, SupportedPlatforms.iOS };

        public string appIdDisplayName => "App ID";

        public string bannerDisplayName => "Banner ID";

        public string interstitialDisplayName => "Interstitial ID";

        public string rewardedVideoDisplayName => "Rewarded Video ID";

        public string rewardedInterstitialDisplayName => "Rewarded Interstitial ID";

        public string nativeAdsDisplayName => throw new System.NotImplementedException();

        public string appOpenAdsDisplayName => "App Open ID";

        public bool directedForChildrenRequired => true;

        public bool testModeRequired => true;

        public bool consentPopupRequired =>false;

        public string mRecDisplayName => "MRec ID";

        public bool testDeviceRequired => true;
    }
}
