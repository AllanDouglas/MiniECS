using UnityEngine;
using UnityEngine.Events;
namespace MiniECS
{
    public abstract class MessageListener<TMessage, TUnityEvent> : IMessageListener
        where TMessage : struct, IMessage
        where TUnityEvent : UnityEvent<TMessage>
    {
#if UNITY_EDITOR
        private MessageBus _bus;
        private GameObject _target;
#endif

        [SerializeField, DebugButtonAttribute("Dispatch")] public TUnityEvent _onPerform;

        public void Enable(GameObject gameObject, MessageBus bus)
        {
#if UNITY_EDITOR
            _bus = bus;
            _target = gameObject;
#endif
            bus.Subscribe<TMessage>(gameObject, Action);
        }

        public void Disable(GameObject gameObject, MessageBus bus)
        {
            bus.Unsubscribe<TMessage>(gameObject, Action);
        }

        public void Action(TMessage message) => _onPerform.Invoke(message);

#if UNITY_EDITOR
        public void Dispatch()
        {
            if (_bus != null && _target != null)
            {
                _bus.Dispatch<TMessage>(_target, new());
            }
        }
#endif
    }

}