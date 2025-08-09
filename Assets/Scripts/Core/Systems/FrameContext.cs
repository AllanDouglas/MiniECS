namespace MiniECS
{
    public readonly struct FrameContext
    {
        public readonly ECSManager ecsManager;
        public readonly Entity entity;
        public readonly float deltaTime;
        public readonly float time;
        public FrameContext(ECSManager ecsManager, Entity entity, float deltaTime, float time)
        {
            this.ecsManager = ecsManager;
            this.entity = entity;
            this.deltaTime = deltaTime;
            this.time = time;
        }
    }
}