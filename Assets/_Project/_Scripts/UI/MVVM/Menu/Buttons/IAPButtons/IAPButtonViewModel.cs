using InAppPurchase;

namespace UIControl.Buttons.IAPButtons
{
    public class IAPButtonViewModel
    {
        private readonly InAppStore _inAppStore;

        public IAPButtonViewModel(InAppStore inAppStore)
        {
            _inAppStore = inAppStore;
        }
        
        public void BuyProduct(string productId)
        {
            _inAppStore.BuyProduct(productId);
        }

    }
}