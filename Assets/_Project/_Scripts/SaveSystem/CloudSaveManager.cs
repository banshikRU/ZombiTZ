using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlayerControl;
using SaveSystem;
using Zenject;

public class CloudSaveManager : ISaveHandler<PlayerData>,IInitializable
{
    private const string PLAYER_DATA = "PlayerData";
    
    public async void Initialize()
    {
        await InitializeUnityServices();
        await SignInAnonymously();
        await CompareSaves();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при инициализации Unity Services: {ex.Message}");
        }
    }

    private async Task SignInAnonymously()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Анонимная аутентификация прошла успешно. Player ID: " + AuthenticationService.Instance.PlayerId);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при аутентификации: {ex.Message}");
        }
    }

    public async Task CompareSaves()
    {
        try
        {
            PlayerData localData = LoadLocal();
            PlayerData cloudData = await LoadCloud();

            if (localData == null && cloudData == null)
            {
                Debug.Log("Нет сохранений.");
                return;
            }

            if (localData == null)
            {
                Debug.Log("Локальное сохранение отсутствует. Используем облачное.");
                return;
            }

            if (cloudData == null)
            {
                Debug.Log("Облачное сохранение отсутствует. Используем локальное.");
                return;
            }

            if (localData.SaveTime > cloudData.SaveTime)
            {
                Debug.Log("Локальное сохранение новее.");
            }
            else if (localData.SaveTime < cloudData.SaveTime)
            {
                Debug.Log("Облачное сохранение новее.");
            }
            else
            {
                Debug.Log("Сохранения синхронизированы.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при сравнении сохранений: {ex.Message}");
        }
    }

    public void SaveLocal(PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("LocalSave", jsonData);
        PlayerPrefs.Save();
    }

    public PlayerData LoadLocal()
    {
        if (PlayerPrefs.HasKey("LocalSave"))
        {
            string jsonData = PlayerPrefs.GetString("LocalSave");
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return null;
    }

    public async Task SaveCloud(PlayerData data)
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            Debug.LogWarning("Unity Services не инициализированы. Попытка инициализации...");
            await InitializeUnityServices();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("Пользователь не аутентифицирован. Попытка аутентификации...");
            await SignInAnonymously();
        }

        try
        {
            data.SaveTime = DateTime.UtcNow;
            string jsonData = JsonUtility.ToJson(data);
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "CloudSave", jsonData } });
            Debug.Log("Данные успешно сохранены в облако.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при сохранении в облако: {ex.Message}");
        }
    }

    public async Task<PlayerData> LoadCloud()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            Debug.LogWarning("Unity Services не инициализированы. Попытка инициализации...");
            await InitializeUnityServices();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("Пользователь не аутентифицирован. Попытка аутентификации...");
            await SignInAnonymously();
        }

        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "CloudSave" });
            if (data.TryGetValue("CloudSave", out var jsonData))
            {
                return JsonUtility.FromJson<PlayerData>(jsonData.Value.GetAsString());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при загрузке из облака: {ex.Message}");
        }
        return null;
    }

    public PlayerData PlayerDataValues { get; set; }
    public void SaveData()
    {
        PlayerDataValues.SaveTime = DateTime.Now;
        string json = JsonConvert.SerializeObject(PlayerDataValues);
        PlayerPrefs.SetString(PLAYER_DATA, json);
        PlayerPrefs.Save();
    }

    public PlayerData LoadData()
    {
        string jsonData = PlayerPrefs.GetString(PLAYER_DATA);
        return JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }

}