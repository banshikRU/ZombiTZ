using GameStateControl;
using UnityEngine;
using Zenject;
using Firebase.RemoteConfig;
using Advertisements;
using Firebase;

namespace GameSystem
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private string _androidGameId;
        [SerializeField]
        private bool _useRemoteConfig;
        [SerializeField]
        private GameSettings _gameSettings;

        public override void InstallBindings()
        {
            Container
                .Bind<FirebaseDependendeciesCheck>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AdsInitializer>()
                .AsSingle()
                .WithArguments(_androidGameId)
                .NonLazy();
            Container
                .Bind<RemoteConfigManager>()
                .AsSingle()
                .WithArguments(_gameSettings,_useRemoteConfig)
                .NonLazy();
            Container
                .Bind<MainSceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}

