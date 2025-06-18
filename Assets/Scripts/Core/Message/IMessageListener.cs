using System;
using UnityEngine;

namespace MiniECS
{
    public interface IMessageListener
    {
        void Enable(GameObject gameObject, MessageBus bus);
        void Disable(GameObject gameObject, MessageBus bus);
    }
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DebugButton : PropertyAttribute
    {
        public readonly string Method;
        public readonly string DisplayText;

        public DebugButton(string method, string displayText = null)
        {
            Method = method;
            DisplayText = displayText ?? method;
        }
    }
}