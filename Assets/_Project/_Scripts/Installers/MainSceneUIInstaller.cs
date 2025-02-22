using UIControl;
using UnityEngine;
using Zenject;
using UIControl.Buttons;
using UIControl.MVVM;
using UIControl.MVVM.Bullets;
using UIControl.MVVM.HealthBar;
using UIControl.MVVM.Buttons;
using UIControl.MVVM.Scores;

namespace Installers
{
    public class MainSceneUIInstaller : MonoInstaller
    {
        [SerializeField] 
        private GameObject _canvas;
        [SerializeField]
        private HealthBarView _healthBarView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<HealthBarFabric>()
                .AsSingle()
                .WithArguments(_healthBarView,_canvas)
                .NonLazy();
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
                .BindInterfacesAndSelfTo<IAPButtonViewModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<UIViewModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<SelectSaveMenuViewModel>()
                .AsSingle();
        }
    }
}

