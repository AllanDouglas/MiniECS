using System;
using UnityEngine;

namespace MiniECS
{
    [RequireComponent(typeof(EntityPrototypeController))]
    public abstract class EntityBehaviour : MiniECSBehaviour
    {
        [SerializeField] private EntityPrototypeController _entityController;
        public ECSManager ECSManager => _entityController != null ? _entityController.ECSManager : null;
        public EntityPrototypeController EntityController { get => _entityController; set => _entityController = value; }

        public event Action<ECSManager, EntityPrototypeController, Entity> OnJoined
        {
            add => _entityController.OnJoined += value;
            remove => _entityController.OnJoined -= value;
        }

        public TComponent GetECSComponent<TComponent>()
            where TComponent : struct, IComponent => EntityController.GetECSComponent<TComponent>();

        public ref TComponent TryGetECSComponent<TComponent>(out bool component)
            where TComponent : struct, IComponent => ref EntityController.TryGetECSComponent<TComponent>(out component);

        void OnValidate()
        {
            if (_entityController == null)
            {
                _entityController = gameObject.GetComponent<EntityPrototypeController>();
            }
        }

    }
}