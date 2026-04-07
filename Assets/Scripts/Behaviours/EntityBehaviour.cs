using System;
using UnityEngine;

namespace MiniECS
{
    public delegate void FinComponentAction<TComponent>(ref TComponent component)
        where TComponent : struct, IComponent;
    public delegate void FinComponentAction<TComponent, TTarget>(ref TComponent component, TTarget target)
        where TComponent : struct, IComponent
        where TTarget : class;
    public delegate void FinComponentAction<TComponent, TTarget, TTarget1>(ref TComponent component, TTarget target, TTarget1 target0)
        where TComponent : struct, IComponent
        where TTarget : class
        where TTarget1 : class;

    [RequireComponent(typeof(EntityPrototypeController))]
    [DefaultExecutionOrder(-51)]
    public abstract class EntityBehaviour : MiniECSBehaviour
    {
        [SerializeField, ReadOnly] private EntityPrototypeController _entityController;
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

        public ref TComponent TryGetECSComponent<TComponent, TTarget>(TTarget target, out bool hasComponent, FinComponentAction<TComponent, TTarget> onHasComponent)
            where TComponent : struct, IComponent
            where TTarget : class
        {
            ref var component = ref EntityController.TryGetECSComponent<TComponent>(out hasComponent);

            if (hasComponent)
            {
                onHasComponent?.Invoke(ref component, target);
            }

            return ref component;
        }

        public ref TComponent TryGetECSComponent<TComponent, TTarget, TTarget1>(
            TTarget target,
            TTarget1 target1,
            out bool hasComponent,
            FinComponentAction<TComponent, TTarget, TTarget1> onHasComponent) where TComponent : struct, IComponent
                                                                              where TTarget : class
                                                                              where TTarget1 : class

        {
            ref var component = ref EntityController.TryGetECSComponent<TComponent>(out hasComponent);

            if (hasComponent)
            {
                onHasComponent?.Invoke(ref component, target, target1);
            }

            return ref component;
        }

        public void ExecuteWith<TComponent>(

            FinComponentAction<TComponent> onHasComponent) where TComponent : struct, IComponent

        {
            ref var component = ref EntityController.TryGetECSComponent<TComponent>(out bool hasComponent);

            if (hasComponent)
            {
                onHasComponent?.Invoke(ref component);
            }
        }

        public void ExecuteWith<TComponent, TTarget>(
            TTarget target,
            FinComponentAction<TComponent, TTarget> onHasComponent) where TComponent : struct, IComponent
                                                                              where TTarget : class
        {
            ref var component = ref EntityController.TryGetECSComponent<TComponent>(out bool hasComponent);

            if (hasComponent)
            {
                onHasComponent?.Invoke(ref component, target);
            }
        }

        public void ExecuteWith<TComponent, TTarget, TTarget1>(
            TTarget target,
            TTarget1 target1,
            FinComponentAction<TComponent, TTarget, TTarget1> onHasComponent) where TComponent : struct, IComponent
                                                                              where TTarget : class
                                                                              where TTarget1 : class

        {
            ref var component = ref EntityController.TryGetECSComponent<TComponent>(out bool hasComponent);

            if (hasComponent)
            {
                onHasComponent?.Invoke(ref component, target, target1);
            }
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_entityController == null)
            {
                _entityController = gameObject.GetComponent<EntityPrototypeController>();
            }
        }
#endif

    }
}