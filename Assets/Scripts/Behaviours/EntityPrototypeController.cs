#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.Events;

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
        [SerializeField] UnityEvent _onJoined = new();
        [SerializeField, ReadOnly] private EntityPrototypeController[] _children;

        private ECSManager _ecsManager;

        public IComponentPrototype[] Components { get => _components; set => _components = value; }
        public Entity Entity { get; set; } = Entity.Null;
        public ECSManager ECSManager
        {
            get => _ecsManager;
            set
            {
                if (_ecsManager is null)
                {
                    _ecsManager = value;
                    OnJoined?.Invoke(_ecsManager, this, Entity);
                    _onJoined.Invoke();
                }
            }
        }
        public EntityPrototypeController[] Children { get => _children; }

        public event Action<ECSManager, EntityPrototypeController, Entity> OnJoined;

        public void Recycle() => ECSManager?.Recycle(Entity);

        public void Deactivate(bool keepGameObjectActive = false)
        {
            ECSManager?.DeactivateEntity(Entity);
            gameObject.SetActive(keepGameObjectActive);
        }

        public void Active(bool keepGameObjectInactive = false)
        {
            ECSManager?.ActiveEntity(Entity);
            gameObject.SetActive(!keepGameObjectInactive);
        }

        public TComponent GetECSComponent<TComponent>() where TComponent : struct, IComponent
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                for (int i = 0; i < _components.Length; i++)
                {
                    if (_components[i].IsFromComponentType<TComponent>())
                    {
                        return _components[i].GetComponent<TComponent>();
                    }
                }

                return default;
            }
#endif

            return ECSManager.GetComponent<TComponent>(Entity);
        }

        public bool TryGetECSComponentPrototype<TComponent>(out TComponent component) where TComponent : struct, IComponent
        {
            for (int i = 0; i < _components.Length; i++)
            {
                if (_components[i].IsFromComponentType<TComponent>())
                {
                    component = _components[i].GetComponent<TComponent>();
                    return true;
                }
            }
            component = default;
            return false;
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
                return ref ECSManager.GetInvalidComponentRef<TComponent>();
            }
#endif
            hasComponent = false;

            if (ECSManager is null)
            {
                return ref ECSManager.GetInvalidComponentRef<TComponent>();
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

            if (!Application.isPlaying)
            {
                if (Components != null)
                {
                    foreach (var item in Components)
                    {
                        (item as IComponentPrototypeEditor)?.OnValidate(this);
                    }
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