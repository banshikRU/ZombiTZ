using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [Serializable]
    public class GeneratedZombies
    {
        public ZombieBehaviour ZombiesPrefab;
        public float ChanceToSpawn;
    }
    [Header("Generator Prefabs")]
    [SerializeField] private List<GeneratedZombies> _zombiePrefabs;
    [SerializeField] private List<Collider2D> _generateAreas;

    [Space(10)]

    [SerializeField] private Transform _player;
    [SerializeField] private ScoreUpdater _scoreUpdater;

    [Header("Generator Parameters")]
    [SerializeField] private float _timeToNewSpawnLevel = 10;
    [SerializeField] private float _minimalTimeToSpawn = 0.5f;
    [SerializeField] private float _baseTimeToSpawnNewZombie = 2f;
    [SerializeField] private float _reductionTime = 0.1f;

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
        GameStateUpdater.OnGamePlayed += StartGenerate;
        Player.OnPlayerDeath += GameOver;
    }
    private void OnDisable()
    {
        GameStateUpdater.OnGamePlayed -= StartGenerate;
        Player.OnPlayerDeath -= GameOver;
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
            if (_baseTimeToSpawnNewZombie <= 0 )
            {
                _baseTimeToSpawnNewZombie = _newTimeToNextSpawn;
                GenerateZombie();
            }
            if (_timeToNewSpawn <= 0 && !_isMinimalValueReached ) // проверка на достижение минимального значения по генерации
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
    private void GenerateZombie()
    {
        Vector2 _randomGeneratedPosition = GetRandomPositionInCollider(ChoseRandomCollider());
        ZombieBehaviour generatedZombie =  Instantiate(GetZombieByChance(), _randomGeneratedPosition, Quaternion.identity);
        generatedZombie.Init(_player,_scoreUpdater);
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

        return new Vector2(x, y);
    }
    private ZombieBehaviour GetZombieByChance()
    {
        float randomValue = Random.Range(0f, 100);
        float cumulativeChance = 0f;
        foreach (var zombie in _zombiePrefabs)
        {
            cumulativeChance += zombie.ChanceToSpawn;
            if (randomValue < cumulativeChance)
            {
                return zombie.ZombiesPrefab;
            }
        }
        return null;
    }
}
