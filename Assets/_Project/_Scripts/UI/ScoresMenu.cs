using TMPro;
using UnityEngine;
using Zenject;

namespace UIControl
{
    public class ScoresMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentScores;

        private ScoreValueUpdater _scoreValueUpdater;

        [Inject]
        public void Construct(ScoreValueUpdater scoreValueUpdater)
        {
            _scoreValueUpdater = scoreValueUpdater;
        }

        private void OnEnable()
        {
            _currentScores.text = _scoreValueUpdater.CurrentScores.ToString(); ;
        }
    }
}


