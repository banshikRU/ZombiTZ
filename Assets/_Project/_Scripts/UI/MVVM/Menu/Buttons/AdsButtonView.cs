using System;
using Advertisements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIControl.MVVM.MainMenu.Buttons
{
    public class AdsButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _showAdButton;
        [SerializeField]
        private string _adsUnitId;
        [SerializeField]
        private int _rewardId;
        [SerializeField]
        private AdsType _adsType;
        
        private AdsButtonViewModel _menuViewModel;

        [Inject]
        public void Construct(AdsButtonViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        private void Awake()
        {
            OnAdsButtonOnOff(false);
            InitAd();
        }
        
        private void InitAd()
        {
            _menuViewModel.AdInit(_showAdButton, _adsUnitId, _rewardId);
            AddListenerToAdsButton();
        }

        private void OnAdsButtonOnOff(bool value)
        {
            _showAdButton.interactable = value;
        }

        private void AddListenerToAdsButton()
        {
            _showAdButton.onClick.AddListener(ShowAd);
        }

        private void ShowAd()
        {
            _menuViewModel.ShowAd(_adsType, _showAdButton, _adsUnitId);
            OnAdsButtonOnOff(false);
        }

        
    }
}