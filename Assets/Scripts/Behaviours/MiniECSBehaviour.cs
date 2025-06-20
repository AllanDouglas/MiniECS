using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public abstract class MiniECSBehaviour : MonoBehaviour
    {

        // static MiniECSBehaviour()
        // {
        //     SceneManager.activeSceneChanged += (_, _) =>
        //     {

        //     };
        // }
        
        private readonly static EventBus _eventBus = new();
        private readonly static MessageBus _messageBus = new();

        public EventBus EventBus => _eventBus;
        public MessageBus MessageBus => _messageBus;

    }
}