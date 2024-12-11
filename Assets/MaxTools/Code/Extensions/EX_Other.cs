using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_Other
    {
        public static int ToInt(this Enum me)
        {
            return (int)(object)me;
        }

        public static Vector2 GetPositionRelativeCenter(this Touch me)
        {
            return me.position - new Vector2(Screen.width, Screen.height) / 2.0f;
        }

        public static Vector2 GetPivot01(this Sprite me)
        {
            return me.pivot / new Vector2(me.texture.width, me.texture.height);
        }
    }
}
