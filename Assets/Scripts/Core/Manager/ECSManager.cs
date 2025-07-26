using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public sealed class ECSManager
    {

        private static readonly MultiPool<EntityController> _Pool = new();

        static ECSManager()
        {
            SceneManager.activeSceneChanged += (_, _) =>
            {
                _Pool.Clear();
            };
        }

        public readonly EntityManager EntityManager;
        public readonly ComponentsManager ComponentsManager;
        public readonly SystemsManager SystemsManager;
        public readonly EventBus EventBus;
        public readonly MessageBus MessageBus;
        public readonly ArchetypeManager ArchetypeManager;

        public ECSManager(int entityBufferSize = 100,
                    int componentsBufferSize = 100,
                    EventBus eventBus = null,
                    MessageBus messageBus = null)
        {
            EntityManager = new(entityBufferSize);
            ArchetypeManager = new(entityBufferSize);
            SystemsManager = new(entityBufferSize);
            ComponentsManager = new(componentsBufferSize, entityBufferSize);
            EventBus = eventBus ?? new();
            MessageBus = messageBus ?? new();
        }

        public void AddEntityController(EntityController entityController)
        {
            Entity entity = EntityManager.AddEntityController(entityController);
            foreach (var item in entityController.Components)
            {
                item.Bind(entityController);
            }
            ComponentArchetype archetype = ComponentsManager.AddComponentPrototype(in entity, entityController.Components);
            entityController.Entity = entity;
            entityController.ECSManager = this;
            ArchetypeManager.Set(in entity, componentArchetype: archetype);

#if UNITY_EDITOR
            entityController.name = $"{entityController.name} - {entity}";
#endif
        }

        public void RemoveComponent<TComponent>(in Entity entity)
            where TComponent : struct, IComponent
        {
            ComponentID componentId = ComponentsManager.RemoveComponent<TComponent>(in entity);
            ArchetypeManager.Set(entity, ArchetypeManager.Get(in entity) - componentId);
        }
        public EntityController GetPooledInstance(EntityController prefab)
        {
            EntityController instance = _Pool.Get(prefab);
            if (instance.Entity == Entity.Null)
            {
                AddEntityController(instance);
            }
            else
            {
                EntityManager.Active(instance.Entity);
            }
            return instance;
        }

        public EntityController GetPooledInstance(EntityController prefab,
            Transform parent)
        {
            EntityController instance = GetPooledInstance(prefab);
            instance.transform.SetParent(parent);
            return instance;
        }

        public EntityController GetPooledInstance(
            EntityController prefab,
            Vector3 position = default,
            Quaternion rotation = default)
        {
            EntityController instance = GetPooledInstance(prefab);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public EntityController GetPooledInstance(
            EntityController prefab,
            Transform parent,
            Vector3 position,
            Quaternion rotation)
        {
            EntityController instance = GetPooledInstance(prefab);
            instance.transform.SetParent(parent);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public void Recycle(in Entity entity)
        {
            var controller = EntityManager.EntityControllers[(int)entity.id];
            _Pool.Release(controller);
            controller.transform.SetParent(null);
            controller.gameObject.SetActive(false);
            EntityManager.Deactivate(entity);
        }

    }
}