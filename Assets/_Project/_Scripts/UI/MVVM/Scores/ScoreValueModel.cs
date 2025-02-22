using GameStateControl;
using System;
using UniRx;
using Zenject;

namespace UIControl
{
    public class ScoreValueModel : IDisposable,IInitializable
    {
        private readonly SaveGameController _saveGameController;
        private readonly GameStateUpdater _gameStateUpdater;

        public readonly ReactiveProperty<int> CurrentScores = new();

        public ScoreValueModel(SaveGameController saveGameController, GameStateUpdater gameStateUpdater)
        {
            _saveGameController = saveGameController;
            _gameStateUpdater = gameStateUpdater;
        }
        
        public void Initialize()
        {
            EventInit();
        }
        
        public void AddScores(int scores)
        {
            CurrentScores.Value += scores;
        }

        public void InitMaxScores()
        {
            CurrentScores.Value = _saveGameController.PlayerDataValues.MaxScores;
        }

        public void UpdateMaxScores()
        {
            if (_saveGameController.PlayerDataValues.MaxScores >= CurrentScores.Value)
                return;
            _saveGameController.PlayerDataValues.MaxScores = CurrentScores.Value;
            _saveGameController.SaveData();
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }
        
        private void UnsubscribeEvent()
        {
            _saveGameController.OnPlayerDataUpdated += InitMaxScores;
            _gameStateUpdater.OnGamePlayed -= ResetCurrentScores;
        }

        private void EventInit()
        {
            _saveGameController.OnPlayerDataUpdated += InitMaxScores;
            _gameStateUpdater.OnGamePlayed += ResetCurrentScores;
        }

        private void ResetCurrentScores()
        {
            CurrentScores.Value = 0;
        }
    }
}
