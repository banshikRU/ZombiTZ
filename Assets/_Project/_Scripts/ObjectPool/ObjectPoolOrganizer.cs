using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObjectPoolSystem 
{
    public partial class ObjectPoolOrganizer: IInitializable
    {
        private readonly List<PoolConfig> _poolsConfig;

        private Dictionary<string, ObjectPool> _pools;

        public ObjectPoolOrganizer(List<PoolConfig> poolsConfig)
        {
            _poolsConfig = poolsConfig;
        }

        public void Initialize()
        {
            FirstPoolsInit();
        }
        
        public ObjectPool GetPool(string prefabName)
        {
            return _pools.GetValueOrDefault(prefabName);
        }

        private void FirstPoolsInit()
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
    }
}

