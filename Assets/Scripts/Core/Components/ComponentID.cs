namespace MiniECS
{
    public readonly struct ComponentID
    {
        public readonly uint value;

        public ComponentID(int value) => this.value = (uint)value;
        public ComponentID(uint value) => this.value = value;

        public readonly override string ToString() => $"Component ID {value}";

        public static ComponentArchetype operator |(ComponentID left, ComponentID right)
        {
            return new ComponentArchetype((int)left.value, (int)right.value);
        }

    }
}