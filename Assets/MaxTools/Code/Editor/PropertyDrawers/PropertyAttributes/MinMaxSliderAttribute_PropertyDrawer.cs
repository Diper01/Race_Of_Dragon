using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute), true)]
    public class MinMaxSliderAttribute_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxAttribute = (MinMaxSliderAttribute)attribute;

            label.tooltip = $"{minMaxAttribute.min} : {minMaxAttribute.max}";

            Rect controlRect = EditorGUI.PrefixLabel(position, label);

            Rect[] splittedRects = GetSplittedRects(controlRect);

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                var p_x = property.FindPropertyRelative("x");
                var p_y = property.FindPropertyRelative("y");

                float minValue = p_x.floatValue;
                float maxValue = p_y.floatValue;

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[0], null, p_x);
                minValue = EditorGUI.DelayedFloatField(splittedRects[0], minValue);
                EditorGUI.EndProperty();

                if (minValue > maxValue)
                    minValue = maxValue;

                if (minValue < minMaxAttribute.min)
                    minValue = minMaxAttribute.min;

                if (EditorGUI.EndChangeCheck())
                {
                    p_x.floatValue = minValue;
                }

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[2], null, p_y);
                maxValue = EditorGUI.DelayedFloatField(splittedRects[2], maxValue);
                EditorGUI.EndProperty();

                if (maxValue < minValue)
                    maxValue = minValue;

                if (maxValue > minMaxAttribute.max)
                    maxValue = minMaxAttribute.max;

                if (EditorGUI.EndChangeCheck())
                {
                    p_y.floatValue = maxValue;
                }

                float oldMinValue = minValue;
                float oldMaxValue = maxValue;

                EditorGUI.MinMaxSlider(splittedRects[1], ref minValue, ref maxValue, minMaxAttribute.min, minMaxAttribute.max);

                if (oldMinValue != minValue)
                {
                    p_x.floatValue = minValue;
                }

                if (oldMaxValue != maxValue)
                {
                    p_y.floatValue = maxValue;
                }
            }
            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                var p_x = property.FindPropertyRelative("x");
                var p_y = property.FindPropertyRelative("y");

                float minValue = p_x.intValue;
                float maxValue = p_y.intValue;

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[0], null, p_x);
                minValue = EditorGUI.DelayedIntField(splittedRects[0], (int)minValue);
                EditorGUI.EndProperty();

                if (minValue > maxValue)
                    minValue = maxValue;

                if (minValue < minMaxAttribute.min)
                    minValue = minMaxAttribute.min;

                if (EditorGUI.EndChangeCheck())
                {
                    p_x.intValue = Tools.RoundToInt(minValue);
                }

                EditorGUI.BeginChangeCheck();

                EditorGUI.BeginProperty(splittedRects[2], null, p_y);
                maxValue = EditorGUI.DelayedIntField(splittedRects[2], (int)maxValue);
                EditorGUI.EndProperty();

                if (maxValue < minValue)
                    maxValue = minValue;

                if (maxValue > minMaxAttribute.max)
                    maxValue = minMaxAttribute.max;

                if (EditorGUI.EndChangeCheck())
                {
                    p_y.intValue = Tools.RoundToInt(maxValue);
                }

                float oldMinValue = minValue;
                float oldMaxValue = maxValue;

                EditorGUI.MinMaxSlider(splittedRects[1], ref minValue, ref maxValue, minMaxAttribute.min, minMaxAttribute.max);

                if (oldMinValue != minValue)
                {
                    p_x.intValue = Tools.RoundToInt(minValue);
                }

                if (oldMaxValue != maxValue)
                {
                    p_y.intValue = Tools.RoundToInt(maxValue);
                }
            }
        }

        Rect[] GetSplittedRects(Rect inputRect)
        {
            var splittedRects = inputRect.Split(3, 5);

            var deltaWidth = splittedRects[0].width - 100.0f;

            if (deltaWidth > 0.0f)
            {
                splittedRects[0].width -= deltaWidth;
                splittedRects[1].x -= deltaWidth;
                splittedRects[1].width += deltaWidth * 2.0f;
                splittedRects[2].x += deltaWidth;
                splittedRects[2].width -= deltaWidth;
            }

            return splittedRects.IndentSub();
        }
    }
}
