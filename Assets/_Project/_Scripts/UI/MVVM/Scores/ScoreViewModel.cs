using System;
using UniRx;
using Zenject;

namespace  UIControl
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        private readonly ScoreValueModel _scoreValueModel;

        public readonly ReactiveProperty<int> Scores = new();

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
