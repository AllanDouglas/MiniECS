namespace MiniECS
{
    public readonly struct ArchetypeId
    {
        public readonly ulong value;

        public ArchetypeId(ulong value) => this.value = value;
        public ArchetypeId(ComponentSet componentSet) => value = componentSet.id;

        public static bool operator ==(ArchetypeId left, ArchetypeId right) => left.value == right.value;
        
        public static bool operator !=(ArchetypeId left, ArchetypeId right) => left.value != right.value;
        public override bool Equals(object obj) => obj is ArchetypeId other && this == other;
        public override int GetHashCode() => value.GetHashCode();
    }
}