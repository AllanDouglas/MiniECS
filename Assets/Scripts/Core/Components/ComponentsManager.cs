using System;
using System.Collections.Generic;

namespace MiniECS
{
    public sealed class ComponentsManager
    {
        private readonly Dictionary<Type, IComponentPool> _componentCache;
        private readonly int _sizeBuffer;

        public ComponentsManager(int sizeBuffer)
        {
            _componentCache = new(sizeBuffer);
            _sizeBuffer = sizeBuffer;
        }

        public void AddComponentPrototype(in Entity entity, IComponentPrototype[] componentPrototypes)
        {
            for (int i = 0; i < componentPrototypes.Length; i++)
            {
                componentPrototypes[i].AddToComponentPool(this, _sizeBuffer);
                componentPrototypes[i].AddComponentToEntity(entity, this);
            }
        }

        public void Add(IComponentPool componentPool)
        {
            _componentCache.TryAdd(componentPool.ComponentType, componentPool);
        }

        public IComponentPool Get<TComponent>() where TComponent : struct, IComponent
        {
            return _componentCache[typeof(TComponent)];
        }

        public bool TryGet<TComponent>(out IComponentPool componentPool)
            where TComponent : struct, IComponent
        {
            return _componentCache.TryGetValue(typeof(TComponent), out componentPool);
        }

        public ref TComponent TryGet<TComponent>(in Entity entity, out bool hasComponent)
           where TComponent : struct, IComponent
        {
            return ref _componentCache[typeof(TComponent)].TryGet<TComponent>(entity, out hasComponent);

        }

    }
}