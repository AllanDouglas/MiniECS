using UnityEngine;

namespace MiniECS
{
    public abstract class MiniECSBehaviour : MonoBehaviour
    {
        private readonly static SignalBus _eventBus = new();
        private readonly static MessageBroker _messageBroker = new();

        public SignalBus EventBus => _eventBus;
        public MessageBroker MessageBroker => _messageBroker;

    }
}