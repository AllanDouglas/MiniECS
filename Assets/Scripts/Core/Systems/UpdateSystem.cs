using System;
using UnityEngine;

namespace MiniECS
{
    public abstract class UpdateSystem<TComponent> : IUpdateSystem
        where TComponent : struct, IComponent
    {
        public readonly ECSManager ECSManager;
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component);
    }

    public abstract class UpdateSystem<TComponent, TComponent1> : IUpdateSystem
        where TComponent : struct, IComponent
        where TComponent1 : struct, IComponent
    {
        public readonly ECSManager ECSManager;
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
                                    ref comp,
                                    ref comp1,
                                    ref comp2);
                        }
                    }
                }
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
                    ReadOnlySpan<Entity> entities = archetype.Entities;
                    for (int e = 0; e < entities.Length; e++)
                    {
                        if (ECSManager.EntityManager.IsActive(entities[e]))
                        {
                            ref TComponent comp = ref archetype.GetComponent<TComponent>(in entities[e]);
                            ref TComponent1 comp1 = ref archetype.GetComponent<TComponent1>(in entities[e]);
                            ref TComponent2 comp2 = ref archetype.GetComponent<TComponent2>(in entities[e]);
                            ref TComponent3 comp3 = ref archetype.GetComponent<TComponent3>(in entities[e]);

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
                                    ref comp,
                                    ref comp1,
                                    ref comp2,
                                    ref comp3);
                        }
                    }
                }
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
                                    ref comp,
                                    ref comp1,
                                    ref comp2,
                                    ref comp3,
                                    ref comp4);
                        }
                    }
                }
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
                                    ref comp,
                                    ref comp1,
                                    ref comp2,
                                    ref comp3,
                                    ref comp4,
                                    ref comp5);
                        }
                    }
                }
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
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
        private readonly Archetype[] _archetype;

        public UpdateSystem(ECSManager ecsManager)
        {
            ECSManager = ecsManager;
            _archetype = ecsManager.ArchetypeManager.Query<TComponent, TComponent1, TComponent2, TComponent3, TComponent4, TComponent5, TComponent6, TComponent7>();
        }

        public bool Enabled { get; set; } = true;

        public void Update(in FrameTime frameTime)
        {
            if (Enabled)
            {
                for (int archIndex = 0; archIndex < _archetype.Length; archIndex++)
                {
                    Archetype archetype = _archetype[archIndex];
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

                            OnUpdate(new(entities[e],
                                         frameTime.deltaTime,
                                         frameTime.time),
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
            }
        }

        protected abstract void OnUpdate(FrameContext context, ref TComponent component, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3, ref TComponent4 component4, ref TComponent5 component5, ref TComponent6 component6, ref TComponent7 component7);
    }


}