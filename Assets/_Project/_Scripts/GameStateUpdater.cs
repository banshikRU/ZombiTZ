using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateUpdater : MonoBehaviour
{
    [SerializeField] private ScoreValueUpdater _scoreUpdater;
    [SerializeField] private PlayerBehaviour _player;

    public event Action OnGamePlayed;

    public  bool isGame { get;private set; }

    private void Awake()
    {
        isGame = false;
        _scoreUpdater.InitMaxScores();
    }
    public void StartGame()
    {
        isGame = true;  
        OnGamePlayed?.Invoke();
    }
    private void OnEnable()
    {
        _player.OnPlayerDeath += GameOver;
    }
    private void OnDisable()
    {
        _player.OnPlayerDeath -= GameOver;   
    }
    private void GameOver()
    {
        isGame = false;
    }
    public void RestartGame()
    {
        _scoreUpdater.UpdateMaxScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
