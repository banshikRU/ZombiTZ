using InAppPurchase;
using System;
using SaveSystem;
using UniRx;
using UnityEngine;

namespace Services
{
    public class NoAdsController : IDisposable
    {
        private const string NO_ADS_ID = "com.DefaultCompany.NoAds";

        private readonly SaveGameController _saveGameController;
        private readonly InAppStore _inAppStore;
        
        public bool IsAdsPurchased { get; private set; }

        public NoAdsController(SaveGameController saveGameController, InAppStore inAppStore )
        {
            _inAppStore = inAppStore;
            _saveGameController = saveGameController;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            _saveGameController.IsSaveSetUp.Dispose();
            _inAppStore.OnProductPurchase -= SetNoAdsPurchase;
        }

        private void SubscribeEvents()
        {
            _saveGameController.IsSaveSetUp.Subscribe(IsNoAdsPurchasedCheck);
            _inAppStore.OnProductPurchase += SetNoAdsPurchase;
        }

        private void IsNoAdsPurchasedCheck(bool value)
        {
            IsAdsPurchased = _saveGameController.PlayerDataValues.NoAdsPurchased;
            Debug.Log(IsAdsPurchased);
        }

        private void SetNoAdsPurchase(string Id)
        {
            if (Id == NO_ADS_ID)
            {
                _saveGameController.PlayerDataValues.NoAdsPurchased = true;
                _saveGameController.SaveData();
                IsNoAdsPurchasedCheck(true);
            }
        }
    }
}

