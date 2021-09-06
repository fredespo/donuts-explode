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
    private DataStorage storage;
    private bool adLoaded = false;

    void Start()
    {
        this.storage = GameObject.FindWithTag("DataStorage").GetComponent<DataStorage>();
        this.adsEnabled = storage.GetAdsEnabled();
        MobileAds.Initialize(initStatus => { });
        LoadInterstitialAd();
    }

    public void ShowInterstitialAdAndThen(Action<bool> action)
    {
        if(Application.isEditor || !this.adsEnabled || levelLoader.GetCurrentLevelIndex() + 1 < this.minLevelForAds)
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
        if(Application.isEditor || !this.adsEnabled || this.adLoaded)
        {
            return;
        }

        if(this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        //https://developers.google.com/admob/unity/interstitial
        this.interstitial = new InterstitialAd(androidAdId);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
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
        this.adLoaded = false;
        LoadInterstitialAd();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        this.adLoaded = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        this.adLoaded = false;
        Invoke("LoadInterstitialAd", 60);
    }

    public void EnableAds()
    {
        this.adsEnabled = true;
        SaveAdsEnabled();
    }

    public void DisableAds()
    {
        this.adsEnabled = false;
        SaveAdsEnabled();
    }

    private void SaveAdsEnabled()
    {
        storage.SaveAdsEnabled(this.adsEnabled);
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            LoadInterstitialAd();
        }
    }
}
