using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gley.EasyIAP;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour
{
    public VoidEventChannel coffeeUnlimitedPurchaseEventChannel;
    private bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing IAP");
        API.Initialize(InitializationComplete);
    }

    private void InitializationComplete(IAPOperationStatus status, string message)
    {
        if (status == IAPOperationStatus.Success)
        {
            Debug.Log("IAP initialized");
            this.isInitialized = true;
            if (API.IsActive(ShopProductNames.UnlimitedCoffee))
            {
                Debug.Log("Unlimited coffee was previously purchased");
                coffeeUnlimitedPurchaseEventChannel.RaiseEvent();
            }
        }
        else
        {
            Debug.Log("Error occurred: " + message);
        }
    }

    public void BuyUnlimitedCoffee()
    {
        if (!this.isInitialized)
        {
            Debug.Log("IAP not initialized");
            return;
        }
        API.BuyProduct(ShopProductNames.UnlimitedCoffee, OnPurchaseComplete);
    }

    private void OnPurchaseComplete(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
            Debug.Log("Successfully purchased " + product.productName);
            OnPurchaseSuccess(product);
        }
        else
        {
            Debug.Log("Error in purchase of " + product.productName + ": " + message);
        }
    }

    private void OnPurchaseSuccess(StoreProduct product)
    {
        ShopProductNames productName = API.ConvertNameToShopProduct(product.productName);
        if (productName == ShopProductNames.UnlimitedCoffee)
        {
            Debug.Log("Purchased unlimited coffee");
            coffeeUnlimitedPurchaseEventChannel.RaiseEvent();
        }
    }

    public string GetPriceStringForUnlimitedCoffee()
    {
        return API.GetLocalizedPriceString(ShopProductNames.UnlimitedCoffee);
    }
}
