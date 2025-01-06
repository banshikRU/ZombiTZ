using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace InAppPurchase
{
    public class InAppStore : IDetailedStoreListener
    {
        public event Action<string> OnProductPurchase;

        public string environment = "production";

        private IStoreController _storeController;

        private InAppStore()
        {
            AppStoreInit();
        }

        public void BuyProduct(string product_ID)
        {
            Debug.Log(product_ID);
            _storeController.InitiatePurchase(product_ID);
            OnProductPurchase?.Invoke(product_ID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
          //  Debug.Log("InAppStoreInit");
            _storeController = controller;
        }

        private void AppStoreInit()
        {
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("com.DefaultCompany.NoAds", ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        { }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
        { }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            throw new NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        { }
    }
}
