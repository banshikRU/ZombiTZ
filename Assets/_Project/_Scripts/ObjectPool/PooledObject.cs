using UnityEngine;

namespace ObjectPoolSystem
{
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool Pool { get; set; }

        public void ReturnToPool()
        {
            Pool.ReturnToPool(this);
        }
    }

}

