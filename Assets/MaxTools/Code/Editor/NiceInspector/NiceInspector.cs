using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using Editor = UnityEditor.Editor;

    public static class NiceInspector
    {
        public static bool HasNiceInspector(Editor editor)
        {
            return editor.target.GetType().IsDefined(typeof(NiceInspectorAttribute));
        }

        public static float DrawNiceInspectorLayout(NiceGeneralPack generalPack)
        {
            var position = EditorTools.GetNextRect();

            position.y -= 2.0f;

            var totalHeight = 0.0f;

            generalPack.DrawProperties(ref position, ref totalHeight);
            generalPack.DrawMethods(ref position, ref totalHeight);

            GUILayout.Space(totalHeight - 18.0f);

            return totalHeight;
        }

        public static float DrawNiceInspectorRect(Rect position, SerializedProperty property, GUIContent label, NiceGeneralPack generalPack)
        {
            var totalHeight = 16.0f;

            position.height = totalHeight;

            if (property.VisualizeRect(position, label))
            {
                EditorGUI.indentLevel++;

                position.y += 16.0f;

                generalPack.DrawProperties(ref position, ref totalHeight);
                generalPack.DrawMethods(ref position, ref totalHeight);

                EditorGUI.indentLevel--;
            }

            return totalHeight;
        }
    }
}
