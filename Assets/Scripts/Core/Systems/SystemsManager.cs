namespace MiniECS
{
    public sealed class SystemsManager
    {
        private readonly IUpdateSystem[] _systems;
        private readonly uint[] _systemEntitiesArchetypes;

        public SystemsManager(int entityBufferSize)
        {
            _systems = new IUpdateSystem[SystemIDProvider.Max];
            _systemEntitiesArchetypes = new uint[entityBufferSize];
        }

        public void Add(IUpdateSystem system) => _systems[system.ID] = system;
        public bool TryGet<TSYSTEM>(out TSYSTEM system) where TSYSTEM : IUpdateSystem
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                if (typeof(TSYSTEM) == _systems[i].GetType())
                {
                    system = (TSYSTEM)_systems[i];
                    return true;
                }
            }
            system = default;
            return false;
        }
        // public void BindEntity(in Entity entity, IUpdateSystem system) => _systemEntitiesArchetypes[entity.id] |= 1u << system.ID;
        // public void UnbindEntity(in Entity entity, IUpdateSystem system) => _systemEntitiesArchetypes[entity.id] ^= 1u << system.ID;
        // public void ClearEntity(in Entity entity) => _systemEntitiesArchetypes[entity.id] = 0;
    }
}