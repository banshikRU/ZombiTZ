using GameStateControl;
using PlayerControl;
using Zenject;
using System;
using UnityEngine;
using DG.Tweening;
using Advertisements;
using Zenject.ReflectionBaking.Mono.CompilerServices.SymbolWriter;

namespace UIControl
{
    public class UIController: IDisposable,IInitializable
    {
        private readonly ScoresMenu _mainMenu;
        private readonly ScoresMenu _deathMenu;
        private readonly GameObject _inGameStats;
        private readonly GameStateUpdater _gameStateUpdater;
        private readonly PlayerBehaviour _player;
        private readonly AdsRewardGiver _adsRewardGiver;

        public UIController([Inject(Id = ZenjectIds.MainMenu)] ScoresMenu mainMenu, [Inject(Id = ZenjectIds.DeadMenu)] ScoresMenu deathMenu,GameStateUpdater gameStateUpdater, PlayerBehaviour player,AdsRewardGiver adsRewardGiver,GameObject inGameStats)
        {
            _inGameStats  = inGameStats;
            _adsRewardGiver = adsRewardGiver;
            _deathMenu = deathMenu;
            _mainMenu = mainMenu;
            _gameStateUpdater = gameStateUpdater;
            _player = player;
        }
        
        public void Initialize()
        {
            _mainMenu.gameObject.SetActive(true);
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
            _mainMenu.gameObject.SetActive(false);
        }

        private void OnEndGameMenu()
        {
            _deathMenu.gameObject.SetActive(true);
            _deathMenu.gameObject.transform.DOScale(new Vector3(1, 1, 1), 3);
        }

        private void OffEndGameMenu()
        {
            _deathMenu.gameObject.SetActive(false);
            _deathMenu.gameObject.transform.DOScale(new Vector3(0, 0, 0), 3);
        }

        private void OnInGameStats()
        {
            _inGameStats.gameObject.SetActive(true);
        }

        private void OffInGameStats()
        {
            _inGameStats.gameObject.SetActive(false);
        }
    }
}

