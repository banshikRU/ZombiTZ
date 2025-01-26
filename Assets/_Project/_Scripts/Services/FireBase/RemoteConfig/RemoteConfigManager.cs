using System;
using UnityEngine;
using System.Collections.Generic;
using GameStateControl;
using Unity.VisualScripting;

namespace Firebase.RemoteConfig
{
    public class RemoteConfigManager: IInitializable
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
            _gameSettingsParameters = new RemoteGameSettingsValues();
        }
        
        public void Initialize()
        {
            SetDefaultValues();
            FetchRemoteConfig();
        }

        private void FetchRemoteConfig()
        {
            FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWith(fetchTask => {
                if (fetchTask.IsCompleted)
                {
                    FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    LoadRemoteConfig();
                    IsRemoteConfigUsed();
                }
                else
                {
                    Debug.LogError("Load Data Error,Load Default Values");
                    LoadDefaultValues();
                }
            });
        }

        private void LoadRemoteConfig()
        {
            string jsonString = FirebaseRemoteConfig.DefaultInstance.GetValue("Game_Settings").StringValue;
            _gameSettingsParameters = JsonUtility.FromJson<RemoteGameSettingsValues>(jsonString);
        }

        private void SetDefaultValues()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Game_Settings");
            string jsonString = jsonFile != null ? jsonFile.text : "{}";
            var defaults = new Dictionary<string, object>
            {
            { "Game_Settings", jsonString }
            };

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
        }

        private void LoadDefaultValues()
        {
            var jsonString = Resources.Load<TextAsset>("GameSettings")?.text ?? "{}";
            _gameSettingsParameters = JsonUtility.FromJson<RemoteGameSettingsValues>(jsonString);

        }

        private void IsRemoteConfigUsed()
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

