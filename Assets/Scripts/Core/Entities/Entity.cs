using System;

namespace MiniECS
{

    public readonly struct Entity : IEquatable<Entity>
    {
        public readonly uint id;
        public Entity(uint id) => this.id = id;
        public bool Equals(Entity other) => id == other.id;
        public override string ToString() => $"Entity({id})";
        public override bool Equals(object obj) => obj is Entity other && Equals(other);
        public override int GetHashCode() => id.GetHashCode();
        
        public static bool operator ==(Entity left, Entity right) => left.Equals(right);
        public static bool operator !=(Entity left, Entity right) => !left.Equals(right);
    }
}