using UnityEngine;
using PlayerControl;
using Zenject;

namespace SaveSystem
{
    public class SaveGameController: IInitializable
    { 
        private const string PLAYER_DATA = "PlayerData";

        public readonly PlayerData PlayerDataValues = new();

        public void Initialize()
        {
            if (PlayerPrefs.HasKey(PLAYER_DATA))
                return;
            PlayerDataValues.MaxScores = 0;
            SaveData();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(PlayerDataValues);
            PlayerPrefs.SetString(PLAYER_DATA, json);
            PlayerPrefs.Save();
        }

        public PlayerData LoadData()
        {
            string jsonData = PlayerPrefs.GetString(PLAYER_DATA);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }

    }
}

