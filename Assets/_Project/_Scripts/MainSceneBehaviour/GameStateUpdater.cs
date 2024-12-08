using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIControl;
using PlayerControl;
using SaveSystem;

namespace GameStateControl
{
    public class GameStateUpdater : MonoBehaviour
    {
        public event Action OnGamePlayed;

        [SerializeField]
        private PlayerBehaviour _player;

        private ScoreValueUpdater _scoreUpdater;
        private SaveGameController _saveGameController;

        public bool isGame { get; private set; }

        public void Init(ScoreValueUpdater scoreValueUpdater, SaveGameController saveGameController)
        {
            _scoreUpdater = scoreValueUpdater;
            _saveGameController = saveGameController;
            isGame = false;
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
            isGame = true;
            OnGamePlayed?.Invoke();
        }

        public void RestartGame()
        {
            _scoreUpdater.UpdateMaxScores();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOver()
        {
            isGame = false;
        }
    }
}

