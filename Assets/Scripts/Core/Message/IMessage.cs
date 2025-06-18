namespace MiniECS
{
    public interface IMessage
    {
        public int Id { get; }
    }

    public readonly struct EmptyMessage : IMessage
    {
        public int Id => int.MinValue;
    }
}