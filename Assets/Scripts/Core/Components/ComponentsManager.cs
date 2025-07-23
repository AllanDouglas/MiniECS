using System;
using System.Collections.Generic;

namespace MiniECS
{
    public sealed class ComponentsManager
    {

        public static IComponent Trash = new TrashComponent();

        private int _componentID = 0;
        private readonly Dictionary<Type, (int id, IComponentPool pool)> _componentCache;
        private readonly IComponentPool[] _componentsPool;
        private readonly int _componentBufferSize;
        private readonly int _componentsPoolSize;

        public ComponentsManager(int componentsPoolSize, int componentsBufferSize)
        {
            _componentCache = new(componentsPoolSize);
            _componentBufferSize = componentsBufferSize;
            _componentsPoolSize = componentsPoolSize;
            _componentsPool = new IComponentPool[componentsPoolSize];
        }

        public ComponentArchetype AddComponentPrototype(in Entity entity, IComponentPrototype[] componentPrototypes)
        {
            ComponentArchetype componentArchetype = default;

            for (int i = 0; i < componentPrototypes.Length; i++)
            {
                ComponentID componentID = componentPrototypes[i].AddToComponentPool(this, _componentBufferSize);
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
                componentPool = new ComponentPool<TComponent>(_componentsPoolSize);
                Add(componentPool);
            }

            componentPool.Add(in entity, component);
        }

        public ComponentID RemoveComponent<TComponent>(in Entity entity)
            where TComponent : struct, IComponent
        {
            if (TryGetComponentPool(typeof(TComponent), out var componentPool))
            {
                componentPool.Remove(in entity);
                return GetComponentID<TComponent>();
            }
            return new(-1);
        }

        public ComponentID GetComponentID<TComponent>() where TComponent : struct, IComponent
        {
            if (_componentCache.TryGetValue(typeof(TComponent), out var poolSet))
            {
                return new(poolSet.id);
            }

            return new(Add(new ComponentPool<TComponent>(_componentBufferSize)));
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

        public IComponentPool GetComponentPool(ComponentID componentId)
        {
#if UNITY_EDITOR
            if (componentId.value < 0 || componentId.value >= _componentsPool.Length)
            {
                UnityEngine.Debug.LogError($"Trying to get a Component Pool out of range. {componentId}");
                return null;
            }
#endif

            return _componentsPool[componentId.value];
        }

        public bool TryGetComponentPool<TComponent>(out IComponentPool componentPool)
            where TComponent : struct, IComponent
        {
            return TryGetComponentPool(typeof(TComponent), out componentPool);
        }

        public ref TComponent TryGet<TComponent>(in Entity entity, out bool hasComponent) where TComponent : struct, IComponent =>
             ref _componentCache[typeof(TComponent)].pool.TryGet<TComponent>(entity, out hasComponent);

        public ref TComponent TryGet<TComponent>(ComponentID componentID, in Entity entity, out bool hasComponent) where TComponent : struct, IComponent =>
            ref _componentsPool[componentID.value].TryGet<TComponent>(entity, out hasComponent);

        public ref TComponent Get<TComponent>(in Entity entity) where TComponent : struct, IComponent =>
             ref _componentCache[typeof(TComponent)].pool.Get<TComponent>(entity);
        public ref TComponent Get<TComponent>(ComponentID componentID, in Entity entity) where TComponent : struct, IComponent =>
            ref _componentsPool[componentID.value].Get<TComponent>(entity);

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

        private struct TrashComponent : IComponent { }

    }
}