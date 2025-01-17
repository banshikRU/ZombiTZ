using System.Collections.Generic;
using _Project._Scripts.FXSystem;
using UnityEngine;
using ZombieGeneratorBehaviour;
using WeaponControl;
using ObjectPoolSystem;
using SfxSystem;
using VFXSystem;

namespace GameStateControl
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Scriptable Objects/New Game Settings", order = 1)]

    public class GameSettings : ScriptableObject
    {
        [Header("Object Pool Settings")]

        public List<ObjectPoolOrganizer.PoolConfig> PoolConfigs;

        [Space(10)]

        [Header("Player Weapon Settings")]

        public BaseBullet BaseBulletPrefab;
        public Weapon Weapon;
        public Bullet Bullet;
        public float FireRate;
        public float WeaponDistanceFromPlayer;

        [Space(10)]

        [Header("Zombie Spawn Parameters")]

        public float TimeToNewSpawnLevel;
        public float MinimalTimeToSpawn ;
        public float BaseTimeToSpawnNewZombie;
        public float ReductionTime;

        public List<GeneratedZombies> ZombiePrefabs;

        [Space(10)]

        [Header("FX Settings")]
        
        public List<FXBehaviour> FXPrefab;

    }

}

