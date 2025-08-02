namespace MiniECS
{
    public interface IUpdateSystem
    {
        bool Enabled { get; set; }
        void Update(in FrameTime frameTime);

    }
}