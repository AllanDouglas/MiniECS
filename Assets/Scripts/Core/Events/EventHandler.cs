namespace MiniECS
{
    public abstract class EventHandler<TEvent> : IEventHandler
        where TEvent : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public void Subscribe(ECSManager ecs) => ecs.EventBus.Subscribe<TEvent>(Handler);
        public void Unsubscribe(ECSManager ecs) => ecs.EventBus.Unsubscribe<TEvent>(Handler);
    }

    public abstract class EventHandler<TEvent, TEvent1> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
        }
    }
    public abstract class EventHandler<TEvent, TEvent1, TEvent2> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
        where TEvent5 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public abstract void Handler(ECSManager ecs, TEvent5 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
            ecs.EventBus.Subscribe<TEvent5>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
            ecs.EventBus.Unsubscribe<TEvent5>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
        where TEvent5 : struct, IEvent
        where TEvent6 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public abstract void Handler(ECSManager ecs, TEvent5 evt);
        public abstract void Handler(ECSManager ecs, TEvent6 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
            ecs.EventBus.Subscribe<TEvent5>(Handler);
            ecs.EventBus.Subscribe<TEvent6>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
            ecs.EventBus.Unsubscribe<TEvent5>(Handler);
            ecs.EventBus.Unsubscribe<TEvent6>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6, TEvent7> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
        where TEvent5 : struct, IEvent
        where TEvent6 : struct, IEvent
        where TEvent7 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public abstract void Handler(ECSManager ecs, TEvent5 evt);
        public abstract void Handler(ECSManager ecs, TEvent6 evt);
        public abstract void Handler(ECSManager ecs, TEvent7 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
            ecs.EventBus.Subscribe<TEvent5>(Handler);
            ecs.EventBus.Subscribe<TEvent6>(Handler);
            ecs.EventBus.Subscribe<TEvent7>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
            ecs.EventBus.Unsubscribe<TEvent5>(Handler);
            ecs.EventBus.Unsubscribe<TEvent6>(Handler);
            ecs.EventBus.Unsubscribe<TEvent7>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6, TEvent7, TEvent8> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
        where TEvent5 : struct, IEvent
        where TEvent6 : struct, IEvent
        where TEvent7 : struct, IEvent
        where TEvent8 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public abstract void Handler(ECSManager ecs, TEvent5 evt);
        public abstract void Handler(ECSManager ecs, TEvent6 evt);
        public abstract void Handler(ECSManager ecs, TEvent7 evt);
        public abstract void Handler(ECSManager ecs, TEvent8 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
            ecs.EventBus.Subscribe<TEvent5>(Handler);
            ecs.EventBus.Subscribe<TEvent6>(Handler);
            ecs.EventBus.Subscribe<TEvent7>(Handler);
            ecs.EventBus.Subscribe<TEvent8>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
            ecs.EventBus.Unsubscribe<TEvent5>(Handler);
            ecs.EventBus.Unsubscribe<TEvent6>(Handler);
            ecs.EventBus.Unsubscribe<TEvent7>(Handler);
            ecs.EventBus.Unsubscribe<TEvent8>(Handler);
        }
    }

    public abstract class EventHandler<TEvent, TEvent1, TEvent2, TEvent3, TEvent4, TEvent5, TEvent6, TEvent7, TEvent8, TEvent9> : IEventHandler
        where TEvent : struct, IEvent
        where TEvent1 : struct, IEvent
        where TEvent2 : struct, IEvent
        where TEvent3 : struct, IEvent
        where TEvent4 : struct, IEvent
        where TEvent5 : struct, IEvent
        where TEvent6 : struct, IEvent
        where TEvent7 : struct, IEvent
        where TEvent8 : struct, IEvent
        where TEvent9 : struct, IEvent
    {
        public abstract void Handler(ECSManager ecs, TEvent evt);
        public abstract void Handler(ECSManager ecs, TEvent1 evt);
        public abstract void Handler(ECSManager ecs, TEvent2 evt);
        public abstract void Handler(ECSManager ecs, TEvent3 evt);
        public abstract void Handler(ECSManager ecs, TEvent4 evt);
        public abstract void Handler(ECSManager ecs, TEvent5 evt);
        public abstract void Handler(ECSManager ecs, TEvent6 evt);
        public abstract void Handler(ECSManager ecs, TEvent7 evt);
        public abstract void Handler(ECSManager ecs, TEvent8 evt);
        public abstract void Handler(ECSManager ecs, TEvent9 evt);
        public void Subscribe(ECSManager ecs)
        {
            ecs.EventBus.Subscribe<TEvent>(Handler);
            ecs.EventBus.Subscribe<TEvent1>(Handler);
            ecs.EventBus.Subscribe<TEvent2>(Handler);
            ecs.EventBus.Subscribe<TEvent3>(Handler);
            ecs.EventBus.Subscribe<TEvent4>(Handler);
            ecs.EventBus.Subscribe<TEvent5>(Handler);
            ecs.EventBus.Subscribe<TEvent6>(Handler);
            ecs.EventBus.Subscribe<TEvent7>(Handler);
            ecs.EventBus.Subscribe<TEvent8>(Handler);
            ecs.EventBus.Subscribe<TEvent9>(Handler);
        }
        public void Unsubscribe(ECSManager ecs)
        {
            ecs.EventBus.Unsubscribe<TEvent>(Handler);
            ecs.EventBus.Unsubscribe<TEvent1>(Handler);
            ecs.EventBus.Unsubscribe<TEvent2>(Handler);
            ecs.EventBus.Unsubscribe<TEvent3>(Handler);
            ecs.EventBus.Unsubscribe<TEvent4>(Handler);
            ecs.EventBus.Unsubscribe<TEvent5>(Handler);
            ecs.EventBus.Unsubscribe<TEvent6>(Handler);
            ecs.EventBus.Unsubscribe<TEvent7>(Handler);
            ecs.EventBus.Unsubscribe<TEvent8>(Handler);
            ecs.EventBus.Unsubscribe<TEvent9>(Handler);
        }
    }
}