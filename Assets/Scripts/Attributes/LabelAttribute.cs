using System;
using UnityEngine;

namespace MiniECS
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelAttribute : PropertyAttribute
    {
        public readonly string text;

        public LabelAttribute(string text)
        {
            this.text = text;
        }

        public LabelAttribute(Type type)
        {
            text = type.Name;
        }
    }

}
