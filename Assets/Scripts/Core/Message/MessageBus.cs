using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniECS
{

    public sealed class MessageBus
    {
        private interface IFlushable
        {
            public void Flush();
        }

        private static readonly List<IFlushable> _flushers = new();

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
            MessageStorage<T>.Instance.Enqueue(target, message);
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
                MessageStorage<T>.Instance.Enqueue(target.gameObject, message);
        }

        public void Dispatch<T>(MiniECSBehaviour target)
            where T : struct, IMessage
        {
            if (target != null)
                MessageStorage<T>.Instance.Enqueue(target.gameObject, default);
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
                MessageStorage<T>.Instance.Enqueue(target.gameObject, message);
        }

        public void Flush<T>() where T : struct, IMessage
            => MessageStorage<T>.Instance.Flush();

        public void FlushAll()
        {
            for (int i = 0; i < _flushers.Count; i++)
            {
                _flushers[i].Flush();
            }
        }

        private sealed class MessageStorage<TMessage> : IFlushable where TMessage : struct, IMessage
        {
            private static MessageStorage<TMessage> _instance;
            public static MessageStorage<TMessage> Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new();
                        _flushers.Add(_instance);
                    }

                    return _instance;
                }
            }

            private readonly Dictionary<GameObject, List<Action<TMessage>>> _listeners = new();
            private readonly Queue<(GameObject target, TMessage message)> _messageQueue = new();

            public void Flush()
            {
                while (_messageQueue.Count > 0)
                {
                    var (target, message) = _messageQueue.Dequeue();
                    Dispatch(target, message);
                }
            }

            public void Enqueue(GameObject target, TMessage message)
            {
                _messageQueue.Enqueue((target, message));
            }

            public void Subscribe(GameObject listener, Action<TMessage> action)
            {
                if (_listeners.TryGetValue(listener, out var actions))
                {
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
                        if (actions[i] == action)
                        {
                            actions[i] = actions[^1];
                            actions.RemoveAt(actions.Count - 1);
                            return;
                        }
                    }
                }
            }

            private void Dispatch(GameObject target, TMessage message)
            {
                if (_listeners.TryGetValue(target, out var actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        actions[i].Invoke(message);
                    }
                }
            }

        }

    }
}