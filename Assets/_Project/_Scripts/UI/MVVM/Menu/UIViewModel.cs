using System;
using Advertisements;
using GameStateControl;
using PlayerControl;
using UniRx;
using UnityEngine;

namespace UIControl
{
    public class UIViewModel :  IDisposable
    {
        private readonly GameStateUpdater _gameStateUpdater;
        private readonly PlayerBehaviour _player;
        private readonly AdsRewardGiver _adsRewardGiver;
        
        public readonly ReactiveProperty<bool> IsMainMenuVisible = new ();
        public readonly ReactiveProperty<bool> IsDeathMenuVisible = new ();
        public readonly ReactiveProperty<bool> IsInGameStatsVisible = new ();

        public UIViewModel(GameStateUpdater gameStateUpdater, PlayerBehaviour player, AdsRewardGiver adsRewardGiver)
        {
            _gameStateUpdater = gameStateUpdater;
            _player = player;
            _adsRewardGiver = adsRewardGiver;

            EventInit();
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }

        private void UnsubscribeEvent()
        {
            _adsRewardGiver.OnGiveSecondChance -= OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed -= OffMainMenu;
            _gameStateUpdater.OnGamePlayed -= OnInGameStats;
            _player.OnPlayerDeath -= OnEndGameMenu;
            _player.OnPlayerDeath -= OffInGameStats;
        }

        private void EventInit()
        {
            _adsRewardGiver.OnGiveSecondChance += OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed += OffMainMenu;
            _gameStateUpdater.OnGamePlayed += OnInGameStats;
            _player.OnPlayerDeath += OnEndGameMenu;
            _player.OnPlayerDeath += OffInGameStats;
        }

        private void OffMainMenu()
        {
            IsMainMenuVisible.Value = false;
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

        public void StartGame()
        {
            _gameStateUpdater.StartGame();
        }

        public void RestartGame()
        {
            _gameStateUpdater.RestartGame();
        }
        
        
        
    }
}