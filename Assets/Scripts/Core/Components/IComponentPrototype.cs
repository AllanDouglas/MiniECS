using System;

namespace MiniECS
{
    public interface IComponentPrototype
    {
        ref TComponent GetComponent<TComponent>();
        ComponentID AddToComponentPool(ComponentsManager componentsPool, int capacity = 4);
        void AddComponentToEntity(in Entity entity, ComponentsManager pool);
        void OnDrawGizmos(EntityController entityController);
        void Bind(EntityController entityController);
    }
}