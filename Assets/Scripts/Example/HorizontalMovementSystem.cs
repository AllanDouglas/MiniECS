using UnityEngine;

namespace MiniECS
{
    public static class HorizontalMovementSystem
    {
        public static void Run(Game game, float deltaTime)
        {
            for (int i = 0; i < game.EntityManager.ActiveEntitiesIndex.Length; i++)
            {
                var entityIndex = game.EntityManager.ActiveEntitiesIndex[i];
                Entity entity = game.EntityManager.Entities[entityIndex];

                ref VelocityComponent velocityComponent = ref game.ComponentsManager.TryGet<VelocityComponent>(in entity, out bool hasVelocity);
                ref TransformComponent transformComponent = ref game.ComponentsManager.TryGet<TransformComponent>(in entity, out bool hasTransform);

                if (hasTransform && hasVelocity)
                {
                    transformComponent.position += deltaTime * velocityComponent.velocity.z * Vector3.forward;
                    game.EntityManager.EntityControllers[(int)entity.id].transform.position = transformComponent.position;
                }
            }
        }
    }
}