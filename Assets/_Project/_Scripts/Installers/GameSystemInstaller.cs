using UnityEngine;
using Zenject;
using Firebase.Analytics;
using SaveSystem;
using Services;
using InAppPurchase;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveGameController>().AsSingle();
            Container.Bind<AnalyticsDataCollector>().AsSingle();
            Container.Bind<AnalyticServiceManager>().AsSingle();
            Container.Bind<UnityAdsService>().AsSingle();
            Container.Bind<AdsRewardGiver>().AsSingle();
            Container.Bind<AdsServiceManager>().AsSingle();
            Container.Bind<InAppStore>().AsSingle().NonLazy();
            Container.Bind<NoAdsController>().AsSingle(); 
        }
    }
}


