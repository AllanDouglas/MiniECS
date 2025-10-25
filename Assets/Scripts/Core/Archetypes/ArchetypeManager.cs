using System;
using System.Buffers;

namespace MiniECS
{
    public sealed class ArchetypeManager
    {
        private readonly ComponentArchetype[] _archetypes;
        private readonly Archetype[] _archetypes2;
        public int Count { get; private set; }
        public ArchetypeManager(int _bufferSize)
        {
            _archetypes = new ComponentArchetype[_bufferSize];
            _archetypes2 = new Archetype[_bufferSize];
        }

        public ComponentArchetype GetId(in Entity entity) => _archetypes[entity.id];
        public Archetype Get(in Entity entity)
        {
            for (int i = 0; i < _archetypes.Length; i++)
            {
                if (_archetypes2[i].ContainsEntity(entity))
                {
                    return _archetypes2[i];
                }
            }

            return null;
        }

        public Archetype CreateArchetype(int componentsBufferSize = -1)
        {
            Archetype archetype = new((uint)_archetypes.Length,
                (uint)componentsBufferSize);

            _archetypes2[Count] = archetype;
            Count++;
            return archetype;
        }

        public void Set(in Entity entity, ComponentArchetype componentArchetype) => _archetypes[entity.id] = componentArchetype;

        public bool TryGetArchetype(ComponentArchetype archetypeId, out Archetype archetype)
        {
            archetype = Query(archetypeId);
            return archetype != null;
        }

        public Archetype Query(ComponentArchetype archetypeId)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i]?.Id == archetypeId)
                {
                    return _archetypes2[i];
                }
            }

            return null;
        }

        public Archetype[] Query<TComponent>()
            where TComponent : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
            where TComponent9 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>()
                    && _archetypes2[i].HasComponent<TComponent9>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public Archetype[] Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10>()
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
            where TComponent9 : struct, IComponent
            where TComponent10 : struct, IComponent
        {
            int found = 0;
            Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Count);
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>()
                    && _archetypes2[i].HasComponent<TComponent9>()
                    && _archetypes2[i].HasComponent<TComponent10>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            var result = new Archetype[found];

            Array.Copy(archetypes, result, found);
            ArrayPool<Archetype>.Shared.Return(archetypes);
            return result;
        }

        public int Query<TComponent>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }

            return found;
        }

        public int Query<TComponent, TComponent1>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
        {
            int found = 0;

            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
            where TComponent9 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>()
                    && _archetypes2[i].HasComponent<TComponent9>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

        public int Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10>(in Archetype[] archetypes)
            where TComponent : struct, IComponent
            where TComponent1 : struct, IComponent
            where TComponent2 : struct, IComponent
            where TComponent3 : struct, IComponent
            where TComponent4 : struct, IComponent
            where TComponent5 : struct, IComponent
            where TComponent6 : struct, IComponent
            where TComponent7 : struct, IComponent
            where TComponent8 : struct, IComponent
            where TComponent9 : struct, IComponent
            where TComponent10 : struct, IComponent
        {
            int found = 0;
            for (int i = 0; i < Count; i++)
            {
                if (_archetypes2[i].HasComponent<TComponent>()
                    && _archetypes2[i].HasComponent<TComponent1>()
                    && _archetypes2[i].HasComponent<TComponent2>()
                    && _archetypes2[i].HasComponent<TComponent3>()
                    && _archetypes2[i].HasComponent<TComponent4>()
                    && _archetypes2[i].HasComponent<TComponent5>()
                    && _archetypes2[i].HasComponent<TComponent6>()
                    && _archetypes2[i].HasComponent<TComponent7>()
                    && _archetypes2[i].HasComponent<TComponent8>()
                    && _archetypes2[i].HasComponent<TComponent9>()
                    && _archetypes2[i].HasComponent<TComponent10>())
                {
                    archetypes[found] = _archetypes2[i];
                    found++;
                }
            }
            return found;
        }

    }
}