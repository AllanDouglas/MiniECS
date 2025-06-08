using System;
using UnityEngine;

namespace MiniECS
{
    [Serializable]
    public abstract class ComponentPrototype<T> : IComponentPrototype
        where T : struct, IComponent
    {
        [SerializeField] protected T _component;

        public void AddComponentToEntity(in Entity entity, ComponentsManager pool)
        {
            pool.Get<T>().Add(entity, _component);
        }

        public void AddToComponentPool(ComponentsManager componentsPool, int capacity = 4)
        {
            if (!componentsPool.TryGet<T>(out _))
            {
                componentsPool.Add(new ComponentPool<T>(capacity));
            }
        }
    }
}