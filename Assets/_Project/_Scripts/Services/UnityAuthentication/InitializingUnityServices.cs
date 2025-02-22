using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace  Services
{
    public class InitializingUnityServices
    {
        public async Task Initialize()
        {
            await InitializeUnityServices();
            await SignInAnonymously();
        }

        private async Task InitializeUnityServices()
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

        private async Task SignInAnonymously()
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