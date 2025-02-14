using InAppPurchase;
using SaveSystem;
using System;
using PlayerControl;
using Zenject;

namespace Services
{
    public class NoAdsController : IDisposable,IInitializable
    {
        private const string NO_ADS_ID = "com.DefaultCompany.NoAds";

        private readonly ISaveHandler<PlayerData> _saveGameController;
        private readonly InAppStore _inAppStore;

        public bool IsAdsPurchased { get; private set; }

        public NoAdsController(ISaveHandler<PlayerData> saveGameController, InAppStore inAppStore)
        {
            _inAppStore = inAppStore;
            _saveGameController = saveGameController;
        }
        
        public void Initialize()
        {
            IsNoAdsPurchasedCheck();
            EventInit();
        }
        
        public void Dispose()
        {
            _inAppStore.OnProductPurchase -= SetNoAdsPurchase;
        }

        private void EventInit()
        {
            _inAppStore.OnProductPurchase += SetNoAdsPurchase;
        }

        private void IsNoAdsPurchasedCheck()
        {
            IsAdsPurchased = _saveGameController.LoadData().NoAdsPurchased;
        }

        private void SetNoAdsPurchase(string Id)
        {
            if (Id == NO_ADS_ID)
            {
                _saveGameController.PlayerDataValues.NoAdsPurchased = true;
                _saveGameController.SaveData();
                IsNoAdsPurchasedCheck();
            }
        }
    }
}

