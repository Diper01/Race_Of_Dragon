using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    [CustomPropertyDrawer(typeof(Range01Attribute), true)]
    public class Range01_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.Slider(position, label, property.floatValue, 0.0f, 1.0f);
            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;
        }
    }
}
