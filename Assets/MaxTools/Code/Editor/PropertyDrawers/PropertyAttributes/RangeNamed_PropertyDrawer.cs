using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    [CustomPropertyDrawer(typeof(RangeNamedAttribute), true)]
    public class RangeNamed_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rangeNamed = attribute as RangeNamedAttribute;

            position.height = 16.0f;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        EditorGUI.BeginChangeCheck();
                        var value = (int)EditorGUI.Slider(position, label,
                            property.intValue, rangeNamed.min, rangeNamed.max);
                        if (EditorGUI.EndChangeCheck())
                            property.intValue = value;
                    }
                    break;

                case SerializedPropertyType.Float:
                    {
                        EditorGUI.BeginChangeCheck();
                        var value = EditorGUI.Slider(position, label,
                            property.floatValue, rangeNamed.min, rangeNamed.max);
                        if (EditorGUI.EndChangeCheck())
                            property.floatValue = value;
                    }
                    break;
            }

            position.y += 16.0f;
            position.x += EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(position, rangeNamed.minName);

            position.x += position.width - EditorGUIUtility.labelWidth;
            position.x -= GUI.skin.label.CalcSize(new GUIContent(rangeNamed.maxName)).x + 55.0f;
            EditorGUI.LabelField(position, rangeNamed.maxName);

            SetPropertyHeight(property, 32.0f);
        }
    }
}
