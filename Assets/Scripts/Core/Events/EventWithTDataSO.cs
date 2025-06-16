using System;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using System.Diagnostics;
using System.Text;
#endif

namespace MiniECS
{
    public abstract class EventFieldSet<TEventSO, TUnityEvent>
        where TUnityEvent : UnityEvent
        where TEventSO : EventSO
    {
        [SerializeField] TEventSO _event;
        [SerializeField] TUnityEvent _action;

        private event Action ShadowEvent;

        public event Action Event
        {
            add => ShadowEvent += value;
            remove => ShadowEvent -= value;
        }

        public virtual void Subscribe(Action action) => ShadowEvent += action;
        public virtual void Unsubscribe(Action action) => ShadowEvent -= action;
        public virtual void Clear() => ShadowEvent = null;
        public virtual void Invoke()
        {
            _action?.Invoke();
            _event?.Dispatch();
            ShadowEvent?.Invoke();
        }
    }

    public abstract class EventField<TData, TUnityEvent>
        where TUnityEvent : UnityEvent<TData>
    {
        [SerializeField] TUnityEvent _action;

        private event Action<TData> ShadowEvent;

        public event Action<TData> Event
        {
            add => ShadowEvent += value;
            remove => ShadowEvent -= value;
        }

        public virtual void Subscribe(Action<TData> action) => ShadowEvent += action;
        public virtual void Unsubscribe(Action<TData> action) => ShadowEvent -= action;
        public virtual void Clear() => ShadowEvent = null;
        public virtual void Invoke(TData data)
        {
            _action?.Invoke(data);
            ShadowEvent?.Invoke(data);
        }
    }

    public abstract class EventSO<T, H> : EventSO<T>, IEvent<T>
        where H : UnityEvent<T>
    {
        [SerializeField] protected H onDispatchWithData;
        //event Action<T> OnDispatch;
        public override void Dispatch(T data)
        {
            if (onDispatchWithData != null)
            {
                onDispatchWithData.Invoke(data);
            }
            base.Dispatch(data);
        }

        //public override void Subscribe(Action<T> action) => OnDispatch += action;
        //public override void Unsubscribe(Action<T> action) => OnDispatch -= action;

        public override void RemoveActions()
        {
            if (onDispatchWithData != null)
            {
                onDispatchWithData.RemoveAllListeners();
            }

            base.RemoveActions();
        }
        private void OnDestroy() => RemoveActions();
        private void OnDisable() => RemoveActions();
    }

    public abstract class EventSO<T> : EventSO, IEvent<T>
    {
        event Action<T> OnDispatch;
        public virtual void Dispatch(T data)
        {
            OnDispatch?.Invoke(data);
            base.Dispatch();
        }

        public virtual void Subscribe(Action<T> action) => OnDispatch += action;
        public virtual void Unsubscribe(Action<T> action) => OnDispatch -= action;
        private void OnDestroy() => RemoveActions();
        public override void RemoveActions()
        {
            OnDispatch = null;
            base.RemoveActions();
        }

        private void OnDisable() => RemoveActions();
    }
}