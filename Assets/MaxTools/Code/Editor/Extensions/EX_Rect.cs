using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor.Extensions
{
    public static class EX_Rect
    {
        public static Rect IndentAdd(this Rect me)
        {
            return new Rect(
                me.x + EditorGUI.indentLevel * 15.0f,
                me.y,
                me.width - EditorGUI.indentLevel * 15.0f,
                me.height);
        }
        public static Rect IndentSub(this Rect me)
        {
            return new Rect(
                me.x - EditorGUI.indentLevel * 15.0f,
                me.y,
                me.width + EditorGUI.indentLevel * 15.0f,
                me.height);
        }

        public static Rect[] IndentAdd(this Rect[] me)
        {
            var rects = new Rect[me.Length];

            for (int i = 0; i < rects.Length; ++i)
            {
                rects[i] = me[i].IndentAdd();
            }

            return rects;
        }
        public static Rect[] IndentSub(this Rect[] me)
        {
            var rects = new Rect[me.Length];

            for (int i = 0; i < rects.Length; ++i)
            {
                rects[i] = me[i].IndentSub();
            }

            return rects;
        }

        public static Rect[] Split(this Rect me, int count)
        {
            var splittedRects = new Rect[count];

            var fieldWidth = me.width / splittedRects.Length;

            for (int i = 0; i < splittedRects.Length; ++i)
            {
                splittedRects[i] = new Rect(
                    me.position.x + i * fieldWidth,
                    me.position.y,
                    fieldWidth,
                    me.height);
            }

            return splittedRects;
        }
        public static Rect[] Split(this Rect me, int count, int space)
        {
            var splittedRects = new Rect[count];

            var totalSpace = space * (splittedRects.Length - 1);
            var totalWidth = me.width - totalSpace;
            var fieldWidth = totalWidth / splittedRects.Length;

            for (int i = 0; i < splittedRects.Length; ++i)
            {
                splittedRects[i] = new Rect(
                    me.position.x + i * (fieldWidth + space),
                    me.position.y,
                    fieldWidth,
                    me.height);
            }

            return splittedRects;
        }
    }
}
