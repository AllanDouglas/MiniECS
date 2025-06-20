using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public sealed class Game
    {

        private static readonly MultPool<EntityController> _Pool = new();

        static Game()
        {
            SceneManager.activeSceneChanged += (_, _) =>
            {
                _Pool.Clear();
            };
        }

        public readonly EntityManager EntityManager;
        public readonly EventBus EventBus;
        public readonly ComponentsManager ComponentsManager;
        public readonly ArchetypeManager ArchetypeManager;

        public Game(int entityBufferSize = 100,
                    int componentsBufferSize = 100,
                    EventBus eventBus = null)
        {
            EntityManager = new(entityBufferSize);
            ArchetypeManager = new(entityBufferSize);
            ComponentsManager = new(componentsBufferSize, entityBufferSize);
            EventBus = eventBus ?? new();
        }

        public void AddEntityPrototype(EntityController entityController)
        {
            Entity entity = EntityManager.AddEntityController(entityController);
            ComponentArchetype archetype = ComponentsManager.AddComponentPrototype(in entity, entityController.Components);
            entityController.Entity = entity;
            ArchetypeManager.Set(in entity, archetype);

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
                AddEntityPrototype(instance);
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