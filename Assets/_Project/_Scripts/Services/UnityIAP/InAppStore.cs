using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class InAppStore : IDetailedStoreListener
{
    private IStoreController m_StoreController;

    public string environment = "production";


    private void Start()
    {
        AppStoreInit();
    }
    private void AppStoreInit()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("NoAds", ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(string product_ID)
    {
        m_StoreController.InitiatePurchase(product_ID);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("InitIap");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    { Debug.Log("pizdec"); }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
    { }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("pizdec");
    }


}
