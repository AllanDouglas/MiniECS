using System;

namespace MiniECS
{
    public interface IComponentPrototype
    {
        ref TComponent GetComponent<TComponent>();
        bool IsFromComponentType<TComponent>();
        ComponentID GetComponentID();
        
        void AddComponentToEntity(Archetype archetype, in Entity entity, int capacity = 4);
        
        void Bind(EntityPrototypeController entityController);
        void OnAdd(EntityPrototypeController entityController);
    }

    public interface IComponentPrototypeEditor
    {
        void OnDrawGizmos(EntityPrototypeController entityController);
        void OnValidate(EntityPrototypeController entityController);
    }

}