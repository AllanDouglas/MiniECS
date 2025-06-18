using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniECS
{
    [CustomPropertyDrawer(typeof(DebugButtonAttribute))]
    public class MessageListenerDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            // Draw the default inspector
            root.Add(new PropertyField(property));
            //_listeners.Array.data[0]
            var lastDot = property.propertyPath.LastIndexOf('.');


            var parentObject = property.serializedObject.FindProperty(property.propertyPath[..lastDot]);
            var fieldInfo = parentObject.managedReferenceValue.GetType().GetField(property.name, System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            var debugButtonAttribute = fieldInfo.GetCustomAttributes(typeof(DebugButtonAttribute), false).FirstOrDefault() as DebugButtonAttribute;

            var listener = parentObject.managedReferenceValue as IMessageListener;

            var dispatchMethod = listener?.GetType().GetMethod(debugButtonAttribute.Method, System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic);

            if (dispatchMethod != null)
            {
                var dispatchButton = new Button(() =>
                {
                    dispatchMethod.Invoke(listener, default);
                })
                {
                    text = debugButtonAttribute.DisplayText
                };
                root.Add(dispatchButton);
            }

            return root;
        }

    }


}