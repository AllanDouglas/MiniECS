using System;
using System.Collections.Generic;

namespace MiniECS
{
    public sealed class ComponentsManager
    {
        private int _componentID;
        private readonly Dictionary<Type, (int id, IComponentPool pool)> _componentCache;
        private readonly IComponentPool[] _componentsPool;
        private readonly int _sizeBuffer;

        public ComponentsManager(int sizeBuffer)
        {
            _componentCache = new(sizeBuffer);
            _sizeBuffer = sizeBuffer;
            _componentsPool = new IComponentPool[sizeBuffer];
        }

        public ComponentArchetype AddComponentPrototype(in Entity entity, IComponentPrototype[] componentPrototypes)
        {
            ComponentArchetype componentArchetype = default;

            for (int i = 0; i < componentPrototypes.Length; i++)
            {
                int componentID = componentPrototypes[i].AddToComponentPool(this, _sizeBuffer);
                componentPrototypes[i].AddComponentToEntity(entity, this);
                componentArchetype += componentID;
            }

            return componentArchetype;
        }

        public void AddComponent<TComponent>(in Entity entity, TComponent component)
            where TComponent : struct, IComponent
        {
            if (!TryGetComponentPool(typeof(TComponent), out var componentPool))
            {
                componentPool = new ComponentPool<TComponent>(_sizeBuffer);
                Add(componentPool);
            }

            componentPool.Add(in entity, component);
        }

        public void RemoveComponent<TComponent>(in Entity entity)
            where TComponent : struct, IComponent
        {
            if (TryGetComponentPool(typeof(TComponent), out var componentPool))
            {
                componentPool.Remove(in entity);
            }

        }

        public int GetComponentID<TComponent>() where TComponent : struct, IComponent
        {
            if (_componentCache.TryGetValue(typeof(TComponent), out var poolSet))
            {
                return poolSet.id;
            }

            return -1;
        }

        public int Add(IComponentPool componentPool)
        {
            int id = _componentID;
            _componentCache.TryAdd(componentPool.ComponentType, (id, componentPool));
            _componentsPool[id] = componentPool;
            _componentID++;
            return id;
        }

        public IComponentPool GetComponentPool<TComponent>() where TComponent : struct, IComponent
        {
            return _componentCache[typeof(TComponent)].pool;
        }

        public bool TryGetComponentPool<TComponent>(out IComponentPool componentPool)
            where TComponent : struct, IComponent
        {
            return TryGetComponentPool(typeof(TComponent), out componentPool);
        }

        public ref TComponent TryGet<TComponent>(in Entity entity, out bool hasComponent)
           where TComponent : struct, IComponent
        {
            return ref _componentCache[typeof(TComponent)].pool.TryGet<TComponent>(entity, out hasComponent);
        }

        private bool TryGetComponentPool(Type componentType, out IComponentPool componentPool)
        {
            if (_componentCache.TryGetValue(componentType, out var poolSet))
            {
                componentPool = poolSet.pool;
                return true;
            }
            componentPool = null;
            return false;
        }

    }
}