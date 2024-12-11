using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    using MaxTools.Extensions;

    [CustomPropertyDrawer(typeof(EditorEnum), true)]
    public class EditorEnum_PropertyDrawer : PropertyDrawerBase
    {
        Type enumType = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 16.0f;

            SetPropertyHeight(property, 16.0f);

            var p_enumItemName = property.FindPropertyRelative("enumItemName");
            var p_enumTypeFullName = property.FindPropertyRelative("enumTypeFullName");

            if (p_enumItemName.stringValue.IsNullOrEmpty() ||
                p_enumTypeFullName.stringValue.IsNullOrEmpty())
            {
                EditorGUI.LabelField(position, label.text, "Variable type is not defined!");
                return;
            }

            if (enumType == null)
                enumType = Tools.GetTypeByFullName(p_enumTypeFullName.stringValue);

            List<string> options = new List<string>(Enum.GetNames(enumType));

            int selectedIndex = options.FindIndex((item) => item == p_enumItemName.stringValue);

            EditorGUI.BeginProperty(position, null, property);

            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, options.ToArray());
            if (EditorGUI.EndChangeCheck())
                p_enumItemName.stringValue = options[selectedIndex];

            EditorGUI.EndProperty();
        }
    }
}
