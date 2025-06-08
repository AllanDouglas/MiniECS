namespace MiniECS
{
    public readonly struct FilterContext
    {
        public readonly Game game;
        public readonly Entity entity;
        public readonly float deltaTime;

        public FilterContext(Game game, Entity entity, float deltaTime)
        {
            this.game = game;
            this.entity = entity;
            this.deltaTime = deltaTime;
        }
    }
}