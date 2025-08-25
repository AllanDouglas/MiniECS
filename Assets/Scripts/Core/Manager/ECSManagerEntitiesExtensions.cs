namespace MiniECS
{
    public static class ECSManagerEntitiesExtensions
    {
        public static void DeactivateEntity(this ECSManager manager, Entity entity) => manager.EntityManager.Deactivate(entity);
        public static void ActiveEntity(this ECSManager manager, Entity entity) => manager.EntityManager.Active(entity);
    }
}