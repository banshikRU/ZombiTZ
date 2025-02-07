using UIControl;
using UnityEngine;
using Zenject;
using InAppPurchase;
using UIControl.MVVM.Bullets;
using UIControl.MVVM.HealthBar;

namespace GameSystem
{
    public class MainSceneUIInstaller : MonoInstaller
    {
        [SerializeField]
        private IAPButtonController _iapButton;
        [SerializeField] 
        private GameObject _canvas;
        [SerializeField]
        private HealthBarView _healthBarView;
        
        public override void InstallBindings()
        {
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

