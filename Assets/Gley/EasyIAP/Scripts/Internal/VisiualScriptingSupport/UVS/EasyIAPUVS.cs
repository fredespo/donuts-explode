#if GLEY_UVS_SUPPORT
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gley.EasyIAP.Internal
{
    [IncludeInSettings(true)]
    public class EasyIAPUVS
    {
        private static GameObject initializeEventTarget;
        private static GameObject buyEventTarget;
        private static GameObject restoreEventTarget;

        public static void Initialize(GameObject _eventTarget)
        {
            initializeEventTarget = _eventTarget;
            Gley.EasyIAP.API.Initialize(InitializeResult);
        }

        private static void InitializeResult(IAPOperationStatus status, string message, List<StoreProduct> products)
        {

            if (status == IAPOperationStatus.Fail)
            {
                Debug.Log(message);
                CustomEvent.Trigger(initializeEventTarget, "InitComplete", false);
            }
            else
            {
                CustomEvent.Trigger(initializeEventTarget, "InitComplete", true);
            }
        }

        public static void RestorePurchases(GameObject _restoreEventTarget)
        {
            restoreEventTarget = _restoreEventTarget;
            Gley.EasyIAP.API.RestorePurchases(ProductBought, RestoreDone);
        }

        private static void RestoreDone()
        {
            CustomEvent.Trigger(restoreEventTarget, "RestoreDone");
        }

        private static void ProductBought(IAPOperationStatus status, string error, StoreProduct product)
        {
            Debug.Log("Product Restored");
        }

        public static bool CheckIfBought(ShopProductNames productToCheck)
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                if (Gley.EasyIAP.API.IsActive(productToCheck))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsInitialized()
        {
            return Gley.EasyIAP.API.IsInitialized();
        }

        public static int GetProductValue(ShopProductNames productToCheck)
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                return Gley.EasyIAP.API.GetValue(productToCheck);
            }

            return 0;
        }

        public static string GetStorePrice(ShopProductNames productToCheck)
        {
            if (Gley.EasyIAP.API.IsInitialized())
            {
                return Gley.EasyIAP.API.GetLocalizedPriceString(productToCheck);
            }
            return "-";
        }

        public static string GetIsoCurrencyCode(ShopProductNames productToCheck)
        {
            if(Gley.EasyIAP.API.IsInitialized())
            {
                return Gley.EasyIAP.API.GetIsoCurrencyCode(productToCheck);
            }
            return "-";
        }

        public static void BuyProduct(GameObject _eventTarget, ShopProductNames productToBuy)
        {
            buyEventTarget = _eventTarget;
            if (Gley.EasyIAP.API.IsInitialized())
            {
                Gley.EasyIAP.API.BuyProduct(productToBuy, BuyComplete);
            }
            else
            {
                CustomEvent.Trigger(buyEventTarget, "BuyComplete", false);
            }
        }

       

        private static void BuyComplete(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                CustomEvent.Trigger(buyEventTarget, "BuyComplete", true);
            }
            else
            {
                CustomEvent.Trigger(buyEventTarget, "BuyComplete", false);
            }
        }
    }
}
#endif
