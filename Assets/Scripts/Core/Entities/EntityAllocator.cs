using System.Collections.Generic;

namespace MiniECS
{
    public sealed class EntityAllocator
    {
        public Entity[] entities;
        private readonly Stack<uint> _freeIndexStack;

        public EntityAllocator(int bufferSize)
        {
            entities = new Entity[bufferSize];
            _freeIndexStack = new Stack<uint>(bufferSize);

            for (int i = bufferSize - 1; i >= 0; i--)
            {
                _freeIndexStack.Push((uint)i);
            }
        }

        public Entity Allocate() => new(_freeIndexStack.Pop());

        public void Free(in Entity entity)
        {
            if (entity.id < entities.Length)
            {
                _freeIndexStack.Push(entity.id);
            }
        }
    }
}