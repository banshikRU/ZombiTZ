using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIControl;
using PlayerControl;
using Zenject;
using Firebase.Analytics;
using Advertisements;

namespace GameStateControl
{
    public class GameStateUpdater : MonoBehaviour,IInitializable
    {
        public event Action OnGamePlayed;

        [SerializeField]
        private PlayerBehaviour _player;

        [Inject]
        private readonly LazyInject<ScoreValueUpdater> _scoreUpdater;

        private AnalyticServiceManager _analyticServiceManager;
        private AdsRewardGiver _adsRewardGiver;

        public bool IsGame { get; private set; }

        [Inject]
        public void Contstruct(AnalyticServiceManager analyticServiceManager,AdsRewardGiver adsRewardGiver)
        {
            _adsRewardGiver = adsRewardGiver;
            _analyticServiceManager = analyticServiceManager;
        }

        public void Initialize()
        {
            IsGame = false;
            _scoreUpdater.Value.InitMaxScores();
        }

        public void StartGame()
        {
            IsGame = true;
            _analyticServiceManager.LogEventStartGame();
            OnGamePlayed?.Invoke();
        }

        public void RestartGame()
        {
            _scoreUpdater.Value.UpdateMaxScores();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnEnable()
        {
            _adsRewardGiver.OnGiveSecondChance += StartGame;
            _player.OnPlayerDeath += GameOver;
        }

        private void OnDisable()
        {
            _adsRewardGiver.OnGiveSecondChance -= StartGame;
            _player.OnPlayerDeath -= GameOver;
        }

        private void GameOver()
        {
            _analyticServiceManager.LogEventEndGame();
            IsGame = false;
        }
    }
}

