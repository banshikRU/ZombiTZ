using TMPro;
using UnityEngine;

namespace UIControl
{
    public class ScoresMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentScores;

        private ScoreValueUpdater _scoreValueUpdater;

        public void Init(ScoreValueUpdater scoreValueUpdater)
        {
            _scoreValueUpdater = scoreValueUpdater;
        }

        private void OnEnable()
        {
            _currentScores.text = _scoreValueUpdater.CurrentScores.ToString(); ;
        }
    }
}


