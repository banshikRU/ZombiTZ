using InputControll;
using ObjectPoolSystem;
using PlayerControl;
using SaveSystem;
using Services;
using UIControl;
using UnityEngine;
using WeaponControl;
using Zenject;
using ZombieGeneratorBehaviour;

namespace GameStateControl
{
    public class EntryPoint : MonoInstaller
    {
        [Header("Game Settings!")]

        [SerializeField]
        private GameSettings _gameSettings;

        [Space(10)]

        [SerializeField]
        private SpriteRenderer _weapon;
        [SerializeField]
        private GameStateUpdater _gameStateUpdater;
        [SerializeField]
        private ScoresMenu _mainMenuScores;
        [SerializeField]
        private ScoresMenu _deadMenuScores;
        [SerializeField]
        private PlayerBehaviour _playerBehaviour;

        public override void InstallBindings()
        {
            Container.Bind<PlayerBehaviour>().FromInstance(_playerBehaviour).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateUpdater>().FromInstance(_gameStateUpdater).AsSingle();
            Container.Bind<SaveGameController>().AsSingle();
            Container.Bind<ScoreValueUpdater>().AsSingle();
            Container.Bind<ScoresMenu>().WithId(ZenjectIds.MainMenu).FromInstance(_mainMenuScores).AsCached();
            Container.Bind<ScoresMenu>().WithId(ZenjectIds.DeadMenu).FromInstance(_deadMenuScores).AsCached();
            Container.BindInterfacesAndSelfTo<UIController>().AsSingle().NonLazy();
            Container.Bind<ObjectPoolOrganizer>().AsSingle().WithArguments(_gameSettings.PoolConfigs);
            Container.Bind<ZombieFactory>().AsSingle().WithArguments(_gameSettings._zombiePrefabs, _playerBehaviour.transform);
            Container.BindInterfacesAndSelfTo<ZombieGeneratorParameters>().AsSingle().WithArguments(_gameSettings.TimeToNewSpawnLevel, _gameSettings.MinimalTimeToSpawn, _gameSettings.BaseTimeToSpawnNewZombie, _gameSettings.ReductionTime).NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponHandler>().AsSingle().WithArguments(_gameSettings.Weapon, _playerBehaviour.transform, _gameSettings.WeaponDistanceFromPlayer, _weapon);
            Container.Bind<BulletFabric>().AsSingle().WithArguments(_gameSettings.BaseBulletPrefab);
            Container.BindInterfacesAndSelfTo<PlayerFireControl>().AsSingle().WithArguments(_gameSettings.FireRate);
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
            Container.Bind<CurrentPlatformChecker>().AsSingle().NonLazy();
        }
    }
}

