using System;

namespace MiniECS
{
    public readonly struct ComponentFilter<TComponent>
        where TComponent : struct, IComponent
    {
        public readonly ComponentArchetype archetype;
        public readonly ComponentID componentID;

        public ComponentFilter(ComponentID componentID)
        {
            this.componentID = componentID;
            archetype = new(componentID);
        }

        public readonly IComponentPool GetComponentPool(ECSManager game) => game.ComponentsManager.GetComponentPool(componentID);

    }
}