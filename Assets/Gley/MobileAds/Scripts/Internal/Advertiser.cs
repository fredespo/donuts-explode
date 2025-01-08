namespace Gley.MobileAds.Internal
{
    [System.Serializable]
    public class Advertiser
    {
        public IAdProvider advertiserScript;
        public SupportedAdvertisers advertiser;
        public PlatformSettings platformSettings;
        public bool directedForChildren;

        public Advertiser(SupportedAdvertisers advertiser, IAdProvider advertiserScript, PlatformSettings platformSettings, bool directedForChildren)
        {
            this.advertiserScript = advertiserScript;
            this.platformSettings = platformSettings;
            this.directedForChildren = directedForChildren;
            this.advertiser = advertiser;
        }

        public Advertiser(SupportedAdvertisers advertiser, PlatformSettings platformSettings, bool directedForChildren)
        {
            this.platformSettings = platformSettings;
            this.advertiser = advertiser;
            this.directedForChildren = directedForChildren;
        }
    }
}
