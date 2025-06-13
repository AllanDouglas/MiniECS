using System;

namespace MiniECS
{
    public interface IComponentPrototype
    {
        ref TComponent GetComponent<TComponent>();
        int AddToComponentPool(ComponentsManager componentsPool, int capacity = 4);
        void AddComponentToEntity(in Entity entity, ComponentsManager pool);
    }
}