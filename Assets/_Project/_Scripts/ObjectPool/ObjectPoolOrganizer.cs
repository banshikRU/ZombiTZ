using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolSystem 
{
    public partial class ObjectPoolOrganizer
    {
        private List<PoolConfig> _poolsConfig;

        private Dictionary<string, ObjectPool> _pools;

        public ObjectPoolOrganizer(List<PoolConfig> poolsConfig)
        {
            _poolsConfig = poolsConfig;

            Init();
        }

        public void Init()
        {
            _pools = new Dictionary<string, ObjectPool>();
            foreach (var config in _poolsConfig)
            {
                GameObject poolGameObject = new GameObject(config.prefab.name + " Pool");
                ObjectPool pool = poolGameObject.AddComponent<ObjectPool>();
                pool.Init(config.prefab, config.initialSize);
                _pools.Add(config.prefab.name, pool);
            }
        }

        public ObjectPool GetPool(string prefabName)
        {
            if (_pools.TryGetValue(prefabName, out ObjectPool pool))
            {
                return pool;
            }
            return null;
        }
    }
}

