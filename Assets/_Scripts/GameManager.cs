using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    public static event Action<bool> OnGamePlayed;

    public static bool isGame { get;private set; }
    public static int CurrentScores { get;private set; }

    private void Awake()
    {
        isGame = false;
        InitMaxScores();
    }
    public void StartGame()
    {
        isGame = true;  
        CurrentScores = 0;
        OnGamePlayed?.Invoke(true);
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void RestartGame()
    {
        UpdateMaxScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddScores(int scores)
    {
        CurrentScores += scores;
    }
    private void InitMaxScores()
    {
        if (!PlayerPrefs.HasKey("MaxScores"))
        {
            CurrentScores = 0;
            PlayerPrefs.SetInt("MaxScores", 0);
        }
        else
        {
            CurrentScores = PlayerPrefs.GetInt("MaxScores");
        }
    }
    private void UpdateMaxScores()
    {
        if (PlayerPrefs.GetInt("MaxScores")<CurrentScores)
        {
            PlayerPrefs.SetInt("MaxScores", CurrentScores);
        }
    }
}
