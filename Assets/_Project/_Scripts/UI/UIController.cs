using GameStateControl;
using PlayerControl;
using Zenject;
using System;
using UnityEngine;
using DG.Tweening;
using Advertisements;

namespace UIControl
{
    public class UIController: IDisposable
    {
        private readonly ScoresMenu _mainMenu;
        private readonly ScoresMenu _deathMenu;
        private readonly GameStateUpdater _gameStateUpdater;
        private readonly PlayerBehaviour _player;
        private readonly AdsRewardGiver _adsRewardGiver;

        public UIController([Inject(Id = ZenjectIds.MainMenu)] ScoresMenu mainMenu, [Inject(Id = ZenjectIds.DeadMenu)] ScoresMenu deathMenu,GameStateUpdater gameStateUpdater, PlayerBehaviour player,AdsRewardGiver adsRewardGiver)
        {
            _adsRewardGiver = adsRewardGiver;
            _deathMenu = deathMenu;
            _mainMenu = mainMenu;
            _gameStateUpdater = gameStateUpdater;
            _player = player;
            _mainMenu.gameObject.SetActive(true);
            EventInit();
        }

        public void Dispose()
        {
            UnsubcribeEvent();
        }

        public void UnsubcribeEvent()
        {
            _adsRewardGiver.OnGiveSecondChance -= OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed -= OffMainMenu;
            _player.OnPlayerDeath -= OnEndGameMenu;
        }

        private void EventInit()
        {
            _adsRewardGiver.OnGiveSecondChance += OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed += OffMainMenu;
            _player.OnPlayerDeath += OnEndGameMenu;
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


    }
}

