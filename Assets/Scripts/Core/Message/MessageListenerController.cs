using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MiniECS
{
    public sealed class MessageListenerController : MiniECSBehaviour
    {
        [SerializeReference, ReferencePicker] private IMessageListener[] _listeners;

        [SerializeField, ReadOnly] EntityPrototypeController _entityTarget;
        // [SerializeField, ReadOnly] MessageListenerController _parent;
        // [SerializeField, ReadOnly] MessageListenerController[] _children;

        void OnEnable()
        {
            // if (_parent == null)
            // {
            //     for (int i = 0; i < _listeners.Length; i++)
            //     {
            //         Bind(_listeners[i]);
            //     }

            //     for (int i = 0; i < _children.Length; i++)
            //     {
            //         for (int j = 0; j < _children[i]._listeners.Length; j++)
            //         {
            //             Bind(_children[i]._listeners[j]);
            //         }
            //     }
            // }

            for (int i = 0; i < _listeners.Length; i++)
            {
                Bind(_listeners[i]);
            }
        }

        void OnDisable()
        {
            for (int i = 0; i < _listeners.Length; i++)
            {
                Unbind(_listeners[i]);
            }

            // for (int i = 0; i < _children.Length; i++)
            // {
            //     for (int j = 0; j < _children[i]._listeners.Length; j++)
            //     {
            //         Unbind(_children[i]._listeners[j]);
            //     }
            // }
        }

        public void Bind(IMessageListener messageListener)
        {
            messageListener.Enable(GetTarget(), MessageBus);
        }

        public void Unbind(IMessageListener messageListener)
        {
            messageListener.Disable(GetTarget(), MessageBus);
        }

        private GameObject GetTarget() => _entityTarget == null ? gameObject : _entityTarget.gameObject;



#if UNITY_EDITOR
        void OnValidate()
        {
            if (SerializationUtility.HasManagedReferencesWithMissingTypes(this))
            {
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(this);
            }


            if (_entityTarget == null)
            {
                _entityTarget = GetComponentInParent<EntityPrototypeController>();
            }

            // if (_parent == null)
            // {
            //     var components = GetComponentsInParent<MessageListenerController>();
            //     foreach (var item in components)
            //     {
            //         if (item != this)
            //         {
            //             _parent = item;
            //         }
            //     }
            // }

            // if (_parent == null)
            // {
            //     _children = GetComponentsInChildren<MessageListenerController>()
            //         .Except(new MessageListenerController[1] { this }).ToArray();
            // }
        }
#endif
    }
}