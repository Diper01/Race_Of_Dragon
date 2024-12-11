using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    public static class EditorTools
    {
        public static Rect GetLastRect()
        {
            return GUILayoutUtility.GetLastRect();
        }

        public static Rect GetNextRect()
        {
            EditorGUILayout.LabelField(string.Empty);

            return GUILayoutUtility.GetLastRect();
        }
    }
}
