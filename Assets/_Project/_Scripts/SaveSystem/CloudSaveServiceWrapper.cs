using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

public class CloudSaveServiceWrapper : ISaveService
{
    public async Task SaveAsync(string key, string data)
    {
        await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { key, data } });
        Debug.Log("Cloud saved!");
    }

    public async Task<string> LoadAsync(string key)
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });
            if (data.TryGetValue(key, out var jsonData))
            {
                string json = jsonData.Value.GetAsString();
                return json;
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при загрузке из облака: {ex.Message}");
            return null;
        }
    }
}