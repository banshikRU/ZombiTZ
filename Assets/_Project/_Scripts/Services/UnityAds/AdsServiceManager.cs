using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Advertisements
{
    public class AdsServiceManager: IDisposable
    {
        private readonly IAdsService _adsService;
        private readonly Dictionary<string, int> _rewardIdDictionary = new ();
        private readonly AdsRewardGiver _rewardGiver;
        
        private Button _button;

        public AdsServiceManager(UnityAdsService adsController, AdsRewardGiver adsRewardGiver)
        {
            _rewardGiver = adsRewardGiver;
            _adsService = adsController;
        }

        public void ShowAd(string adsId)
        {
            _adsService.ShowAd(adsId);
        }

        public void AdInit(Button button, string adId, int rewardId = -1)
        {
            _button = button;
            LoadAd(adId);
            AddNewRewardId(adId, rewardId);
            SubscribeEvents();
            ActivateButton();
        }
        
        public void Dispose()
        {
            _adsService.OnRewardAdsShowed -= GiveReward;
        }

        private void SubscribeEvents()
        {
            _adsService.OnRewardAdsShowed += GiveReward;
        }
        
        private void LoadAd(string adId)
        {
            _adsService.LoadAd(adId);
        }

        private void ActivateButton()
        {
            _button.interactable = true;
        }

        private void GiveReward(string adsId)
        {
            _rewardGiver.GiveReward(GiveRewardId(adsId));
        }

        private void AddNewRewardId(string adsId, int rewardId)
        {
            if (!_rewardIdDictionary.TryAdd(adsId, rewardId))
                return;
        }

        private int GiveRewardId(string adsId)
        {
            return _rewardIdDictionary[adsId];
        }
    }
}
