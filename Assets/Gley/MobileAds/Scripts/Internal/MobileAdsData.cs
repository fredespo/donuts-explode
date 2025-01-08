namespace Gley.MobileAds.Internal
{
    using UnityEngine;

    //settings for all advertisers saved from Settings Window
    public class MobileAdsData : ScriptableObject
    {
        public GameObject consentCanvas;
        public GameObject consentPopup;
        public Advertiser androidAdvertiser;
        public Advertiser iOSAdvertiser;
        public bool debugMode;
        public bool enableATT;
        public string nativePopupText;

        public void Init(Advertiser androidAdvertiser, Advertiser iOSAdvertiser, bool debugMode, bool enableATT, string nativePopupText, GameObject consentCanvas, GameObject consentPopup)
        {
            this.consentCanvas = consentCanvas;
            this.consentPopup = consentPopup;
            this.androidAdvertiser = androidAdvertiser;
            this.iOSAdvertiser = iOSAdvertiser;
            this.debugMode = debugMode;
            this.enableATT = enableATT;
            this.nativePopupText = nativePopupText;
        }
    }
}
