using System.Collections.Generic;
using UnityEngine;
using UIControl;
using ObjectPoolSystem;

namespace ZombieGeneratorBehaviour
{
    public partial class ZombieFabric : MonoBehaviour
    {
        private ObjectPoolOrganizer _objectPoolOrganizer;
        private List<GeneratedZombies> _zombiePrefabs;
        private Transform _player;
        private ScoreValueUpdater _scoreUpdater;

        public void Init(ObjectPoolOrganizer objectPoolOrganizer, List<GeneratedZombies> zombiesPrefab, Transform player, ScoreValueUpdater scoreValueUpdater)
        {
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
            return objectPool.GetPooledObject().gameObject;
        }
    }
}
