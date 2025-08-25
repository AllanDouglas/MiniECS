#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;
using UnityEngine.Scripting.APIUpdating;

namespace MiniECS
{

    [MovedFrom(true, sourceClassName: "ECSController")]
    [DisallowMultipleComponent]
    public sealed class EntityPrototypeController : MiniECSBehaviour
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif
    {
        [SerializeReference, ReferencePicker] private IComponentPrototype[] _components;
        [SerializeField, ReadOnly] private EntityPrototypeController[] _children;
        public IComponentPrototype[] Components { get => _components; set => _components = value; }
        public Entity Entity { get; set; } = Entity.Null;
        public ECSManager ECSManager { get; set; }
        public EntityPrototypeController[] Children { get => _children; }

        public void Recycle() => ECSManager?.Recycle(Entity);

        public void Deactivate(bool keepGameObjectActive = false)
        {
            ECSManager?.DeactivateEntity(Entity);
            gameObject.SetActive(keepGameObjectActive);
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
            _children = GetComponentsInChildren<EntityPrototypeController>().Where(e => e != this).ToArray();

            if (Components != null)
            {
                foreach (var item in Components)
                {
                    if (item is IComponentPrototypeEditor prototypeEditor)
                        prototypeEditor?.OnValidate(this);
                }
            }
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
                    if (item is IComponentPrototypeEditor prototypeEditor)
                        prototypeEditor?.OnDrawGizmos(this);
                }
            }
        }


#endif

    }

}