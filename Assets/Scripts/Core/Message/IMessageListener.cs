using UnityEngine;

namespace MiniECS
{
    public interface IMessageListener
    {
        void Enable(GameObject gameObject, MessageBus bus);
        void Disable(GameObject gameObject, MessageBus bus);
    }
}