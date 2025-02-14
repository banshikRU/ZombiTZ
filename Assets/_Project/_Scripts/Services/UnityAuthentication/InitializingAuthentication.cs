using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

public class InitializingAuthentication: IInitializable
{
    public event Action OnInitializationComplete; 
    
    public async void Initialize()
    {
        await InitializeUnityServices();
        await SignInAnonymously();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при инициализации Unity Services: {ex.Message}");
        }
    }

    private async Task SignInAnonymously()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            OnInitializationComplete?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка при аутентификации: {ex.Message}");
        }
    }
}