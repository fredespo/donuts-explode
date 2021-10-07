using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour
{
    public Button btn;
    public Image img;
    public Text txt;
    private Ads ads;
    private IAP iap;
    private IapAnalytics iapAnalytics;

    public void Start()
    {
        this.ads = GameObject.FindWithTag("AdsManager").GetComponent<Ads>();
        GameObject iapObj = GameObject.FindWithTag("IAP");
        this.iap = iapObj.GetComponent<IAP>();
        this.iapAnalytics = iapObj.GetComponent<IapAnalytics>();
    }

    public void BuyNoAds()
    {
        this.iap.BuyNoAds();
    }

    public void Update()
    {
        bool enabled = (this.ads.adsEnabled && this.iap.IsInitialized()) || Application.isEditor;
        this.btn.enabled = enabled;
        this.img.enabled = enabled;
        this.txt.enabled = enabled;
    }

    public void TappedRemoveAdsButtonOnLevelWonMenu()
    {
        this.iapAnalytics.TappedRemoveAdsOnLevelWonMenu();
    }
}