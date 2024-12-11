using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    [CustomPropertyDrawer(typeof(EditorVariable), true)]
    public class EditorVariable_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var variableType = property.FindPropertyRelative("variableType");
            var intVariable = property.FindPropertyRelative("intVariable");
            var boolVariable = property.FindPropertyRelative("boolVariable");
            var floatVariable = property.FindPropertyRelative("floatVariable");
            var stringVariable = property.FindPropertyRelative("stringVariable");
            var colorVariable = property.FindPropertyRelative("colorVariable");
            var vector2Variable = property.FindPropertyRelative("vector2Variable");
            var vector3Variable = property.FindPropertyRelative("vector3Variable");
            var vector2IntVariable = property.FindPropertyRelative("vector2IntVariable");
            var vector3IntVariable = property.FindPropertyRelative("vector3IntVariable");
            var objectVariable = property.FindPropertyRelative("objectVariable");
            var staticVariable = property.FindPropertyRelative("staticVariable");

            var oldWidth = position.width;

            var totalHeight = 16.0f;
            position.height = 16.0f;
            position.x = EditorGUIUtility.labelWidth + 14.0f - EditorGUI.indentLevel * 15.0f;
            position.width = position.width - EditorGUIUtility.labelWidth + EditorGUI.indentLevel * 15.0f;

            variableType.VisualizeRect(position, string.Empty);

            position.x = 14.0f;
            position.width = EditorGUIUtility.labelWidth;

            if (property.VisualizeRect(position, label) && !variableType.hasMultipleDifferentValues)
            {
                EditorGUI.indentLevel++;

                totalHeight += 18.0f;
                position.y += 18.0f;
                position.width = oldWidth;

                switch ((EditorVariable.VariableType)variableType.intValue)
                {
                    case EditorVariable.VariableType.Int:
                        intVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Bool:
                        boolVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Float:
                        floatVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.String:
                        stringVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Color:
                        colorVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Vector2:
                        vector2Variable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Vector3:
                        vector3Variable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Vector2Int:
                        vector2IntVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Vector3Int:
                        vector3IntVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Object:
                        objectVariable.VisualizeRect(position, "Value");
                        break;

                    case EditorVariable.VariableType.Static:
                        staticVariable.VisualizeRect(position, "Value");
                        totalHeight += EditorGUI.GetPropertyHeight(staticVariable) - 16.0f;
                        break;
                }

                EditorGUI.indentLevel--;
            }

            SetPropertyHeight(property, totalHeight);
        }
    }
}
