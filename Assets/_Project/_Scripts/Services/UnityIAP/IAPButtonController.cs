using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

namespace InAppPurchase
{ 
    public class IAPButtonController : MonoBehaviour
    {
        private Button _button;
        private CodelessIAPButton _codelessIAPButton;
        private InAppStore _inAppStore;

        [Inject]
        public void Construct(InAppStore inAppStore)
        {
            _inAppStore = inAppStore;

            Initialize();

            _button.onClick.AddListener(BuyProduct);
            _codelessIAPButton.onPurchaseComplete.AddListener(OnPurchaseComplete);
        }

        public void Initialize()
        {
            _button = GetComponent<Button>();
            _codelessIAPButton = GetComponent<CodelessIAPButton>();
        }

        public void BuyProduct()
        {
            _inAppStore.BuyProduct(_codelessIAPButton.productId);
        }

        public void OnPurchaseComplete(Product product)
        {
            Debug.Log(product + "Purchased");
        }

    }
}

