namespace MiniECS
{
    public interface IGameMode
    {

        public void OnEnable(ECSManager game);
        public void OnDisable(ECSManager game);
        public void Start(ECSManager game);
        public void Update(ECSManager game);
        public void FixedUpdate(ECSManager game);
        public void LateUpdate(ECSManager game);
    }
}