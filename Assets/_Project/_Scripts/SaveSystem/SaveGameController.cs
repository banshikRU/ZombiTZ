using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using PlayerControl;
using UniRx;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveGameController
    {
        private const string PLAYER_DATA = "PlayerData";

        private readonly ISaveService _localSaveService;
        private readonly ISaveService _cloudSaveService;

        public PlayerData LocalPlayerData { get; private set; }
        public PlayerData CloudPlayerData { get; private set; }
        public PlayerData PlayerDataValues { get; private set; }
        
        public readonly ReactiveProperty<bool> IsSaveSetUp = new ();

        public SaveGameController([Inject(Id = SaveServices.Local)] ISaveService localSaveService, [Inject(Id = SaveServices.Cloud)] ISaveService cloudSaveService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
        }

        public async UniTask Initialize()
        {
            await CompareSaves();
        }

        public async void SaveData()
        {
            IsSaveSetUp.Value = true;
            PlayerDataValues.SaveTime = DateTime.Now;
            var json = JsonConvert.SerializeObject(PlayerDataValues);
            await _localSaveService.SaveAsync(PLAYER_DATA, json);
            await _cloudSaveService.SaveAsync(PLAYER_DATA, json);
        }

        public async Task<PlayerData> LoadLocal()
        {
            var jsonData = await _localSaveService.LoadAsync(PLAYER_DATA);
            return JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
        
        public void SetUpLocalSave()
        {
            PlayerDataValues = LocalPlayerData;
            SaveData();
        }

        public void SetUpCloudSave()
        {
            PlayerDataValues = CloudPlayerData;
            SaveData();
        }

        private async Task<PlayerData> LoadCloud()
        {
            var jsonData = await _cloudSaveService.LoadAsync(PLAYER_DATA);
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
                LocalPlayerData = await LoadLocal();
                CloudPlayerData = await LoadCloud();

                if (LocalPlayerData == null && CloudPlayerData == null)
                {
                    Debug.Log("Нет сохранений.");
                    MakeFirstSave();
                }
                else if (LocalPlayerData == null)
                {
                    PlayerDataValues = CloudPlayerData;
                    SaveData();
                }
                else if (CloudPlayerData == null)
                {
                    PlayerDataValues = LocalPlayerData;
                    SaveData();
                }
                else if (LocalPlayerData.SaveTime == CloudPlayerData.SaveTime)
                {
                    PlayerDataValues = LocalPlayerData;
                    SaveData();
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
}