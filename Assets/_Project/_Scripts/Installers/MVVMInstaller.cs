using UIControl;
using UIControl.MVVM.Bullets;
using Zenject;

namespace GameSystem
{
    public class MVVMInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreValueModel>()
                .AsSingle();
            Container 
                .BindInterfacesAndSelfTo<BulletValueModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<BulletViewModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle();
        }
    }
}
