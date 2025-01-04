using GameStateControl;
using UnityEngine;

public class UsingRemoteConfigCheck 
{
    private readonly GameSettings _gameSettings;
    private readonly RemoteConfigManager _remoteConfigManager;
    private readonly bool _useRemoteConfig = true;

    public UsingRemoteConfigCheck(GameSettings gameSettings,RemoteConfigManager remoteConfigManager)
    {
        _remoteConfigManager = remoteConfigManager;
        _gameSettings = gameSettings;
        IsRemoteConfigUsed();
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
        _gameSettings.TimeToNewSpawnLevel = _remoteConfigManager.gameSettingsValues.TimeToNewSpawnLevel;
        _gameSettings.WeaponDistanceFromPlayer = _remoteConfigManager.gameSettingsValues.WeaponDistanceFromPlayer;
        _gameSettings.FireRate = _remoteConfigManager.gameSettingsValues.FireRate;
    }
}
