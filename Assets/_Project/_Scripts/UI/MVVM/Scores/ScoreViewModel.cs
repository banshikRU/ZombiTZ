using System;
using UniRx;
using Zenject;

namespace  UIControl
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<int> Scores = new();

        private readonly ScoreValueModel _scoreValueModel;

        public ScoreViewModel(ScoreValueModel scoreValueModel)
        {
            _scoreValueModel = scoreValueModel;
        }

        public void Initialize()
        {
            OnMoneyChanged(_scoreValueModel.CurrentScores.Value);
            _scoreValueModel.CurrentScores.Subscribe(OnMoneyChanged);
        }

        public void Dispose()
        {
            _scoreValueModel.CurrentScores.Dispose();
        }
        
        private void OnMoneyChanged(int value)
        {
            Scores.Value = value;
        }

        
    }
}
