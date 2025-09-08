namespace MiniECS
{
    public interface IGameMode
    {

        public void OnEnable(ECSManager ecs);
        public void OnDisable(ECSManager ecs);
        public void Start(ECSManager ecs);
        public void Update(ECSManager ecs);
        public void FixedUpdate(ECSManager ecs);
        public void LateUpdate(ECSManager ecs);
    }
}