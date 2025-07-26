namespace MiniECS
{
    public readonly struct FrameTime
    {
        public readonly float deltaTime;
        public readonly float time;

        public FrameTime(float deltaTime, float time)
        {
            this.deltaTime = deltaTime;
            this.time = time;
        }
    }
}