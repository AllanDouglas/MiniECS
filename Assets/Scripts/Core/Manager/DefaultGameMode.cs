using UnityEngine;

namespace MiniECS
{
    public abstract class DefaultGameMode : IGameMode
    {
        public virtual void OnEnable(ECSManager ecs) { }
        public virtual void Start(ECSManager ecs) { }
        public virtual void BeforeStart(ECSManager ecs) { }
        public virtual void OnDisable(ECSManager ecs) { }
        public virtual void OnDestroy(ECSManager ecs) { }
        public virtual void Update(ECSManager ecs)
        {
            FrameTime frameTime = new(Time.deltaTime, Time.time);

            for (int i = 0; i < ecs.SystemsManager.Systems.Length; i++)
            {
                if (ecs.SystemsManager.Systems[i].Enabled)
                {
                    ecs.SystemsManager.Systems[i].Update(in frameTime);
                }
            }
        }

        public virtual void FixedUpdate(ECSManager ecs)
        {
            FrameTime frameTime = new(Time.fixedDeltaTime, Time.time);

            for (int i = 0; i < ecs.SystemsManager.FixedTimeSystems.Length; i++)
            {
                if (ecs.SystemsManager.FixedTimeSystems[i].Enabled)
                {
                    ecs.SystemsManager.FixedTimeSystems[i].Update(in frameTime);
                }
            }
        }

        public virtual void LateUpdate(ECSManager ecs)
        {
            ecs.EventBus.FlushAll(ecs);
            ecs.MessageBus.FlushAll();
        }
    }
}