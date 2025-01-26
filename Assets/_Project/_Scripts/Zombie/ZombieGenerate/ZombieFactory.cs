using System.Collections.Generic;
using UnityEngine;
using UIControl;
using ObjectPoolSystem;
using System;
using _Project._Scripts.FXSystem;
using Random = UnityEngine.Random;
using Firebase.Analytics;
using Advertisements;

namespace ZombieGeneratorBehaviour
{
    public class ZombieFactory: IDisposable , IFXEventSender
    {
        public event Action<FXType, Vector3> OnFXEvent;
        public event Action <ZombieBehaviour> OnZombieSpawned;
        public event Action <ZombieBehaviour> OnZombieDestroyed;

        private readonly ObjectPoolOrganizer _objectPoolOrganizer;
        private readonly List<GeneratedZombies> _zombiePrefabs;
        private readonly Transform _player;
        private readonly ScoreValueModel _scoreModel;
        private readonly AnalyticsDataCollector _analyticsDataCollector;
        private readonly AdsRewardGiver _adsRewardGiver;
        private readonly List<ZombieBehaviour> _generatedActiveZombies;

        public ZombieFactory(ObjectPoolOrganizer objectPoolOrganizer, List<GeneratedZombies> zombiesPrefab, Transform player, ScoreValueModel scoreValueModel,AnalyticsDataCollector analyticsDataCollector,AdsRewardGiver adsRewardGiver)
        {
            _adsRewardGiver = adsRewardGiver;
            _generatedActiveZombies = new List<ZombieBehaviour>();
            _analyticsDataCollector = analyticsDataCollector;
            _objectPoolOrganizer = objectPoolOrganizer;
            _player = player;
            _scoreModel = scoreValueModel;
            _zombiePrefabs = zombiesPrefab;
            EventInit();
        }

        private void EventInit()
        {
            _adsRewardGiver.OnGiveSecondChance += DeactivateAllZombies;
        }

        private void UnsubscribeEvent()
        {
            _adsRewardGiver.OnGiveSecondChance -= DeactivateAllZombies;
        }

        public void Dispose()
        {
            UnsubscribeEvent();
        }

        public void GenerateZombie(Vector2 zombiePosition)
        {
            GameObject zombie = GetZombieByChance();
            zombie.transform.position = zombiePosition;
            ZombieBehaviour zombieBehaviour = zombie.GetComponent<ZombieBehaviour>();
            zombieBehaviour.Init(_player, _scoreModel, this);
            _generatedActiveZombies.Add(zombieBehaviour);
            zombie.SetActive(true);
            OnZombieSpawned?.Invoke(zombieBehaviour);
            _analyticsDataCollector.AddAnalyzedParameterValue(zombie.name, 1);

        }

        public void DeleteFromZombieList(ZombieBehaviour zombieBehaviour)
        {
            OnZombieDestroyed?.Invoke(zombieBehaviour);
             OnFXEvent?.Invoke(FXType.ZombieDie,zombieBehaviour.gameObject.transform.position);
            _generatedActiveZombies.Remove(zombieBehaviour);

        }

        private void DeactivateAllZombies()
        {
            foreach (var zombieBehaviour in _generatedActiveZombies)
            {
                zombieBehaviour.DeactivateObject();
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

