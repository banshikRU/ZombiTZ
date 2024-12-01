using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PooledObject objectToPool;

    private Stack<PooledObject> stack;

    public uint InitPoolSize { get; private set; }

    public void Init(PooledObject prefab, uint initialSize)
    {
        objectToPool = prefab;
        InitPoolSize = initialSize;

        SetupPool();
    }

    private void SetupPool()
    {
        if (objectToPool == null)
        {
            return;
        }

        stack = new Stack<PooledObject>();


        for (int i = 0; i < InitPoolSize; i++)
        {
            CreateNewInstance();
        }
    }

    private PooledObject CreateNewInstance()
    {
        PooledObject instance = Instantiate(objectToPool);
        instance.Pool = this;
        instance.gameObject.SetActive(false);
        stack.Push(instance);
        return instance;
    }

    public PooledObject GetPooledObject()
    {
        if (objectToPool == null)
        {
            return null;
        }

        if (stack.Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}

