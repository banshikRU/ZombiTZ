using System.Collections.Generic;
using UnityEngine;
using UIControl;
using ObjectPoolSystem;
using System;
using Random = UnityEngine.Random;
using Firebase;

namespace ZombieGeneratorBehaviour
{
    public class ZombieFactory
    {
        [Serializable]
        public class GeneratedZombies
        {
            public ZombieBehaviour ZombiesPrefab;
            public float ChanceToSpawn;
        }

        private readonly ObjectPoolOrganizer _objectPoolOrganizer;
        private readonly List<GeneratedZombies> _zombiePrefabs;
        private readonly Transform _player;
        private readonly ScoreValueUpdater _scoreUpdater;
        private readonly AnalyticsDataCollector _analyticsDataCollector;

        public ZombieFactory(ObjectPoolOrganizer objectPoolOrganizer, List<GeneratedZombies> zombiesPrefab, Transform player, ScoreValueUpdater scoreValueUpdater,AnalyticsDataCollector analyticsDataCollector)
        {
            _analyticsDataCollector = analyticsDataCollector;
            _objectPoolOrganizer = objectPoolOrganizer;
            _player = player;
            _scoreUpdater = scoreValueUpdater;
            _zombiePrefabs = zombiesPrefab;
        }

        public void GenerateZombie(Vector2 zombiePosition)
        {
            GameObject zombie = GetZombieByChance();
            zombie.transform.position = zombiePosition;
            zombie.GetComponent<ZombieBehaviour>().Init(_player, _scoreUpdater);
            zombie.SetActive(true);
            _analyticsDataCollector.AddAnalizedParameterValue(zombie.name, 1);

        }

        private GameObject GetZombieByChance()
        {
            float randomValue = Random.Range(0f, 100);
            float cumulativeChance = 0f;
            foreach (var zombie in _zombiePrefabs)
            {
                cumulativeChance += zombie.ChanceToSpawn;
                if (randomValue < cumulativeChance)
                {
                    return GetPooledZombie(zombie.ZombiesPrefab);
                }
            }
            return null;
        }

        private GameObject GetPooledZombie(ZombieBehaviour zombie)
        {
            ObjectPool objectPool = _objectPoolOrganizer.GetPool(zombie.gameObject.name);
            return objectPool.GetObject().gameObject;
        }

    }
}

