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
    }
}