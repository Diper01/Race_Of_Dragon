using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using MaxTools.Extensions;

    [CustomPropertyDrawer(typeof(StaticMethod), true)]
    public class StaticMethod_PropertyDrawer : PropertyDrawerBase
    {
        static List<string> options = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var totalHeight = 16.0f;

            if (options == null)
            {
                options = new List<string>() { "None", "" };

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

                foreach (var type in Tools.GetAllTypes())
                {
                    foreach (var method in type.GetMethods(bindingFlags | BindingFlags.Static))
                    {
                        if (method.IsDefined(typeof(StaticMethodAttribute)))
                        {
                            options.Add($"{type.FullName}/{method.Name}");
                        }
                    }
                }
            }

            if (options.Count > 2)
            {
                var methodPath = property.FindPropertyRelative("methodPath");

                int popupIndex = 0;

                if (!methodPath.stringValue.IsNullOrEmpty())
                {
                    int i = options.IndexOf(methodPath.stringValue);

                    if (i > 0)
                    {
                        popupIndex = i;
                    }
                }

                var oldWidth = position.width;

                position.height = 16.0f;
                position.x = EditorGUIUtility.labelWidth + 14.0f - EditorGUI.indentLevel * 15.0f;
                position.width = position.width - EditorGUIUtility.labelWidth + EditorGUI.indentLevel * 15.0f;

                EditorGUI.BeginProperty(position, null, methodPath);

                EditorGUI.BeginChangeCheck();
                string option = options[EditorGUI.Popup(position, popupIndex, options.ToArray())];
                if (EditorGUI.EndChangeCheck())
                    methodPath.stringValue = option;

                EditorGUI.EndProperty();

                position.x = 14.0f;
                position.width = EditorGUIUtility.labelWidth;

                if (property.VisualizeRect(position, label) && !methodPath.hasMultipleDifferentValues)
                {
                    var extraParams = property.FindPropertyRelative("extraParams");

                    EditorGUI.indentLevel++;

                    position.y += 18.0f;
                    position.width = oldWidth;

                    extraParams.VisualizeRect(position, true);

                    totalHeight += EditorGUI.GetPropertyHeight(extraParams) + 2.0f;

                    EditorGUI.indentLevel--;
                }
            }
            else
                EditorGUI.LabelField(position, label.text, "No static methods!");

            SetPropertyHeight(property, totalHeight);
        }
    }
}
