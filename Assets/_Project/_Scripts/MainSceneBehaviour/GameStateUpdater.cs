using System;
using Advertisements;
using Firebase.Analytics;
using PlayerControl;
using UIControl;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameStateControl
{
    public class GameStateUpdater : MonoBehaviour, IInitializable
    {
        public event Action OnGamePlayed;
        
        [SerializeField] private PlayerBehaviour _player;

        [Inject] private readonly LazyInject<ScoreValueModel> _scoreUpdater;
        
        private AdsRewardGiver _adsRewardGiver;

        private AnalyticServiceManager _analyticServiceManager;

        public bool IsGame { get; private set; }

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

        public void Initialize()
        {
            IsGame = false;
            _scoreUpdater.Value.InitMaxScores();
        }

        [Inject]
        public void Construct(AnalyticServiceManager analyticServiceManager, AdsRewardGiver adsRewardGiver)
        {
            _adsRewardGiver = adsRewardGiver;
            _analyticServiceManager = analyticServiceManager;
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

        private void GameOver()
        {
            _analyticServiceManager.LogEventEndGame();
            IsGame = false;
        }
    }
}