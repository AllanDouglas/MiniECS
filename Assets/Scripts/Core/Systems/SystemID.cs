namespace MiniECS
{
    public readonly struct SystemID
    {
        public readonly byte id;

        public SystemID(byte id)
        {
            this.id = id;
        }

        public static implicit operator byte(SystemID id) => id.id;
        public static implicit operator int(SystemID id) => id.id;
    }
}