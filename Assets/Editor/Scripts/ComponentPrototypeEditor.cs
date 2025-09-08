using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniECS
{
    [CustomPropertyDrawer(typeof(IComponentPrototype), true)]
    public class ComponentPrototypeEditor : PropertyDrawer
    {

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            root.Add(new PropertyField(property, $"{ObjectNames.NicifyVariableName(property.managedReferenceValue.GetType().Name)}"));

            if (property.boxedValue is IComponentPrototypeEditor prototype
             && property.serializedObject.targetObject is EntityPrototypeController entityPrototype)
            {
                prototype.OnValidate(entityPrototype);
                property.serializedObject.ApplyModifiedProperties();
            }

            return root;
        }
    }
}