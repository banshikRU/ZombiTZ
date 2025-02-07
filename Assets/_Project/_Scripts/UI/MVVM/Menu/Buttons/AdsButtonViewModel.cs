using Advertisements;
using Services;
using UnityEngine.UI;
using Zenject;

namespace UIControl.MVVM.MainMenu
{
    public class AdsButtonViewModel
    {
        private readonly AdsServiceManager _adsServiceManager;
        private readonly NoAdsController _noAdsController;

        public AdsButtonViewModel(AdsServiceManager adsServiceManager, NoAdsController noAdsController)
        {
            _adsServiceManager = adsServiceManager;
            _noAdsController = noAdsController;
        }

        public void AdInit(Button button,string adsUnitId,int rewardId )
        {
            _adsServiceManager.AdInit(button, adsUnitId, rewardId);
        }
        
        public void ShowAd(AdsType adsType,Button button,string adsUnitId)
        {
            if (_noAdsController.IsAdsPurchased && adsType == AdsType.Interstitial)
                return;
            button.interactable = false;
            _adsServiceManager.ShowAd(adsUnitId);
        }
    }
}