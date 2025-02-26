using System;
using UniRx;
using Zenject;

namespace  UIControl.MVVM.Scores
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
            SubscribeEvents();
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _scoreValueModel.CurrentScores.Subscribe(OnMoneyChanged);
        }

        private void UnsubscribeEvents()
        {
            _scoreValueModel.CurrentScores.Dispose();
        }

        private void OnMoneyChanged(int value)
        {
            Scores.Value = value;
        }
    }
}
