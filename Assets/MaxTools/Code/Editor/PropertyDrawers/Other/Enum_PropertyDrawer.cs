using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    [CustomPropertyDrawer(typeof(Enum), true)]
    public class Enum_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool HasFlagsAttribute()
            {
                if (fieldInfo.FieldType.IsArray)
                {
                    return fieldInfo.FieldType.GetElementType().IsDefined(typeof(FlagsAttribute));
                }
                else if (fieldInfo.FieldType.IsGenericType)
                {
                    return fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>)
                        && fieldInfo.FieldType.GetGenericArguments()[0].IsDefined(typeof(FlagsAttribute));
                }
                else
                    return fieldInfo.FieldType.IsDefined(typeof(FlagsAttribute));
            }

            if (HasFlagsAttribute())
            {
                EditorGUI.BeginProperty(position, label, property);

                EditorGUI.BeginChangeCheck();
                int mask = EditorGUI.MaskField(position, label, property.intValue, property.enumDisplayNames);
                if (EditorGUI.EndChangeCheck())
                    property.intValue = mask;

                EditorGUI.EndProperty();
            }
            else
                EditorGUI.PropertyField(position, property, label);
        }
    }
}
