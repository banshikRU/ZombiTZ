using InputControll;
using Services;
using UnityEngine;
using Zenject;

namespace GameSystem
{
    [CreateAssetMenu(fileName = "GameSystemInstallers", menuName = "Scriptable Objects/ProjectInstallers", order = 1)]

    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnalyticServiceManager>().AsSingle().NonLazy();
        }
    }
}


