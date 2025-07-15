using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MiniECS
{
    [CustomPropertyDrawer(typeof(IComponentPrototype), true)]
    public class ComponentPrototypeEditor : UnityEditor.PropertyDrawer
    {

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            // Get the type of the serialized object
            var prototypeType = property.serializedObject.GetType();
            var field = prototypeType.GetField("_component", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var componentType = field.FieldType;
                var label = new Label(componentType.Name);
                label.style.unityFontStyleAndWeight = UnityEngine.FontStyle.Bold;
                root.Add(label);
                var propertyField = new PropertyField(property, ""); // No label, since we use our own
                root.Add(propertyField);

            }
            else
            {
                root.Add(new Label("_component field not found."));
            }

            return root;
        }
    }
}