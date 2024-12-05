using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieGeneratornParameters : MonoBehaviour
{
    [Header("Generator Prefabs")]

    [SerializeField]
    private List<Collider2D> _generateAreas;

    [Space(10)]

    [SerializeField]
    private GameStateUpdater _gameStateUpdater;
    [SerializeField]
    private PlayerBehaviour _playerBehaviour;
    [SerializeField]
    private ZombieFabric _zombieFabric;

    [Header("Generator Parameters")]

    [SerializeField]
    private float _timeToNewSpawnLevel = 10;
    [SerializeField]
    private float _minimalTimeToSpawn = 0.5f;
    [SerializeField]
    private float _baseTimeToSpawnNewZombie = 2f;
    [SerializeField]
    private float _reductionTime = 0.1f;

    private bool _isGame;
    private bool _isMinimalValueReached;

    private float _newTimeToNextSpawn;
    private float _timeToNewSpawn;

    private void Awake()
    {
        _newTimeToNextSpawn = _baseTimeToSpawnNewZombie;
        _timeToNewSpawn = _timeToNewSpawnLevel;
        _isMinimalValueReached = false;
    }

    private void OnEnable()
    {
        _gameStateUpdater.OnGamePlayed += StartGenerate;
        _playerBehaviour.OnPlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        _gameStateUpdater.OnGamePlayed -= StartGenerate;
        _playerBehaviour.OnPlayerDeath -= GameOver;
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
