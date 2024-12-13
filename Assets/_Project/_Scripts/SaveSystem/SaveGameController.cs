using UnityEngine;

namespace SaveSystem
{
    public partial class SaveGameController
    {
        private const string PLAYER_DATA = "PlayerData";

        public PlayerData PlayerDataValues = new();

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

