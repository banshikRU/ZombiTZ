using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

namespace UIControl.Buttons
{
    [RequireComponent(typeof(CodelessIAPButton))]
    public class IAPButtonView : MonoBehaviour
    {
        private Button _button;
        private CodelessIAPButton _codelessIAPButton;
        private IAPButtonViewModel _iapButtonViewModel;

        [Inject]
        public void Construct(IAPButtonViewModel iapButtonViewModel)
        {
            _iapButtonViewModel = iapButtonViewModel;
        }
        
        public void Awake()
        {
            _button = GetComponent<Button>();
            _codelessIAPButton = GetComponent<CodelessIAPButton>();
            AddButtonsListeners();
        }

        private void AddButtonsListeners()
        {
            _codelessIAPButton.onPurchaseComplete.AddListener(OnPurchaseComplete);
            _button.onClick.AddListener(BuyProduct);
        }

        private void OnPurchaseComplete(Product product)
        {
            _button.interactable = false;
        }

        private void BuyProduct()
        {
            _iapButtonViewModel.BuyProduct(_codelessIAPButton.productId);
        }
    }
}