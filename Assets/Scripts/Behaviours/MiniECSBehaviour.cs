using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniECS
{
    public abstract class MiniECSBehaviour : MonoBehaviour
    {


#if UNITY_EDITOR
        public static int MainThreadIndex;
        [InitializeOnLoadMethod]
        private static void ResetEventBus()
        {
            MainThreadIndex = Thread.CurrentThread.ManagedThreadId;
            _eventBus.Clear();
            _messageBus.Clear();
        }
#endif

        static MiniECSBehaviour()
        {
            SceneManager.sceneUnloaded += (_) =>
            {
                _eventBus.Clear();
                _messageBus.Clear();
            };
        }

        private readonly static EventBus _eventBus = new();
        private readonly static MessageBus _messageBus = new();

        public EventBus EventBus => _eventBus;
        public MessageBus MessageBus => _messageBus;

    }
}