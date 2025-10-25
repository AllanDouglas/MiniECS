namespace MiniECS
{
    public static class ECSManagerComponentsExtensions
    {

        // Extension for GetComponentID<TComponent>()
        public static ComponentID GetComponentID<TComponent>(this ECSManager ecsManager)
            where TComponent : struct, IComponent => ComponentIdHelper.GetID<TComponent>();

        // Extension for Get<TComponent>(in Entity entity)
        public static ref TComponent GetComponent<TComponent>(this ECSManager ecsManager, in Entity entity)
            where TComponent : struct, IComponent
        {
            return ref ecsManager.ArchetypeManager.Get(entity).GetComponent<TComponent>(entity);
        }

        // Extension for Get<TComponent>(ComponentID componentID, in Entity entity)
        public static ref TComponent GetComponent<TComponent>(this ECSManager ecsManager, ComponentID componentID, in Entity entity)
            where TComponent : struct, IComponent
        {
            return ref ecsManager.ArchetypeManager.Get(entity).GetComponent<TComponent>(componentID, entity);
        }

        // Extension for TryGet<TComponent>(in Entity entity, out bool hasComponent)
        public static ref TComponent TryGetComponent<TComponent>(this ECSManager ecsManager, in Entity entity, out bool hasComponent)
            where TComponent : struct, IComponent
        {
            if (!ecsManager.ArchetypeManager.GetId(entity).Contains(ecsManager.GetComponentID<TComponent>()))
            {
                hasComponent = false;
                return ref ComponentsManager.GetInvalidRef<TComponent>();
            }
            hasComponent = true;
            return ref ecsManager.GetComponent<TComponent>(in entity);
        }

        // Extension for TryGet<TComponent>(ComponentID componentID, in Entity entity, out bool hasComponent)
        public static ref TComponent TryGetComponent<TComponent>(this ECSManager ecsManager, ComponentID componentID, in Entity entity, out bool hasComponent)
            where TComponent : struct, IComponent
        {
            if (!ecsManager.ArchetypeManager.GetId(entity).Contains(ecsManager.GetComponentID<TComponent>()))
            {
                hasComponent = false;
                return ref ComponentsManager.GetInvalidRef<TComponent>();
            }
            hasComponent = true;
            return ref ecsManager.GetComponent<TComponent>(componentID, in entity);
        }
    }
}