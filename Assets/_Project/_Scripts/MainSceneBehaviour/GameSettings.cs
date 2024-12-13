using System.Collections.Generic;
using UnityEngine;
using ZombieGeneratorBehaviour;
using WeaponControl;
using ObjectPoolSystem;

namespace GameStateControl
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Scriptable Objects/New Game Settings", order = 1)]

    public class GameSettings : ScriptableObject
    {
        [Header("Object Pool Settings")]

        public List<ObjectPoolOrganizer.PoolConfig> poolConfigs;

        [Space(10)]

        [Header("Player Weapon Settings")]

        public BaseBullet BaseBulletPrefab;
        public Weapon Weapon;
        public Bullet Bullet;
        public float FireRate;
        public float WeaponDistanceFromPlayer;

        [Space(10)]

        [Header("Zombie Spawn Parameters")]

        public float TimeToNewSpawnLevel = 10;
        public float MinimalTimeToSpawn = 0.5f;
        public float BaseTimeToSpawnNewZombie = 2f;
        public float ReductionTime = 0.1f;

        public List<ZombieFabric.GeneratedZombies> _zombiePrefabs;

    }
}

