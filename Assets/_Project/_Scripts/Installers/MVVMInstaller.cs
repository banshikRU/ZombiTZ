using System.Runtime.ConstrainedExecution;
using UIControl;
using UIControl.MVVM.Bullets;
using UIControl.MVVM.MainMenu;
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
            Container
                .BindInterfacesAndSelfTo<AdsButtonViewModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<UIViewModel>()
                .AsSingle();
            
        }
    }
}
