using UnityEngine;
using InputControll;
using WeaponControl;
using ZombieGeneratorBehaviour;
using UIControl;
using PlayerControl;
using ObjectPoolSystem;
using SaveSystem;
using Services;

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
        private PlayerFireControl _playerFireControll;
        private ScoreValueUpdater _scoreValueUpdater;
        private ObjectPoolOrganizer _objectPoolOrganizer;
        private SaveGameController _saveGameController;
        private BulletFabric _bulletFabric;
        private InputController _inputController;
        private ZombieFactory _zombieFabric;
        private CurrentPlatformChecker _currentPlatformChecker;

        private bool isInit;

        private void Awake()
        {
            isInit = false;
            GenerateNewObjects();
            InitObjects();
            SubscribingEvent();
            ActivateObjects();
        }

        private void Update()
        {
            if (!isInit)
                return;
            _inputController.Update();
            _zombieGeneratorParameters.Update();
            _weaponHandler.Update();
        }

        private void OnDisable()
        {
            UnsubctibingEvent();
        }

        private void GenerateNewObjects()
        {
            _saveGameController = new SaveGameController();
            _scoreValueUpdater = new ScoreValueUpdater(_saveGameController, _gameStateUpdater);
            _uiController = new UIController(_mainMenuScores, _DeadMenuScores, _gameStateUpdater, _playerBehaviour);
            _objectPoolOrganizer = new ObjectPoolOrganizer(_gameSettings.PoolConfigs);
            _zombieFabric = new ZombieFactory(_objectPoolOrganizer, _gameSettings._zombiePrefabs, _playerBehaviour.transform, _scoreValueUpdater);
            _zombieGeneratorParameters = new ZombieGeneratorParameters(_gameSettings.TimeToNewSpawnLevel, _gameSettings.MinimalTimeToSpawn, _gameSettings.BaseTimeToSpawnNewZombie, _gameSettings.ReductionTime, _zombieFabric);
            _weaponHandler = new WeaponHandler(_gameSettings.Weapon, _playerBehaviour.transform, _gameSettings.WeaponDistanceFromPlayer, _weapon);
            _bulletFabric = new BulletFabric(_objectPoolOrganizer, _gameSettings.BaseBulletPrefab, _weaponHandler);
            _playerFireControll = new PlayerFireControl(_weaponHandler, _bulletFabric, _gameStateUpdater);
            _inputController = new InputController(_playerFireControll);
            _currentPlatformChecker = new CurrentPlatformChecker(_inputController);

        }

        private void InitObjects()
        {
            _mainMenuScores.Init(_scoreValueUpdater);
            _DeadMenuScores.Init(_scoreValueUpdater);
            _gameStateUpdater.Init(_scoreValueUpdater);
            _playerFireControll.Init();

        }

        private void SubscribingEvent()
        {
            _playerBehaviour.OnPlayerDeath += GameOver;
            _gameStateUpdater.OnGamePlayed += StartGame;
        }

        private void UnsubcribeEvent()
        {
            _playerBehaviour.OnPlayerDeath -= GameOver;
            _gameStateUpdater.OnGamePlayed -= StartGame;
        }

        private void UnsubctibingEvent()
        {
            UnsubcribeEvent();
            _inputController.UnsubcribeEvent();
            _playerFireControll.UnsubcribeEvent();
            _scoreValueUpdater.UnsubcribeEvent();
            _uiController.UnsubcribeEvent();
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

