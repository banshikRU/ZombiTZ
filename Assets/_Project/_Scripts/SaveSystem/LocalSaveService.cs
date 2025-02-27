using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SaveSystem
{
    public class LocalSaveService : ISaveService
    {
        public UniTask SaveAsync(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }

        public UniTask<String> LoadAsync(string key)
        {
            string data = PlayerPrefs.GetString(key);
            return UniTask.FromResult(data);
        }
    }
}
