using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace MiniECS
{
    [Serializable]
    public abstract class ComponentPrototype<TComponent> : IComponentPrototype
        where TComponent : struct, IComponent
    {
        [SerializeField] protected TComponent _component;

        public ref TComponent Component => ref _component;

        public void AddComponentToEntity(in Entity entity, ComponentsManager pool) =>
            pool.GetComponentPool<TComponent>().Add(entity, _component);

        public ref T GetComponent<T>() => ref UnsafeUtility.As<TComponent, T>(ref _component);

        public ComponentID AddToComponentPool(ComponentsManager componentsPool, int capacity = 4)
        {
            if (!componentsPool.TryGetComponentPool<TComponent>(out _))
            {
                componentsPool.Add(new ComponentPool<TComponent>(capacity));
            }

            return componentsPool.GetComponentID<TComponent>();
        }

    }
}