#if GLEY_IAP_IOS || GLEY_IAP_GOOGLEPLAY || GLEY_IAP_AMAZON || GLEY_IAP_MACOS || GLEY_IAP_WINDOWS
#define GleyIAPEnabled
#endif

using UnityEngine.Events;
using System.Collections.Generic;
using System;

#if GleyIAPEnabled
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using System.Linq;
using System.Collections;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.Purchasing.Extension;
#endif

namespace Gley.EasyIAP.Internal
{
#if GleyIAPEnabled
    public class IAPManager : MonoBehaviour, IDetailedStoreListener
    {
        internal bool debug;

        const string environment = "production";

        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;
        private IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
        private IAppleExtensions m_AppleExtensions;
        private List<StoreProduct> shopProducts;
        private ConfigurationBuilder builder;
        private UnityAction<IAPOperationStatus, string, List<StoreProduct>> onInitComplete;
        private UnityAction<IAPOperationStatus, string> OnInitComplete;
        private UnityAction<IAPOperationStatus, string, StoreProduct> OnCompleteMethod;
        private UnityAction RestoreDone;

        /// <summary>
        /// Static instance of the class for easy access
        /// </summary>
        private static IAPManager instance;
        private bool initializePurchasing;

        public static IAPManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("GleyIAPManager");
                    instance = go.AddComponent<IAPManager>();
                    DontDestroyOnLoad(go);
                }
                return instance;
            }
        }


        void Update()
        {
            if (initializePurchasing == true)
            {
                InitializePurchasing();
                initializePurchasing = false;
            }
        }

        #region Initialize
        /// <summary>
        /// Checks if shop is initialized 
        /// </summary>
        /// <returns>true if shop was already initialized</returns>
        public bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void Initialize(UnityAction<IAPOperationStatus, string> initComplete)
        {
            OnInitComplete = initComplete;
            Initialize((UnityAction<IAPOperationStatus, string, List<StoreProduct>>)null);
        }


        public void Initialize(UnityAction<IAPOperationStatus, string, List<StoreProduct>> initComplete)
        {
            EasyIAPData settings = Resources.Load<EasyIAPData>(Constants.DATA_NAME_RUNTIME);
            if (settings == null)
            {
                Debug.LogError("No products available -> Go to Tools->Gley->Easy IAP and define your products");
                return;
            }
            if (!IsInitialized())
            {
                shopProducts = settings.shopProducts;
            }
            debug = settings.debug;
            onInitComplete = initComplete;
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                if (debug)
                {
                    Debug.Log("Unity Gaming Services Initialization Started");
                    ScreenWriter.Write("Unity Gaming Services Initialization Started");
                }
               
                Initialize(OnSuccess, OnError);
            }
            else
            {
                InitializePurchasing();
            }
        }

        void Initialize(Action onSuccess, Action<string> onError)
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(environment);
                UnityServices.InitializeAsync(options).ContinueWith(task => onSuccess());
            }
            catch (Exception exception)
            {
                onError(exception.Message);
            }
        }

        void OnSuccess()
        {
            initializePurchasing = true;
            if (debug)
            {
                Debug.Log("Unity Gaming Services has been successfully initialized.");
                ScreenWriter.Write("Unity Gaming Services has been successfully initialized.");
            }
        }

        void OnError(string message)
        {
            string text = $"Unity Gaming Services failed to initialize with error: {message}.";
            if (debug)
            {
                Debug.Log(text);
                ScreenWriter.Write(text);
            }
            onInitComplete?.Invoke(IAPOperationStatus.Fail, text, null);
            OnInitComplete?.Invoke(IAPOperationStatus.Fail, text);
        }

        /// <summary>
        /// Initialize store products, Call this method once at the beginning of your game
        /// </summary>
        /// <param name="InitComplete">callback method, returns a list of all store products, use this method for initializations</param>

        public void InitializePurchasing()
        {
            if (IsInitialized())
                return;

            if (debug)
            {
                Debug.Log("Unity IAP Initialization Started");
                ScreenWriter.Write("Unity IAP Initialization Started");
            }

            if (m_StoreController == null)
            {
                builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

                for (int i = 0; i < shopProducts.Count; i++)
                {
                    builder.AddProduct(shopProducts[i].GetStoreID(), shopProducts[i].GetProductType());
                }

                UnityPurchasing.Initialize(this, builder);
            }
        }

        /// <summary>
        /// IStoreListener event handler called after initialization is done
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="extensions"></param>
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            if (debug)
            {
                Debug.Log("Unity IAP has been successfully initialized.");
                ScreenWriter.Write("Unity IAP has been successfully initialized.");
            }
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
            m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

            StartCoroutine(InitializeProducts());
        }

        /// <summary>
        /// Update local products with the store ones
        /// </summary>
        /// <returns></returns>
        IEnumerator InitializeProducts()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < shopProducts.Count; i++)
            {
                Product product = m_StoreController.products.WithID(shopProducts[i].GetStoreID());

                if (debug)
                {
                    Debug.Log($"{product.metadata.localizedTitle} is available {product.availableToPurchase}");
                    ScreenWriter.Write($"{product.metadata.localizedTitle} is available {product.availableToPurchase}");
                }

                if (shopProducts[i].productType == ProductType.Subscription)
                {
                    if (product != null && product.hasReceipt)
                    {
                        IAPSecurityException exception;
                        if (ReceiptIsValid(shopProducts[i].productName, product.receipt, out exception))
                        {
                            SubscriptionManager p = new SubscriptionManager(product, null);
                            shopProducts[i].subscriptionInfo = p.getSubscriptionInfo();
                            shopProducts[i].active = shopProducts[i].subscriptionInfo.isSubscribed() == Result.True;
                            shopProducts[i].receipt = product.receipt;
                        }
                    }
                }

                if (shopProducts[i].productType == ProductType.NonConsumable)
                {
                    if (product != null && product.hasReceipt)
                    {
                        IAPSecurityException exception;
                        if (ReceiptIsValid(shopProducts[i].productName, product.receipt, out exception))
                        {
                            shopProducts[i].active = true;
                            shopProducts[i].receipt = product.receipt;
                        }
                    }
                }

                if (product != null && product.availableToPurchase)
                {
                    shopProducts[i].localizedPriceString = product.metadata.localizedPriceString;
                    shopProducts[i].price = System.Decimal.ToDouble(product.metadata.localizedPrice);
                    shopProducts[i].isoCurrencyCode = product.metadata.isoCurrencyCode;
                    shopProducts[i].localizedDescription = product.metadata.localizedDescription;
                    shopProducts[i].localizedTitle = product.metadata.localizedTitle;
                }
            }
            onInitComplete?.Invoke(IAPOperationStatus.Success, "Success", shopProducts);
            OnInitComplete?.Invoke(IAPOperationStatus.Success, "Success");
        }

        /// <summary>
        /// IStoreListener event handler called when initialization fails 
        /// </summary>
        /// <param name="error"></param>
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        /// <summary>
        /// IStoreListener event handler called when initialization fails 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            string text = $"OnInitializedFailed, error: {error} + Message: {message}";
            if (debug)
            {
                Debug.Log(text);
                ScreenWriter.Write(text);
            }
            onInitComplete?.Invoke(IAPOperationStatus.Fail, text, null);
            OnInitComplete?.Invoke(IAPOperationStatus.Fail, text);
        }
        #endregion


        #region BuyProduct
        /// <summary>
        /// Call this method to buy a product
        /// </summary>
        /// <param name="productName">An enum member generated from Settings Window</param>
        /// <param name="OnCompleteMethod">callback method that returns the bought product details for initializations</param>
        public void BuyProduct(ShopProductNames productName, UnityAction<IAPOperationStatus, string, StoreProduct> OnCompleteMethod)
        {
            if (debug)
            {
                Debug.Log(this + "Buy Process Started for " + productName);
                ScreenWriter.Write(this + "Buy Process Started for " + productName);
            }

            this.OnCompleteMethod = OnCompleteMethod;

            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (shopProducts[i].productName == productName.ToString())
                {
                    BuyProductID(shopProducts[i].GetStoreID());
                }
            }
        }

        /// <summary>
        /// initializes the buy product process
        /// </summary>
        /// <param name="productId"></param>
        private void BuyProductID(string productId)
        {
            if (debug)
            {
                Debug.Log(this + "Buy product with id: " + productId);
                ScreenWriter.Write(this + "Buy product with id: " + productId);
            }

            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);
                if (product != null && product.availableToPurchase)
                {
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log(this + "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                        ScreenWriter.Write(this + "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }

                    if (OnCompleteMethod != null)
                    {
                        OnCompleteMethod(IAPOperationStatus.Fail, "Not purchasing product, either is not found or is not available for purchase", null);
                    }
                }
            }
            else
            {
                if (debug)
                {
                    Debug.Log(this + "BuyProductID FAIL. Store not initialized.");
                    ScreenWriter.Write(this + "BuyProductID FAIL. Store not initialized.");
                }

                if (OnCompleteMethod != null)
                {
                    OnCompleteMethod(IAPOperationStatus.Fail, "Store not initialized.", null);
                }
            }
        }

        /// <summary>
        /// IStoreListener event handler called when a purchase is done
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            if (debug)
            {
                Debug.Log(this + "Product bought " + e.purchasedProduct.definition.id);
                ScreenWriter.Write(this + "Product bought " + e.purchasedProduct.definition.id);
            }

            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (String.Equals(e.purchasedProduct.definition.id, shopProducts[i].GetStoreID(), StringComparison.Ordinal))
                {
                    IAPSecurityException exception;
                    bool validPurchase = ReceiptIsValid(shopProducts[i].productName, e.purchasedProduct.receipt, out exception);
                    if (validPurchase)
                    {
                        shopProducts[i].receipt = e.purchasedProduct.receipt;
                        if (shopProducts[i].productType == ProductType.NonConsumable)
                        {
                            shopProducts[i].active = true;
                        }

                        if (shopProducts[i].productType == ProductType.Subscription)
                        {
                            SubscriptionManager p = new SubscriptionManager(e.purchasedProduct, null);
                            try
                            {
                                shopProducts[i].subscriptionInfo = p.getSubscriptionInfo();
                                shopProducts[i].active = shopProducts[i].subscriptionInfo.isSubscribed() == Result.True;
                            }
                            catch
                            {
                                shopProducts[i].active = true;
                            }
                        }

                        if (OnCompleteMethod != null)
                        {
                            OnCompleteMethod(IAPOperationStatus.Success, "Purchase Successful", shopProducts[i]);
                        }
                    }
                    else
                    {
                        if (OnCompleteMethod != null)
                        {
                            OnCompleteMethod(IAPOperationStatus.Fail, "Invalid Receipt " + exception.Message + exception.Data, null);
                        }
                    }
                    break;
                }
            }
            return PurchaseProcessingResult.Complete;
        }

        /// <summary>
        /// IStoreListener event  handler called when a purchase fails
        /// </summary>
        /// <param name="product"></param>
        /// <param name="reason"></param>
        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            if (debug)
            {
                Debug.Log("Buy Product failed for " + product.metadata.localizedTitle + " Failed. Reason: " + reason);
                ScreenWriter.Write("Buy Product failed for " + product.metadata.localizedTitle + " Failed. Reason: " + reason);
            }
        }

        /// <summary>
        /// IStoreListener event  handler called when a purchase fails
        /// </summary>
        /// <param name="product"></param>
        /// <param name="failureDescription"></param>
        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            string text = $"Product purchase failed for {product.definition.id} reason: {failureDescription.reason}, details: {failureDescription.message}";

            if (OnCompleteMethod != null)
            {
                OnCompleteMethod(IAPOperationStatus.Fail, text, null);
            }
        }
        #endregion


        #region ReceiptValidation
        /// <summary>
        /// Receipt validation method
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="receipt"></param>
        /// <param name="exception"></param>
        /// <returns>true if receipt is valid</returns>
        bool ReceiptIsValid(string productName, string receipt, out IAPSecurityException exception)
        {
            exception = null;
            bool validPurchase = true;
#if GLEY_IAP_VALIDATION
            if (IsCurrentStoreSupportedByValidator())
            {
#if !UNITY_EDITOR
                CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

                try
                {
                    var result = validator.Validate(receipt);
                    if (debug)
                    {
                        Debug.Log(this + " Receipt is valid for " + productName);
                        ScreenWriter.Write(this + " Receipt is valid for " + productName);
                        LogReceipts(result);
                    }
                }
                catch (IAPSecurityException ex)
                {
                    exception = ex;
                    if (debug)
                    {
                        Debug.Log($"Receipt is NOT valid for {productName}, reason {ex}");
                        ScreenWriter.Write($"Receipt is NOT valid for {productName}, reason {ex}");
                    }
                    validPurchase = false;
                }
#endif
            }
            else
            {
                var warningMsg = $"The cross-platform validator is not implemented for the currently selected store: {StandardPurchasingModule.Instance().appStore}. \n" +
                                             "Build the project for Android, iOS, macOS, or tvOS and use the Google Play Store or Apple App Store. See README for more information.";
                Debug.LogWarning(warningMsg);
            }
#endif
            return validPurchase;
        }
#if GLEY_IAP_VALIDATION
        void LogReceipts(IEnumerable<IPurchaseReceipt> receipts)
        {
            Debug.Log("Receipt is valid. Contents:");
            foreach (var receipt in receipts)
            {
                LogReceipt(receipt);
            }
        }

        void LogReceipt(IPurchaseReceipt receipt)
        {
            Debug.Log($"Product ID: {receipt.productID}\n" +
                $"Purchase Date: {receipt.purchaseDate}\n" +
                $"Transaction ID: {receipt.transactionID}");

            if (receipt is GooglePlayReceipt googleReceipt)
            {
                Debug.Log($"Purchase State: {googleReceipt.purchaseState}\n" +
                    $"Purchase Token: {googleReceipt.purchaseToken}");
            }

            if (receipt is AppleInAppPurchaseReceipt appleReceipt)
            {
                Debug.Log($"Original Transaction ID: {appleReceipt.originalTransactionIdentifier}\n" +
                    $"Subscription Expiration Date: {appleReceipt.subscriptionExpirationDate}\n" +
                    $"Cancellation Date: {appleReceipt.cancellationDate}\n" +
                    $"Quantity: {appleReceipt.quantity}");
            }
        }

        bool IsCurrentStoreSupportedByValidator()
        {
            //The CrossPlatform validator only supports the GooglePlayStore and Apple's App Stores.
            return IsGooglePlayStoreSelected() || IsAppleAppStoreSelected();
        }

        bool IsGooglePlayStoreSelected()
        {
            var currentAppStore = StandardPurchasingModule.Instance().appStore;
            return currentAppStore == AppStore.GooglePlay;
        }
        static bool IsAppleAppStoreSelected()
        {
            var currentAppStore = StandardPurchasingModule.Instance().appStore;
            return currentAppStore == AppStore.AppleAppStore ||
                currentAppStore == AppStore.MacAppStore;
        }
#endif
        #endregion


        #region RestorePurchases
        /// <summary>
        /// Restore previously bought products (Only required on iOS)
        /// </summary>
        /// <param name="OnCompleteMethod">called when a product needs to be restored/param>
        /// <param name="RestoreDone">called when restore process is done/param>
        public void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> OnCompleteMethod, UnityAction RestoreDone)
        {
            this.RestoreDone = RestoreDone;
            RestorePurchases(OnCompleteMethod);
        }


        /// <summary>
        /// Restore previously bought products (Only required on iOS)
        /// </summary>
        /// <param name="OnCompleteMethod">called when the restore process is done</param>
        public void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> OnCompleteMethod)
        {
            if (!IsInitialized())
            {
                if (debug)
                {
                    Debug.Log(this + "RestorePurchases FAIL. Not initialized.");
                    ScreenWriter.Write(this + "RestorePurchases FAIL. Not initialized.");
                }
                if (RestoreDone != null)
                {
                    RestoreDone();
                }
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                if (debug)
                {
                    Debug.Log(this + "RestorePurchases started ...");
                    ScreenWriter.Write(this + "RestorePurchases started ...");
                }
                this.OnCompleteMethod = OnCompleteMethod;
                m_AppleExtensions.RestoreTransactions(OnRestore);
            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    if (debug)
                    {
                        Debug.Log(this + "RestorePurchases started ...");
                        ScreenWriter.Write(this + "RestorePurchases started ...");
                    }
                    this.OnCompleteMethod = OnCompleteMethod;
                    m_GooglePlayStoreExtensions.RestoreTransactions(OnRestore);
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log(this + "RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                        ScreenWriter.Write(this + "RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                    }
                    if (RestoreDone != null)
                    {
                        RestoreDone();
                    }
                    if (OnCompleteMethod != null)
                    {
                        OnCompleteMethod(IAPOperationStatus.Fail, "Not supported on this platform.Current = " + Application.platform, null);
                    }
                }
            }
        }

        void OnRestore(bool success, string error)
        {
            if (success)
            {
                if (debug)
                {
                    Debug.Log("Restore Successful");
                    ScreenWriter.Write("Restore Successful");
                }
            }
            else
            {
                if (debug)
                {
                    Debug.Log("Restoration failed " + error);
                    ScreenWriter.Write("Restoration failed " + error);
                }
            }

            if (RestoreDone != null)
            {
                RestoreDone();
            }
        }
        #endregion



        /// <summary>
        /// Returns the type of the given product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Consumable/NonConsumable/Subscription</returns>
        public ProductType GetProductType(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).productType;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return 0;
            }
        }


        public string GetReceipt(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).receipt;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return "";
            }
        }


        /// <summary>
        /// Get product reward
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns>the amount of in game currency received</returns>
        public int GetValue(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).value;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return 0;
            }
        }


        /// <summary>
        /// Get the price and currency code of the product as a string
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns>the localized price and currency of the product</returns>
        public string GetLocalizedPriceString(ShopProductNames product)
        {
            if (IsInitialized())
            {
                if (shopProducts != null)
                {
                    return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).localizedPriceString;
                }
                else
                {
                    Debug.LogError("No products available -> Go to Window->Gley->Easy IAP and define your products");
                }
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
            }
            return "-";
        }


        /// <summary>
        /// Get decimal product price denominated in the local currency
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns></returns>
        public double GetPrice(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).price;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return 0;
            }
        }


        /// <summary>
        /// Get product currency in ISO 4217 format; e.g. GBP or USD.
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns></returns>
        public string GetIsoCurrencyCode(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).isoCurrencyCode;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return "-";
            }
        }


        /// <summary>
        /// Get description from the store
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns></returns>
        public string GetLocalizedDescription(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).localizedDescription;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return "-";
            }
        }


        /// <summary>
        /// Get title from the store
        /// </summary>
        /// <param name="product">store product</param>
        /// <returns></returns>
        public string GetLocalizedTitle(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).localizedTitle;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return "-";
            }
        }


        /// <summary>
        /// Gets the status of the product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true if the product was already bought</returns>
        public bool IsActive(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).active;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return false;
            }
        }

        /// <summary>
        /// Converts a given name into enum member 
        /// </summary>
        /// <param name="name">string to convert</param>
        /// <returns>an enum member, or the first one if not found</returns>
        public ShopProductNames ConvertNameToShopProduct(string name)
        {
            try
            {
                return (ShopProductNames)Enum.Parse(typeof(ShopProductNames), name);
            }
            catch
            {
                return 0;
            }
        }


        #region Subscription
        /// <summary>
        /// Get additional info for subscription
        /// </summary>
        /// <param name="product">the subscription product</param>
        /// <returns>all infos available for the subscription</returns>
        public SubscriptionInfo GetSubscriptionInfo(ShopProductNames product)
        {
            if (IsInitialized())
            {
                return shopProducts.First(cond => String.Equals(cond.productName, product.ToString())).subscriptionInfo;
            }
            else
            {
                Debug.LogError("Not Initialized -> Call Gley.EasyIAP.API.Initialize() before anything else");
                return null;
            }
        }

        public DateTime GetPurchaseDate(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getPurchaseDate();
            }
            return default;
        }

        public Result IsSubscribed(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isSubscribed();
            }
            return default;
        }

        public Result IsExpired(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isExpired();
            }
            return default;
        }

        public Result IsCancelled(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isCancelled();
            }
            return default;
        }

        public Result IsFreeTrial(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isFreeTrial();
            }
            return default;
        }

        public Result IsAutoRenewing(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isAutoRenewing();
            }
            return default;
        }

        public TimeSpan GetRemainingTime(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getRemainingTime();
            }
            return default;
        }

        public Result IsIntroductoryPricePeriod(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.isIntroductoryPricePeriod();
            }
            return default;
        }

        public TimeSpan GetIntroductoryPricePeriod(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getIntroductoryPricePeriod();
            }
            return default;
        }

        public string GetIntroductoryPrice(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getIntroductoryPrice();
            }
            return default;
        }

        public long GetIntroductoryPricePeriodCycles(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getIntroductoryPricePeriodCycles();
            }
            return default;
        }

        public DateTime GetExpireDate(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getExpireDate();
            }
            return default;
        }

        public DateTime GetCancelDate(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getCancelDate();
            }
            return default;
        }

        public TimeSpan GetFreeTrialPeriod(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getFreeTrialPeriod();
            }
            return default;
        }

        public TimeSpan GetSubscriptionPeriod(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getSubscriptionPeriod();
            }
            return default;
        }

        public string GetFreeTrialPeriodString(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getFreeTrialPeriodString();
            }
            return default;
        }

        public string GetSkuDetails(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getSkuDetails();
            }
            return default;
        }

        public string GetSubscriptionInfoJsonString(ShopProductNames product)
        {
            SubscriptionInfo info = GetSubscriptionInfo(product);
            if (info != null)
            {
                return info.getSubscriptionInfoJsonString();
            }
            return default;
        }
        #endregion
    }
#else

    public class IAPManager
    {
        internal bool debug;
        private static IAPManager instance;
        public static IAPManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IAPManager();
                }
                return instance;
            }
        }

        internal bool IsInitialized()
        {
            return false;
        }

        public void BuyProduct(ShopProductNames productName, UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod)
        {
            completeMethod?.Invoke(IAPOperationStatus.Fail, "Not Implemented", null);
        }

        public void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod)
        {
            completeMethod?.Invoke(IAPOperationStatus.Fail, "Not Implemented", null);
        }

        public void RestorePurchases(UnityAction<IAPOperationStatus, string, StoreProduct> completeMethod, UnityAction restoreDone)
        {
            completeMethod?.Invoke(IAPOperationStatus.Fail, "Not Implemented", null);
        }

        public int GetValue(ShopProductNames product)
        {
            return 0;
        }

        public string GetLocalizedPriceString(ShopProductNames product)
        {
            return "";
        }

        public ShopProductNames ConvertNameToShopProduct(string name)
        {
            return 0;
        }

        public bool IsActive(ShopProductNames product)
        {
            return false;
        }

        internal string GetLocalizedDescription(ShopProductNames productToCheck)
        {
            return "";
        }

        internal string GetLocalizedTitle(ShopProductNames productToCheck)
        {
            return "";
        }

        internal string GetIsoCurrencyCode(ShopProductNames productToCheck)
        {
            return "";
        }

        public SubscriptionInfo GetSubscriptionInfo(ShopProductNames productToCheck)
        {
            return null;
        }

        internal void Initialize(UnityAction<IAPOperationStatus, string, List<StoreProduct>> completeMethod)
        {
            completeMethod?.Invoke(IAPOperationStatus.Fail, "Not Implemented", null);
        }

        internal void Initialize(UnityAction<IAPOperationStatus, string> completeMethod)
        {
            completeMethod?.Invoke(IAPOperationStatus.Fail, "Not Implemented");
        }

        internal ProductType GetProductType(ShopProductNames product)
        {
            return default;
        }

        internal string GetReceipt(ShopProductNames product)
        {
            return default;
        }

        internal int GetPrice(ShopProductNames product)
        {
            return default;
        }

        internal DateTime GetPurchaseDate(ShopProductNames product)
        {
            return default;
        }

        internal Result IsSubscribed(ShopProductNames product)
        {
            return default;
        }

        internal Result IsExpired(ShopProductNames product)
        {
            return default;
        }

        internal Result IsCancelled(ShopProductNames product)
        {
            return default;
        }

        internal Result IsFreeTrial(ShopProductNames product)
        {
            return default;
        }

        internal Result IsAutoRenewing(ShopProductNames product)
        {
            return default;
        }

        internal TimeSpan GetRemainingTime(ShopProductNames product)
        {
            return default;
        }

        internal Result IsIntroductoryPricePeriod(ShopProductNames product)
        {
            return default;
        }

        internal TimeSpan GetIntroductoryPricePeriod(ShopProductNames product)
        {
            return default;
        }

        internal string GetIntroductoryPrice(ShopProductNames product)
        {
            return default;
        }

        internal long GetIntroductoryPricePeriodCycles(ShopProductNames product)
        {
            return default;
        }

        internal DateTime GetExpireDate(ShopProductNames product)
        {
            return default;
        }

        internal DateTime GetCancelDate(ShopProductNames product)
        {
            return default;
        }

        internal TimeSpan GetFreeTrialPeriod(ShopProductNames product)
        {
            return default;
        }

        internal TimeSpan GetSubscriptionPeriod(ShopProductNames product)
        {
            return default;
        }
    }
#endif
}