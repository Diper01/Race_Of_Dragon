using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_Camera
    {
        public static void SetOrthographicWidth(this Camera me, float width, float pixelsPerUnit = 100.0f)
        {
            float k = (float)Screen.height / Screen.width;

            me.orthographicSize = width / 2.0f * k / pixelsPerUnit;
        }
        public static void SetOrthographicHeight(this Camera me, float height, float pixelsPerUnit = 100.0f)
        {
            me.orthographicSize = height / 2.0f / pixelsPerUnit;
        }

        public static Rect GetWorldRect(this Camera me)
        {
            if (!me.orthographic)
            {
                throw new Exception("Orthographic mode only!");
            }

            var world_min = me.ScreenToWorldPoint(me.pixelRect.min);
            var world_max = me.ScreenToWorldPoint(me.pixelRect.max);

            var delta = world_max - world_min;

            var w = Tools.Abs(delta.Project(me.transform.right));
            var h = Tools.Abs(delta.Project(me.transform.up));

            var rect = new Rect(world_min.x, world_min.y, w, h);

            rect.min = world_min;
            rect.max = world_max;

            return rect;
        }
    }
}
