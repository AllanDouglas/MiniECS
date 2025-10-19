using System.Threading;

namespace MiniECS
{
    public static class ComponentIdHelper
    {
        private static int _nextId = -1;

        public static void Reset() => _nextId = -1;
        public static ComponentID GetID<TComponent>()
            where TComponent : struct, IComponent => new(ComponentIdValue<TComponent>.Value);

        private static class ComponentIdValue<TComponent> where TComponent : struct, IComponent
        {
            public static readonly int Value = Interlocked.Increment(ref _nextId);
        }
    }


}
