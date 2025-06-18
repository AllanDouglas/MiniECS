using System;
using UnityEngine;

namespace MiniECS
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute { }

}
