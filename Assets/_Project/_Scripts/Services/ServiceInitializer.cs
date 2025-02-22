using System;
using Firebase;
using Firebase.RemoteConfig;
using SaveSystem;
using Services;
using Zenject;

namespace GameStateControl
{
    public class ServiceInitializer :IInitializable
    {
        public event Action OnInitializationCompleted; 
        
        private readonly InitializingUnityServices _initializingUnityServices;
        private readonly FirebaseDependencies _firebaseDependencies;
        private readonly RemoteConfigManager _remoteConfigManager;
        private readonly SaveGameController _saveGameController;
        private readonly NoAdsController _noAdsController;

        public ServiceInitializer(InitializingUnityServices initializingUnityServices, FirebaseDependencies firebaseDependencies, RemoteConfigManager remoteConfigManager,SaveGameController saveGameController, NoAdsController noAdsController)
        {
            _initializingUnityServices = initializingUnityServices;
            _firebaseDependencies = firebaseDependencies;
            _remoteConfigManager = remoteConfigManager;
            _saveGameController = saveGameController;
            _noAdsController = noAdsController;
        }
        
        public async void Initialize()
        {
            await _initializingUnityServices.Initialize();
            await _firebaseDependencies.Initialize();
            await _remoteConfigManager.Initialize();
            await _saveGameController.Initialize();
            _noAdsController.Initialize();
            OnInitializationCompleted?.Invoke();
        }
    }
}