using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolSystem
{
    public class ObjectPool : MonoBehaviour 
    {
        private PooledObject _objectToPool;
        private Stack<PooledObject> _stack;

        private uint _initPoolSize;

        public void Init(PooledObject prefab, uint initialSize)
        {
            _objectToPool = prefab;
            _initPoolSize = initialSize;

            SetupPool();
        }
        
        public PooledObject GetObject()
        {
            if (_objectToPool == null)
            {
                return null;
            }

            if (_stack.Count == 0)
            {
                PooledObject newInstance = Instantiate(_objectToPool);
                newInstance.Pool = this;
                return newInstance;
            }

            PooledObject nextInstance = _stack.Pop();
            nextInstance.gameObject.SetActive(true);
            return nextInstance;
        }

        public void ReturnToPool(PooledObject pooledObject)
        {
            _stack.Push(pooledObject);
            pooledObject.gameObject.SetActive(false);
        }
        
        private void SetupPool()
        {
            if (_objectToPool == null)
            {
                return;
            }
            _stack = new Stack<PooledObject>();
            for (int i = 0; i < _initPoolSize; i++)
            {
                CreateNewInstance();
            }
        }
        
        private PooledObject CreateNewInstance()
        {
            PooledObject instance = Instantiate(_objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            _stack.Push(instance);
            return instance;
        }
    }
}


