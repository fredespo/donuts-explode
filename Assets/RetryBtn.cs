using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryBtn : MonoBehaviour
{
    public Ads ads;
    public LevelLoader levelLoader;

    public void Retry()
    {
        ads.ShowInterstitialAdAndThen((wasAdShown) =>
        {
            levelLoader.ResetCurrentLevel();
            levelLoader.StartCurrentLevelAfterDelaySec(wasAdShown ? 0.7f : 0.1f);
        });   
    }
}
