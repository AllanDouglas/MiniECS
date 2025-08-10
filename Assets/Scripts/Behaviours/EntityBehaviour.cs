using UnityEngine;

namespace MiniECS
{
    [RequireComponent(typeof(EntityPrototypeController))]
    public abstract class EntityBehaviour : MiniECSBehaviour
    {
        [SerializeField] private EntityPrototypeController _entityController;
        public EntityPrototypeController EntityController { get => _entityController; set => _entityController = value; }

        void OnValidate()
        {
            if (_entityController == null)
            {
                _entityController = gameObject.GetComponent<EntityPrototypeController>();
            }
        }

    }
}