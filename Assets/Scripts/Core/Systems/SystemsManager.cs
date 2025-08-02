using System.Collections.Generic;
using UnityEngine.Assertions;

namespace MiniECS
{
    public sealed class SystemsManager
    {
        private readonly List<IUpdateSystem> _systems;

        public SystemsManager(int bufferSize = 25)
        {
            _systems = new(bufferSize);
        }

        public void Register(IUpdateSystem system) => _systems.Add(system);
        public TSystem Register<TSystem>(TSystem system) where TSystem : class, IUpdateSystem
        {
            Assert.IsNotNull(system, "system cant be null");

            Register(system as IUpdateSystem);
            return system;
        }

        public bool TryGet<TSYSTEM>(out TSYSTEM system) where TSYSTEM : IUpdateSystem
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                if (typeof(TSYSTEM) == _systems[i].GetType())
                {
                    system = (TSYSTEM)_systems[i];
                    return true;
                }
            }
            system = default;
            return false;
        }

    }
}