using System.Collections.Generic;
using UnityEngine;

namespace MiniECS
{
    public class MultPool<TObject> where TObject : Component
    {
        private readonly Dictionary<TObject, Pool<TObject>> _poolsDict = new();
        private readonly Dictionary<TObject, Pool<TObject>> _trackInstanceDict = new(25);

        public MultPool(int initialBuckets = 4, int initialInstanceCapacity = 25)
        {
            _poolsDict = new(initialBuckets);
            _trackInstanceDict = new(initialInstanceCapacity);
        }

        public virtual void Clear()
        {
            _poolsDict.Clear();
            _trackInstanceDict.Clear();
        }

        public virtual TObject Get(TObject prefab)
        {
            if (!_poolsDict.TryGetValue(prefab, out _))
            {
                _poolsDict.Add(prefab, new Pool<TObject>(prefab));
            }

            var instance = _poolsDict[prefab].Get();
            _trackInstanceDict.Add(instance, _poolsDict[prefab]);
            return instance;
        }

        public virtual void Release(TObject instance)
        {
            if (_trackInstanceDict.TryGetValue(instance, out var pool))
            {
                _trackInstanceDict.Remove(instance);
                pool.Release(instance);
            }
        }

    }
}