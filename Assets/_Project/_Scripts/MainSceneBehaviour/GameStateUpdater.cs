using System;
using Advertisements;
using Firebase.Analytics;
using PlayerControl;
using Services;
using UIControl.MVVM.Scores;
using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class GameStateUpdater : MonoBehaviour, IInitializable
    {
        public event Action OnGamePlayed;
        public event Action OnGameContinued;
        
        [SerializeField] private PlayerBehaviour _player;

        [Inject] private readonly LazyInject<ScoreValueModel> _scoreUpdater;
        
        private AdsRewardGiver _adsRewardGiver;
        private AnalyticServiceManager _analyticServiceManager;
        private SceneController _sceneController;
        private NoAdsController _noAdsController;

        public bool IsGame { get; private set; }

        public void Initialize()
        {
            IsGame = false;
            _noAdsController.IsNoAdsPurchasedCheck();
        }

        [Inject]
        public void Construct(AnalyticServiceManager analyticServiceManager, AdsRewardGiver adsRewardGiver,SceneController sceneController,NoAdsController noAdsController)
        {
            _sceneController = sceneController;
            _adsRewardGiver = adsRewardGiver;
            _analyticServiceManager = analyticServiceManager;
            _noAdsController = noAdsController;
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
            _sceneController.ReloadCurrentScene();
        }

        private void GameContinuation()
        {
            IsGame = true;
            OnGameContinued?.Invoke();
        }

        private void OnEnable()
        {
            _adsRewardGiver.OnGiveSecondChance += GameContinuation;
            _player.OnPlayerDeath += GameOver;
        }

        private void OnDisable()
        {
            _adsRewardGiver.OnGiveSecondChance -= GameContinuation;
            _player.OnPlayerDeath -= GameOver;
        }
        
        private void GameOver()
        {
            _analyticServiceManager.LogEventEndGame();
            IsGame = false;
        }
    }
}