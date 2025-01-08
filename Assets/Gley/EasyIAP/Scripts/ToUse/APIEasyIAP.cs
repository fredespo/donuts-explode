#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#define GleyIAPEnabled
#endif
using Gley.EasyIAP.Internal;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
#if GleyIAPEnabled
using UnityEngine.Purchasing;
#endif

namespace Gley.EasyIAP
{
    public static class API
    {
        /// <summary>
        /// Initialize store products. Call this method once at the beginning of your game.
        /// </summary>
        /// <param name="completeMethod">Callback method, returns a list of all store products. Use this method for initializations</param>
        public static void Initialize(UnityAction<IAPOperationStatus, string, List<StoreProduct>> completeMethod)
        {
            IAPManager.Instance.Initialize(completeMethod);
        }


        /// <summary>
        /// Initialize store products. Call this method once at the beginning of your game.
        /// </summary>
        /// <param name="completeMethod">Callback method. Use this method for initializations</param>
        public static void Initialize(UnityAction<IAPOperationStatus, string> completeMethod)
        {
            IAPManager.Instance.Initialize(completeMethod);
        }


        /// <summary>
        /// Checks if IAP is initialized 
        /// </summary>
        /// <returns>true if shop was already initialized</returns>
        public static bool IsInitialized()
        {
            return IAPManager.Instance.IsInitialized();
        }


        /// <summary>
        /// Buy an IAP product
        /// </summary>
        /// <param name="productName">An enum member generated from Settings Window</param>
        /// <param name="completeMethod">Callback method that returns the bought product details</param>
        public static void BuyProduct(ShopProductNames productName, UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod)
        {
            IAPManager.Instance.BuyProduct(productName, completeMethod);
        }


        /// <summary>
        /// Restore previously bought products (Only required on iOS)
        /// </summary>
        /// <param name="completeMethod">Called for each product that is restored</param>
        public static void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod)
        {
            IAPManager.Instance.RestorePurchases(completeMethod);
        }


        /// <summary>
        /// Restore previously bought products (Only required on iOS)
        /// </summary>
        /// <param name="completeMethod">Called for each product that is restored</param>
        /// <param name="restoreDone">Called when restore process is complete</param>
        public static void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod, UnityAction restoreDone)
        {
            IAPManager.Instance.RestorePurchases(completeMethod, restoreDone);
        }


        /// <summary>
        /// Returns the type of the given product
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>Consumable/NonConsumable/Subscription</returns>
        public static ProductType GetProductType(ShopProductNames product)
        {
            return IAPManager.Instance.GetProductType(product);
        }

        /// <summary>
        /// Returns the raw receipt of a product
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The receipt in JSON format</returns>
        public static string GetReceipt(ShopProductNames product)
        {
            return IAPManager.Instance.GetReceipt(product);
        }


        /// <summary>
        /// Get product reward
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The amount of in game currency received</returns>
        public static int GetValue(ShopProductNames product)
        {
            return IAPManager.Instance.GetValue(product);
        }


        /// <summary>
        /// Get the price and currency code of the product as a string
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>the localized price and currency of the product</returns>
        public static string GetLocalizedPriceString(ShopProductNames product)
        {
            return IAPManager.Instance.GetLocalizedPriceString(product);
        }


        /// <summary>
        /// Get decimal product price denominated in the local currency
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The decimal price</returns>
        public static double GetPrice(ShopProductNames product)
        {
            return IAPManager.Instance.GetPrice(product);
        }


        /// <summary>
        /// Get product currency in ISO 4217 format
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>Product currency ex. GBP or USD</returns>
        public static string GetIsoCurrencyCode(ShopProductNames product)
        {
            return IAPManager.Instance.GetIsoCurrencyCode(product);
        }


        /// <summary>
        /// Get description from the store
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The product description from the store</returns>
        public static string GetLocalizedDescription(ShopProductNames product)
        {
            return IAPManager.Instance.GetLocalizedDescription(product);
        }


        /// <summary>
        /// Get title from the store
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The product title from the store</returns>
        public static string GetLocalizedTitle(ShopProductNames product)
        {
            return IAPManager.Instance.GetLocalizedTitle(product);
        }


        /// <summary>
        /// Check if a product is bought
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>true if the product was already bought or a subscription is active</returns>
        public static bool IsActive(ShopProductNames product)
        {
            return IAPManager.Instance.IsActive(product);
        }


        /// <summary>
        /// Get additional info for subscription
        /// Get more info: https://docs.unity3d.com/Manual/UnityIAPSubscriptionProducts.html
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>All infos available for the subscription</returns>
        public static SubscriptionInfo GetSubscriptionInfo(ShopProductNames product)
        {
            return IAPManager.Instance.GetSubscriptionInfo(product);
        }


        /// <summary>
        /// Converts a given name into enum member 
        /// </summary>
        /// <param name="name">String to convert</param>
        /// <returns>An ShopProductNames enum member</returns>
        public static ShopProductNames ConvertNameToShopProduct(string name)
        {
            return IAPManager.Instance.ConvertNameToShopProduct(name);
        }


        /// <summary>
        /// Returns the Product’s purchase date.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>Date of the purchase</returns>
        public static DateTime GetPurchaseDate(ShopProductNames product)
        {
            return IAPManager.Instance.GetPurchaseDate(product);
        }


        /// <summary>
        /// Checks is a product is currently subscribed or not.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product is currently subscribed or not.</returns>
        public static Result IsSubscribed(ShopProductNames product)
        {
            return IAPManager.Instance.IsSubscribed(product);
        }


        /// <summary>
        /// Checks if a product is expired or not
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product has expired or not.</returns>
        public static Result IsExpired(ShopProductNames product)
        {
            return IAPManager.Instance.IsExpired(product);
        }


        /// <summary>
        /// Checks if a product is canceled
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product has been canceled.</returns>
        public static Result IsCancelled(ShopProductNames product)
        {
            return IAPManager.Instance.IsCancelled(product);
        }


        /// <summary>
        /// Checks if this Product is a free trial.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product is a free trial.</returns>
        public static Result IsFreeTrial(ShopProductNames product)
        {
            return IAPManager.Instance.IsFreeTrial(product);
        }


        /// <summary>
        /// Indicates if the product is auto-renewable. 
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product is auto-renewable.</returns>
        public static Result IsAutoRenewing(ShopProductNames product)
        {
            return IAPManager.Instance.IsAutoRenewing(product);
        }


        /// <summary>
        /// Indicate how much time remains until the next billing date.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A TimeSpan to indicate how much time remains until the next billing date.</returns>
        public static TimeSpan GetRemainingTime(ShopProductNames product)
        {
            return IAPManager.Instance.GetRemainingTime(product);
        }


        /// <summary>
        /// Indicate whether this Product is within an introductory price period.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A Result enum to indicate whether this Product is within an introductory price period.</returns>
        public static Result IsIntroductoryPricePeriod(ShopProductNames product)
        {
            return IAPManager.Instance.IsIntroductoryPricePeriod(product);
        }


        /// <summary>
        /// Indicate how much time remains for the introductory price period.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A TimeSpan to indicate how much time remains for the introductory price period.</returns>
        public static TimeSpan GetIntroductoryPricePeriod(ShopProductNames product)
        {
            return IAPManager.Instance.GetIntroductoryPricePeriod(product);
        }


        /// <summary>
        /// Get the introductory price of the Product.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>A string to indicate the introductory price of the Product.</returns>
        public static string GetIntroductoryPrice(ShopProductNames product)
        {
            return IAPManager.Instance.GetIntroductoryPrice(product);
        }


        /// <summary>
        /// Returns the number of introductory price periods that can be applied to this Product.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns></returns>
        public static long GetIntroductoryPricePeriodCycles(ShopProductNames product)
        {
            return IAPManager.Instance.GetIntroductoryPricePeriodCycles(product);
        }


        /// <summary>
        /// Returns the date of the Product’s next auto-renew or expiration (for a canceled auto-renewing subscription).
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The expiration date</returns>
        public static DateTime GetExpireDate(ShopProductNames product)
        {
            return IAPManager.Instance.GetExpireDate(product);
        }


        /// <summary>
        /// Returns the date of the Product’s cancel date
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The cancel date</returns>
        public static DateTime GetCancelDate(ShopProductNames product)
        {
            return IAPManager.Instance.GetCancelDate(product);
        }


        /// <summary>
        /// Returns the free trial period.
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The free trial period</returns>
        public static TimeSpan GetFreeTrialPeriod(ShopProductNames product)
        {
            return IAPManager.Instance.GetFreeTrialPeriod(product);
        }


        /// <summary>
        /// Return the subscription period
        /// </summary>
        /// <param name="product">An enum member generated from Settings Window</param>
        /// <returns>The subscription period.</returns>
        public static TimeSpan GetSubscriptionPeriod(ShopProductNames product)
        {
            return IAPManager.Instance.GetSubscriptionPeriod(product);
        }
    }
}
