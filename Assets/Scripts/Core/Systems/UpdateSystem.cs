using System;
using System.Buffers;
using UnityEngine;

namespace MiniECS
{
    public abstract class UpdateSystem<TComponent> : IUpdateSystem
        where TComponent : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {

                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent component = ref archetype.GetComponent<TComponent>(in entities[e]);
                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time), ref component);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component);
    }

    public abstract class UpdateSystem<TComponent, TComponent1> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
                                    ref comp,
                                    ref comp1);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);
                            ref TComponent6 comp6 = ref archetype.GetComponent<TComponent6>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5,
                                     ref comp6);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
        where TComponent5 : struct, IComponent
        where TComponent6 : struct, IComponent
        where TComponent7 : struct, IComponent
    {
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);
                            ref TComponent6 comp6 = ref archetype.GetComponent<TComponent6>(in entities[e]);
                            ref TComponent7 comp7 = ref archetype.GetComponent<TComponent7>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5,
                                     ref comp6,
                                     ref comp7);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8> : IUpdateSystem
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
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);
                            ref TComponent6 comp6 = ref archetype.GetComponent<TComponent6>(in entities[e]);
                            ref TComponent7 comp7 = ref archetype.GetComponent<TComponent7>(in entities[e]);
                            ref TComponent8 comp8 = ref archetype.GetComponent<TComponent8>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5,
                                     ref comp6,
                                     ref comp7,
                                     ref comp8);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7, ref TComponent8 component8);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9> : IUpdateSystem
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
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);
                            ref TComponent6 comp6 = ref archetype.GetComponent<TComponent6>(in entities[e]);
                            ref TComponent7 comp7 = ref archetype.GetComponent<TComponent7>(in entities[e]);
                            ref TComponent8 comp8 = ref archetype.GetComponent<TComponent8>(in entities[e]);
                            ref TComponent9 comp9 = ref archetype.GetComponent<TComponent9>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5,
                                     ref comp6,
                                     ref comp7,
                                     ref comp8,
                                     ref comp9);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7, ref TComponent8 component8, ref TComponent9 component9);
    }

    public abstract class UpdateSystem<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10> : IUpdateSystem
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
        public readonly ECSManager ECSManager;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                Archetype[] archetypes = ArrayPool<Archetype>.Shared.Rent(Archetype.MAX);

                int length = ECSManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7, TComponent8, TComponent9, TComponent10>(archetypes);

                for (int archIndex = 0; archIndex < length; archIndex++)
                {
                    Archetype archetype = archetypes[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);
                            ref TComponent4 comp4 = ref archetype.GetComponent<TComponent4>(in entities[e]);
                            ref TComponent5 comp5 = ref archetype.GetComponent<TComponent5>(in entities[e]);
                            ref TComponent6 comp6 = ref archetype.GetComponent<TComponent6>(in entities[e]);
                            ref TComponent7 comp7 = ref archetype.GetComponent<TComponent7>(in entities[e]);
                            ref TComponent8 comp8 = ref archetype.GetComponent<TComponent8>(in entities[e]);
                            ref TComponent9 comp9 = ref archetype.GetComponent<TComponent9>(in entities[e]);
                            ref TComponent10 comp10 = ref archetype.GetComponent<TComponent10>(in entities[e]);

                            OnUpdate(new(entities[e], frameTime.deltaTime, frameTime.time),
                                     ref comp,
                                     ref comp1,
                                     ref comp2,
                                     ref comp3,
                                     ref comp4,
                                     ref comp5,
                                     ref comp6,
                                     ref comp7,
                                     ref comp8,
                                     ref comp9,
                                     ref comp10);
                        }
                    }
                }

                ArrayPool<Archetype>.Shared.Return(archetypes);
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7, ref TComponent8 component8, ref TComponent9 component9, ref TComponent10 component10);
    }

}