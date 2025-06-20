using System;
using UnityEngine;
using UnityEngine.Pool;

namespace MiniECS
{
    public class Pool<T>
        where T : Component
    {
        private readonly ObjectPool<T> _pool;
        private readonly T _prefab;

        public Pool(T prefab, int initialCapacity = 25)
        {
            _pool = new(OnCreate,
                        OnGet,
                        null,
                        null,
                        true,
                        initialCapacity);
            _prefab = prefab;
        }

        public virtual T Get() => _pool.Get();

        public virtual void Release(T obj)
        {
#if UNITY_EDITOR
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
#endif

            _pool?.Release(obj);
        }



        private void OnGet(T t) => t.gameObject.SetActive(true);
        private T OnCreate() => UnityEngine.Object.Instantiate(_prefab);
    }
}