using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gley.EasyIAP.Internal
{
    public class EasyIAPExample : MonoBehaviour
    {
        [SerializeField] Text coinsText;
        [SerializeField] Text removeAdsText;
        [SerializeField] Text buyCoinsText;
        [SerializeField] Text buyRemoveAdsText;

        int coins;
        bool removeAds;
        void Start()
        {
            Gley.EasyIAP.API.Initialize(InitializationComplete);
            if(Gley.EasyIAP.API.IsInitialized())
            {
                RefreshUI();
            }
        }


        void RefreshUI()
        {
            coinsText.text = coins.ToString();
            removeAdsText.text = removeAds.ToString();
            //this should be the example
            //but since your products will not have the same names, we will use the string version 
            //buyCoinsText.text = $"Buy {Gley.EasyIAP.API.GetValue(ShopProductNames.Coins)} Coins {Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.Coins)}";
            //buyRemoveAdsText.text = $"Remove ads - {Gley.EasyIAP.API.GetLocalizedPriceString(ShopProductNames.RemoveAds)}";

            //this is just a workaround to avoid errors
            ShopProductNames coinsProduct = Gley.EasyIAP.API.ConvertNameToShopProduct("Coins");
            ShopProductNames adsProduct = Gley.EasyIAP.API.ConvertNameToShopProduct("RemoveAds");
            buyCoinsText.text = $"Buy {Gley.EasyIAP.API.GetValue(coinsProduct)} Coins {Gley.EasyIAP.API.GetLocalizedPriceString(coinsProduct)}";
            buyRemoveAdsText.text = $"Remove ads - {Gley.EasyIAP.API.GetLocalizedPriceString(adsProduct)}";
        }


        private void InitializationComplete(IAPOperationStatus status, string message)
        {
            if (status == IAPOperationStatus.Success)
            {
                // IAP was successfully initialized.
                // If remove ads was bought before, mark it as owned.
                
                //this should be your call
                //if (API.IsActive(ShopProductNames.RemoveAds))

                // This is just a workaround to avoid errors
                ShopProductNames adsProduct = Gley.EasyIAP.API.ConvertNameToShopProduct("RemoveAds");
                if(API.IsActive(adsProduct))
                //
                {
                    removeAds = true;
                }
            }
            else
            {
                Debug.Log("Error occurred: " + message);
            }
            RefreshUI();
        }


        public void BuyCoins()
        {
            //this is the normal implementation
            ////but since your products will not have the same names, we will use the string version to avoid compile errors
            //Gley.EasyIAP.API.BuyProduct(ShopProductNames.Coins, ProductBought);


            ShopProductNames coinsProduct = Gley.EasyIAP.API.ConvertNameToShopProduct("Coins");
            Gley.EasyIAP.API.BuyProduct(coinsProduct, ProductBought);
        }


        public void BuyRemoveAds()
        {
            // this is the normal implementation
            //but since your products will not have the same names, we will use the string version to avoid compile errors
            //Gley.EasyIAP.API.BuyProduct(ShopProductNames.RemoveAds, ProductBought);

            ShopProductNames adsProduct = Gley.EasyIAP.API.ConvertNameToShopProduct("RemoveAds");
            Gley.EasyIAP.API.BuyProduct(adsProduct, ProductBought);
        }


        private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                //since all consumable products reward the same coin, a simple type check is enough 
                if (product.productType == ProductType.Consumable)
                {
                    coins += product.value;
                }

                if (product.productName == "RemoveAds")
                {
                    removeAds = true;
                    //disable ads here
                }
            }
            else
            {
                Debug.Log("Error occurred: " + message);
            }
            RefreshUI();
        }


        public void RestorePurchases()
        {
            Gley.EasyIAP.API.RestorePurchases(ProductRestored);

            //the product bought method can also be used as a callback for restore.
            //The callback is triggered for each product to be restored
            //This example is also valid, if you are using a single product bought example for all your purchases like this
            //Gley.EasyIAP.API.RestorePurchases(ProductBought);
        }


        private void ProductRestored(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                if (product.productName == "RemoveAds")
                {
                    removeAds = true;
                    //disable ads here
                }
            }
            else
            {
                Debug.Log("Error occurred: " + message);
            }
            RefreshUI();
        }
    }
}
