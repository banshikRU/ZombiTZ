using System;

namespace ZombieGeneratorBehaviour
{
    public partial class ZombieFactory 
    {
        [Serializable]
        public class GeneratedZombies
        {
            public ZombieBehaviour ZombiesPrefab;
            public float ChanceToSpawn;
        }

    }
}

