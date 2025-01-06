using InputControll;
using ObjectPoolSystem;
using PlayerControl;
using Services;
using UIControl;
using UnityEngine;
using WeaponControl;
using Zenject;
using ZombieGeneratorBehaviour;
using GameStateControl;
using System.Collections.Generic;
using SFXSystem;
using VFXSystem;

namespace GameSystem
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Game Settings!")]

        [SerializeField]
        private GameSettings _gameSettings;

        [Space(10)]

        [SerializeField]
        private SpriteRenderer _weapon;
        [SerializeField]
        private PlayerBehaviour _playerBehaviour;
        [SerializeField]
        private AudioSource _audioSource;

        public override void InstallBindings()
        {
            Container.Bind<SfxPlayer>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<VFXGenerator>().AsSingle().WithArguments(_gameSettings.VFXPrefabs).NonLazy();
            Container.Bind<VFXEventCatcher>().AsSingle();
            Container.Bind<SfxEventCatcher>().AsSingle();
            Container.Bind<PlayerBehaviour>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameStateUpdater>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScoreValueUpdater>().AsSingle();
            
            Container.Bind<ObjectPoolOrganizer>().AsSingle().WithArguments(_gameSettings.PoolConfigs);
            Container.Bind<ZombieFactory>().AsSingle().WithArguments(_gameSettings.ZombiePrefabs, _playerBehaviour.transform);
            Container.BindInterfacesAndSelfTo<ZombieGeneratorParameters>().AsSingle().WithArguments(_gameSettings.TimeToNewSpawnLevel, _gameSettings.MinimalTimeToSpawn, _gameSettings.BaseTimeToSpawnNewZombie, _gameSettings.ReductionTime).NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponHandler>().AsSingle().WithArguments(_gameSettings.Weapon, _playerBehaviour.transform, _gameSettings.WeaponDistanceFromPlayer, _weapon);
            Container.Bind<BulletFabric>().AsSingle().WithArguments(_gameSettings.BaseBulletPrefab);
            Container.BindInterfacesAndSelfTo<PlayerFireControl>().AsSingle().WithArguments(_gameSettings.FireRate);

            Container.Bind<CurrentPlatformChecker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();




        }
    }
}

