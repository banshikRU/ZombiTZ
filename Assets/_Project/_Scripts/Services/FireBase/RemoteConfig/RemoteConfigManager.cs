using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStateControl;

namespace Firebase.RemoteConfig
{
    public class RemoteConfigManager
    {
        public class RemoteGameSettingsValues
        {
            public int FireRate;
            public int WeaponDistanceFromPlayer;
            public int TimeToNewSpawnLevel;
        }

        private readonly bool _useRemoteConfig;
        private readonly GameSettings _gameSettings;

        private RemoteGameSettingsValues _gameSettingsParameters;

        public RemoteConfigManager(GameSettings gameSettings, bool useRemoteConfig)
        {
            _useRemoteConfig = useRemoteConfig;
            _gameSettings = gameSettings;
        }
        
        public async Task Initialize()
        {
            _gameSettingsParameters = new RemoteGameSettingsValues();
            await SetDefaultValues();
            await FetchRemoteConfig();
        }

        private Task FetchRemoteConfig()
        {
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWith(fetchTask => {
                if (fetchTask.IsCompleted)
                {
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    LoadRemoteConfig();
                    CheckingUseRemoteConfiguration();
                }
                else
                {
                    Debug.LogError("Load Data Error,Load Default Values");
                    LoadDefaultValues();
                }
            });
            return Task.CompletedTask;
        }

        private void LoadRemoteConfig()
        {
            string jsonString = FirebaseRemoteConfig.DefaultInstance.GetValue("Game_Settings").StringValue;
            _gameSettingsParameters = JsonUtility.FromJson<RemoteGameSettingsValues>(jsonString);
        }

        private Task SetDefaultValues()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Game_Settings");
            string jsonString = jsonFile != null ? jsonFile.text : "{}";
            var defaults = new Dictionary<string, object>
            {
            { "Game_Settings", jsonString }
            };

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
            return Task.CompletedTask;
        }

        private void LoadDefaultValues()
        {
            var jsonString = Resources.Load<TextAsset>("GameSettings")?.text ?? "{}";
            _gameSettingsParameters = JsonUtility.FromJson<RemoteGameSettingsValues>(jsonString);

        }

        private void CheckingUseRemoteConfiguration()
        {
            if (_useRemoteConfig)
            {
                SetUpNewGameSettings();
            }
        }

        private void SetUpNewGameSettings()
        {
            _gameSettings.TimeToNewSpawnLevel = _gameSettingsParameters.TimeToNewSpawnLevel;
            _gameSettings.WeaponDistanceFromPlayer = _gameSettingsParameters.WeaponDistanceFromPlayer;
            _gameSettings.FireRate = _gameSettingsParameters.FireRate;
        }
    }
}

