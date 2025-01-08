using Gley.MobileAds.Internal;
using System.Collections.Generic;
using UnityEngine;

namespace Gley.MobileAds.Editor
{
    //settings for all advertisers saved from Settings Window
    public class MobileAdsSettingsData : ScriptableObject
    {
        public List<AdvertiserSettings> advertiserSettings = new List<AdvertiserSettings>();
        public bool debugMode = false;
        public bool usePlaymaker = false;
        public bool useUnityVisualScripting=false;
        public bool enableATT = false;
        public string nativePopupText= "We will use your data to provide a better and personalized ad experience.";

        public SupportedAdvertisers selectedAndroidAdvertiser;
        public SupportedAdvertisers selectediOSAdvertiser;
    }
}
