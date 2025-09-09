using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

namespace MiniECS
{

    public sealed class MessageBus
    {
        private interface IFlushable
        {
            void Flush();
            void Clear();
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

        public void Clear()
        {
            for (int i = 0; i < _flushers.Count; i++)
            {
                _flushers[i].Clear();
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

            // private readonly Dictionary<GameObject, List<Action<TMessage>>> _listeners = new();
            private readonly Dictionary<GameObject, ActionBuffer<TMessage>> _listeners = new();
            private readonly Queue<(GameObject target, TMessage message)> _messageQueue = new();

            public void Flush()
            {
#if UNITY_EDITOR
                if (Thread.CurrentThread.ManagedThreadId != MiniECSBehaviour.MainThreadIndex)
                {
                    throw new Exception("MessageBus.Flush() can only be called from the main thread.");
                }
#endif

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
                    actions.Add(action);
                    return;
                }

                ActionBuffer<TMessage> buffer = new();
                buffer.Add(action);

                _listeners.Add(listener, buffer);
            }

            public void Unsubscribe(GameObject listener, Action<TMessage> action)
            {
                if (_listeners.TryGetValue(listener, out var actions))
                {
                    if (actions.dispatching)
                    {
                        actions.lazyActions += () => LocalUnsubscribe(action, actions);
                        return;
                    }

                    LocalUnsubscribe(action, actions);

                }

                static bool LocalUnsubscribe(Action<TMessage> action, ActionBuffer<TMessage> actions)
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        if (actions[i] == action)
                        {
                            actions[i] = actions[^1];
                            actions.RemoveAt(actions.Count - 1);
                            return false;
                        }
                    }

                    return true;
                }
            }

            private void Dispatch(GameObject target, TMessage message)
            {
                if (_listeners.TryGetValue(target, out var actions))
                {
                    if (actions.dispatching)
                    {
                        actions.lazyActions += () => LocalDispatch(in message, actions);
                        return;
                    }

                    if (actions.Count <= 0)
                    {
                        return;
                    }

                    actions.dispatching = true;

                    LocalDispatch(in message, actions);

                    actions.dispatching = false;

                    actions.lazyActions?.Invoke();
                    actions.lazyActions = null;
                }

                static void LocalDispatch(in TMessage message, ActionBuffer<TMessage> actions)
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        if (actions.Count <= 0)
                        {
                            return;
                        }

                        actions[i].Invoke(message);
                    }

                    return;
                }
            }

            public void Clear()
            {
                _messageQueue.Clear();
                _listeners.Clear();
            }

            private class ActionBuffer<M> where M : struct, IMessage
            {
                public List<Action<M>> Actions;
                public bool dispatching;
                public Action lazyActions;
                public int Count => Actions?.Count ?? 0;

                public Action<M> this[int index]
                {
                    get => Actions[index];
                    set => Actions[index] = value;
                }

                public void RemoveAt(int index) => Actions.RemoveAt(index);

                internal void Add(Action<M> action)
                {
                    Actions ??= new List<Action<M>>();

                    Actions.Add(action);
                }
            }

        }

    }
}