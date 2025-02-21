using System.Threading.Tasks;
using UnityEngine;

public class LocalSaveService : ISaveService
{
    public Task SaveAsync(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
        return Task.CompletedTask;
    }

    public Task<string> LoadAsync(string key)
    {
        string data = PlayerPrefs.GetString(key);
        return Task.FromResult(data);
    }
}