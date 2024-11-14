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
    [SerializeField] private List<GeneratedZombies> _zombiePrefabs;
    [SerializeField] private List<Collider2D> _generateAreas;
    [SerializeField] private Transform _player;
    private void Start()
    {
        GenerateZombie();
    }
    private void GenerateZombie()
    {
        Vector2 _randomGeneratedPosition = GetRandomPositionInCollider(ChoseRandomCollider());
        ZombieBehaviour generatedZombie =  Instantiate(GetZombieByChance(), _randomGeneratedPosition, Quaternion.identity);
        generatedZombie.Init(_player);
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
