using GameStateControl;
using SaveSystem;
using System;
using UniRx;
using UnityEngine;

namespace UIControl
{
    public class ScoreValueModel : IDisposable
    {
        private readonly SaveGameController _saveGameController;
        private readonly GameStateUpdater _gameStateUpdater;

        public readonly ReactiveProperty<int> CurrentScores = new();

        public ScoreValueModel(SaveGameController saveGameController, GameStateUpdater gameStateUpdater)
        {
            _saveGameController = saveGameController;
            _gameStateUpdater = gameStateUpdater;

            InitMaxScores();
            EventInit();
        }

        public void UnsubcribeEvent()
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
            if (_saveGameController.LoadData().MaxScores < CurrentScores.Value)
            {
                _saveGameController.PlayerDataValues.MaxScores = CurrentScores.Value;
                _saveGameController.SaveData();
            }
        }

        public void Dispose()
        {
            UnsubcribeEvent();
        }
    }

}
