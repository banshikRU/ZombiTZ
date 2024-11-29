using TMPro;
using UnityEngine;

public class ScoresMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScores;
    [SerializeField] private ScoreValueUpdater _scoreValueUpdater;
    private void OnEnable()
    {
        _currentScores.text = _scoreValueUpdater.CurrentScores.ToString(); ;
    }
}
