using System.Collections.Generic;
using UnityEngine;


    public class ObjectPool : MonoBehaviour
    {

        [SerializeField] private uint initPoolSize;
        public uint InitPoolSize => initPoolSize;

        [SerializeField] private PooledObject objectToPool;


        private Stack<PooledObject> stack;

        private void Start()
        {
            SetupPool();
        }

        private void SetupPool()
        {
            if (objectToPool == null)
            {
                return;
            }

            stack = new Stack<PooledObject>();

            PooledObject instance = null;

            for (int i = 0; i < initPoolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.Pool = this;
                instance.gameObject.SetActive(false);
                stack.Push(instance);
            }
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

