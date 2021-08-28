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

    public void Start()
    {
        this.ads = GameObject.FindWithTag("AdsManager").GetComponent<Ads>();
        this.iap = GameObject.FindWithTag("IAP").GetComponent<IAP>();
    }

    public void BuyNoAds()
    {
        GameObject.FindWithTag("IAP").GetComponent<IAP>().BuyNoAds();
    }

    public void Update()
    {
        bool enabled = (this.ads.adsEnabled && this.iap.IsInitialized()) || Application.isEditor;
        this.btn.enabled = enabled;
        this.img.enabled = enabled;
        this.txt.enabled = enabled;
    }
}