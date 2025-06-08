using System;

namespace MiniECS
{

    public readonly struct FilterSystem<TComponent> where TComponent : struct, IComponent
    {
        public delegate void FilterSystemAction(FilterContext context, ref TComponent component);

        private readonly FilterSystemAction _action;

        public FilterSystem(FilterSystemAction action)
        {
            _action = action;
        }

        public void Execute(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndex.Length; i++)
            {
                var entityIndex = game.EntityManager.ActiveEntitiesIndex[i];
                Entity entity = game.EntityManager.Entities[entityIndex];
                ref TComponent component = ref game.ComponentsManager.TryGet<TComponent>(in entity, out bool hasComponent);
                if (hasComponent)
                {
                    _action.Invoke(new(game, entity, deltaTime), ref component);
                }
            }
        }
    }

    public readonly struct FilterSystem<TComponent1, TComponent2>
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
    {
        public delegate void FilterSystemAction(FilterContext context, ref TComponent1 component1, ref TComponent2 component2);

        private readonly FilterSystemAction _action;

        public FilterSystem(FilterSystemAction action)
        {
            _action = action;
        }

        public void Execute(Game game, float deltaTime, FilterSystemAction action)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndex.Length; i++)
            {
                var entityIndex = game.EntityManager.ActiveEntitiesIndex[i];
                Entity entity = game.EntityManager.Entities[entityIndex];
                ref TComponent1 component1 = ref game.ComponentsManager.TryGet<TComponent1>(in entity, out bool hasComponent1);
                ref TComponent2 component2 = ref game.ComponentsManager.TryGet<TComponent2>(in entity, out bool hasComponent2);
                if (hasComponent1 && hasComponent2)
                {
                    action.Invoke(new(game, entity, deltaTime), ref component1, ref component2);
                }
            }
        }
    }

    public readonly struct FilterSystem<TComponent1, TComponent2, TComponent3>
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
    {
        public delegate void FilterSystemAction(FilterContext context, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3);

        private readonly FilterSystemAction _action;

        public FilterSystem(FilterSystemAction action)
        {
            _action = action;
        }

        public void Execute(Game game, float deltaTime, FilterSystemAction action)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndex.Length; i++)
            {
                var entityIndex = game.EntityManager.ActiveEntitiesIndex[i];
                Entity entity = game.EntityManager.Entities[entityIndex];
                ref TComponent1 component1 = ref game.ComponentsManager.TryGet<TComponent1>(in entity, out bool hasComponent1);
                ref TComponent2 component2 = ref game.ComponentsManager.TryGet<TComponent2>(in entity, out bool hasComponent2);
                ref TComponent3 component3 = ref game.ComponentsManager.TryGet<TComponent3>(in entity, out bool hasComponent3);
                if (hasComponent1 && hasComponent2 && hasComponent3)
                {
                    action.Invoke(new(game, entity, deltaTime), ref component1, ref component2, ref component3);
                }
            }
        }
    }
}