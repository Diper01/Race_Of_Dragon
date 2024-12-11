using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace MaxTools.Editor.Extensions
{
    using MaxTools.Extensions;

    using Editor = UnityEditor.Editor;

    public static class EX_Tools
    {
        public static void Initialize(this Editor editor)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var field in editor.GetType().GetFields(bindingFlags))
            {
                if (field.FieldType == typeof(SerializedProperty))
                {
                    var property = editor.serializedObject.FindProperty(field.Name);

                    if (property != null)
                    {
                        field.SetValue(editor, property);
                    }
                }
            }
        }

        public static object GetValueByType(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.AnimationCurve: return property.animationCurveValue;
                case SerializedPropertyType.Boolean: return property.boolValue;
                case SerializedPropertyType.Bounds: return property.boundsValue;
                case SerializedPropertyType.BoundsInt: return property.boundsIntValue;
                case SerializedPropertyType.Color: return property.colorValue;
                case SerializedPropertyType.Enum: return property.enumValueIndex;
                case SerializedPropertyType.ExposedReference: return property.exposedReferenceValue;
                case SerializedPropertyType.Float: return property.floatValue;
                case SerializedPropertyType.Integer: return property.intValue;
                case SerializedPropertyType.ObjectReference: return property.objectReferenceValue;
                case SerializedPropertyType.Quaternion: return property.quaternionValue;
                case SerializedPropertyType.Rect: return property.rectValue;
                case SerializedPropertyType.RectInt: return property.rectIntValue;
                case SerializedPropertyType.String: return property.stringValue;
                case SerializedPropertyType.Vector2: return property.vector2Value;
                case SerializedPropertyType.Vector2Int: return property.vector2IntValue;
                case SerializedPropertyType.Vector3: return property.vector3Value;
                case SerializedPropertyType.Vector3Int: return property.vector3IntValue;
                case SerializedPropertyType.Vector4: return property.vector4Value;
            }

            throw new Exception("[SerializedProperty.GetValueByType] Invalid propertyType!");
        }
        public static object GetValue(this SerializedProperty property)
        {
            try
            {
                return property.GetValueByType();
            }
            catch
            {
                return property.serializedObject.targetObject.GetNestedInstance(property.GetPropertyPath());
            }
        }

        public static void SetValueByType(this SerializedProperty property, object value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.AnimationCurve: property.animationCurveValue = (AnimationCurve)value; return;
                case SerializedPropertyType.Boolean: property.boolValue = (bool)value; return;
                case SerializedPropertyType.Bounds: property.boundsValue = (Bounds)value; return;
                case SerializedPropertyType.BoundsInt: property.boundsIntValue = (BoundsInt)value; return;
                case SerializedPropertyType.Color: property.colorValue = (Color)value; return;
                case SerializedPropertyType.Enum: property.enumValueIndex = (int)value; return;
                case SerializedPropertyType.ExposedReference: property.exposedReferenceValue = (UnityEngine.Object)value; return;
                case SerializedPropertyType.Float: property.floatValue = (float)value; return;
                case SerializedPropertyType.Integer: property.intValue = (int)value; return;
                case SerializedPropertyType.ObjectReference: property.objectReferenceValue = (UnityEngine.Object)value; return;
                case SerializedPropertyType.Quaternion: property.quaternionValue = (Quaternion)value; return;
                case SerializedPropertyType.Rect: property.rectValue = (Rect)value; return;
                case SerializedPropertyType.RectInt: property.rectIntValue = (RectInt)value; return;
                case SerializedPropertyType.String: property.stringValue = (string)value; return;
                case SerializedPropertyType.Vector2: property.vector2Value = (Vector2)value; return;
                case SerializedPropertyType.Vector2Int: property.vector2IntValue = (Vector2Int)value; return;
                case SerializedPropertyType.Vector3: property.vector3Value = (Vector3)value; return;
                case SerializedPropertyType.Vector3Int: property.vector3IntValue = (Vector3Int)value; return;
                case SerializedPropertyType.Vector4: property.vector4Value = (Vector4)value; return;
            }

            throw new Exception("[SerializedProperty.SetValueByType] Invalid propertyType!");
        }
        public static void SetValue(this SerializedProperty property, object value)
        {
            try
            {
                property.SetValueByType(value);
            }
            catch
            {
                property.serializedObject.targetObject.SetNestedInstance(property.GetPropertyPath(), value);
            }
        }

        public static Type GetPropertyType(this SerializedProperty property)
        {
            var pathElements = property.GetPropertyPath()
                .Split('.', '/', '\\')
                .Select((part) => Tools.GetVariableNameWithoutArrayIndices(part)).ToArray();

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var nextType = property.serializedObject.targetObject.GetType();

            for (int i = 0; i < pathElements.Length; ++i)
            {
                nextType = nextType.GetMember(pathElements[i], bindingFlags)[0].GetVariableType();

                if (nextType.IsArray)
                {
                    nextType = nextType.GetElementType();
                }
            }

            return nextType;
        }

        public static string GetPropertyPath(this SerializedProperty property)
        {
            var pathElements = property.propertyPath.Split('.').ToList();

            for (int i = 0; i < pathElements.Count; ++i)
            {
                if (pathElements[i].Contains("["))
                {
                    pathElements[i - 2] += $"[{Tools.GetArrayIndicesWithoutVariableName(pathElements[i])[0].ToString()}]";

                    pathElements.RemoveAt(i);
                    pathElements.RemoveAt(i - 1);

                    i -= 2;
                }
            }

            return string.Join(".", pathElements);
        }

        public static SerializedProperty[] GetArrayElements(this SerializedProperty array)
        {
            var elements = new SerializedProperty[array.arraySize];

            for (int i = 0; i < array.arraySize; ++i)
            {
                elements[i] = array.GetArrayElementAtIndex(i);
            }

            return elements;
        }

        public static SerializedProperty FindPropertyNear(this SerializedProperty property, string propertyName)
        {
            var varNames = property.propertyPath.Split('.');

            varNames[varNames.Length - 1] = propertyName;

            return property.serializedObject.FindProperty(string.Join(".", varNames));
        }
    }
}
