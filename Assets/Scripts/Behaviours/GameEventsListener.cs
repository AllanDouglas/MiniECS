// using System;
// using UnityEngine;

// namespace MiniECS
// {
//     //[DefaultExecutionOrder(-50)]
//     public sealed class GameEventsListener : MiniECSBehaviour
//     {

//         [SerializeField] private Listener[] _events;

//         private bool _warmed;

//         public Listener[] Events { get => _events; set => _events = value; }

//         private void Awake()
//         {
//             Warm();
//         }

//         private void OnEnable()
//         {
//             if (!_warmed)
//             {
//                 Warm();
//             }

//         }


//         private void OnDestroy()
//         {
//             _warmed = false;
//             for (int i = 0; i < _events.Length; i++)
//             {
//                 var evt = _events[i];
//                 MessageBroker.Unsubscribe(evt.EventId, gameObject, evt.OnTrigger.Invoke);
//             }
//         }

//         private void Warm()
//         {
//             _warmed = true;

//             for (int i = 0; i < _events.Length; i++)
//             {
//                 var evt = _events[i];
//                 MessageBroker.Subscribe(evt.EventId, gameObject, evt.OnTrigger.Invoke);
//             }
//         }


//         [Serializable]
//         public struct Listener
//         {
//             public int EventId;
//             public EventField OnTrigger;

//         }
// #if UNITY_EDITOR
//         public void Dispatch(int eventId) => MessageBroker.Dispatch(eventId, gameObject);
// #endif

//     }

// }