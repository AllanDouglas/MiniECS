using UnityEngine;

namespace MiniECS
{
    [RequireComponent(typeof(EntityPrototypeController))]
    public abstract class EntityBehaviour : MiniECSBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private EntityPrototypeController _entityController;
        public EntityPrototypeController EntityController { get => _entityController; set => _entityController = value; }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (_entityController == null)
            {
                _entityController = GetComponent<EntityPrototypeController>();
            }
        }
    }
}