using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsService :IUnityAdsLoadListener, IUnityAdsShowListener,IAdsService
{
    public event Action<string> OnRewardAdsShowed;

    public void ShowAd(string adUnitId)
    {
        Advertisement.Show(adUnitId, this);
    }

    public void LoadAd(string adUnitId)
    {
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            OnRewardAdsShowed.Invoke(adUnitId);
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

}
