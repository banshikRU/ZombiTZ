using UnityEngine;

namespace SaveSystem
{
    public partial class SaveGameController
    {
        private const string _PLAYER_DATA = "PlayerData";

        public PlayerData PlayerDataValues = new PlayerData();

        public void Init()
        {
            if (!PlayerPrefs.HasKey(_PLAYER_DATA))
            {
                PlayerDataValues.MaxScores = 0;
                SaveData();
            }
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(PlayerDataValues);
            PlayerPrefs.SetString("PlayerData", json);
            PlayerPrefs.Save();
        }

        public PlayerData LoadData()
        {
            string jsonData = PlayerPrefs.GetString(_PLAYER_DATA);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
    }
}

