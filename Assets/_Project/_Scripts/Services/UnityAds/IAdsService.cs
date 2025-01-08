using System;

namespace Advertisements
{
    public interface IAdsService
    {
        public event Action<string> OnRewardAdsShowed;

        public void ShowAd(string adsId);
        public void LoadAd(string adsId);
    }
}

