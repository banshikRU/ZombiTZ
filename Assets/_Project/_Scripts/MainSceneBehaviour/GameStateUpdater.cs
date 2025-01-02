using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIControl;
using PlayerControl;
using Zenject;

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

        public bool IsGame { get; private set; }

        [Inject]
        public void Contstruct(AnalyticServiceManager analyticServiceManager)
        {
            _analyticServiceManager = analyticServiceManager;
        }

        public void Initialize()
        {
            IsGame = false;
            _scoreUpdater.Value.InitMaxScores();
        }

        private void OnEnable()
        {
            _player.OnPlayerDeath += GameOver;
        }

        private void OnDisable()
        {
            _player.OnPlayerDeath -= GameOver;
        }

        public void StartGame()
        {
            IsGame = true;
            _analyticServiceManager.LogEvent();
            OnGamePlayed?.Invoke();
        }

        public void RestartGame()
        {
            _scoreUpdater.Value.UpdateMaxScores();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOver()
        {
            IsGame = false;
        }


    }
}

