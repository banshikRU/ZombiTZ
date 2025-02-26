using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Services
{
    public class UnityServicesInitializer
    {
        public async UniTask Initialize()
        {
            await InitializeUnityServices();
            await SignInAnonymously();
        }

        private async UniTask InitializeUnityServices()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при инициализации Unity Services: {ex.Message}");
            }
        }

        private async UniTask SignInAnonymously()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при аутентификации: {ex.Message}");
            }
        }
    }
}