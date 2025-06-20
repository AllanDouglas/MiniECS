namespace MiniECS
{
    public sealed class Game
    {
        public readonly EntityManager EntityManager;
        public readonly EventBus EventBus;
        public readonly ComponentsManager ComponentsManager;
        public readonly ArchetypeManager ArchetypeManager;

        public Game(int entityBufferSize = 100,
                    int componentsBufferSize = 100,
                    int componentsPoolSize = 100,
                    EventBus eventBus = null)
        {
            EntityManager = new(entityBufferSize);
            ArchetypeManager = new(entityBufferSize);
            ComponentsManager = new(componentsBufferSize, componentsPoolSize);
            EventBus = eventBus ?? new();
        }

        public void AddEntityPrototype(EntityController entityController)
        {
            Entity entity = EntityManager.AddEntityController(entityController);
            ComponentArchetype archetype = ComponentsManager.AddComponentPrototype(in entity, entityController.Components);
            entityController.Entity = entity;
            ArchetypeManager.Set(in entity, archetype);

        }

        public void RemoveComponent<TComponent>(in Entity entity)
            where TComponent : struct, IComponent
        {
            ComponentID componentId = ComponentsManager.RemoveComponent<TComponent>(in entity);
            ArchetypeManager.Set(entity, ArchetypeManager.Get(in entity) - componentId);
        }
    }
}