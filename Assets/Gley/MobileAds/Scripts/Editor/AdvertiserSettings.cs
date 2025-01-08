using Gley.MobileAds.Internal;
using System.Collections.Generic;

namespace Gley.MobileAds.Editor
{
    //used by settings window for plugin configuration
    [System.Serializable]
    public class AdvertiserSettings
    {
        public SupportedAdvertisers advertiser;
        public bool useSDK;
        public string preprocessorDirective;
        public string sdkLink;
        public bool directedForChildrenRequired;
        public bool testModeRequired;
        public bool testDeviceRequired;
        public bool directedForChildren;
        public bool consentPopupRequired;
        public bool showConsentPopup;
        public List<PlatformSettings> platformSettings;

        public AdvertiserSettings(IAdvertiserSettings advertiser)
        {
            this.advertiser = advertiser.advertiser;
            platformSettings = new List<PlatformSettings>();
            for (int i = 0; i < advertiser.supportedPlatforms.Length; i++)
            {
                platformSettings.Add(new PlatformSettings(advertiser.supportedPlatforms[i],
                    new AdUnitID(advertiser.appIdDisplayName),
                    new AdUnitID(advertiser.bannerDisplayName),
                    new AdUnitID(advertiser.interstitialDisplayName),
                    new AdUnitID(advertiser.rewardedVideoDisplayName),
                    new AdUnitID(advertiser.mRecDisplayName),
                    new AdUnitID(advertiser.rewardedInterstitialDisplayName),
                    new AdUnitID(advertiser.appOpenAdsDisplayName)
                    ));
            }
            UpdateSettings(advertiser);
        }

        internal void UpdateSettings(IAdvertiserSettings advertiser)
        {
            sdkLink = advertiser.sdkLink;
            preprocessorDirective = advertiser.preprocessorDirective;
            directedForChildrenRequired = advertiser.directedForChildrenRequired;
            testModeRequired = advertiser.testModeRequired;
            testDeviceRequired = advertiser.testDeviceRequired;
            consentPopupRequired = advertiser.consentPopupRequired;
            for (int i = 0; i < advertiser.supportedPlatforms.Length; i++)
            {
                platformSettings[i].appId.SetDisplayName(advertiser.appIdDisplayName);
                platformSettings[i].idBanner.SetDisplayName(advertiser.bannerDisplayName);
                platformSettings[i].idMRec.SetDisplayName(advertiser.mRecDisplayName);
                platformSettings[i].idInterstitial.SetDisplayName(advertiser.interstitialDisplayName);
                platformSettings[i].idRewarded.SetDisplayName(advertiser.rewardedVideoDisplayName);
                platformSettings[i].idRewardedInterstitial.SetDisplayName(advertiser.rewardedInterstitialDisplayName);
                platformSettings[i].idOpenApp.SetDisplayName(advertiser.appOpenAdsDisplayName);
            }
        }
    }
}