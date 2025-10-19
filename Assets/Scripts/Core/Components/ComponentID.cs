namespace MiniECS
{
    public readonly struct ComponentID
    {
        public readonly ulong value;
        
        public ComponentID(int value) => this.value = (uint)value;
        public ComponentID(uint value) => this.value = value;

        public readonly override string ToString() => $"Component ID {value}";

        public static ComponentArchetype operator |(ComponentID left, ComponentID right)
        {
            return new ComponentArchetype((int)left.value, (int)right.value);
        }

        public static bool operator ==(ComponentID left, ComponentID right) => left.value == right.value;
        public static bool operator !=(ComponentID left, ComponentID right) => left.value != right.value;

        public override bool Equals(object obj) => obj is ComponentID other && this.value == other.value;
        public bool Equals(ComponentID other) => this.value == other.value;

        public override int GetHashCode() => value.GetHashCode();

    }
}