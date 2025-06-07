
using System.Threading.Tasks;
using UnityEngine;

namespace MiniECS
{
    public class DefaultGameMode : IGameMode
    {
        public void FixedUpdate(Game game)
        {

        }

        public void LateUpdate(Game game)
        {

        }

        public void Start(Game game)
        {

        }

        public void Update(Game game)
        {
            HorizontalMovementSystem.Run(game, Time.deltaTime);
        }
    }
}