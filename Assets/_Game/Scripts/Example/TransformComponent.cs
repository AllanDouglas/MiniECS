using System;
using UnityEngine;

namespace MiniECS
{
    [Serializable]
    public struct TransformComponent : IComponent
    {
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 rotation;
        [SerializeField] public Vector3 scale;
    }

    [Serializable]
    public sealed class TransformComponentPrototype : ComponentPrototype<TransformComponent> { }


}