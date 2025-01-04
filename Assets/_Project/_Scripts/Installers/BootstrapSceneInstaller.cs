using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private string _androidGameId;

        public override void InstallBindings()
        {
            Container.Bind<MainSceneLoader>().AsSingle().NonLazy();
            Container.Bind<FirebaseDependendeciesCheck>().AsSingle().NonLazy();

            Container.Bind<AdsInitializer>().AsSingle().WithArguments(_androidGameId).NonLazy();
        }
    }
}

