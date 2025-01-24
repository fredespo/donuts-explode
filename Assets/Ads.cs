using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;

public class Ads : MonoBehaviour
{
    public VoidEventChannel rewardForExtraCoffeeCompleted;
    public VoidEventChannel rewardForExtraCoffeeClosed;
    public VoidEventChannel rewardForExtraCoffeeUnavailable;
    private bool isInitialized = false;

    void Start()
    {
        Gley.MobileAds.API.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        isInitialized = true;
    }

    public void ShowRewardedVideoForExtraCoffee()
    {
        if (isInitialized)
        {
            Gley.MobileAds.API.ShowRewardedVideo(OnRewardedVideoForExtraCoffeeCompleteOrClosed);
        }
        else
        {
            rewardForExtraCoffeeUnavailable.RaiseEvent();
        }
    }

    private void OnRewardedVideoForExtraCoffeeCompleteOrClosed(bool completed)
    {
        if (completed)
        {
            rewardForExtraCoffeeCompleted.RaiseEvent();
        }
        else
        {
            rewardForExtraCoffeeClosed.RaiseEvent();
        }
    }
}
