namespace MiniECS
{
    public abstract class UpdateSystem<TComponent>
        where TComponent : struct, IComponent
    {

        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _archetype = new(_componentID);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component);
    }

    public abstract class UpdateSystem<TComponent, TComponent1>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
    {

        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1;


        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _archetype = new(_componentID | _componentID1);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent1>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _archetype = new(_componentID | _componentID1 | _componentID2);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4, _componentID5;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _componentID5 = game.ComponentsManager.GetComponentID<TComponent5>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4 | _componentID5);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    ref TComponent5 component5 = ref game.ComponentsManager.GetComponentPool(_componentID5).Get<TComponent5>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4, _componentID5, _componentID6;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _componentID5 = game.ComponentsManager.GetComponentID<TComponent5>();
            _componentID6 = game.ComponentsManager.GetComponentID<TComponent6>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4 | _componentID5 | _componentID6);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    ref TComponent5 component5 = ref game.ComponentsManager.GetComponentPool(_componentID5).Get<TComponent5>(in entity);
                    ref TComponent6 component6 = ref game.ComponentsManager.GetComponentPool(_componentID6).Get<TComponent6>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
        where TComponent7 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4, _componentID5, _componentID6, _componentID7;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _componentID5 = game.ComponentsManager.GetComponentID<TComponent5>();
            _componentID6 = game.ComponentsManager.GetComponentID<TComponent6>();
            _componentID7 = game.ComponentsManager.GetComponentID<TComponent7>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4 | _componentID5 | _componentID6 | _componentID7);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    ref TComponent5 component5 = ref game.ComponentsManager.GetComponentPool(_componentID5).Get<TComponent5>(in entity);
                    ref TComponent6 component6 = ref game.ComponentsManager.GetComponentPool(_componentID6).Get<TComponent6>(in entity);
                    ref TComponent7 component7 = ref game.ComponentsManager.GetComponentPool(_componentID7).Get<TComponent7>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
        where TComponent7 : struct, IComponent
        where TComponent8 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4, _componentID5, _componentID6, _componentID7, _componentID8;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _componentID5 = game.ComponentsManager.GetComponentID<TComponent5>();
            _componentID6 = game.ComponentsManager.GetComponentID<TComponent6>();
            _componentID7 = game.ComponentsManager.GetComponentID<TComponent7>();
            _componentID8 = game.ComponentsManager.GetComponentID<TComponent8>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4 | _componentID5 | _componentID6 | _componentID7 | _componentID8);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    ref TComponent5 component5 = ref game.ComponentsManager.GetComponentPool(_componentID5).Get<TComponent5>(in entity);
                    ref TComponent6 component6 = ref game.ComponentsManager.GetComponentPool(_componentID6).Get<TComponent6>(in entity);
                    ref TComponent7 component7 = ref game.ComponentsManager.GetComponentPool(_componentID7).Get<TComponent7>(in entity);
                    ref TComponent8 component8 = ref game.ComponentsManager.GetComponentPool(_componentID8).Get<TComponent8>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7, ref TComponent8 component8);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
        where TComponent7 : struct, IComponent
        where TComponent8 : struct, IComponent
        where TComponent9 : struct, IComponent
    {
        private readonly ComponentArchetype _archetype;
        private readonly ComponentID _componentID, _componentID1, _componentID2, _componentID3, _componentID4, _componentID5, _componentID6, _componentID7, _componentID8, _componentID9;

        public UpdateSystem(Game game)
        {
            _componentID = game.ComponentsManager.GetComponentID<TComponent>();
            _componentID1 = game.ComponentsManager.GetComponentID<TComponent1>();
            _componentID2 = game.ComponentsManager.GetComponentID<TComponent2>();
            _componentID3 = game.ComponentsManager.GetComponentID<TComponent3>();
            _componentID4 = game.ComponentsManager.GetComponentID<TComponent4>();
            _componentID5 = game.ComponentsManager.GetComponentID<TComponent5>();
            _componentID6 = game.ComponentsManager.GetComponentID<TComponent6>();
            _componentID7 = game.ComponentsManager.GetComponentID<TComponent7>();
            _componentID8 = game.ComponentsManager.GetComponentID<TComponent8>();
            _componentID9 = game.ComponentsManager.GetComponentID<TComponent9>();
            _archetype = new(_componentID | _componentID1 | _componentID2 | _componentID3 | _componentID4 | _componentID5 | _componentID6 | _componentID7 | _componentID8 | _componentID9);
        }

        public void Update(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndies.Length; i++)
            {
                int entityIndex = game.EntityManager.ActiveEntitiesIndies[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ComponentArchetype entityArchetype = game.ArchetypeManager.Get(in entity);

                if (_archetype == entityArchetype)
                {
                    ref TComponent component = ref game.ComponentsManager.GetComponentPool(_componentID).Get<TComponent>(in entity);
                    ref TComponent1 component1 = ref game.ComponentsManager.GetComponentPool(_componentID1).Get<TComponent1>(in entity);
                    ref TComponent2 component2 = ref game.ComponentsManager.GetComponentPool(_componentID2).Get<TComponent2>(in entity);
                    ref TComponent3 component3 = ref game.ComponentsManager.GetComponentPool(_componentID3).Get<TComponent3>(in entity);
                    ref TComponent4 component4 = ref game.ComponentsManager.GetComponentPool(_componentID4).Get<TComponent4>(in entity);
                    ref TComponent5 component5 = ref game.ComponentsManager.GetComponentPool(_componentID5).Get<TComponent5>(in entity);
                    ref TComponent6 component6 = ref game.ComponentsManager.GetComponentPool(_componentID6).Get<TComponent6>(in entity);
                    ref TComponent7 component7 = ref game.ComponentsManager.GetComponentPool(_componentID7).Get<TComponent7>(in entity);
                    ref TComponent8 component8 = ref game.ComponentsManager.GetComponentPool(_componentID8).Get<TComponent8>(in entity);
                    ref TComponent9 component9 = ref game.ComponentsManager.GetComponentPool(_componentID9).Get<TComponent9>(in entity);
                    OnUpdate(new(game, entity, deltaTime), ref component, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
                }
            }
        }
        protected abstract void OnUpdate(FilterContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7, ref TComponent8 component8, ref TComponent9 component9);
    }
}