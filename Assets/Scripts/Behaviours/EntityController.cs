#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace MiniECS
{

    public sealed class EntityController : MiniECSBehaviour
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif
    {
        [SerializeReference, ReferencePicker] private IComponentPrototype[] _components;
        public IComponentPrototype[] Components { get => _components; set => _components = value; }
        public Entity Entity { get; set; }

#if UNITY_EDITOR
        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
            }

            foreach (var item in Components)
            {
                if (item != null)
                {
                    item.Bind(this);
                }
            }
        }
        void OnValidate()
        {

        }

        void OnDrawGizmos()
        {
            foreach (var item in Components)
            {
                if (item != null)
                {
                    item.OnDrawGizmos(this);
                }
            }
        }


#endif

    }

}