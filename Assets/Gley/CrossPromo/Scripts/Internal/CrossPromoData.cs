using System.Collections.Generic;
using UnityEngine;

namespace Gley.CrossPromo.Internal
{
    //Used for settings
    [System.Serializable]
    public class PromoFile
    {
        public string gameName;
        public string storeLink;
        public List<string> imageUrls = new List<string>() { "" };
    }


    [System.Serializable]
    public class PlatformSettings
    {
        public bool enabled;
        public string externalFileLink;
        public PromoFile promoFile;
    }


    public class CrossPromoData : ScriptableObject
    {
        public PlatformSettings googlePlaySettings;
        public PlatformSettings appStoreSettings;
        public int nrOfTimesToShow = 5;
        public bool doNotShowAfterImageClick = true;
        public bool allowMultipleDisplaysPerSession;
        public GameObject crossPromoPopup;
        public bool usePlaymaker;
        public bool useUVS;

        public string GetFileURL()
        {
            PlatformSettings platformSettings = new PlatformSettings();
#if UNITY_ANDROID
            platformSettings = googlePlaySettings;
#endif

#if UNITY_IOS
            platformSettings = appStoreSettings;
#endif
            if (platformSettings.enabled)
            {
                //return "file://" + Application.dataPath + "/GleyPlugins/CrossPromo/PromoFiles/GooglePlayPromoFile.txt";
                return platformSettings.externalFileLink;
            }
            return null;
        }
    }
}
