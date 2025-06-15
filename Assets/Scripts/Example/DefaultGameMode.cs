using UnityEngine;

namespace MiniECS
{
    public class DefaultGameMode : IGameMode
    {
        UpdateTransformSystem _updateTransformSystem;

        public void FixedUpdate(Game game) { }

        public void LateUpdate(Game game) { }

        public void OnDisable(Game game) { }

        public void OnEnable(Game game)
        {
            _updateTransformSystem = new(game);
        }

        public void Start(Game game) { }

        public void Update(Game game)
        {
            _updateTransformSystem.Update(game, Time.deltaTime);
        }
    }
}