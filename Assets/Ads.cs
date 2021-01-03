using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    public string gameId = "1234567";
    public string interstitialAdPlacementId = "FullScreenAd";
    public bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAdPlacementId))
        {
            Advertisement.Show(interstitialAdPlacementId);
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
}
