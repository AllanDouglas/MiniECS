using UnityEngine;

namespace MiniECS
{
    [RequireComponent(typeof(EntityPrototypeController))]
    public abstract class EntityBehaviour : MiniECSBehaviour
    {
        [SerializeField] private EntityPrototypeController _entityController;
        public EntityPrototypeController EntityController { get => _entityController; set => _entityController = value; }

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