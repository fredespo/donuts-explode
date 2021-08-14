using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAds : MonoBehaviour
{
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
        if(!this.ads.adsEnabled)
        {
            gameObject.SetActive(false);
        }
    }
}