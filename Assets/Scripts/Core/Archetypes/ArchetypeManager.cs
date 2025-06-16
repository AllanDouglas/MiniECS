namespace MiniECS
{
    public sealed class ArchetypeManager
    {
        private readonly ComponentArchetype[] _archetypes;

        public ArchetypeManager(int _bufferSize)
        {
            _archetypes = new ComponentArchetype[_bufferSize];
        }

        public ComponentArchetype Get(in Entity entity) => _archetypes[entity.id];
        public void Set(in Entity entity, ComponentArchetype componentArchetype) => _archetypes[entity.id] = componentArchetype;
    }
}
