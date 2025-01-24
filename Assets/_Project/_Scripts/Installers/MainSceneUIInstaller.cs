using UIControl;
using UnityEngine;
using Zenject;
using InAppPurchase;
using Advertisements;
using UIControl.MVVM.Bullets;
using UIControl.MVVM.HealthBar;

namespace GameSystem
{
    public class MainSceneUIInstaller : MonoInstaller
    {
        [SerializeField]
        private ScoresMenu _deadMenuScores;
        [SerializeField]
        private ScoresMenu _mainMenuScores;
        [SerializeField]
        private IAPButtonController _iapButton;
        [SerializeField] 
        private GameObject _canvas;
        [SerializeField]
        private HealthBarView _healthBarView;
        

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
                .Bind<ScoresMenu>()
                .WithId(ZenjectIds.MainMenu)
                .FromInstance(_mainMenuScores)
                .AsCached();
            Container
                .Bind<ScoresMenu>()
                .WithId(ZenjectIds.DeadMenu)
                .FromInstance(_deadMenuScores)
                .AsCached();
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<UIController>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesTo<IAPButtonController>()
                .FromInstance(_iapButton)
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesTo<HealthBarFabric>()
                .AsSingle()
                .WithArguments(_healthBarView,_canvas)
                .NonLazy();
        }
    }
}

