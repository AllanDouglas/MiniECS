using System;

namespace MiniECS
{
    public interface IComponentPrototype
    {
        void AddToComponentPool(ComponentsManager componentsPool, int capacity = 4);
        void AddComponentToEntity(in Entity entity, ComponentsManager pool);
    }
}