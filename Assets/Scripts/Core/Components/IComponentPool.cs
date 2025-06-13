using System;

namespace MiniECS
{
    public interface IComponentPool
    {
        Type ComponentType { get; }

        public ref TComponent Get<TComponent>(in Entity entity) where TComponent : struct, IComponent;
        public void Add<TComponent>(in Entity entity, TComponent component) where TComponent : struct, IComponent;
        public void Remove(in Entity entity);
        public ref TComponent TryGet<TComponent>(in Entity entity, out bool hasComponent) where TComponent : struct, IComponent;

    }
}