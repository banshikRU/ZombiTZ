using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIControl;
using PlayerControl;

namespace GameStateControl
{
    public class GameStateUpdater : MonoBehaviour
    {
        public event Action OnGamePlayed;

        [SerializeField]
        private PlayerBehaviour _player;

        private ScoreValueUpdater _scoreUpdater;

        public bool IsGame { get; private set; }

        public void Init(ScoreValueUpdater scoreValueUpdater)
        {
            _scoreUpdater = scoreValueUpdater;
            IsGame = false;
            _scoreUpdater.InitMaxScores();
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
            OnGamePlayed?.Invoke();
        }

        public void RestartGame()
        {
            _scoreUpdater.UpdateMaxScores();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOver()
        {
            IsGame = false;
        }
    }
}

