using System;
using UnityEngine;

namespace ZombieGeneratorBehaviour
{
    public partial class ZombieFabric 
    {
        [Serializable]
        public class GeneratedZombies
        {
            public ZombieBehaviour ZombiesPrefab;
            public float ChanceToSpawn;
        }

    }
}

