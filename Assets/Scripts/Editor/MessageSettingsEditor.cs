using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace MiniECS.Editor
{

    [CustomEditor(typeof(MessageSettingsSO))]
    public class MessageSettingsEditor : UnityEditor.Editor
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
            var eventsProperty = serializedObject.FindProperty("_messages");
            if (eventsProperty != null)
            {
                foreach (var child in root.Children())
                {
                    child.RegisterCallback<FocusOutEvent>(evt =>
                    {
                        (target as MessageSettingsSO).Generate();
                    });

                    if (child.name == "_messages")
                    {
                        var property = child as PropertyField;
                        property.RegisterValueChangeCallback(e =>
                        {
                            (target as MessageSettingsSO).Generate();
                        });
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
            return root;
        }

    }
}