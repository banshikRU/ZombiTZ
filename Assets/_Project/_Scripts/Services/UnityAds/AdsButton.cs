using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Services;

namespace Advertisements
{
    public class AdsButton : MonoBehaviour
    {
        [SerializeField]
        private Button _showAdButton;
        [SerializeField]
        private string _adsUnitId;
        [SerializeField]
        private int _rewardId;
        [SerializeField]
        private AdsType _adsType;

        private AdsServiceManager _adsServiceManager;
        private NoAdsController _noAdsController;

        [Inject]
        public void Construct(AdsServiceManager adsServiceManager, NoAdsController noAdsController)
        {
            _noAdsController = noAdsController;
            _showAdButton.interactable = false;
            _adsServiceManager = adsServiceManager;
            _adsServiceManager.AdInit(_showAdButton, _adsUnitId, _rewardId);
        }

        public void ShowAd()
        {
            if (_noAdsController.IsAdsPurchased && _adsType == AdsType.Interstitial)
                return;
            _showAdButton.interactable = false;
            _adsServiceManager.ShowAd(_adsUnitId);
        }

    }
}
