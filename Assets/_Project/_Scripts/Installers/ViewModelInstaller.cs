using UIControl;
using Zenject;

namespace GameSystem
{
    public class ViewModelInstaller :MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle()
                .NonLazy();
                
        }
    }
}