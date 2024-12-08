using UnityEngine;
using InputControll;
using FireSystem;

public class EntryPoint : MonoBehaviour
{
    [Header("Game Settings!")]

    [SerializeField]
    private GameSettings _gameSettings;

    [Space(10)]

    [SerializeField]
    private GameStateUpdater _gameStateUpdater;
    [SerializeField]
    private ScoresMenu _mainMenuScores;
    [SerializeField]
    private ScoresMenu _DeadMenuScores;
    [SerializeField]
    private InputHandler _inputHandler;
    [SerializeField]
    private PlayerBehaviour _playerBehaviour;
    [SerializeField]
    private ZombieFabric _zombieFabric;
    [SerializeField]
    private ZombieGeneratorParameters _zombieGeneratorParameters;
    [SerializeField]
    private WeaponHandler _weaponHandler;

    private UIController _uiController;
    private PlayerFireControll _playerFireControll;
    private ScoreValueUpdater _scoreValueUpdater;
    private ObjectPoolOrganizer _objectPoolOrganizer;
    private SaveGameController _saveGameController;
    private BulletFabric _bulletFabric;


    private void Awake()
    {
        GenerateNewObjects();
        InitObjects();
        _mainMenuScores.gameObject.SetActive(true);
    }
    private void GenerateNewObjects()
    {
        _saveGameController = new SaveGameController();
        _scoreValueUpdater = new ScoreValueUpdater(_saveGameController,_gameStateUpdater);
        _uiController = new UIController(_mainMenuScores, _DeadMenuScores, _gameStateUpdater, _playerBehaviour);
        _objectPoolOrganizer = new ObjectPoolOrganizer(_gameSettings.poolConfigs);
        _bulletFabric = new BulletFabric(_objectPoolOrganizer, _gameSettings.BaseBulletPrefab, _weaponHandler);
        _playerFireControll = new PlayerFireControll(_weaponHandler, _bulletFabric, _gameStateUpdater, _inputHandler);

    }
    private void InitObjects()
    {
        _mainMenuScores.Init(_scoreValueUpdater);
        _DeadMenuScores.Init(_scoreValueUpdater);
        _gameStateUpdater.Init(_scoreValueUpdater, _saveGameController);
        _inputHandler.Init(_playerFireControll);
        _weaponHandler.Init(_gameSettings.Weapon, _playerBehaviour.transform, _gameSettings.WeaponDistanceFromPlayer);
        _playerFireControll.Init();
        _zombieFabric.Init(_objectPoolOrganizer, _gameSettings._zombiePrefabs, _playerBehaviour.transform, _scoreValueUpdater);
        _zombieGeneratorParameters.Init(_gameSettings.TimeToNewSpawnLevel, _gameSettings.MinimalTimeToSpawn, _gameSettings.BaseTimeToSpawnNewZombie, _gameSettings.ReductionTime, _gameStateUpdater, _playerBehaviour, _zombieFabric);
        
    }
}
