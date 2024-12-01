using UnityEngine;

public class ScoreValueUpdater : MonoBehaviour
{
    public int CurrentScores { get; private set; }

    private void Start()
    {
        CurrentScores = 0;
    }

    public void AddScores(int scores)
    {
        CurrentScores += scores;
    }

    public void InitMaxScores()
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

    public void UpdateMaxScores()
    {
        if (PlayerPrefs.GetInt("MaxScores") < CurrentScores)
        {
            PlayerPrefs.SetInt("MaxScores", CurrentScores);
        }
    }
}
