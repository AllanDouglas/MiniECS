#if UNITY_EDITOR
using System;
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
        [SerializeField, ReadOnly] private EntityController _parentEntity;
        public IComponentPrototype[] Components { get => _components; set => _components = value; }
        public Entity Entity { get; set; } = Entity.Null;
        public EntityController ParentEntity => _parentEntity;
        public Game Game { get; set; }

        public TComponent GetECSComponent<TComponent>() where TComponent : struct, IComponent
        {
            return Game.ComponentsManager.Get<TComponent>(Entity);
        }

#if UNITY_EDITOR

        void OnValidate()
        {
            var parentEntity = GetComponentInParent<EntityController>();
            _parentEntity = parentEntity != this ? parentEntity : null;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
            }

            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item?.Bind(this);
                }
            }
        }

        void OnDrawGizmos()
        {
            if (Components != null)
            {
                foreach (var item in Components)
                {
                    item?.OnDrawGizmos(this);
                }
            }
        }

#endif

    }

}