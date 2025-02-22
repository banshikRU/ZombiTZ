using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace  UIControl.MVVM.Scores
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _currencyValue;

        private ScoreViewModel _scoreViewModel;
        
        [Inject]
        public void Construct(ScoreViewModel scoreViewModel)
        {
            _scoreViewModel = scoreViewModel;
        }

        private void Awake()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            _scoreViewModel.Scores.Dispose();
        }

        private void SubscribeEvents()
        {
            _scoreViewModel.Scores.Subscribe(DisplayScore);
        }

        private void DisplayScore(int score)
        {
            _currencyValue.text = score.ToString();
        }
    }
}
