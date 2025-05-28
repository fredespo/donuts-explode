using UnityEngine;
using Gley.EasyIAP;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAP : MonoBehaviour
{
    public VoidEventChannel coffeeUnlimitedPurchaseEventChannel;
    public VoidEventChannel coffeeUnlimitedPurchaseInactiveEventChannel;
    public VoidEventChannel purchaseRestoreDoneChannel;
    private bool isInitialized = false;

    public Text errorMsgText;
    public GameObject errorScreen;
    private bool isPurchaseRestoreInProgress = false;

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
            } else
            {
                Debug.Log("Unlimited coffee was not previously purchased or was refunded");
                coffeeUnlimitedPurchaseInactiveEventChannel.RaiseEvent();
            }
        }
        else
        {
            Debug.Log("Error occurred: " + message);
            ShowErrorMessage(message);
        }
    }

    public bool IsUnlimitedCoffeePurchaseActive() {
        return API.IsActive(ShopProductNames.UnlimitedCoffee);
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
        if (product == null) {
            Debug.Log("Product is null");
            return;
        }

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
        if (!this.isInitialized)
        {
            Debug.Log("IAP not initialized");
            return string.Empty;
        }
        return API.GetLocalizedPriceString(ShopProductNames.UnlimitedCoffee);
    }

    public bool IsPurchaseRestoreInProgress() {
        return this.isPurchaseRestoreInProgress;
    }

    private void ShowErrorMessage(string message)
    {
        if (errorScreen != null)
        {
            errorScreen.SetActive(true);
        }

        if (errorMsgText != null)
        {
            errorMsgText.text = message;
        }
    }

    public void RestorePurchases()
    {
        isPurchaseRestoreInProgress = true;
        API.RestorePurchases(OnPurchaseComplete, (UnityAction)RestorePurchasesDone);
    }

    private void RestorePurchasesDone()
    {
        isPurchaseRestoreInProgress = false;
        purchaseRestoreDoneChannel.RaiseEvent();
        Debug.Log("Restore done");
    }
}
