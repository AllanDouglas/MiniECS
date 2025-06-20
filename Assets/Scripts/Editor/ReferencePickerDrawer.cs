
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace MiniECS
{
    [CustomPropertyDrawer(typeof(ReferencePickerAttribute))]
    public class ReferencePickerDrawer : PropertyDrawer
    {
        private static readonly Dictionary<Type, Type[]> _typeCache = new();

        public SerializedProperty _currentProperty;
        private SerializedObject _serializedObject;
        private string _propertyPath;
        private FieldInfo _fieldInfo;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _currentProperty = property;
            _serializedObject = property.serializedObject;
            _propertyPath = property.propertyPath;
            _fieldInfo = _serializedObject.targetObject.GetType().GetField(_propertyPath,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);


            var container = new VisualElement();

            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                container.Add(new Label("Use [ReferencePickerDrawer] only with SerializeReference fields."));
                return container;
            }

            Type baseType = GetBaseType(property);
            if (baseType == null)
            {
                container.Add(new Label("Could not resolve base type."));
                return container;
            }

            if (!_typeCache.TryGetValue(baseType, out var types))
            {
                types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                    .OrderBy(t => t.Name)
                    .ToArray();

                _typeCache[baseType] = types;
            }

            var typeChoices = types.Select(t => t.FullName).Prepend($"(None) {property.displayName}").ToList();
            var currentType = property.managedReferenceValue?.GetType();
            var currentIndex = currentType == null ? 0 : typeChoices.IndexOf(currentType.FullName);

            var popup = new PopupField<string>("Select", typeChoices, currentIndex);

            container.Add(popup);
            var subContainer = new VisualElement();

            if (property.managedReferenceValue != null)
            {
                container.Add(subContainer);
            }

            void RefreshSubProperty()
            {
                subContainer.Clear();
                var property = _serializedObject.FindProperty(_propertyPath);
                if (property.managedReferenceValue != null)
                {
                    var iterator = property.Copy();
                    var enterChildren = true;
                    while (iterator.NextVisible(enterChildren))
                    {
                        if (SerializedProperty.EqualContents(iterator, property))
                            continue;

                        if (iterator.propertyPath.Contains(property.propertyPath + "."))
                        {
                            Label label = new(ObjectNames.NicifyVariableName(property.managedReferenceValue.GetType().Name));
                            label.style.unityFontStyleAndWeight = FontStyle.Bold;
                            label.style.paddingBottom = Length.Percent(1);
                            label.style.paddingTop = Length.Percent(1);
                            var box = new Box();
                            box.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 1f));
                            box.style.marginBottom = 4;
                            box.style.paddingLeft = 10;
                            box.style.paddingRight = 8;
                            box.style.paddingTop = 4;
                            box.style.paddingBottom = 4;

                            PropertyField field = new(iterator);
                            box.Add(field);

                            subContainer.Add(label);
                            subContainer.Add(box);

                            enterChildren = false;
                        }
                    }
                }
            }

            var serializedObject = property.serializedObject;
            var propertyPath = property.propertyPath;

            popup.RegisterValueChangedCallback(evt =>
            {
                int selectedIndex = typeChoices.IndexOf(evt.newValue);

                var property = _serializedObject.FindProperty(_propertyPath);

                if (selectedIndex == 0)
                {
                    property.managedReferenceValue = null;
                }
                else
                {
                    Type selectedType = types[selectedIndex - 1];
                    property.managedReferenceValue = Activator.CreateInstance(selectedType);
                }

                _serializedObject.ApplyModifiedProperties();
                RefreshSubProperty();
            });
            if (property.managedReferenceValue != null)
            {
                RefreshSubProperty();
            }
            return container;
        }

        private static Type GetBaseType(SerializedProperty property)
        {
            string typeName = property.managedReferenceFieldTypename;
            if (string.IsNullOrEmpty(typeName))
                return null;

            var split = typeName.Split(' ');
            if (split.Length != 2)
                return null;

            string assemblyName = split[0];
            string className = split[1];
            return Type.GetType($"{className}, {assemblyName}");
        }
    }
}