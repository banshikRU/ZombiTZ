using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PlayerControl;
using Zenject;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

namespace SaveSystem
{
    public class SaveGameController: ISaveHandler<PlayerData>,IInitializable
    { 
        private const string PLAYER_DATA = "PlayerData";
        
        private readonly AuthenticationInitialization _authenticationInitialization;
        public PlayerData PlayerDataValues { get; set; }

        public SaveGameController(AuthenticationInitialization authenticationInitialization)
        {
            _authenticationInitialization = authenticationInitialization;
        }

        private void SubscribeEvent()
        {
            _authenticationInitialization.OnInitializationComplete += Init;
        }
        
        public void Initialize()
        {
            SubscribeEvent();
        }
        
        private async void Init()
        {
            await CompareSaves();
        }

        public async void SaveData()
        {
            PlayerDataValues.SaveTime = DateTime.Now;
            string json = JsonConvert.SerializeObject(PlayerDataValues);
            PlayerPrefs.SetString(PLAYER_DATA, json);
            PlayerPrefs.Save();
            await SaveCloud(json); 
        }

        public PlayerData LoadData()
        {
            string jsonData = PlayerPrefs.GetString(PLAYER_DATA);
            return JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }

        private async Task SaveCloud(string saveData)
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "CloudSave", saveData } });
            Debug.Log("Cloud saved!");
        }
        
        private async Task<PlayerData> LoadCloud()
        {
            try
            {
                var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "CloudSave" });
                if (data.TryGetValue("CloudSave", out var jsonData))
                {
                    string json = jsonData.Value.GetAsString();
                    PlayerData a =  JsonConvert.DeserializeObject<PlayerData>(json);
                    Debug.Log(a.SaveTime);
                    if (string.IsNullOrEmpty(json))
                    {
                        Debug.Log("Облачные данные пусты.");
                        return null;
                    }
                    return JsonConvert.DeserializeObject<PlayerData>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при загрузке из облака: {ex.Message}");
                return null;
            }
        }



        private async Task<PlayerData> CompareSaves()
        {
            PlayerDataValues = new PlayerData();
            try
            {
                PlayerData localData = LoadData();
                PlayerData cloudData = await LoadCloud();

                if (localData == null && cloudData == null)
                {
                    Debug.Log("Нет сохранений.");
                    MakeFirstSave();
                    return null;
                }

                if (localData == null)
                {
                    PlayerDataValues = cloudData;
                    SaveData();
                    Debug.Log("Локальное сохранение отсутствует. Используем облачное.");
                    return null;
                }

                if (cloudData == null)
                {
                    PlayerDataValues = localData;
                    SaveData();
                    Debug.Log("Облачное сохранение отсутствует. Используем локальное.");
                    return null;
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
                    Debug.Log("Сохранения синхронизированы.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при сравнении сохранений: {ex.Message}");
            }

            return null;
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
}

