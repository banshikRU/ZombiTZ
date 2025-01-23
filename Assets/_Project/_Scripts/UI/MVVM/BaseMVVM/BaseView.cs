using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UIControl.BaseMVVM
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _currencyValue;
        
        private BaseViewModel _currentViewModel;

        [Inject]
        public void Construct(BaseViewModel baseViewModel)
        {
            _currentViewModel = baseViewModel;
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            _currentViewModel.ReactiveValue.Subscribe(OnValueChanged);
        }

        private void OnEnable()
        {
            _currentViewModel.ReactiveValue.Dispose();
        }

        private void OnValueChanged(int value)
        {
            _currencyValue.text = value.ToString();
        }
    }
}