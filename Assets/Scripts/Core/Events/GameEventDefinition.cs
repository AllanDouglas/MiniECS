using System;
using UnityEngine;

namespace MiniECS.Framework
{
    [Serializable]
    public struct GameEventDefinition 
    {
        public string Name;

        [ReadOnly]
        public int Id;

        public readonly override int GetHashCode() => Id;
    }


    public interface IGameEvent
    {
        public int Id { get; }
        public string Name { get; }
    }

    [Serializable]
    public struct EmptyEvent : IGameEvent, IEquatable<EmptyEvent>
    {
        public readonly int Id => -1;
        public readonly string Name => "Empty";

        public readonly bool Equals(EmptyEvent other) => other.Id == Id;
    }

}