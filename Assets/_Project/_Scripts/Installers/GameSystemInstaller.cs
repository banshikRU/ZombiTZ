using UnityEngine;
using Zenject;
using Firebase;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnalyticsDataCollector>().AsSingle();
            Container.Bind<AnalyticServiceManager>().AsSingle().NonLazy();
        }
    }
}


