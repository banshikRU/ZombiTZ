using System;
using UnityEngine;

namespace ZombieGeneratorBehaviour
{
    public partial class ZombieFabric : MonoBehaviour
    {
        [Serializable]
        public class GeneratedZombies
        {
            public ZombieBehaviour ZombiesPrefab;
            public float ChanceToSpawn;
        }

    }
}

