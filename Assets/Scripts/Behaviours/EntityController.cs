#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;

namespace MiniECS
{

    public sealed class EntityController : MiniECSBehaviour
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif
    {
        [SerializeReference, ReferencePicker] private IComponentPrototype[] _components;
        [SerializeField, ReadOnly] private EntityController[] _children;
        public IComponentPrototype[] Components { get => _components; set => _components = value; }
        public Entity Entity { get; set; } = Entity.Null;
        public ECSManager ECSManager { get; set; }
        public EntityController[] Children { get => _children; }

        public void Recycle()
        {
            ECSManager?.Recycle(Entity);
        }

        public TComponent GetECSComponent<TComponent>() where TComponent : struct, IComponent
        {
            return ECSManager.ComponentsManager.Get<TComponent>(Entity);
        }

        public ref TComponent TryGetECSComponent<TComponent>(out bool hasComponent) where TComponent : struct, IComponent
        {
            hasComponent = false;

            if (ECSManager is null)
            {
                return ref UnsafeUtility.As<IComponent, TComponent>(ref ComponentsManager.Trash);
            }

            ref var component = ref ECSManager.ComponentsManager.TryGet<TComponent>(Entity, out hasComponent);

            return ref component;
        }

#if UNITY_EDITOR

        void OnValidate()
        {
            _children = GetComponentsInChildren<EntityController>().Where(e => e != this).ToArray();
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

        void OnDrawGizmosSelected()
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