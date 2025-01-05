using System.Collections.Generic;
using UnityEngine;
using UIControl;
using ObjectPoolSystem;
using System;
using Random = UnityEngine.Random;
using Firebase;
using Unity.VisualScripting;
using Zenject;

namespace ZombieGeneratorBehaviour
{
    public class ZombieFactory: IDisposable
    {
        [Serializable]
        public class GeneratedZombies
        {
            public ZombieBehaviour ZombiesPrefab;
            public float ChanceToSpawn;
        }

        public event Action<VFXEvent> OnZombieDie;

        private readonly ObjectPoolOrganizer _objectPoolOrganizer;
        private readonly List<GeneratedZombies> _zombiePrefabs;
        private readonly Transform _player;
        private readonly ScoreValueUpdater _scoreUpdater;
        private readonly AnalyticsDataCollector _analyticsDataCollector;
        private readonly AdsRewardGiver _adsRewardGiver;

        private List<ZombieBehaviour> _geratedActiveZombies;

        public ZombieFactory(ObjectPoolOrganizer objectPoolOrganizer, List<GeneratedZombies> zombiesPrefab, Transform player, ScoreValueUpdater scoreValueUpdater,AnalyticsDataCollector analyticsDataCollector,AdsRewardGiver adsRewardGiver)
        {
            _adsRewardGiver = adsRewardGiver;
            _geratedActiveZombies = new List<ZombieBehaviour>();
            _analyticsDataCollector = analyticsDataCollector;
            _objectPoolOrganizer = objectPoolOrganizer;
            _player = player;
            _scoreUpdater = scoreValueUpdater;
            _zombiePrefabs = zombiesPrefab;
            EventInit();
        }

        public void EventInit()
        {
            _adsRewardGiver.OnGiveSecondChance += DeactivateAllZombies;
        }

        public void UnsubcribeEvent()
        {
            _adsRewardGiver.OnGiveSecondChance -= DeactivateAllZombies;
        }

        public void Dispose()
        {
            UnsubcribeEvent();
        }

        public void GenerateZombie(Vector2 zombiePosition)
        {
            GameObject zombie = GetZombieByChance();
            zombie.transform.position = zombiePosition;
            ZombieBehaviour zombieBehaviour = zombie.GetComponent<ZombieBehaviour>();
            zombieBehaviour.Init(_player, _scoreUpdater, this);
            _geratedActiveZombies.Add(zombieBehaviour);
            zombie.SetActive(true);
            _analyticsDataCollector.AddAnalizedParameterValue(zombie.name, 1);

        }

        public void DeleteFromZombieList(ZombieBehaviour zombieBehaviour)
        {
            OnZombieDie.Invoke(new VFXEvent(zombieBehaviour.gameObject.transform.position, Quaternion.identity, VFXTypes.ZombieDie));
            _geratedActiveZombies.Remove(zombieBehaviour);

        }

        public void DeactivateAllZombies()
        {
            for (int i = 0; i < _geratedActiveZombies.Count; i++)
            {
                _geratedActiveZombies[i].DeactivateObject();
            }
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

