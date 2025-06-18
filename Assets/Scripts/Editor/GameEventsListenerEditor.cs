// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEditor;
// using UnityEngine;

// namespace MiniECS.Editor
// {

//     [CustomEditor(typeof(GameEventsListener))]
//     [CanEditMultipleObjects]
//     public class GameEventListenerEditor : UnityEditor.Editor
//     {
//         private static List<IGameEvent> EventTypes;
//         private SerializedProperty _events;

//         private void OnEnable()
//         {
//             _events = serializedObject.FindProperty("_events");
//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);

//             for (int i = 0; i < _events.arraySize; i++)
//             {
//                 var eventElement = _events.GetArrayElementAtIndex(i);
//                 var idProperty = eventElement.FindPropertyRelative("EventId");
//                 var onTriggerProperty = eventElement.FindPropertyRelative("OnTrigger");

//                 EditorGUILayout.BeginVertical(GUI.skin.box);

//                 int selectedIndex = GetEventTypes().FindIndex(e => e.Id == idProperty.intValue);
//                 if (selectedIndex < 0)
//                     selectedIndex = 0;

//                 string[] eventNames = GetEventTypes().Select(e => e.Name).ToArray();
//                 selectedIndex = EditorGUILayout.Popup("Event", selectedIndex, eventNames);
//                 idProperty.intValue = GetEventTypes()[selectedIndex].Id;
//                 EditorGUI.indentLevel++;
//                 EditorGUILayout.PropertyField(onTriggerProperty);
//                 EditorGUI.indentLevel--;
//                 if (EditorApplication.isPlaying)
//                 {
//                     if (GUILayout.Button("Dispatch"))
//                     {
//                         (target as GameEventsListener).Dispatch(EventTypes[selectedIndex].Id);
//                     }
//                 }
//                 else
//                 {
//                     if (GUILayout.Button("Remove"))
//                     {
//                         _events.DeleteArrayElementAtIndex(i);
//                         break;
//                     }
//                 }

//                 EditorGUILayout.EndVertical();
//                 EditorGUILayout.Space();
//             }

//             if (GUILayout.Button("Add Event"))
//             {
//                 _events.InsertArrayElementAtIndex(_events.arraySize);
//             }

//             serializedObject.ApplyModifiedProperties();
//         }
//         private static List<IGameEvent> GetEventTypes()
//         {

//             return EventTypes ??= AppDomain.CurrentDomain.GetAssemblies()
//                             .SelectMany(a => a.GetTypes())
//                             .OrderBy(t => t.Name)
//                             .Where(t => typeof(IGameEvent).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
//                             .Select(t => (IGameEvent)Activator.CreateInstance(t))
//                             .ToList();

//         }
//     }


// }
