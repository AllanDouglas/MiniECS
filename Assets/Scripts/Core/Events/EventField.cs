using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MiniECS
{
    [Serializable]
    public sealed class EventField : EventFieldSet<EventSO, UnityEvent> { }

}