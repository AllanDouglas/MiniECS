namespace MiniECS
{
    public readonly struct ComponentArchetype
    {
        public readonly uint value;

        public ComponentArchetype(int id, int id1 = 0, int id2 = 0, int id3 = 0, int id4 = 0)
        {
            value = (1u << id) | (1u << id1) | (1u << id2) | (1u << id3) | (1u << id4);
        }
        public ComponentArchetype(uint value) => this.value = value;
        public static ComponentArchetype operator +(ComponentArchetype a, ComponentArchetype b) => new(a.value | b.value);
        public static ComponentArchetype operator -(ComponentArchetype a, ComponentArchetype b) => new(a.value & ~b.value);
        public static ComponentArchetype operator +(ComponentArchetype a, int b) => new(a.value | 1u << b);
        public static ComponentArchetype operator -(ComponentArchetype a, int b) => new(a.value & ~(1u << b));

    }
}