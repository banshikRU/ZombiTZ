using UnityEngine;

public class ScoreValueUpdater : MonoBehaviour 
{
    [SerializeField]
    private SaveGameController _saveGameController;

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
        CurrentScores = _saveGameController.LoadData().MaxScores;
    }

    public void UpdateMaxScores()
    {
        if (_saveGameController.LoadData().MaxScores < CurrentScores)
        {
            _saveGameController.PlayerDataValues.MaxScores = CurrentScores;
            _saveGameController.SaveData();
        }
    }
}
