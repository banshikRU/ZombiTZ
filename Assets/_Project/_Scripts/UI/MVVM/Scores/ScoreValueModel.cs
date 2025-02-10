using GameStateControl;
using SaveSystem;
using System;
using PlayerControl;
using UniRx;
using Zenject;

namespace UIControl
{
    public class ScoreValueModel : IDisposable,IInitializable
    {
        private readonly ISaveHandler<PlayerData> _saveGameController;
        private readonly GameStateUpdater _gameStateUpdater;

        public readonly ReactiveProperty<int> CurrentScores = new();

        public ScoreValueModel(ISaveHandler<PlayerData> saveGameController, GameStateUpdater gameStateUpdater)
        {
            _saveGameController = saveGameController;
            _gameStateUpdater = gameStateUpdater;
        }
        
        public void Initialize()
        {
            InitMaxScores();
            EventInit();
        }
        
        private void UnsubscribeEvent()
        {
            _gameStateUpdater.OnGamePlayed -= ResetCurrentScores;
        }

        private void EventInit()
        {
            _gameStateUpdater.OnGamePlayed += ResetCurrentScores;
        }

        private void ResetCurrentScores()
        {
            CurrentScores.Value = 0;
        }

        public void AddScores(int scores)
        {
            CurrentScores.Value += scores;
        }

        public void InitMaxScores()
        {
            CurrentScores.Value = _saveGameController.LoadData().MaxScores;
        }

        public void UpdateMaxScores()
        {
            if (_saveGameController.LoadData().MaxScores >= CurrentScores.Value)
                return;
            _saveGameController.PlayerDataValues.MaxScores = CurrentScores.Value;
            _saveGameController.SaveData();
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }
    }
}
