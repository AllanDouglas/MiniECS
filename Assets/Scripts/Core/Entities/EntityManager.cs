using System;

namespace MiniECS
{

    public sealed class EntityManager
    {
        private readonly Entity[] _entities;
        private readonly EntityPrototypeController[] _entityControllers;
        private readonly EntityAllocator _entityAllocator;
        private readonly int[] _activeEntities;
        private int _entityCount;
        private int _activeEntitiesCount;

        public EntityManager(int entityBufferSize = 4)
        {
            _entities = new Entity[entityBufferSize];
            _entityAllocator = new EntityAllocator(entityBufferSize);
            _entityControllers = new EntityPrototypeController[entityBufferSize];
            _activeEntities = new int[entityBufferSize];
        }

        public int EntityCount { get => _entityCount; private set => _entityCount = value; }
        public ReadOnlySpan<Entity> Entities => new(_entities);
        public ReadOnlySpan<int> ActiveEntitiesIndices => new(_activeEntities, 0, _activeEntitiesCount);
        public ReadOnlySpan<EntityPrototypeController> EntityControllers => new(_entityControllers);
        public EntityAllocator EntityAllocator => _entityAllocator;

        public ref Entity AddEntityController(EntityPrototypeController entityController)
        {
            Entity entity = _entityAllocator.Allocate();
            _entities[entity.id] = entity;
            _entityControllers[entity.id] = entityController;
            _entityCount++;

            if (entityController.gameObject.activeSelf)
            {
                _activeEntities[_activeEntitiesCount] = (int)entity.id;
                _activeEntitiesCount++;
            }

            return ref _entities[entity.id];
        }

        public void Deactivate(int index)
        {
            _activeEntities[index] = _activeEntities[_activeEntitiesCount - 1];
            _activeEntitiesCount--;
        }

        public void Deactivate(in Entity entity) => Deactivate(Array.IndexOf(_activeEntities, (int)entity.id));

        public void Active(in Entity entity)
        {
            _activeEntities[_activeEntitiesCount] = (int)entity.id;
            _activeEntitiesCount++;
        }

        public EntityPrototypeController GetEntityController(in Entity target) => _entityControllers[target.id];
    }
}