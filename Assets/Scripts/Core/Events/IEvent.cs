using System;

namespace MiniECS
{

    public interface IEvent<T>
    {
        void Dispatch(T data);
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }

    public interface IEvent
    {
        void Dispatch();
        void Subscribe(Action action);
        void Unsubscribe(Action action);
    }

}

