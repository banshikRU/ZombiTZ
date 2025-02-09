using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIControl
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _mainMenu;
        [SerializeField] 
        private GameObject _deathMenu;
        [SerializeField] 
        private GameObject _inGameStats;
        [SerializeField] 
        private Button _playButton;
        [SerializeField] 
        private Button _gameEndButton;
        [SerializeField]
        private Button _purchaseNoAdsButton;

        private UIViewModel _viewModel;

        [Inject]
        public void Construct(UIViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Awake()
        {
            SubscribeEvents();
            AddListenersForButtons();
            _viewModel.IsMainMenuVisible.Value = true;
        }
        
        private void OnDestroy()
        {
            _viewModel.IsDeathMenuVisible.Dispose();
            _viewModel.IsMainMenuVisible.Dispose();
            _viewModel.IsInGameStatsVisible.Dispose();
        }

        private void SubscribeEvents()
        {
            _viewModel.IsDeathMenuVisible.Skip(1).Subscribe(OnDeathMenuStateChanged);
            _viewModel.IsMainMenuVisible.Skip(1).Subscribe(OnMainMenuStateChanged);
            _viewModel.IsInGameStatsVisible.Skip(1).Subscribe(OnInGameStatsStateChanged);
        }
        
        private void OnMainMenuStateChanged(bool isMainMenuVisible)
        {
            _mainMenu.SetActive(isMainMenuVisible);
        }

        private void OnDeathMenuStateChanged(bool isDeathMenuVisible)
        {
            Debug.Log(isDeathMenuVisible);
            _deathMenu.SetActive(isDeathMenuVisible);
        }

        private void OnInGameStatsStateChanged(bool isInGameStatsVisible)
        {
            _inGameStats.SetActive(isInGameStatsVisible);
        }
        
        private void AddListenersForButtons()
        {
            _playButton.onClick.AddListener(_viewModel.StartGame);
            _gameEndButton.onClick.AddListener(_viewModel.RestartGame);
        }
    }
}