using System;
using Advertisements;
using GameSystem;
using PlayerControl;
using SaveSystem;
using UniRx;
using Zenject;

namespace UIControl
{
    public class AllGameMenusViewModel :  IDisposable,IInitializable
    {
        private readonly GameStateUpdater _gameStateUpdater;
        private readonly PlayerBehaviour _player;
        private readonly AdsRewardGiver _adsRewardGiver;
        private readonly SaveGameController _saveGameController;
        
        public readonly ReactiveProperty<bool> IsMainMenuVisible = new ();
        public readonly ReactiveProperty<bool> IsDeathMenuVisible = new ();
        public readonly ReactiveProperty<bool> IsInGameStatsVisible = new ();
        public readonly ReactiveProperty<bool> IsSelectSaveMenuVisible = new ();

        public AllGameMenusViewModel(GameStateUpdater gameStateUpdater, PlayerBehaviour player, AdsRewardGiver adsRewardGiver, SaveGameController saveGameController)
        {
            _gameStateUpdater = gameStateUpdater;
            _player = player;
            _adsRewardGiver = adsRewardGiver;
            _saveGameController = saveGameController;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
            OnMainMenu();
            ControlSelectSaveMenu();
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }

        public void SaveSelected()
        {
            OffSelectSaveMenu();
        }
        
        public void StartGame()
        {
            _gameStateUpdater.StartGame();
        }

        public void RestartGame()
        {
            _gameStateUpdater.RestartGame();
        }

        private void UnsubscribeEvent()
        {
            _adsRewardGiver.OnGiveSecondChance -= OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed -= OffMainMenu;
            _gameStateUpdater.OnGameContinued -= OnInGameStats;
            _gameStateUpdater.OnGamePlayed -= OnInGameStats;
            _player.OnPlayerDeath -= OnEndGameMenu;
            _player.OnPlayerDeath -= OffInGameStats;
        }

        private void SubscribeEvents()
        {
            _adsRewardGiver.OnGiveSecondChance += OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed += OffMainMenu;
            _gameStateUpdater.OnGameContinued += OnInGameStats;
            _gameStateUpdater.OnGamePlayed += OnInGameStats;
            _player.OnPlayerDeath += OnEndGameMenu;
            _player.OnPlayerDeath += OffInGameStats;
        }

        private void OffMainMenu()
        {
            IsMainMenuVisible.Value = false;
        }

        private void OnMainMenu()
        {
            IsMainMenuVisible.Value = true;
        }

        private void OnEndGameMenu()
        {
            IsDeathMenuVisible.Value = true;
        }

        private void OffEndGameMenu()
        {
            IsDeathMenuVisible.Value = false;
        }

        private void OnInGameStats()
        {
            IsInGameStatsVisible.Value = true;
        }

        private void OffInGameStats()
        {
            IsInGameStatsVisible.Value = false;
        }

        private void OnSelectSaveMenu()
        {
            IsSelectSaveMenuVisible.Value = true;
        }

        private void OffSelectSaveMenu()
        {
            IsSelectSaveMenuVisible.Value = false;
        }

        private void ControlSelectSaveMenu()
        {
            if (_saveGameController.IsSaveSetUp.Value)
                return;
            TimeSpan difference = _saveGameController.LocalPlayerData.SaveTime - _saveGameController.CloudPlayerData.SaveTime;
            if(difference.TotalSeconds < 30)
                return;
            if (_saveGameController.CloudPlayerData != null && _saveGameController.LocalPlayerData != null)
                OnSelectSaveMenu();
            
        }
    }
}