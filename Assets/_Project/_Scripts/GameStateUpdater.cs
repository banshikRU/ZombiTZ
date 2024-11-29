using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateUpdater : MonoBehaviour
{
    [SerializeField] private ScoreUpdater _scoreUpdater;

    public static event Action OnGamePlayed;

    public static bool isGame { get;private set; }
    public static int CurrentScores { get;private set; }

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
        Player.OnPlayerDeath += GameOver;
    }
    private void OnDisable()
    {
        Player.OnPlayerDeath -= GameOver;   
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
