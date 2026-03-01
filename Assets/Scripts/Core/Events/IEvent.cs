namespace MiniECS
{
    public interface IEvent
    {

    }

    public interface IEventHandler
    {
        void Subscribe(ECSManager ecs);
        void Unsubscribe(ECSManager ecs);
    }

    public interface IEventData<TEvent>
        where TEvent : struct, IEvent
    {
        TEvent ToEvent();
    }

    public abstract class EventDispatcher<TEvent> : MiniECSBehaviour
        where TEvent : struct, IEvent
    {
        public virtual void Dispatch() => EventBus.Dispatch<TEvent>();
    }

    public abstract class EventDispatcher<TEvent, TEventData> : MiniECSBehaviour
        where TEvent : struct, IEvent
        where TEventData : IEventData<TEvent>
    {
        [UnityEngine.SerializeField] private TEventData _data;

        public TEventData Data { get => _data; set => _data = value; }

        public virtual void Dispatch() => EventBus.Dispatch(_data.ToEvent());
    }
}