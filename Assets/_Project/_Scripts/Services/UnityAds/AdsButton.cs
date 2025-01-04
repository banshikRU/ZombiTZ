using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AdsButton : MonoBehaviour
{
    [SerializeField] 
    private Button _showAdButton;
    [SerializeField]
    private string _adUnitId;
    [SerializeField]
    private int _rewardId;

    private AdsServiceManager _adsServiceManager;

    [Inject]
    public void Construct(AdsServiceManager adsServiceManager)
    {
        _showAdButton.interactable = false;
        _adsServiceManager = adsServiceManager;
        _adsServiceManager.AdInit(_showAdButton, _adUnitId,_rewardId);
    }

    public void ShowAd()
    {
        _showAdButton.interactable = false;
        _adsServiceManager.ShowAd(_adUnitId);
    }

}