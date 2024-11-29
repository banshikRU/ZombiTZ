using TMPro;
using UnityEngine;

public class ScoresMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScores;
    private void OnEnable()
    {
        _currentScores.text = GameStateUpdater.CurrentScores.ToString(); ;
    }
}
