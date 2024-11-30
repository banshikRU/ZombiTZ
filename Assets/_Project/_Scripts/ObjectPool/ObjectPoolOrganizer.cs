using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolOrganizer : MonoBehaviour
{
    [System.Serializable]
    public class PoolConfig
    {
        public PooledObject prefab;
        public uint initialSize;
    }

    [SerializeField] private List<PoolConfig> poolsConfig;

    private Dictionary<string, ObjectPool> pools;

    private void Awake()
    {
        pools = new Dictionary<string, ObjectPool>();
        foreach (var config in poolsConfig)
        {
            GameObject poolGameObject = new GameObject(config.prefab.name + " Pool");
            ObjectPool pool = poolGameObject.AddComponent<ObjectPool>();
            pool.Init(config.prefab, config.initialSize);
            pools.Add(config.prefab.name, pool);
        }
    }

    public ObjectPool GetPool(string prefabName)
    {
        if (pools.TryGetValue(prefabName, out ObjectPool pool))
        {
            return pool;
        }
        return null;
    }
}
