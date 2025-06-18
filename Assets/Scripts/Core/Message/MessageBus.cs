using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniECS
{

    public sealed class MessageBus
    {
        public void Subscribe<TMessage>(GameObject listener, Action<TMessage> action)
            where TMessage : struct, IMessage
        {
            MessageStorage<TMessage>.Instance.Subscribe(listener, action);
        }

        public void Unsubscribe<T>(GameObject listener, Action<T> action)
            where T : struct, IMessage
        {
            MessageStorage<T>.Instance.Unsubscribe(listener, action);
        }

        public void Dispatch<T>(GameObject target, T message)
            where T : struct, IMessage
        {
            MessageStorage<T>.Instance.Dispatch(target, message);
        }
        public void Subscribe<T>(MiniECSBehaviour listener, Action<T> action)
            where T : struct, IMessage
        {
            if (listener != null)
                MessageStorage<T>.Instance.Subscribe(listener.gameObject, action);
        }

        public void Unsubscribe<T>(MiniECSBehaviour listener, Action<T> action)
            where T : struct, IMessage
        {
            if (listener != null)
                MessageStorage<T>.Instance.Unsubscribe(listener.gameObject, action);
        }

        public void Dispatch<T>(MiniECSBehaviour target, T message)
            where T : struct, IMessage
        {
            if (target != null)
                MessageStorage<T>.Instance.Dispatch(target.gameObject, message);
        }

        public void Subscribe<T>(MonoBehaviour listener, Action<T> action)
            where T : struct, IMessage
        {
            if (listener != null)
                MessageStorage<T>.Instance.Subscribe(listener.gameObject, action);
        }

        public void Unsubscribe<T>(MonoBehaviour listener, Action<T> action)
            where T : struct, IMessage
        {
            if (listener != null)
                MessageStorage<T>.Instance.Unsubscribe(listener.gameObject, action);
        }

        public void Dispatch<T>(MonoBehaviour target, T message)
            where T : struct, IMessage
        {
            if (target != null)
                MessageStorage<T>.Instance.Dispatch(target.gameObject, message);
        }


        private sealed class MessageStorage<TMessage> where TMessage : struct, IMessage
        {
            private readonly Dictionary<GameObject, List<WeakReference<Action<TMessage>>>> _listeners;
            private static MessageStorage<TMessage> _instance;
            public static MessageStorage<TMessage> Instance => _instance ??= new();
            public void Subscribe(GameObject listener, Action<TMessage> action)
            {
                if (_listeners.TryGetValue(listener, out var actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        if (!actions[i].TryGetTarget(out var target))
                        {
                            actions[i].SetTarget(action);
                            return;
                        }
                    }

                    actions.Add(new(action));
                    return;
                }

                _listeners.Add(listener, new() { new(action) });
            }

            public void Unsubscribe(GameObject listener, Action<TMessage> action)
            {
                if (_listeners.TryGetValue(listener, out var actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        if (actions[i].TryGetTarget(out var target) && target == action)
                        {
                            actions[i].SetTarget(null);
                        }
                    }
                }
            }

            public void Dispatch(GameObject target, TMessage message)
            {
                if (_listeners.TryGetValue(target, out var actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        if (actions[i].TryGetTarget(out var action))
                        {
                            action.Invoke(message);
                        }
                    }
                }
            }

        }

    }
}