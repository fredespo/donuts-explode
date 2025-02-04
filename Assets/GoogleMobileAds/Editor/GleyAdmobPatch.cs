namespace GoogleMobileAds.Editor
{
    public class GleyAdmobPatch
    {
        public static void SetAdmobAppID(string androidAppId, string iosAppID, string nativePopupText)
        {
#if GLEY_ADMOB
            GoogleMobileAdsSettings instance = GoogleMobileAdsSettings.LoadInstance();
            instance.OptimizeAdLoading = true;
            instance.OptimizeInitialization = true;
            instance.GoogleMobileAdsAndroidAppId = androidAppId;
            instance.GoogleMobileAdsIOSAppId = iosAppID;
            instance.UserTrackingUsageDescription = nativePopupText;
            UnityEditor.EditorUtility.SetDirty(instance);
#endif
        }
    }
}