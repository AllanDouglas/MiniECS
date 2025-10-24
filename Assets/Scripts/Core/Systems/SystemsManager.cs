using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace MiniECS
{
    public sealed class SystemsManager
    {
        private readonly IUpdateSystem[] _systems;
        private readonly IUpdateSystem[] _fixedTimeSystems;

        private int _systemsCount;
        private int _fixedTimeSystemsCount;

        public ReadOnlySpan<IUpdateSystem> Systems => _systems.AsSpan(0, _systemsCount);
        public ReadOnlySpan<IUpdateSystem> FixedTimeSystems => _fixedTimeSystems.AsSpan(0, _fixedTimeSystemsCount);


        public SystemsManager(int bufferSize = 25)
        {
            _systems = new IUpdateSystem[bufferSize];
            _fixedTimeSystems = new IUpdateSystem[bufferSize];
        }



        public void Register(IUpdateSystem system, bool runAtFixedUpdate = false)
        {
            if (!runAtFixedUpdate)
            {
                _systems[_systemsCount++] = system;
                return;
            }

            _fixedTimeSystems[_fixedTimeSystemsCount++] = system;
        }

        public TSystem Register<TSystem>(TSystem system) where TSystem : class, IUpdateSystem
        {
            Register(system as IUpdateSystem, false);
            return system;
        }

        public TSystem Register<TSystem>(TSystem system, bool runAtFixedUpdate) where TSystem : class, IUpdateSystem
        {
            Assert.IsNotNull(system, "system cant be null");

            Register(system as IUpdateSystem, runAtFixedUpdate);
            return system;
        }

        public bool TryGet<TSYSTEM>(out TSYSTEM system) where TSYSTEM : IUpdateSystem
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                if (_systems[i] is null)
                {
                    continue;
                }

                if (typeof(TSYSTEM) == _systems[i].GetType())
                {
                    system = (TSYSTEM)_systems[i];

                    return true;
                }
            }

            for (int i = 0; i < _fixedTimeSystems.Length; i++)
            {
                if (_fixedTimeSystems[i] is null)
                {
                    continue;
                }

                if (typeof(TSYSTEM) == _fixedTimeSystems[i].GetType())
                {
                    system = (TSYSTEM)_fixedTimeSystems[i];
                    return true;
                }
            }

            system = default;
            return false;
        }

        public void Clear()
        {
            _systemsCount = 0;
            _fixedTimeSystemsCount = 0;
            Array.Clear(_systems, 0, _systems.Length);
            Array.Clear(_fixedTimeSystems, 0, _fixedTimeSystems.Length);
        }
    }
}