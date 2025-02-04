using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCoffeeButton : MonoBehaviour
{
    public IAP iap;
    public Text text;

    void OnEnable()
    {
        text.text = "Unlimited - " + iap.GetPriceStringForUnlimitedCoffee();
    }
}
