using UnityEngine;
using Zenject;
using Firebase.RemoteConfig;
using Advertisements;
using Firebase;
using GameSystem;
using Services;

namespace Installers
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
                .BindInterfacesAndSelfTo<ServiceInitializer>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<FirebaseDependencies>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<AdsInitializer>()
                .AsSingle()
                .WithArguments(_androidGameId)
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<RemoteConfigManager>()
                .AsSingle()
                .WithArguments(_gameSettings, _useRemoteConfig);
            Container
                .BindInterfacesTo<BootstrapEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
    }
}

