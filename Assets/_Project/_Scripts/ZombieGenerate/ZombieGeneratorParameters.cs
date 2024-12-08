using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieGeneratorParameters : MonoBehaviour
{
    [Header("Generator Prefabs")]

    [SerializeField]
    private List<Collider2D> _generateAreas;

    [Space(10)]

    private GameStateUpdater _gameStateUpdater;
    private PlayerBehaviour _playerBehaviour;
    private ZombieFabric _zombieFabric;

    [Header("Generator Parameters")]

    private float _timeToNewSpawnLevel = 10;
    private float _minimalTimeToSpawn = 0.5f;
    private float _baseTimeToSpawnNewZombie = 2f;
    private float _reductionTime = 0.1f;

    private bool _isGame;
    private bool _isMinimalValueReached;

    private float _newTimeToNextSpawn;
    private float _timeToNewSpawn;

    public void Init(float timeToNewSpawnLevel,float minimalTimeToSpawn,float baseTimeToSpawnNewZombie,float reductionTime, GameStateUpdater gameStateUpdater,PlayerBehaviour playerBehaviour,ZombieFabric zombieFabric )
    {
        _timeToNewSpawn = timeToNewSpawnLevel;
        _minimalTimeToSpawn = minimalTimeToSpawn;
        _baseTimeToSpawnNewZombie= baseTimeToSpawnNewZombie;
        _reductionTime = reductionTime;
        _gameStateUpdater = gameStateUpdater;
        _playerBehaviour = playerBehaviour;
        _zombieFabric = zombieFabric;

        _newTimeToNextSpawn = _baseTimeToSpawnNewZombie;
        _timeToNewSpawn = _timeToNewSpawnLevel;
        _isMinimalValueReached = false;

        EventInit();
    }

    private void EventInit()
    {
        _gameStateUpdater.OnGamePlayed += StartGenerate;
        _playerBehaviour.OnPlayerDeath += GameOver;
    }

    private void Update()
    {
        if (_isGame)
        {
            _baseTimeToSpawnNewZombie -= Time.deltaTime;
            if (!_isMinimalValueReached)
            {
                _timeToNewSpawn -= Time.deltaTime;
            }
            if (_baseTimeToSpawnNewZombie <= 0)
            {
                _baseTimeToSpawnNewZombie = _newTimeToNextSpawn;
                _zombieFabric.GenerateZombie(GetRandomPositionInCollider(ChoseRandomCollider()));
            }
            if (_timeToNewSpawn <= 0 && !_isMinimalValueReached)
            {
                _timeToNewSpawn = _timeToNewSpawnLevel;
                _newTimeToNextSpawn -= _reductionTime;
                if (_newTimeToNextSpawn <= _minimalTimeToSpawn) { _isMinimalValueReached = true; }
            }
        }
    }

    private void GameOver()
    {
        _isGame = false;
    }

    private void StartGenerate()
    {
        _isGame = true;
    }

    private Collider2D ChoseRandomCollider()
    {
        int randomAreaIndex = Random.Range(0, _generateAreas.Count);
        return _generateAreas[randomAreaIndex];
    }

    private Vector2 GetRandomPositionInCollider(Collider2D collider)
    {
        Bounds bounds = collider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        if (Utilities.IsPointVisible(new Vector2(x, y))) GetRandomPositionInCollider(ChoseRandomCollider());
        return new Vector2(x, y);
    }
}
