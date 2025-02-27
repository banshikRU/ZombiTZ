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
        public PlayerData SelectedPlayerData { get; private set; }
        
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
            SelectedPlayerData.SaveTime = DateTime.Now;
            var json = JsonConvert.SerializeObject(SelectedPlayerData);
            await _localSaveService.SaveAsync(PLAYER_DATA, json);
            await _cloudSaveService.SaveAsync(PLAYER_DATA, json);
        }

        public async UniTask<PlayerData> LoadLocal()
        {
            var jsonData = await _localSaveService.LoadAsync(PLAYER_DATA);
            return JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
        
        public void SetUpLocalSave()
        {
            SelectedPlayerData = LocalPlayerData;
            SaveData();
        }

        public void SetUpCloudSave()
        {
            SelectedPlayerData = CloudPlayerData;
            SaveData();
        }

        private async UniTask<PlayerData> LoadCloud()
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
            SelectedPlayerData = new PlayerData();
            
            try
            {
                var (localPlayerData,cloudPlayerData) = await UniTask.WhenAll(LoadLocal(), LoadCloud());

                LocalPlayerData = localPlayerData;
                CloudPlayerData = cloudPlayerData;

                if (LocalPlayerData == null && CloudPlayerData == null)
                {
                    Debug.Log("Нет сохранений.");
                    MakeFirstSave();
                }
                else if (LocalPlayerData == null)
                {
                    SelectedPlayerData = CloudPlayerData;
                    SaveData();
                }
                else if (CloudPlayerData == null)
                {
                    SelectedPlayerData = LocalPlayerData;
                    SaveData();
                }
                else if (LocalPlayerData.SaveTime == CloudPlayerData.SaveTime)
                {
                    SelectedPlayerData = LocalPlayerData;
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
            SelectedPlayerData = new PlayerData
            {
                MaxScores = 0,
                SaveTime = DateTime.Now,
                NoAdsPurchased = false
            };
            SaveData();
        }
    }
}