#if UNITY_EDITOR
using UnityEditor;
#endif
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
            return ECSManager.GetComponent<TComponent>(Entity);
        }

        public bool HasComponent<TComponent>()
            where TComponent : struct, IComponent
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                for (int i = 0; i < _components.Length; i++)
                {
                    if (_components[i].IsFromComponentType<TComponent>())
                    {
                        return true;
                    }
                }
                return false;
            }
#endif

            ref var component = ref ECSManager.TryGetComponent<TComponent>(Entity, out bool hasComponent);

            return hasComponent;

        }

        public ref TComponent TryGetECSComponent<TComponent>(out bool hasComponent) where TComponent : struct, IComponent
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                for (int i = 0; i < _components.Length; i++)
                {
                    if (_components[i].IsFromComponentType<TComponent>())
                    {
                        hasComponent = true;
                        return ref _components[i].GetComponent<TComponent>();
                    }
                }

                hasComponent = false;
                return ref ComponentsManager.GetInvalidRef<TComponent>();
            }
#endif


            hasComponent = false;

            if (ECSManager is null)
            {
                return ref ComponentsManager.GetInvalidRef<TComponent>();
            }

            ref var component = ref ECSManager.TryGetComponent<TComponent>(Entity, out hasComponent);

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
                    (item as IComponentPrototypeEditor)?.OnValidate(this);
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