using TMPro;
using UnityEngine;
using Zenject;

namespace UIControl
{
    public class ScoresMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentScores;

        private ScoreValueModel _scoreValueModel;

        [Inject]
        public void Construct(ScoreValueModel scoreValueModel)
        {
            _scoreValueModel = scoreValueModel;
        }
        

        private void OnEnable()
        {
            _currentScores.text = _scoreValueModel.CurrentScores.ToString(); ;
        }
    }
}


