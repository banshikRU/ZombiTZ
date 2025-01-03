using Zenject;

namespace GameSystem
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainSceneLoader>().AsSingle().NonLazy();
            Container.Bind<FirebaseDependendeciesCheck>().AsSingle().NonLazy();
        }
    }
}

