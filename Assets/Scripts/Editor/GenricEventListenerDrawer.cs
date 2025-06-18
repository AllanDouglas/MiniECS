// using System;
// using System.Collections.Generic;
// using System.Linq;
// using MiniECS;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine;
// using UnityEngine.UIElements;
// using static MiniECS.GameEventsListener;

// namespace MiniECS
// {

//     // [CustomPropertyDrawer(typeof(Listener))]
//     public class ListenerDrawer : PropertyDrawer
//     {
//         private static List<Type> TypesCache;

//         public override VisualElement CreatePropertyGUI(SerializedProperty property)
//         {
//             VisualElement root = new();

//             SerializedProperty eventIDProp = property.FindPropertyRelative("EventID");
//             SerializedProperty onTriggerProp = property.FindPropertyRelative("OnTrigger");

//             Label titleLabel = new("Listener Event");
//             titleLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
//             root.Add(titleLabel);

//             List<Type> eventTypes = GetAllEventTypes();

//             var eventsInstances = eventTypes.Select(t => Activator.CreateInstance(t) as IMessage).ToList();

//             List<string> eventNames = eventTypes
//                 .Select(t => Activator.CreateInstance(t) is IMessage evt ? evt.Name : t.Name)
//                 .Where(n => !string.IsNullOrEmpty(n))
//                 .OrderBy(n => n)
//                 .ToList();

//             PopupField<string> popup = new("Event Type", eventNames, 0);
//             popup.RegisterValueChangedCallback(evt =>
//             {
//                 int index = eventNames.IndexOf(evt.newValue);
//                 if (index >= 0 && index < eventTypes.Count)
//                 {
//                     IMessage instance = eventsInstances[index];
//                     eventIDProp.intValue = instance.Id;
//                     property.serializedObject.ApplyModifiedProperties();
//                 }
//             });

//             int currentID = eventIDProp.intValue;
//             int selectedIndex = eventsInstances.FindIndex(evt => evt.Id == currentID);
//             if (selectedIndex >= 0)
//                 popup.index = selectedIndex;

//             root.Add(popup);

//             PropertyField unityEventField = new(onTriggerProp);
//             root.Add(unityEventField);

//             return root;
//         }

//         private List<Type> GetAllEventTypes()
//         {
//             return TypesCache ?? AppDomain.CurrentDomain.GetAssemblies()
//                 .SelectMany(a => a.GetTypes())
//                 .Where(t => typeof(IMessage).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
//                 .ToList();
//         }
//     }
// }