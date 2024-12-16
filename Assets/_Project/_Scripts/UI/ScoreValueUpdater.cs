using GameStateControl;
using SaveSystem;
using System.Xml.Serialization;

namespace UIControl
{
    public class ScoreValueUpdater
    {
        private SaveGameController _saveGameController;
        private GameStateUpdater _gameStateUpdater;

        public int CurrentScores { get; private set; }

        public ScoreValueUpdater(SaveGameController saveGameController, GameStateUpdater gameStateUpdater)
        {
            _saveGameController = saveGameController;
            _gameStateUpdater = gameStateUpdater;

            EventInit();
        }

        public void UnsubcribeEvent()
        {
            _gameStateUpdater.OnGamePlayed -= ResetCurrentScores;
        }

        public void EventInit()
        {
            _gameStateUpdater.OnGamePlayed += ResetCurrentScores;
        }

        private void ResetCurrentScores()
        {
            CurrentScores = 0;
        }

        public void AddScores(int scores)
        {
            CurrentScores += scores;
        }

        public void InitMaxScores()
        {
            CurrentScores = _saveGameController.LoadData().MaxScores;
        }

        public void UpdateMaxScores()
        {
            if (_saveGameController.LoadData().MaxScores < CurrentScores)
            {
                _saveGameController.PlayerDataValues.MaxScores = CurrentScores;
                _saveGameController.SaveData();
            }
        }
    }

}
