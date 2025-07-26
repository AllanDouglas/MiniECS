namespace MiniECS
{
    public interface IUpdateSystem
    {
        SystemID ID { get; }
        bool Enabled { get; set; }
        void Update(in FrameTime frameTime);

    }
}