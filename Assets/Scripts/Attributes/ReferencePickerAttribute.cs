using UnityEngine;
namespace MiniECS
{
    public class ReferencePickerAttribute : PropertyAttribute
    {
        public readonly bool DrawProperty;

        public ReferencePickerAttribute(bool drawProperty = true)
        {
            DrawProperty = drawProperty;
        }
    }
}