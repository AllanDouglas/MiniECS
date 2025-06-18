using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiniECS
{
    public delegate void MessageHandlerDelegate<T>(in T evtContext) where T : struct, IMessage;
    public delegate void MessageHandlerDelegate();

    public sealed class MessageBroker
    {
        private readonly Dictionary<GameObject, EventHashMap> _eventsContextDict;

        public MessageBroker(int initialCapacity = 32)
        {
            _eventsContextDict = new(initialCapacity);
        }

        public void Subscribe<TEvent>(GameObject listenerContext, MessageHandlerDelegate<TEvent> handler)
            where TEvent : struct, IMessage
        {
            Subscribe(new TEvent().Id, listenerContext, handler);
        }

        public void Subscribe(int eventId, GameObject listenerContext, MessageHandlerDelegate handler)
        {
            Assert.IsNotNull(handler);

            if (!_eventsContextDict.TryGetValue(listenerContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(listenerContext, eventsDict);
            }


            if (eventsDict.TryGetValue(eventId, out var eventHandler))
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                action += handler;
            }
            else
            {
                eventsDict.Add(eventId, new(eventId)
                {
                    Handler = UnsafeUtility.As<MessageHandlerDelegate, object>(ref handler)
                });
            }
        }

        public void Subscribe<TEvent>(int eventId, GameObject listenerContext, MessageHandlerDelegate<TEvent> handler)
            where TEvent : struct, IMessage
        {
            Assert.IsNotNull(handler);

            if (!_eventsContextDict.TryGetValue(listenerContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(listenerContext, eventsDict);
            }


            if (eventsDict.TryGetValue(eventId, out var eventHandler))
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate<TEvent>>(ref eventHandler.HandlerWithParam);
                action += handler;
            }
            else
            {
                eventsDict.Add(eventId, new(eventId)
                {
                    HandlerWithParam = UnsafeUtility.As<MessageHandlerDelegate<TEvent>, object>(ref handler)
                });
            }
        }

        public void Unsubscribe(int eventId, GameObject listenerContext, MessageHandlerDelegate handler)
        {
            Assert.IsNotNull(handler);

            if (!_eventsContextDict.TryGetValue(listenerContext, out var eventsDict))
            {
                return;
            }


            if (eventsDict.TryGetValue(eventId, out var eventHandler))
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                action -= handler;
                eventsDict.Remove(eventId);
            }
        }

        public void Unsubscribe<TEvent>(GameObject listenerContext, MessageHandlerDelegate<TEvent> handler)
            where TEvent : struct, IMessage
        {
            Assert.IsNotNull(handler);

            if (!_eventsContextDict.TryGetValue(listenerContext, out EventHashMap eventsDict))
            {
                return;
            }

            TEvent evt = new TEvent();

            if (eventsDict.TryGetValue(evt.Id, out var eventHandler))
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate<TEvent>>(ref eventHandler.HandlerWithParam);

                action -= handler;

                eventsDict.Remove(evt.Id);

            }
        }

        public void Dispatch<T>(GameObject targetContext, T evtContext) where T : struct, IMessage
        {
            Assert.IsNotNull(targetContext);

            if (!_eventsContextDict.TryGetValue(targetContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(targetContext, eventsDict);
            }

            if (eventsDict.TryGetValue(evtContext.Id, out var eventHandler))
            {
                if (eventHandler.Handler is not null)
                {
                    var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                    action.Invoke();
                }

                if (eventHandler.HandlerWithParam is not null)
                {
                    var action = UnsafeUtility.As<object, MessageHandlerDelegate<T>>(ref eventHandler.HandlerWithParam);
                    action.Invoke(in evtContext);
                }
            }
        }

        public void Dispatch<T>(MiniECSBehaviour target) where T : struct, IMessage => Dispatch<T>(target.gameObject);
        public void Dispatch(IMessage evtContext, GameObject targetContext)
        {
            Assert.IsNotNull(targetContext);

            if (!_eventsContextDict.TryGetValue(targetContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(targetContext, eventsDict);
            }

            if (eventsDict.TryGetValue(evtContext.Id, out var eventHandler))
            {
                if (eventHandler.Handler is not null)
                {
                    var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                    action.Invoke();
                }
            }
        }
        public void Dispatch(IMessage eventContext, MonoBehaviour target) => Dispatch(eventContext, target.gameObject);
        public void Dispatch(IMessage eventContext, MiniECSBehaviour target) => Dispatch(eventContext, target.gameObject);
        public void Dispatch<T>(MonoBehaviour target) where T : struct, IMessage => Dispatch<T>(target.gameObject);
        public void Dispatch<T>(Transform target) where T : struct, IMessage => Dispatch<T>(target.gameObject);

        public void Dispatch<T>(GameObject targetContext) where T : struct, IMessage
        {
            Assert.IsNotNull(targetContext);
            var eventType = default(T);

            if (!_eventsContextDict.TryGetValue(targetContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(targetContext, eventsDict);
            }

            if (!eventsDict.TryGetValue(eventType.Id, out var eventHandler))
            {
                return;
            }

            if (eventHandler.Handler is not null)
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                action.Invoke();
            }

            if (eventHandler.HandlerWithParam is not null)
            {
                var action = UnsafeUtility.As<object, MessageHandlerDelegate<T>>(ref eventHandler.HandlerWithParam);
                action.Invoke(in eventType);
            }

        }

        public void Dispatch(int eventId, GameObject targetContext)
        {
            Assert.IsNotNull(targetContext);

            if (!_eventsContextDict.TryGetValue(targetContext, out var eventsDict))
            {
                eventsDict = new();
                _eventsContextDict.Add(targetContext, eventsDict);
            }

            if (eventsDict.TryGetValue(eventId, out var eventHandler))
            {
                if (eventHandler.Handler is not null)
                {
                    var action = UnsafeUtility.As<object, MessageHandlerDelegate>(ref eventHandler.Handler);
                    action.Invoke();
                }
            }
        }

        private sealed class EventHashMap
        {
            private readonly Dictionary<int, MessageHandler> _eventHandlers = new();

            public int Count => _eventHandlers.Count;

            public MessageHandler this[int index]
            {
                get { return _eventHandlers[index]; }
                set { _eventHandlers[index] = value; }
            }

            public void Add(int eventId, MessageHandler handler) => _eventHandlers.Add(eventId, handler);
            public void Remove(int eventId) => _eventHandlers.Remove(eventId);
            public bool TryGetValue(int eventId, out MessageHandler eventHandler) => _eventHandlers.TryGetValue(eventId, out eventHandler);
            public bool ContainsKey(int eventId) => _eventHandlers.ContainsKey(eventId);

        }

        private sealed class MessageHandler
        {
            public readonly int EventId;
            public object HandlerWithParam;
            public object Handler;

            public MessageHandler(int eventId)
            {
                EventId = eventId;
                HandlerWithParam = null;
                Handler = null;
            }

        }
    }
}