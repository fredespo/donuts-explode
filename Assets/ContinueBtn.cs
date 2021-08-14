using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    private Ads ads;
    private LevelLoader levelLoader;

    public void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        ads = GameObject.FindGameObjectWithTag("AdsManager").GetComponent<Ads>();
    }

    public void Continue()
    {
        ads.ShowInterstitialAdAndThen((wasAdShown) =>
        {
            levelLoader.LoadNextLevelAndStartAfterDelay(wasAdShown ? 0.7f : 0.1f);
        });
    }
}
