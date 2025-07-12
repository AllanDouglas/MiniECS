namespace MiniECS
{
    public readonly struct FilterContext
    {
        public readonly ECSManager ecsManager;
        public readonly Entity entity;
        public readonly float deltaTime;


        public FilterContext(ECSManager ecsManager, Entity entity, float deltaTime)
        {
            this.ecsManager = ecsManager;
            this.entity = entity;
            this.deltaTime = deltaTime;
        }
    }
}