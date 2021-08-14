using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAds : MonoBehaviour
{
    public void BuyNoAds()
    {
        GameObject.FindWithTag("IAP").GetComponent<IAP>().BuyNoAds();
    }
}
