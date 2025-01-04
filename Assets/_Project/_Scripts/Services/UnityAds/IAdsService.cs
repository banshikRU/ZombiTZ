using System;
using UnityEngine.UI;

public interface IAdsService
{
    public event Action<string> OnRewardAdsShowed;

    public void ShowAd(string adsId);
    public void LoadAd(string adsId);
}
