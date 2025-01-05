using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class UnityServiceInitializer 
{
    public event Action OnServicesInitialized;

    public string environment = "production";

    private InAppStore _inAppStore;

    public UnityServiceInitializer(InAppStore inAppStore)
    {
        Initialize(OnSuccess, OnError);
        _inAppStore = inAppStore;
    }

    private void Initialize(Action onSuccess, Action<string> onError)
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);

            UnityServices.InitializeAsync(options).ContinueWith(task => onSuccess());
        }
        catch (Exception exception)
        {
            onError(exception.Message);
        }
    }

    void OnSuccess()
    {
        OnServicesInitialized?.Invoke();    
        Debug.Log("Congratulations!\nUnity Gaming Services has been successfully initialized.");
      //  _inAppStore.AppStoreInit(); 
    }

    void OnError(string message)
    {
        Debug.Log("Unity Gaming Services failed to initialize with error: {message}.");
    }
}
