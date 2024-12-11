using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    [CustomPropertyDrawer(typeof(MinMaxUnlimitedAttribute), true)]
    public class MinMaxUnlimitedAttribute_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect controlRect = EditorGUI.PrefixLabel(position, label);

            Rect[] splittedRects = GetSplittedRects(controlRect);

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                var p_x = property.FindPropertyRelative("x");
                var p_y = property.FindPropertyRelative("y");

                var minValue = p_x.floatValue;

                EditorGUI.LabelField(splittedRects[0], "Min");

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[1], null, p_x);
                minValue = EditorGUI.DelayedFloatField(splittedRects[1], minValue);
                EditorGUI.EndProperty();

                if (minValue > p_y.floatValue)
                {
                    p_y.floatValue = minValue;
                }

                if (EditorGUI.EndChangeCheck())
                {
                    p_x.floatValue = minValue;
                }

                var maxValue = p_y.floatValue;

                EditorGUI.LabelField(splittedRects[2], "Max");

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[3], null, p_y);
                maxValue = EditorGUI.DelayedFloatField(splittedRects[3], maxValue);
                EditorGUI.EndProperty();

                if (maxValue < p_x.floatValue)
                {
                    p_x.floatValue = maxValue;
                }

                if (EditorGUI.EndChangeCheck())
                {
                    p_y.floatValue = maxValue;
                }
            }
            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                var p_x = property.FindPropertyRelative("x");
                var p_y = property.FindPropertyRelative("y");

                var minValue = p_x.intValue;
                var maxValue = p_y.intValue;

                EditorGUI.LabelField(splittedRects[0], "Min");

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[1], null, p_x);
                minValue = EditorGUI.DelayedIntField(splittedRects[1], minValue);
                EditorGUI.EndProperty();

                if (minValue > maxValue)
                    minValue = maxValue;

                if (EditorGUI.EndChangeCheck())
                {
                    p_x.intValue = minValue;
                }

                EditorGUI.LabelField(splittedRects[2], "Max");

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[3], null, p_y);
                maxValue = EditorGUI.DelayedIntField(splittedRects[3], maxValue);
                EditorGUI.EndProperty();

                if (maxValue < minValue)
                    maxValue = minValue;

                if (EditorGUI.EndChangeCheck())
                {
                    p_y.intValue = maxValue;
                }
            }
        }

        Rect[] GetSplittedRects(Rect inputRect)
        {
            var splittedRects = inputRect.Split(4);

            var deltaWidth = splittedRects[0].width - 26.0f;

            if (deltaWidth > 0.0f)
            {
                splittedRects[0].width -= deltaWidth;
                splittedRects[1].x -= deltaWidth;
                splittedRects[1].width += deltaWidth - 4.0f;
            }

            deltaWidth = splittedRects[2].width - 30.0f;

            if (deltaWidth > 0.0f)
            {
                splittedRects[2].width -= deltaWidth;
                splittedRects[3].x -= deltaWidth;
                splittedRects[3].width += deltaWidth;
            }

            return splittedRects.IndentSub();
        }
    }
}
