using System;

namespace MiniECS
{
    public partial class Archetype
    {
        public const int MAX = 16;
        public const int HALF_MAX = MAX / 2;

        public readonly struct ComponentSet : IEquatable<ComponentSet>
        {
            private const byte BIT_OFFSET = 8;

            public static ComponentSet New<TComponent>() where TComponent : struct, IComponent
                => new(ComponentIdHelper.GetID<TComponent>());

            public static ComponentSet Sum(in ComponentSet componentSet, in ComponentID componentID)
            {
                if (componentSet.Contains(componentID))
                {
                    return componentSet;
                }

                ulong newLow = componentSet._low;
                ulong newHi = componentSet._hight;

                if (componentSet.count < HALF_MAX)
                {
                    int offset = BIT_OFFSET * componentSet.count;
                    ulong aValue = componentSet._low;
                    ulong bValue = componentID.value << offset;
                    newLow = aValue | bValue;
                }
                else
                {
                    int offset = BIT_OFFSET * (componentSet.count - HALF_MAX);
                    ulong aValue = componentSet._hight;
                    ulong bValue = componentID.value << offset;
                    newHi = aValue | bValue;
                }

                return new(newLow, newHi, componentSet.count + 1);
            }

            public readonly int count;
            private readonly ulong _low;
            private readonly ulong _hight;

            private ComponentSet(ComponentID componentID)
            {
                count = 1;
                _low = (byte)(componentID.value & 0xFF);
                _hight = 0;

            }

            private ComponentSet(ComponentSet set, int count)
            {
                this.count = count;
                _low = set._low;
                _hight = set._hight;
            }

            private ComponentSet(ulong low, ulong hight, int count)
            {
                this.count = count;
                _low = low;
                _hight = hight;
            }

            public readonly int GetPositionOf<TComponent>()
                where TComponent : struct, IComponent
            {
                return GetPositionOf(ComponentIdHelper.GetID<TComponent>());
            }

            public readonly int GetPositionOf(ComponentID componentID)
            {
                for (int i = 0; i < MathF.Min(count, HALF_MAX); i++)
                {
                    int offset = BIT_OFFSET * i;
                    ulong idPart = (_low >> offset) & 0xFF;

                    if (componentID.value == idPart)
                    {
                        return i;
                    }
                }

                for (int j = HALF_MAX, i = 0; j < count; j++, i++)
                {
                    int offset = BIT_OFFSET * i;
                    ulong idPart = (_hight >> offset) & 0xFF;

                    if (componentID.value == idPart)
                    {
                        return i + HALF_MAX;
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
                index = GetPositionOf(componentID);
                return index > -1;
            }

            public readonly bool Contains<TComponent>() where TComponent : struct, IComponent
                => Contains(ComponentIdHelper.GetID<TComponent>());

            public readonly bool Contains(ComponentID componentID) => TryGetPositionOf(componentID, out _);
            public bool Equals(ComponentSet other) => _low == other._low;
            public override bool Equals(object? obj) => obj is ComponentSet other && Equals(other);
            public override int GetHashCode() => _low.GetHashCode();

            public static ComponentSet operator +(in ComponentSet set, in ComponentID id)
                => Sum(in set, in id);

            public static ComponentSet operator +(in ComponentID id, in ComponentSet set)
                => Sum(in set, in id);
        }

    }

    public static class ComponentSetExtensions
    {
        public static Archetype.ComponentSet Plus(in this Archetype.ComponentSet componentSet, in ComponentID componentID)
        {
            return Archetype.ComponentSet.Sum(in componentSet, in componentID);
        }
    }
}