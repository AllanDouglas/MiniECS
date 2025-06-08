namespace MiniECS
{

    public readonly struct Entity
    {
        public readonly uint id;
        public Entity(uint id)
        {
            this.id = id;
        }
    }
}