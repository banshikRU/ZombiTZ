using UIControl;
using UnityEngine;
using Zenject;
using InAppPurchase;
using Advertisements;

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

        public override void InstallBindings()
        {
            Container.Bind<ScoresMenu>().WithId(ZenjectIds.MainMenu).FromInstance(_mainMenuScores).AsCached();
            Container.Bind<ScoresMenu>().WithId(ZenjectIds.DeadMenu).FromInstance(_deadMenuScores).AsCached();
            Container.BindInterfacesAndSelfTo<UIController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<IAPButtonController>().FromInstance(_iapButton).AsSingle().NonLazy();
        }
    }
}

