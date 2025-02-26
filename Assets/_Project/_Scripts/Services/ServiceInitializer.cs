using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using SaveSystem;
using Zenject;

namespace Services
{
    public class ServiceInitializer :IInitializable
    {
        public event Action OnInitializationCompleted; 
        
        private readonly UnityServicesInitializer _unityServicesInitializer;
        private readonly FirebaseDependencies _firebaseDependencies;
        private readonly RemoteConfigManager _remoteConfigManager;
        private readonly SaveGameController _saveGameController;
        private readonly NoAdsController _noAdsController;

        public ServiceInitializer(UnityServicesInitializer unityServicesInitializer, FirebaseDependencies firebaseDependencies, RemoteConfigManager remoteConfigManager,SaveGameController saveGameController, NoAdsController noAdsController)
        {
            _unityServicesInitializer = unityServicesInitializer;
            _firebaseDependencies = firebaseDependencies;
            _remoteConfigManager = remoteConfigManager;
            _saveGameController = saveGameController;
            _noAdsController = noAdsController;
        }
        
        public async void Initialize()
        {
             await InitializeServices();
             OnInitializationCompleted?.Invoke();
        }

        private async UniTask InitializeServices()
        {
            await UniTask.WhenAll(_unityServicesInitializer.Initialize(), _firebaseDependencies.Initialize(),_remoteConfigManager.Initialize());
            await _saveGameController.Initialize();
            _noAdsController.Initialize();
        }
    }
}