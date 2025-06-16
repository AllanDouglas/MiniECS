using System;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

namespace MiniECS
{
    public abstract class EventFieldSet<TData, TEventSO, TUnityEvent>
        where TUnityEvent : UnityEvent<TData>
        where TEventSO : EventSO<TData, TUnityEvent>
    {
        [SerializeField] TEventSO _event;
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
            _event?.Dispatch(data);
            ShadowEvent?.Invoke(data);
        }
    }
}