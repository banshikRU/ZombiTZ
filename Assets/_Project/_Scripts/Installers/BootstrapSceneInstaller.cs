using Zenject;

public class BootstrapSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainSceneLoader>().AsSingle().NonLazy();
        Container.Bind<GooglePlayRequirementsCheck>().AsSingle().NonLazy();
    }
}