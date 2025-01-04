using GameStateControl;
using PlayerControl;
using Zenject;
using System;
using UnityEngine;

namespace UIControl
{
    public class UIController: IDisposable
    {
        private readonly ScoresMenu _mainMenu;
        private readonly ScoresMenu _deathMenu;
        private readonly GameStateUpdater _gameStateUpdater;
        private readonly PlayerBehaviour _player;
        private readonly AdsRewardGiver _dsRewardGiver;

        public UIController([Inject(Id = ZenjectIds.MainMenu)] ScoresMenu mainMenu, [Inject(Id = ZenjectIds.DeadMenu)] ScoresMenu deathMenu,GameStateUpdater gameStateUpdater, PlayerBehaviour player,AdsRewardGiver adsRewardGiver)
        {
            _dsRewardGiver = adsRewardGiver;
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
            _dsRewardGiver.OnGiveSecondChance -= OffEndGameMenu;
            _gameStateUpdater.OnGamePlayed -= OffMainMenu;
            _player.OnPlayerDeath -= OnEndGameMenu;
        }

        private void EventInit()
        {
            _dsRewardGiver.OnGiveSecondChance += OffEndGameMenu;
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
        }

        private void OffEndGameMenu()
        {
            _deathMenu.gameObject.SetActive(false);
        }


    }
}

