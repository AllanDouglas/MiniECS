using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public sealed class ECSManager
    {

        private static readonly MultiPool<EntityPrototypeController> _Pool = new();

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

        public void AddEntityController(EntityPrototypeController entityController)
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
        public EntityPrototypeController GetPooledEntityInstance(EntityPrototypeController prefab)
        {
            EntityPrototypeController instance = _Pool.Get(prefab);
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

        public EntityPrototypeController GetPooledEntityInstance(EntityPrototypeController prefab,
            Transform parent)
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab);
            instance.transform.SetParent(parent);
            return instance;
        }

        public EntityPrototypeController GetPooledEntityInstance(
            EntityPrototypeController prefab,
            Vector3 position = default,
            Quaternion rotation = default)
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public EntityPrototypeController GetPooledEntityInstance(
            EntityPrototypeController prefab,
            Transform parent,
            Vector3 position,
            Quaternion rotation)
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab);
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