using UnityEngine;

namespace MiniECS
{
    public sealed class UpdateTransformSystem : UpdateSystem<TransformComponent, VelocityComponent>
    {
        public UpdateTransformSystem(Game game) : base(game) { }

        protected override void OnUpdate(FilterContext context,
            ref TransformComponent transformComponent,
            ref VelocityComponent velocityComponent)
        {
            transformComponent.position += context.deltaTime * velocityComponent.velocity.z * Vector3.forward;
            context.game.EntityManager.EntityControllers[(int)context.entity.id].transform.position = transformComponent.position;
        }
    }
}