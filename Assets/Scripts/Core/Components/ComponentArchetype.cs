using System;

namespace MiniECS
{
    public readonly struct ComponentArchetype : IEquatable<ComponentArchetype>
    {
        public static readonly int MaxCapacity = 32;

        public readonly uint value;

        public ComponentArchetype(int id, int id1 = 0, int id2 = 0, int id3 = 0, int id4 = 0)
        {
            value = (1u << id) | (1u << id1) | (1u << id2) | (1u << id3) | (1u << id4);
        }
        public ComponentArchetype(uint value) => this.value = value;
        public ComponentArchetype(ComponentID componentID) : this((int)componentID.value) { }
        public ComponentArchetype(ComponentArchetype archetype) : this(archetype.value) { }

        public bool Contains(ComponentID componentID) => (value & (1u << (int)componentID.value)) != 0;
        public bool Contains(ComponentArchetype other) => (value & other.value) == other.value;
        public bool Equals(ComponentArchetype other) => value == other.value;
        public override bool Equals(object obj) => obj is ComponentArchetype other && value == other.value;
        public override int GetHashCode() => value.GetHashCode();

        public static ComponentArchetype operator +(ComponentArchetype a, ComponentArchetype b) => new(a.value | b.value);
        public static ComponentArchetype operator -(ComponentArchetype a, ComponentArchetype b) => new(a.value & ~b.value);
        public static ComponentArchetype operator +(ComponentArchetype a, int b) => new(a.value | 1u << b);
        public static ComponentArchetype operator -(ComponentArchetype a, int b) => new(a.value & ~(1u << b));
        public static ComponentArchetype operator +(ComponentArchetype a, ComponentID b) => new(a.value | (1u << (int)b.value));
        public static ComponentArchetype operator -(ComponentArchetype a, ComponentID b) => new(a.value & ~(1u << (int)b.value));
        public static ComponentArchetype operator |(ComponentArchetype a, ComponentID b) => new(a.value | (1u << (int)b.value));
        public static bool operator ==(ComponentArchetype a, ComponentArchetype b) => a.value == b.value;
        public static bool operator !=(ComponentArchetype a, ComponentArchetype b) => a.value != b.value;
    }
}