using System;
using GameSystem;
using SaveSystem;
using UniRx;
using Zenject;

namespace UIControl.MVVM.Scores
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
            InitMaxScores();
            SubscribeEvents();
        }
        
        public void AddScores(int scores)
        {
            CurrentScores.Value += scores;
        }

        public void InitMaxScores()
        {
            CurrentScores.Value = _saveGameController.SelectedPlayerData.MaxScores;
        }

        public void UpdateMaxScores()
        {
            if (_saveGameController.SelectedPlayerData.MaxScores >= CurrentScores.Value)
                return;
            _saveGameController.SelectedPlayerData.MaxScores = CurrentScores.Value;
            _saveGameController.SaveData();
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }
        
        private void UnsubscribeEvent()
        {
            _gameStateUpdater.OnGamePlayed -= ResetCurrentScores;
        }

        private void SubscribeEvents()
        {
            _gameStateUpdater.OnGamePlayed += ResetCurrentScores;
        }

        private void ResetCurrentScores()
        {
            CurrentScores.Value = 0;
        }
    }
}
