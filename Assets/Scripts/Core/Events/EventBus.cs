using System;
using System.Collections.Generic;

namespace MiniECS
{

    public sealed class EventBus
    {
        private interface IFlushable
        {
            public void Flush();
            void Clear();
        }

        private readonly List<IFlushable> _flushers = new();

        public void Subscribe<T>(Action<T> callback) where T : struct, IEvent
        {
            EventStorage<T>.GetInstance(_flushers).Subscribe(callback);
        }

        public void Unsubscribe<T>(Action<T> callback) where T : struct, IEvent
        {
            EventStorage<T>.GetInstance(_flushers).Unsubscribe(callback);
        }

        public void Dispatch<T>(T evt) where T : struct, IEvent
        {
            EventStorage<T>.GetInstance(_flushers).Dispatch(evt);
        }
        public void Dispatch<T>() where T : struct, IEvent
        {
            EventStorage<T>.GetInstance(_flushers).Dispatch(default);
        }

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

        private sealed class EventStorage<T> : IFlushable where T : struct, IEvent
        {
            public List<T> queue = new();
            public Action<T> listeners;

            private static EventStorage<T> _instance;
            public static EventStorage<T> GetInstance(List<IFlushable> flushables)
            {
                if (_instance is null)
                {
                    _instance = new();
                    flushables.Add(_instance);
                }

                return _instance;
            }
            public void Subscribe(Action<T> callback) => listeners += callback;
            public void Unsubscribe(Action<T> callback) => listeners -= callback;
            public void Dispatch(T evt) => queue.Add(evt);
            public void Flush()
            {
                if (listeners != null)
                {
                    for (int i = 0; i < queue.Count; i++)
                    {
                        listeners.Invoke(queue[i]);
                    }
                }

                queue.Clear();
            }

            public void Clear()
            {
                listeners = null;
            }
        }
    }
}