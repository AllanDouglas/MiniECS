using System;
using UnityEngine;

namespace MiniECS
{
    [Serializable]
    public struct VelocityComponent : IComponent
    {
        public Vector3 velocity;
    }

    [Serializable]
    public sealed class VelocityComponentPrototype : ComponentPrototype<VelocityComponent> { }
}