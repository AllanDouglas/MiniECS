using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public delegate void EntityJoined(ECSManager ecs, EntityPrototypeController entityController);

    public sealed class ECSManager
    {

        private struct TrashComponent : IComponent { }
        private static TrashComponent _trashComponent = default;
        public static ref T GetInvalidComponentRef<T>() where T : struct, IComponent
                    => ref UnsafeUtility.As<TrashComponent, T>(ref _trashComponent);

        private static readonly MultiPool<EntityPrototypeController> _Pool = new();


        static ECSManager()
        {
            SceneManager.activeSceneChanged += (_, _) =>
            {
                _Pool.Clear();
            };
        }

        public event EntityJoined OnEnemyJoins;
        public readonly EntityManager EntityManager;
        public readonly SystemsManager SystemsManager;
        public readonly EventBus EventBus;
        public readonly MessageBus MessageBus;
        private readonly int _entityBufferSize;
        private readonly int _componentsBufferSize;
        public readonly ArchetypeManager ArchetypeManager;

        public ECSManager(int entityBufferSize = 100,
                    int componentsBufferSize = 100,
                    EventBus eventBus = null,
                    MessageBus messageBus = null)
        {
            EntityManager = new(entityBufferSize);
            ArchetypeManager = new(entityBufferSize);
            SystemsManager = new(entityBufferSize);
            EventBus = eventBus ?? new();
            MessageBus = messageBus ?? new();
            _entityBufferSize = entityBufferSize;
            _componentsBufferSize = componentsBufferSize;
        }

        public void AddEntityController(EntityPrototypeController entityController)
        {
            Entity entity = EntityManager.AddEntityController(entityController);

            ComponentArchetype componentArchetype = default;
            for (int i = 0; i < entityController.Components.Length; i++)
            {
                IComponentPrototype component = entityController.Components[i];
                component.Bind(entityController);
                componentArchetype += component.GetComponentID();
            }

            if (!ArchetypeManager.TryGetArchetype(componentArchetype, out Archetype archetype))
            {
                archetype = ArchetypeManager.CreateArchetype(_componentsBufferSize);
            }

            for (int i = 0; i < entityController.Components.Length; i++)
            {
                IComponentPrototype component = entityController.Components[i];
                component.AddComponentToEntity(archetype, entity, _entityBufferSize);
            }

            entityController.Entity = entity;
            entityController.ECSManager = this;
#if UNITY_EDITOR
            entityController.name = $"{entityController.name} - {entity}";
#endif

            OnEnemyJoins?.Invoke(this, entityController);
        }

        // public void RemoveComponent<TComponent>(in Entity entity)
        //     where TComponent : struct, IComponent
        // {
        //     ComponentID componentId = ComponentsManager.RemoveComponent<TComponent>(in entity);
        //     ArchetypeManager.Set(entity, ArchetypeManager.GetId(in entity) - componentId);
        // }
        public EntityPrototypeController GetPooledEntityInstance(EntityPrototypeController prefab, Action<EntityPrototypeController> onCreate = null)
        {
            EntityPrototypeController instance = _Pool.Get(prefab);
            if (instance.Entity == Entity.Null)
            {
                AddEntityController(instance);
                onCreate?.Invoke(instance);
            }
            else
            {
                EntityManager.Active(instance.Entity);
            }
            return instance;
        }

        public EntityBehaviour GetPooledEntityInstance(EntityBehaviour prefab, Action<EntityPrototypeController> onCreate = null)
        {
            EntityPrototypeController instance = _Pool.Get(prefab.EntityController);
            if (instance.Entity == Entity.Null)
            {
                AddEntityController(instance);
                onCreate?.Invoke(instance);
            }
            else
            {
                EntityManager.Active(instance.Entity);
            }

            return instance.GetComponent<EntityBehaviour>();
        }

        public T GetPooledEntityInstance<T>(T prefab)
            where T : EntityBehaviour
        {
            EntityPrototypeController instance = _Pool.Get(prefab.EntityController);
            if (instance.Entity == Entity.Null)
            {
                AddEntityController(instance);
            }
            else
            {
                EntityManager.Active(instance.Entity);
            }

            return instance.GetComponent<T>();
        }

        public EntityPrototypeController GetPooledEntityInstance(EntityPrototypeController prefab,
            Transform parent, Action<EntityPrototypeController> onCreate = null
            )
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab, onCreate);
            instance.transform.SetParent(parent);
            return instance;
        }

        public EntityPrototypeController GetPooledEntityInstance(
            EntityPrototypeController prefab,
            Vector3 position = default,
            Quaternion rotation = default,
            Action<EntityPrototypeController> onCreate = null)
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab, onCreate);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public EntityPrototypeController GetPooledEntityInstance(
            EntityPrototypeController prefab,
            Transform parent,
            Vector3 position,
            Quaternion rotation,
            Action<EntityPrototypeController> onCreate = null)
        {
            EntityPrototypeController instance = GetPooledEntityInstance(prefab, onCreate);
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

        public EntityPrototypeController GetEntityController(Entity entity) => EntityManager.GetEntityController(in entity);
    }
}