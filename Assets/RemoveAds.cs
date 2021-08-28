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

    public void Start()
    {
        this.ads = GameObject.FindWithTag("AdsManager").GetComponent<Ads>();
    }

    public void BuyNoAds()
    {
        GameObject.FindWithTag("IAP").GetComponent<IAP>().BuyNoAds();
    }

    public void Update()
    {
        bool enabled = this.ads.adsEnabled || Application.isEditor;
        this.btn.enabled = enabled;
        this.img.enabled = enabled;
        this.txt.enabled = enabled;
    }
}