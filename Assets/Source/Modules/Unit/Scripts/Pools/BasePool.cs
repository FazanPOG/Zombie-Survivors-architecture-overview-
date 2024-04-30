using Modules.Unit.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Modules.Unit.Pools
{
    internal abstract class BasePool<T> : IPool<T> where T : BaseUnit
    {
        protected ObjectPool<T> _objectPool;

        private T _prefab;

        public void Init(T prefab, int defaultCapacity = 10, int maxSize = 1000)
        {
            _prefab = prefab;
            _objectPool = new ObjectPool<T>(Create, OnGet, OnRelease, OnDestroy, true, defaultCapacity, maxSize);
        }

        public abstract T Get();

        public abstract void Release(T obj);

        private T Create() 
        {
            var t = GameObject.Instantiate(_prefab);
            return t;
        }

        private void OnGet(T obj) 
        {
            obj.gameObject.SetActive(true);
        }
        
        private void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroy(T obj)
        {
            GameObject.Destroy(obj.gameObject);
        }

        
    }
}
