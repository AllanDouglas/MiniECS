using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;

namespace MiniECS
{
    // [CustomEditor(typeof(EntityController))]
    public sealed class EntityControllerEditor : UnityEditor.Editor
    {
        private static List<Type> s_Types;
        private VisualElement _root;
        private EntityController _controller;

        public override VisualElement CreateInspectorGUI()
        {
            _controller = (EntityController)target;
            _root = new VisualElement();

            DrawComponentList();
            DrawAddButton();

            return _root;
        }

        private void DrawComponentList()
        {
            var componentsBox = new Box();
            componentsBox.style.marginTop = Length.Percent(1);


            Label headerLabel = new("Components:");
            headerLabel.style.fontSize = 16;
            headerLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            componentsBox.Add(headerLabel);
            _root.Add(componentsBox);

            var so = new SerializedObject(_controller);
            var componentsProp = so.FindProperty("_components");

            if (componentsProp == null || !componentsProp.isArray || componentsProp.arraySize == 0)
            {
                var box = new Box();
                box.style.backgroundColor = new(new Color(0, 0, 0, 0));
                box.style.alignItems = Align.Center;
                box.Add(new Label("(No components)"));
                box.style.marginBottom = 5;
                box.style.marginTop = 4;
                box.style.paddingLeft = Length.Percent(2);
                box.style.paddingRight = Length.Percent(2);
                componentsBox.Add(box);
                return;
            }

            for (int i = 0; i < componentsProp.arraySize; i++)
            {
                var elementProp = componentsProp.GetArrayElementAtIndex(i);

                var box = new Box();
                box.style.backgroundColor = new(new Color(0, 0, 0, 0));
                box.style.paddingLeft = 4;
                box.style.paddingLeft = Length.Percent(2);
                box.style.paddingRight = Length.Percent(2);

                // var label = new Label(elementProp.managedReferenceFullTypename.Split('.').Last());
                // label.style.unityFontStyleAndWeight = FontStyle.Bold;
                // box.Add(label);

                var field = new PropertyField(elementProp, ObjectNames.NicifyVariableName(elementProp.managedReferenceFullTypename.Split('.').Last()));
                field.Bind(so);
                field.RegisterCallback<GeometryChangedEvent>(_ =>
                {
                    ExpandRecursive(field);
                });
                box.Add(field);

                int indexToRemove = i;
                var removeButton = new Button(() =>
                {
                    Undo.RecordObject(_controller, "Remove Component");
                    // _controller.RemoveComponentAt(indexToRemove);
                    EditorUtility.SetDirty(_controller);
                    Refresh();
                })
                { text = "Remove" };
                box.Add(removeButton);

                componentsBox.Add(box);
            }
        }
        private void DrawAddButton()
        {
            var types = GetAllComponentTypes()
                .Select(t => (ObjectNames.NicifyVariableName(t.Name), t))
                .Prepend(("Empty", typeof(void)))
                .ToList();

            var popup = new PopupField<(string name, Type type)>("Add Component", types, 0, t => t.name, t => t.name);
            var addButton = new Button(() =>
            {
                var element = popup.value;
                if (element.type == null || element.type == typeof(void))
                {
                    return;
                }

                var instance = (IComponent)Activator.CreateInstance(element.type);
                Undo.RecordObject(_controller, "Add Component");
                // _controller.AddComponent(instance);  
                EditorUtility.SetDirty(_controller);
                Refresh();
            })
            { text = "Add" };
            var box = new Box();
            box.Add(popup);
            box.Add(addButton);
            _root.Add(box);

        }

        private List<Type> GetAllComponentTypes()
        {
            return s_Types ??= AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .Where(t =>
                                typeof(IComponent).IsAssignableFrom(t) &&
                                !t.IsInterface && !t.IsAbstract &&
                                (t.IsClass || t.IsValueType))
                            .OrderBy(t => t.Name)
                            .ToList();

        }

        private void Refresh()
        {
            _root.Clear();
            DrawComponentList();
            DrawAddButton();
        }

        private void ExpandRecursive(VisualElement element)
        {
            foreach (var child in element.Children())
            {
                // Força o Foldout a expandir, se houver algum
                if (child is Foldout foldout)
                {
                    foldout.value = true;
                }

                ExpandRecursive(child);
            }
        }
    }
}