using System;

namespace MiniECS
{

    public sealed partial class Archetype
    {
        private readonly Entity[] _entities;
        private readonly uint[] _sparseArray;
        private readonly IComponentPool[] _componentPools;
        private ComponentSet _componentSet;

        public ArchetypeId Id => new(_componentSet.id);

        public Archetype(uint initialCapacity, uint componentPoolCapacity = 4)
        {
            _sparseArray = new uint[initialCapacity];
            _entities = new Entity[initialCapacity];
            Array.Fill(_sparseArray, Entity.Null.id);
            _componentPools = new IComponentPool[componentPoolCapacity];
            _componentSet = default;
        }

        public Archetype(ComponentSet componentSet, uint initialCapacity, uint componentPoolCapacity = 4)
            : this(initialCapacity, componentPoolCapacity)
        {
            _componentSet = componentSet;
        }
        public ReadOnlySpan<Entity> Entities => new(_entities, 0, EntitiesCount);
        public int EntitiesCount { get; private set; }
        public int ComponentCount { get; private set; }

        public void Add<TComponent>(in Entity entity, TComponent component, int componentPoolCapacity = -1)
            where TComponent : struct, IComponent
        {
            if (!ContainsEntity(entity))
            {
                _entities[EntitiesCount] = entity;
                _sparseArray[entity.id] = (uint)EntitiesCount;
                EntitiesCount++;
            }

            if (!_componentSet.Contains<TComponent>())
            {
                _componentSet = _componentSet.Plus(ComponentIdHelper.GetID<TComponent>());
            }
            var id = ComponentIdHelper.GetID<TComponent>();
            var position = _componentSet.GetPositionOf<TComponent>();

            if (_componentPools[_componentSet.GetPositionOf<TComponent>()] is null)
            {
                _componentPools[_componentSet.GetPositionOf<TComponent>()] = new ComponentPool<TComponent>(_sparseArray, componentPoolCapacity);
                ComponentCount++;
            }

            _componentPools[_componentSet.GetPositionOf<TComponent>()].Add(entity, component);

        }

        public bool ContainsEntity(in Entity entity) => _sparseArray[entity.id] != Entity.Null.id;

        public void Remove(in Entity entity)
        {
            uint currentIndex = _sparseArray[entity.id];
            _entities[currentIndex] = _entities[EntitiesCount - 1];
            EntitiesCount--;

            for (int i = 0; i < _componentPools.Length; i++)
            {
                var componentPool = _componentPools[i];
                componentPool.Remove(entity);
            }

            _sparseArray[entity.id] = Entity.Null.id;
        }

        public bool HasComponent<TComponent>() where TComponent : struct, IComponent
            => _componentSet.Contains<TComponent>();

        public bool HasComponent(ComponentID componentID) => _componentSet.Contains(componentID);

        public ref TComponent GetComponent<TComponent>(in Entity entity)
           where TComponent : struct, IComponent 
            => ref _componentPools[_componentSet.GetPositionOf<TComponent>()].Get<TComponent>(entity);

        public ref TComponent TryGetComponent<TComponent>(in Entity entity, out bool hasComponent)
           where TComponent : struct, IComponent
        {
            return ref _componentPools[_componentSet.GetPositionOf<TComponent>()]
                .TryGet<TComponent>(entity, out hasComponent);
        }
    }
}