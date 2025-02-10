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
            PlayerDataValues = new PlayerData();
            if (PlayerPrefs.HasKey(PLAYER_DATA))
            {
                PlayerDataValues = LoadData();
                return;
            }
            PlayerDataValues = new PlayerData
            {
                MaxScores = 0
            };
            SaveData();
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
        }
        
        private async Task<PlayerData> LoadCloud()
        {
            try
            {
                var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "CloudSave" });
                if (data.TryGetValue("CloudSave", out var jsonData))
                {
                    string json = jsonData.Value.GetAsString();
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



        public async Task<PlayerData> CompareSaves()
        {
            Debug.Log("хуй");
            try
            {
                PlayerData localData = LoadData();
                PlayerData cloudData = await LoadCloud();

                if (localData == null && cloudData == null)
                {
                    Debug.Log("Нет сохранений.");
                    return null;
                }

                if (localData == null)
                {
                    Debug.Log("Локальное сохранение отсутствует. Используем облачное.");
                    return null;
                }

                if (cloudData == null)
                {
                    Debug.Log("Облачное сохранение отсутствует. Используем локальное.");
                    return null;
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

