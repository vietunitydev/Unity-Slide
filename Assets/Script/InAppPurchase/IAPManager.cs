using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    public static IAPManager Instance { get; private set; }

    public string coin100 = "coins_100";
    public string coin200 = "coins_200";
    public string no_ads = "no_ads";
    public string premium_month = "premium_month";
    public ShopPanel shopPanel;
    public static bool IsInitialized { get; private set; } = false;

    private static StoreController storeController;

    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        await InitIAP();
    }

    private async Task InitIAP()
    {
        try
        {
            var option = new InitializationOptions().SetEnvironmentName("production");
            await UnityServices.InitializeAsync(option);

            storeController = UnityIAPServices.StoreController();

            // Register all event listeners
            storeController.OnStoreDisconnected += OnStoreDisconnected;
            storeController.OnProductsFetched += OnProductsFetched;
            storeController.OnProductsFetchFailed += OnProductsFetchFailed;
            storeController.OnPurchasesFetched += OnPurchasesFetched;
            storeController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
            storeController.OnPurchasePending += OnPurchasePending;
            storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;
            storeController.OnPurchaseFailed += OnPurchaseFailed;
            storeController.OnPurchaseDeferred += OnPurchaseDeferred;

            await storeController.Connect();

            var initialProducts = BuildProductDefinitions();
            storeController.FetchProducts(initialProducts);
        }
        catch (Exception e)
        {
            Debug.Log($"Initialization failed with: {e}");
        }
    }

    private List<ProductDefinition> BuildProductDefinitions()
    {
        var initialProductToFetch = new List<ProductDefinition>();

        initialProductToFetch.Add(new ProductDefinition(coin100, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(coin200, ProductType.Consumable));
        initialProductToFetch.Add(new ProductDefinition(no_ads, ProductType.NonConsumable));
        initialProductToFetch.Add(new ProductDefinition(premium_month, ProductType.Consumable));

        return initialProductToFetch;
    }

    private void OnProductsFetched(List<Product> products)
    {
        // Products are ready. Now Fetch Purchases
        storeController.FetchPurchases();

        foreach (var product in products)
        {
            string price = product.metadata.localizedPrice + " " + product.metadata.isoCurrencyCode;
            string id = product.definition.id;
            
            Debug.Log(id + " " + price);
        }
    }

    private void OnProductsFetchFailed(ProductFetchFailed reason)
    {
        Debug.Log($"Product fetch failed: {reason}");
    }

    private void OnPurchasesFetched(Orders orders)
    {
        IsInitialized = true;
    }

    private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription reason)
    {
        Debug.Log($"Purchases fetch failed: {reason}");
    }

    private void OnStoreDisconnected(StoreConnectionFailureDescription description)
    {
        Debug.Log($"Initialization/Connection failed: {description.message}");
    }

    public void BuyProduct(IAPProductKey productKey)
    {
        if (!IsInitialized)
        {
            Debug.Log("IAP Module is not initialized. Try again later.");
            return;
        }

        if (productKey == IAPProductKey.Coin100)
        {
            storeController.PurchaseProduct(coin100);
        }
        else if (productKey == IAPProductKey.Coin200)
        {
            storeController.PurchaseProduct(coin200);
        }
        else if (productKey == IAPProductKey.NoAds)
        {
            storeController.PurchaseProduct(no_ads);
        }
        else if (productKey == IAPProductKey.PremiumMonth)
        {
            storeController.PurchaseProduct(premium_month);
        }
    }

    private void OnPurchasePending(PendingOrder order)
    {
        Debug.Log($"Pending order: {order}");
        storeController.ConfirmPurchase(order);
    }

    private void OnPurchaseDeferred(DeferredOrder deferredOrder)
    {
        Debug.Log($"Purchase Deferred for product: {deferredOrder?.Info}");
        // Show UI: "Purchase pending approval" if needed
    }

    private void OnPurchaseConfirmed(Order order)
    {
        Debug.Log($"Purchase confirmed: {order}");

        // Reward Here
        if (order?.Info?.PurchasedProductInfo != null && order.Info.PurchasedProductInfo.Count > 0)
        {
            string productId = order.Info.PurchasedProductInfo[0].productId;

            if (productId == coin100)
            {
                Debug.Log($"Purchase: {productId}");
                shopPanel.UpdateReward(IAPProductKey.Coin100); // Reward for Coin Pack 1
            }
            else if (productId == coin200)
            {
                Debug.Log($"Purchase: {productId}");
                shopPanel.UpdateReward(IAPProductKey.Coin200); // Reward for Coin Pack 2
            }
            else if (productId == no_ads)
            {
                Debug.Log($"Purchase: {productId}");
                shopPanel.UpdateReward(IAPProductKey.NoAds); // Remove Ads Reward
            }
            else if (productId == premium_month)
            {
                Debug.Log($"Purchase: {productId}");
                shopPanel.UpdateReward(IAPProductKey.PremiumMonth);
            }
        }
    }

    private void OnPurchaseFailed(FailedOrder failedOrder)
    {
        if (failedOrder?.Info?.PurchasedProductInfo == null || failedOrder.Info.PurchasedProductInfo.Count == 0)
        {
            Debug.Log("Purchase failed but no product info available");
            return;
        }

        var productId = failedOrder.Info.PurchasedProductInfo[0].productId;
        var reason = failedOrder.FailureReason;
        var message = failedOrder.Details;

        Debug.Log($"Purchase failed. Product is {productId}. Reason is {reason}. Message: {message}");
    }
}

public enum IAPProductKey
{
    Coin100,
    Coin200,
    NoAds,
    PremiumMonth
}
