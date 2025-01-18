using System;
using MVVM;
using UniRx;
using Zenject;

namespace  UIControl
{
    public class MoneyViewModel : IInitializable, IDisposable
    {
        [Data("Currency")]
        public readonly ReactiveProperty<string> Money = new();

        private readonly ScoreValueUpdater _scoreValueUpdater;

        public MoneyViewModel(ScoreValueUpdater scoreValueUpdater)
        {
            _scoreValueUpdater = scoreValueUpdater;
        }

        public void Initialize()
        {
            OnMoneyChanged(_scoreValueUpdater.CurrentScores);
            _scoreValueUpdater.OnScoreValueUpdate += OnMoneyChanged;
        }

        public void Dispose()
        {
            _scoreValueUpdater.OnScoreValueUpdate -= OnMoneyChanged;
        }
        
        private void OnMoneyChanged(int value)
        {
            Money.Value = value.ToString();
        }

        
    }
}
