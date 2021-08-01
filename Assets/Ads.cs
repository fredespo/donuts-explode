using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class Ads : MonoBehaviour
{
    public bool adsEnabled = true;
    public AudioSource music;
    public string testAdId = "ca-app-pub-3940256099942544/1033173712";
    public string androidAdId = "ca-app-pub-8136996048120593/8224216593";
    private InterstitialAd interstitial;
    public int minLevelForAds = 4;
    public LevelLoader levelLoader;
    private float origTimeScale;
    private Action<bool> onAdClosed;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadInterstitialAd();
    }

    public void ShowInterstitialAdAndThen(Action<bool> action)
    {
        if(!this.adsEnabled || levelLoader.GetCurrentLevelIndex() + 1 < this.minLevelForAds)
        {
            action.Invoke(false);
            return;
        }

        if(interstitial != null && interstitial.IsLoaded())
        {
            origTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            this.onAdClosed = action;
            interstitial.Show();
        }
        else
        {
            action.Invoke(false);
        }
    }

    private void LoadInterstitialAd()
    {
        if(Application.isEditor || !this.adsEnabled)
        {
            return;
        }

        if(this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new InterstitialAd(androidAdId);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Time.timeScale = origTimeScale;
        if(this.onAdClosed != null)
        {
            this.onAdClosed.Invoke(true);
            this.onAdClosed = null;
        }
        LoadInterstitialAd();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Invoke("LoadInterstitialAd", 60);
    }
}
