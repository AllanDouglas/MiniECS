using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace MiniECS.Framework.Editor
{

    [CustomEditor(typeof(GameEventSettingsSO))]
    public class GenericEventsSettingsEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Use the default inspector to expose all serializable fields
            var serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true); // Skip the script reference

            while (serializedProperty.NextVisible(false))
            {
                var propertyField = new PropertyField(serializedProperty.Copy());
                propertyField.Bind(serializedObject);
                propertyField.name = serializedProperty.name;
                root.Add(propertyField);
            }

            // Find all relative children of _events propertyField and register a FocusOutEvent
            var eventsProperty = serializedObject.FindProperty("_events");
            if (eventsProperty != null)
            {
                foreach (var child in root.Children())
                {
                    child.RegisterCallback<FocusOutEvent>(evt =>
                    {
                        (target as GameEventSettingsSO).Generate();
                    });

                    if (child.name == "_events")
                    {
                        var property = child as PropertyField;
                        property.RegisterValueChangeCallback(e =>
                        {
                            (target as GameEventSettingsSO).Generate();
                        });
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
            return root;
        }

    }
}