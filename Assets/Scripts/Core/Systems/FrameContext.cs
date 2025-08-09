namespace MiniECS
{
    public readonly struct FrameContext
    {
        public readonly Entity entity;
        public readonly float deltaTime;
        public readonly float time;
        public FrameContext(Entity entity, float deltaTime, float time)
        {
            this.entity = entity;
            this.deltaTime = deltaTime;
            this.time = time;
        }
    }
}