using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using MaxTools.Extensions;

    [CustomPropertyDrawer(typeof(MaxAttribute), true)]
    public class MaxAttribute_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var maxAttribute = attribute as MaxAttribute;

            property.VisualizeRect(position, label);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = Tools.ClampMax(property.intValue, (int)maxAttribute.max);
                    break;

                case SerializedPropertyType.Float:
                    property.floatValue = Tools.ClampMax(property.floatValue, maxAttribute.max);
                    break;

                case SerializedPropertyType.Vector2:
                    property.vector2Value = property.vector2Value.ClampMax(maxAttribute.max);
                    break;

                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = property.vector2IntValue.ClampMax((int)maxAttribute.max);
                    break;

                case SerializedPropertyType.Vector3:
                    property.vector3Value = property.vector3Value.ClampMax(maxAttribute.max);
                    break;

                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = property.vector3IntValue.ClampMax((int)maxAttribute.max);
                    break;
            }
        }
    }
}
