using System;
using Unity.Collections.LowLevel.Unsafe;

namespace MiniECS
{

    public sealed class ComponentPool<T> : IComponentPool
        where T : struct, IComponent
    {
        private T _invalidComponent;

        private readonly T[] _components;
        private readonly uint[] _dense;
        private readonly int[] _sparse;
        public int Count { get; private set; }

        public Type ComponentType => typeof(T);

        public ComponentPool(int capacity)
        {
            _sparse = new int[capacity];
            _dense = new uint[capacity];
            _components = new T[capacity];
            Array.Fill(_sparse, -1);
        }


        public void Add<TComponent>(in Entity entity, TComponent component)
            where TComponent : struct, IComponent
        {
            Add(entity.id, UnsafeUtility.As<TComponent, T>(ref component));
        }

        public void Add(uint entityId, T component)
        {
            if (Has(entityId))
            {
                return;
            }

            _sparse[entityId] = Count;
            _dense[Count] = entityId;
            _components[Count] = component;
            Count++;
        }

        public ref TComponent Get<TComponent>(in Entity entity)
            where TComponent : struct, IComponent
        {
            ref T component = ref Get(entity.id);
            return ref UnsafeUtility.As<T, TComponent>(ref component);
        }

        public bool TryGet<TComponent>(in Entity entity, ref TComponent component) where TComponent : struct, IComponent
        {
            if (!Has(entity.id))
            {
                return false;
            }
            ref T componentTmp = ref Get(entity.id);
            component = ref UnsafeUtility.As<T, TComponent>(ref componentTmp);
            return true;
        }

        public ref TComponent TryGet<TComponent>(in Entity entity, out bool hasComponent) where TComponent : struct, IComponent
        {
            if (!Has(entity.id))
            {
                hasComponent = false;
                return ref UnsafeUtility.As<T, TComponent>(ref _invalidComponent);
            }
            hasComponent = true;
            return ref UnsafeUtility.As<T, TComponent>(ref Get(entity.id));
        }

        public ref T Get(uint entityId)
        {
#if UNITY_EDITOR
            if (entityId >= (uint)_sparse.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(entityId), $"Entity ID {entityId} is out of bounds.");
            }
#endif

            int index = _sparse[entityId];
#if UNITY_EDITOR
            if (index < 0 || index >= Count || _dense[index] != entityId)
            {
                throw new InvalidOperationException($"Entity {entityId} does not have a component of type {typeof(T).Name}");
            }
#endif

            return ref _components[index];
        }

        public bool Has(uint entityId)
        {
            int index = _sparse[entityId];
            return index >= 0 && index < Count && _dense[index] == entityId;
        }

        public void Remove(uint entityId)
        {
            int index = _sparse[entityId];
            if (!Has(entityId)) return;

            int last = Count - 1;
            uint lastEntity = _dense[last];

            // Swap current with last
            _components[index] = _components[last];
            _dense[index] = lastEntity;
            _sparse[lastEntity] = index;

            // Invalidate removed
            _sparse[entityId] = -1;
            Count--;
        }

        private struct EmptyComponent : IComponent { }

    }
}