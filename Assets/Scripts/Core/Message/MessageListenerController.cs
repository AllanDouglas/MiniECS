using UnityEditor;
using UnityEngine;

namespace MiniECS
{
    public sealed class MessageListenerController : MiniECSBehaviour

    {
        [SerializeReference, ReferencePicker] private IMessageListener[] _listeners;

        void OnEnable()
        {
            for (int i = 0; i < _listeners.Length; i++)
            {
                _listeners[i].Enable(gameObject, this.MessageBus);
            }
        }

        void OnDisable()
        {
            for (int i = 0; i < _listeners.Length; i++)
            {
                _listeners[i].Disable(gameObject, this.MessageBus);
            }
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
            }
        }
#endif


    }
}