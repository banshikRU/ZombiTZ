using UnityEngine;

namespace SaveSystem
{
    public class SaveGameController
    {
        public class PlayerData
        {
            public int MaxScores;
        }

        private const string PLAYER_DATA = "PlayerData";

        public PlayerData PlayerDataValues = new();

        public SaveGameController()
        {
            Init();
        }

        public void Init()
        {
            if (!PlayerPrefs.HasKey(PLAYER_DATA))
            {
                PlayerDataValues.MaxScores = 0;
                SaveData();
            }
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

