namespace MiniECS
{
    public readonly struct FilterContext
    {
        public readonly ECSManager game;
        public readonly Entity entity;
        public readonly float deltaTime;
        

        public FilterContext(ECSManager game, Entity entity, float deltaTime)
        {
            this.game = game;
            this.entity = entity;
            this.deltaTime = deltaTime;
                        
        }
    }
}