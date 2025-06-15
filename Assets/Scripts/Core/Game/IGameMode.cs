namespace MiniECS
{
    public interface IGameMode
    {

        public void OnEnable(Game game);
        public void OnDisable(Game game);
        public void Start(Game game);
        public void Update(Game game);
        public void FixedUpdate(Game game);
        public void LateUpdate(Game game);
    }
}