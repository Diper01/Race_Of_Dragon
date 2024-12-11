using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor.Extensions
{
    using MaxTools.Extensions;

    public static class EX_Visualize
    {
        public static bool VisualizeLayout(this SerializedProperty property)
        {
            return EditorGUILayout.PropertyField(property);
        }
        public static bool VisualizeLayout(this SerializedProperty property, string label)
        {
            return EditorGUILayout.PropertyField(property, new GUIContent(label));
        }
        public static bool VisualizeLayout(this SerializedProperty property, GUIContent label)
        {
            return EditorGUILayout.PropertyField(property, label);
        }
        public static bool VisualizeLayout(this SerializedProperty property, bool includeChildren)
        {
            return EditorGUILayout.PropertyField(property, includeChildren);
        }
        public static bool VisualizeLayout(this SerializedProperty property, string label, bool includeChildren)
        {
            return EditorGUILayout.PropertyField(property, new GUIContent(label), includeChildren);
        }
        public static bool VisualizeLayout(this SerializedProperty property, GUIContent label, bool includeChildren)
        {
            return EditorGUILayout.PropertyField(property, label, includeChildren);
        }

        public static bool VisualizeRect(this SerializedProperty property, Rect position)
        {
            return EditorGUI.PropertyField(position, property);
        }
        public static bool VisualizeRect(this SerializedProperty property, Rect position, string label)
        {
            return EditorGUI.PropertyField(position, property, new GUIContent(label));
        }
        public static bool VisualizeRect(this SerializedProperty property, Rect position, GUIContent label)
        {
            return EditorGUI.PropertyField(position, property, label);
        }
        public static bool VisualizeRect(this SerializedProperty property, Rect position, bool includeChildren)
        {
            return EditorGUI.PropertyField(position, property, includeChildren);
        }
        public static bool VisualizeRect(this SerializedProperty property, Rect position, string label, bool includeChildren)
        {
            return EditorGUI.PropertyField(position, property, new GUIContent(label), includeChildren);
        }
        public static bool VisualizeRect(this SerializedProperty property, Rect position, GUIContent label, bool includeChildren)
        {
            return EditorGUI.PropertyField(position, property, label, includeChildren);
        }

        public static void SourceScriptLayout(this SerializedObject me)
        {
            EditorGUI.BeginDisabledGroup(true);
            me.FindProperty("m_Script").VisualizeLayout();
            EditorGUI.EndDisabledGroup();
        }
        public static void SourceScriptRect(this SerializedObject me, Rect position)
        {
            EditorGUI.BeginDisabledGroup(true);
            me.FindProperty("m_Script").VisualizeRect(position);
            EditorGUI.EndDisabledGroup();
        }

        public static void FoldoutLayout(this SerializedProperty me, string tabName, SerializedProperty toggle = null)
        {
            me.FoldoutRect(EditorTools.GetNextRect(), tabName, toggle);
        }
        public static void FoldoutRect(this SerializedProperty me, Rect position, string tabName, SerializedProperty toggle = null)
        {
            position.height = 16.0f;

            EditorGUI.DrawRect(position.IndentSub(), Color.gray.GetWithAlpha32(100));

            if (toggle != null)
            {
                var oldWidth = position.width;
                position.width = 16.0f + EditorGUI.indentLevel * 15.0f;

                EditorGUI.BeginProperty(position, null, toggle);

                EditorGUI.BeginChangeCheck();
                var toggleValue = EditorGUI.Toggle(position, toggle.boolValue);
                if (EditorGUI.EndChangeCheck())
                    toggle.boolValue = toggleValue;

                EditorGUI.EndProperty();

                position.width = oldWidth;
                position.x += 28.0f;
                position.width -= 28.0f;
            }

            EditorGUI.BeginChangeCheck();
            var foldoutValue = EditorGUI.Foldout(position, me.boolValue, tabName, true);
            if (EditorGUI.EndChangeCheck())
                me.boolValue = foldoutValue;
        }
    }
}
