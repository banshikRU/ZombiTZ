using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateUpdater : MonoBehaviour
{
    public event Action OnGamePlayed;

    [SerializeField]
    private ScoreValueUpdater _scoreUpdater;
    [SerializeField]
    private SaveGameController _saveGameController;
    [SerializeField]
    private PlayerBehaviour _player;

    public  bool isGame { get;private set; }

    private void Awake()
    {
        isGame = false;
        _scoreUpdater.InitMaxScores();
        _saveGameController.Init();
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
