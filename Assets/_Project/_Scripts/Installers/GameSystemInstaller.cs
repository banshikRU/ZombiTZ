using UnityEngine;
using Zenject;
using Firebase;
using GameStateControl;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private GameSettings _gameSettings;

        public override void InstallBindings()
        {
            Container.Bind<AnalyticsDataCollector>().AsSingle();
            Container.Bind<AnalyticServiceManager>().AsSingle().NonLazy();
            Container.Bind<RemoteConfigManager>().AsSingle().NonLazy();
            Container.Bind<UsingRemoteConfigCheck>().AsSingle().WithArguments(_gameSettings).NonLazy();

            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();
            Container.Bind<AdsRewardGiver>().AsSingle();
            Container.Bind<AdsServiceManager>().AsSingle();

            //  Container.BindInterfacesAndSelfTo<InAppStore>().AsSingle().NonLazy();
            //  Container.Bind<UnityServiceInitializer>().AsSingle().NonLazy();


        }
    }
}


