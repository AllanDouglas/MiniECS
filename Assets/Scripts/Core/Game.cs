namespace MiniECS
{
    public sealed class Game
    {
        public readonly EntityManager EntityManager;
        public readonly ComponentsManager ComponentsManager;

        public Game(int entityBufferSize = 100, int componentsBufferSize = 100)
        {
            EntityManager = new(entityBufferSize);
            ComponentsManager = new(componentsBufferSize);
        }
    }
}