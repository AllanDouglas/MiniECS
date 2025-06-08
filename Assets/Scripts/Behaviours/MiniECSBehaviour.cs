using UnityEngine;

namespace MiniECS
{
    public abstract class MiniECSBehaviour : MonoBehaviour
    {
        private readonly static EventBus _eventBus = new();

        public EventBus EventBus => _eventBus;

    }
}