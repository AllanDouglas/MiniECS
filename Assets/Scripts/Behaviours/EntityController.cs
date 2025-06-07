using UnityEngine;

namespace MiniECS
{

    public sealed class EntityController : MiniECSBehaviour
    {
        [SerializeReference, ReferencePicker] private IComponentPrototype[] _components;
        public IComponentPrototype[] Components { get => _components; set => _components = value; }

    }

}