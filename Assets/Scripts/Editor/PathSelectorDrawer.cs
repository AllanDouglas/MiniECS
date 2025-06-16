using MiniECS.Framework;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(PathSelectorAttribute))]
public class PathSelectorUIDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Only allow the attribute to be used on string properties
        if (property.propertyType != SerializedPropertyType.String)
        {
            return new Label("Use PathSelector with string.");
        }

        var container = new VisualElement();

        var horizontalContainer = new VisualElement();

        horizontalContainer.style.flexDirection = FlexDirection.Row;
        horizontalContainer.style.paddingRight = 2;
        container.Add(horizontalContainer);

        var textField = new TextField(property.displayName);
        textField.style.width= Length.Percent(85);
         //= Length.Percent(70);
        textField.style.justifyContent = Justify.FlexEnd;

        textField.bindingPath = property.propertyPath;

        textField.BindProperty(property);

        var pathButton = new Button(() =>
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");

            if (!string.IsNullOrEmpty(selectedPath))
            {

                if (selectedPath.StartsWith(Application.dataPath))
                {
                    selectedPath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                }

                property.stringValue = selectedPath;
                property.serializedObject.ApplyModifiedProperties();
                textField.value = selectedPath;
            }
        })
        {
            text = "Select"
        };
        //pathButton.style.maxWidth = Length.Percent(50);
        //pathButton.style.minWidth = Length.Percent(30);
        pathButton.style.justifyContent = Justify.FlexEnd;

        // Add the text field and button to the container
        horizontalContainer.Add(textField);
        horizontalContainer.Add(pathButton);

        return container;
    }
}
