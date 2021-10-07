using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class IapAnalytics : MonoBehaviour
{
    public void TappedRemoveAdsOnTitle()
    {
        TappedRemoveAds("title");
    }

    public void TappedRemoveAdsOnLevelLostMenu()
    {
        TappedRemoveAds("levelLost");
    }

    public void TappedRemoveAdsOnLevelWonMenu()
    {
        TappedRemoveAds("levelWon");
    }

    public void PurchasedRemoveAds()
    {
        Analytics.CustomEvent("purchasedRemoveAds");
    }

    public void AdsDisabled()
    {
        Analytics.CustomEvent("adsDisabled");
    }

    public void FailedToPurchaseRemoveAds(string reason)
    {
        Analytics.CustomEvent("removeAdsPurchaseFailed", new Dictionary<string, object>
        {
            { "reason", reason },
        });
    }

    private void TappedRemoveAds(string screen)
    {
        Analytics.CustomEvent("tappedRemoveAds", new Dictionary<string, object>
        {
            { "screen", screen },
        });
    }
}
