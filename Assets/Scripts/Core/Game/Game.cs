namespace MiniECS
{
    public sealed class Game
    {
        public readonly EntityManager EntityManager;
        public readonly EventBus EventBus;
        public readonly ComponentsManager ComponentsManager;

        public Game(int entityBufferSize = 100, int componentsBufferSize = 100, EventBus eventBus = null)
        {
            EntityManager = new(entityBufferSize);
            ComponentsManager = new(componentsBufferSize);
            EventBus = eventBus ?? new();
        }

        public void AddEntityPrototype(EntityController entityController)
        {
            Entity entity = EntityManager.AddEntityController(entityController);
            ComponentsManager.AddComponentPrototype(in entity, entityController.Components);
        }

        public void RemoveComponent<TComponent>(in Entity entity) where TComponent : struct, IComponent
        {
            ComponentsManager.RemoveComponent<TComponent>(in entity);
        }
    }
}