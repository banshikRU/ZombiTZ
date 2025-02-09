using InAppPurchase;
using SaveSystem;
using System;
using PlayerControl;
using UnityEngine;

namespace Services
{
    public class NoAdsController : IDisposable
    {
        private const string NO_ADS_ID = "com.DefaultCompany.NoAds";

        private readonly ISaveHandler<PlayerData> _saveGameController;
        private readonly InAppStore _inAppStore;

        public bool IsAdsPurchased { get; private set; }

        public NoAdsController(ISaveHandler<PlayerData> saveGameController, InAppStore inAppStore)
        {
            _inAppStore = inAppStore;
            _saveGameController = saveGameController;
            IsNoAdsPurchasedCheck();
            EventInit();

        }

        private void EventInit()
        {
            _inAppStore.OnProductPurchase += SetNoAdsPurchase;
        }

        public void Dispose()
        {
            _inAppStore.OnProductPurchase -= SetNoAdsPurchase;
        }

        private void IsNoAdsPurchasedCheck()
        {
            IsAdsPurchased = _saveGameController.LoadData().NoAdsPurchased;
        }

        private void SetNoAdsPurchase(string Id)
        {
            if (Id == NO_ADS_ID)
            {
                Debug.Log("set");
                _saveGameController.PlayerDataValues.NoAdsPurchased = true;
                _saveGameController.SaveData();
                IsNoAdsPurchasedCheck();
            }
        }
    }
}

