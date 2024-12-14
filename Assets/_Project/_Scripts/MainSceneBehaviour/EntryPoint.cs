using UnityEngine;
using InputControll;
using WeaponControl;
using ZombieGeneratorBehaviour;
using UIControl;
using PlayerControl;
using ObjectPoolSystem;
using SaveSystem;

namespace GameStateControl
{
    public class EntryPoint : MonoBehaviour
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
        private ScoresMenu _DeadMenuScores;
        [SerializeField]
        private PlayerBehaviour _playerBehaviour;
        [SerializeField]
        private WeaponHandler _weaponHandler;

        private ZombieGeneratorParameters _zombieGeneratorParameters;
        private UIController _uiController;
        private PlayerFireControll _playerFireControll;
        private ScoreValueUpdater _scoreValueUpdater;
        private ObjectPoolOrganizer _objectPoolOrganizer;
        private SaveGameController _saveGameController;
        private BulletFabric _bulletFabric;
        private InputController _inputHandler;
        private ZombieFabric _zombieFabric;

        private bool isInit;

        private void Awake()
        {
            isInit = false;
            GenerateNewObjects();
            InitObjects();
            InitEvents();
            ActivateObjects();
        }

        private void Update()
        {
            if (!isInit)
                return;
            _inputHandler.Update();
            _zombieGeneratorParameters.Update();
            _weaponHandler.Update();
        }

        private void GenerateNewObjects()
        {
            _saveGameController = new SaveGameController();
            _scoreValueUpdater = new ScoreValueUpdater(_saveGameController, _gameStateUpdater);
            _uiController = new UIController(_mainMenuScores, _DeadMenuScores, _gameStateUpdater, _playerBehaviour);
            _objectPoolOrganizer = new ObjectPoolOrganizer(_gameSettings.PoolConfigs);
            _zombieFabric = new ZombieFabric(_objectPoolOrganizer, _gameSettings._zombiePrefabs, _playerBehaviour.transform, _scoreValueUpdater);
            _zombieGeneratorParameters = new ZombieGeneratorParameters(_gameSettings.TimeToNewSpawnLevel, _gameSettings.MinimalTimeToSpawn, _gameSettings.BaseTimeToSpawnNewZombie, _gameSettings.ReductionTime, _zombieFabric);
            _weaponHandler = new WeaponHandler(_gameSettings.Weapon, _playerBehaviour.transform, _gameSettings.WeaponDistanceFromPlayer, _weapon);
            _bulletFabric = new BulletFabric(_objectPoolOrganizer, _gameSettings.BaseBulletPrefab, _weaponHandler);
            _playerFireControll = new PlayerFireControll(_weaponHandler, _bulletFabric, _gameStateUpdater);
            _inputHandler = new InputController(_playerFireControll);

        }

        private void InitObjects()
        {
            _mainMenuScores.Init(_scoreValueUpdater);
            _DeadMenuScores.Init(_scoreValueUpdater);
            _gameStateUpdater.Init(_scoreValueUpdater);
            _playerFireControll.Init();

        }

        private void InitEvents()
        {
            _playerBehaviour.OnPlayerDeath += GameOver;
            _gameStateUpdater.OnGamePlayed += StartGame;
        }

        private void ActivateObjects()
        {
            _mainMenuScores.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            isInit = true;
        }

        private void GameOver()
        {
            isInit = false;
        }
    }
}

