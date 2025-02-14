using UnityEngine;
using Zenject;
using Firebase.Analytics;
using SaveSystem;
using Services;
using InAppPurchase;
using Advertisements;
using InputControl;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<InitializingAuthentication>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<DesktopInput>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<SaveGameController>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<AnalyticsDataCollector>()
                .AsSingle();
            Container
                .Bind<AnalyticServiceManager>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<UnityAdsService>()
                .AsSingle();
            Container
                .Bind<AdsRewardGiver>()
                .AsSingle();
            Container.Bind<AdsServiceManager>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<InAppStore>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<NoAdsController>()
                .AsSingle(); 
        }
    }
}


