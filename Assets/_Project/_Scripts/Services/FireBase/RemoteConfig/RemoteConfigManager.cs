using Firebase.RemoteConfig;
using System;
using UnityEngine;
using System.Collections.Generic;

public class RemoteConfigManager
{
    [Serializable]
    public class GameSettingsValues
    {
        public int FireRate;
        public int WeaponDistanceFromPlayer;
        public int TimeToNewSpawnLevel;
    }

    public GameSettingsValues gameSettingsValues { get;private set; }

    public RemoteConfigManager()
    {
        gameSettingsValues = new GameSettingsValues();
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
            }
            else
            {
                Debug.LogError("Load Data Error,Load Default Values");
                LoadDefalultValues();
            }
        });
    }

    private void LoadRemoteConfig()
    {
        string jsonString = FirebaseRemoteConfig.DefaultInstance.GetValue("Game_Settings").StringValue;
        gameSettingsValues = JsonUtility.FromJson<GameSettingsValues>(jsonString);
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

    private void LoadDefalultValues()
    {
        var jsonString = Resources.Load<TextAsset>("GameSettings")?.text ?? "{}";
        gameSettingsValues = JsonUtility.FromJson<GameSettingsValues>(jsonString);
    }
}

