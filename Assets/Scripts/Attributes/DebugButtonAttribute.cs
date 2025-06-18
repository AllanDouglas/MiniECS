using System;
using UnityEngine;

namespace MiniECS
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DebugButtonAttribute : PropertyAttribute
    {
        public readonly string Method;
        public readonly string DisplayText;

        public DebugButtonAttribute(string method, string displayText = null)
        {
            Method = method;
            DisplayText = displayText ?? method;
        }
    }
}