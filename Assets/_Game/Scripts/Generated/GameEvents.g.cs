// Auto-generated file. Do not edit.
using System;
using MiniECS; 
using UnityEngine.Events;
namespace Game {
    
    [Serializable]
    public partial struct Teste01Event: IMessage
    {
        public readonly int Id => 2;
    }

    [Serializable]
    public sealed class Teste01UnityEvent : UnityEvent<Teste01Event> {}

    [Serializable]
    public sealed class Teste01Listener : MessageListener<Teste01Event, Teste01UnityEvent> {}
    [Serializable]
    public partial struct Teste02Event: IMessage
    {
        public readonly int Id => 3;
    }

    [Serializable]
    public sealed class Teste02UnityEvent : UnityEvent<Teste02Event> {}

    [Serializable]
    public sealed class Teste02Listener : MessageListener<Teste02Event, Teste02UnityEvent> {}
}