using System;
using System.Threading.Tasks;
using _Project._Scripts.SaveSystem;
using Newtonsoft.Json;
using PlayerControl;
using Services;
using UnityEngine;
using Zenject;

public class SaveGameController 
{
    private const string PLAYER_DATA = "PlayerData";
    
    private readonly ISaveService _localSaveService;
    private readonly ISaveService _cloudSaveService;

    public PlayerData PlayerDataValues { get; private set; }

    public SaveGameController([Inject(Id = SaveServices.Local)] ISaveService localSaveService , [Inject(Id = SaveServices.Cloud)]ISaveService cloudSaveService )
    {
        _localSaveService = localSaveService;
        _cloudSaveService = cloudSaveService;
    }
    
    public async Task Initialize()
    {
        await CompareSaves();
    }

    public async void SaveData()
    {
        PlayerDataValues.SaveTime = DateTime.Now;
        string json = JsonConvert.SerializeObject(PlayerDataValues);
        await _localSaveService.SaveAsync(PLAYER_DATA, json);
        await _cloudSaveService.SaveAsync(PLAYER_DATA, json);
    }

    public async Task<PlayerData> LoadLocal()
    {
        string jsonData = await _localSaveService.LoadAsync(PLAYER_DATA);
        return JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }

    private async Task<PlayerData> LoadCloud()
    {
        string jsonData = await _cloudSaveService.LoadAsync(PLAYER_DATA);
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Облачные данные пусты.");
            return null;
        }
        return JsonConvert.DeserializeObject<PlayerData>(jsonData);
    }

    private async Task CompareSaves()
    {
        PlayerDataValues = new PlayerData();
        try
        {
            PlayerData localData = await LoadLocal();
            PlayerData cloudData = await LoadCloud();
            
            if (localData == null && cloudData == null)
            {
                Debug.Log("Нет сохранений.");
                MakeFirstSave();
                return;
            }

            if (localData == null)
            {
                PlayerDataValues = cloudData;
                SaveData();
                Debug.Log("Локальное сохранение отсутствует. Используем облачное.");
                return;
            }

            if (cloudData == null)
            {
                PlayerDataValues = localData;
                SaveData();
                Debug.Log("Облачное сохранение отсутствует. Используем локальное.");
                return;
            }

            if (localData.SaveTime > cloudData.SaveTime)
            {
                PlayerDataValues = localData;
                SaveData();
            }
            else if (localData.SaveTime < cloudData.SaveTime)
            {
                PlayerDataValues = cloudData;
                SaveData();
            }
            else
            {
                PlayerDataValues = localData;
                Debug.Log("Сохранения синхронизированы.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при сравнении сохранений: {ex.Message}");
        }
    }

    private void MakeFirstSave()
    {
        PlayerDataValues = new PlayerData
        {
            MaxScores = 0,
            SaveTime = DateTime.Now,
            NoAdsPurchased = false
        };
        SaveData();
    }
}
    
