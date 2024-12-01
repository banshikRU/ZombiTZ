using System;
using UnityEngine;

public partial class EnemyGenerator : MonoBehaviour
{
    [Serializable]
    public class GeneratedZombies
    {
        public ZombieBehaviour ZombiesPrefab;
        public float ChanceToSpawn;
    }

}
