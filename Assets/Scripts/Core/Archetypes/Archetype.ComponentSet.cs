using System;

namespace MiniECS
{
    public readonly struct ComponentSet : IEquatable<ComponentSet>
    {
        private const int MAX = 8;
        private const byte BIT_OFFSET = 8;

        public static ComponentSet New<TComponent>() where TComponent : struct, IComponent
        {
            return new(ComponentIdHelper.GetID<TComponent>());
        }

        public static ComponentSet Add(in ComponentSet componentSet, in ComponentID componentID)
        {
            if (componentSet.Contains(componentID))
            {
                return componentSet;
            }

            var aValue = componentSet.id;
            int offset = (BIT_OFFSET * componentSet.count);
            var bValue = componentID.value << offset;

            var value = aValue | bValue;

            return new(value, componentSet.count + 1);
        }

        public readonly int count;
        public readonly ulong id;

        private ComponentSet(ComponentID componentID)
        {
            count = 1;
            id = (byte)(componentID.value & 0xFF);
        }

        private ComponentSet(ulong value, int count)
        {
            this.count = count;
            id = value;
        }

        public readonly int GetPositionOf<TComponent>()
            where TComponent : struct, IComponent
        {
            return GetPositionOf(ComponentIdHelper.GetID<TComponent>());
        }

        public readonly int GetPositionOf(ComponentID componentID)
        {
            for (int i = 0; i < count; i++)
            {
                int offset = (BIT_OFFSET * i);
                ulong idPart = (id >> offset) & 0xFF;

                if (componentID.value == idPart)
                {
                    return i;
                }
            }

            return -1;
        }
        public readonly bool TryGetPositionOf<TComponent>(out int index)
            where TComponent : struct, IComponent
        {
            return TryGetPositionOf(ComponentIdHelper.GetID<TComponent>(), out index);
        }

        public readonly bool TryGetPositionOf(ComponentID componentID, out int index)
        {
            for (int i = 0; i < count; i++)
            {
                ulong idPart = (id >> (BIT_OFFSET * i)) & 0xFF;

                if (componentID.value == idPart)
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        public readonly bool Contains<TComponent>() where TComponent : struct, IComponent
            => Contains(ComponentIdHelper.GetID<TComponent>());

        public readonly bool Contains(ComponentID componentID)
        {
            for (int i = 0; i < count; i++)
            {
                ulong idPart = (id >> (BIT_OFFSET * i)) & 0xFF;

                if (componentID.value == idPart)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Equals(ComponentSet other) => id == other.id;
        public override bool Equals(object? obj) => obj is ComponentSet other && Equals(other);
        public override int GetHashCode() => id.GetHashCode();
    }

    public static class ComponentSetExtensions
    {
        public static ComponentSet Plus(in this ComponentSet componentSet, in ComponentID componentID)
        {
            return ComponentSet.Add(in componentSet, in componentID);
        }
    }
}