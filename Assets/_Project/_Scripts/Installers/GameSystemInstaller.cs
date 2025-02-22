using UnityEngine;
using Zenject;
using Firebase.Analytics;
using Services;
using InAppPurchase;
using Advertisements;
using InputControl;
using SaveSystem;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneController>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<InitializingUnityServices>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<DesktopInput>()
                .AsSingle();
            Container.Bind<ISaveService>()
                .WithId(SaveServices.Local)
                .To<LocalSaveService>()
                .AsSingle();
            Container.Bind<ISaveService>()
                .WithId(SaveServices.Cloud)
                .To<CloudSaveServiceWrapper>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<SaveGameController>()
                .AsSingle();
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


