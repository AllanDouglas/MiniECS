using System;
using UnityEngine;

namespace MiniECS.Framework
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute { }

}
