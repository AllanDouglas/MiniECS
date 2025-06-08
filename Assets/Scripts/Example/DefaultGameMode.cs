
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
            new FilterSystem<VelocityComponent, TransformComponent>().Execute(game,
                Time.deltaTime, (FilterContext context, ref VelocityComponent velocityComponent, ref TransformComponent transformComponent) =>
            {
                transformComponent.position += context.deltaTime * velocityComponent.velocity.z * Vector3.forward;
                context.game.EntityManager.EntityControllers[(int)context.entity.id].transform.position = transformComponent.position;
            });
        }



    }
}