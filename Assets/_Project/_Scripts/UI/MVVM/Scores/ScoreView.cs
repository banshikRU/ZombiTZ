using System;
using TMPro;
using UIControl.BaseMVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace  UIControl
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _currencyValue;

        private ScoreViewModel _scoreViewModel;
        
        [Inject]
        public void Construct(ScoreViewModel scoreViewModel)
        {
            this._scoreViewModel = scoreViewModel;
            
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
