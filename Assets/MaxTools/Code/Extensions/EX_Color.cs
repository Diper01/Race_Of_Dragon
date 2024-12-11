using UnityEngine;

namespace MaxTools.Extensions
{
    public static class EX_Color
    {
        public static bool IsOne(this Color me, bool considerAlpha = true)
        {
            if (considerAlpha)
            {
                return me.r == 1.0f
                    && me.g == 1.0f
                    && me.b == 1.0f
                    && me.a == 1.0f;
            }
            else
            {
                return me.r == 1.0f
                    && me.g == 1.0f
                    && me.b == 1.0f;
            }
        }
        public static bool IsOne(this Color32 me, bool considerAlpha = true)
        {
            if (considerAlpha)
            {
                return me.r == 255
                    && me.g == 255
                    && me.b == 255
                    && me.a == 255;
            }
            else
            {
                return me.r == 255
                    && me.g == 255
                    && me.b == 255;
            }
        }

        public static bool IsClear(this Color me, bool considerAlpha = true)
        {
            if (considerAlpha)
            {
                return me.r == 0.0f
                    && me.g == 0.0f
                    && me.b == 0.0f
                    && me.a == 0.0f;
            }
            else
            {
                return me.r == 0.0f
                    && me.g == 0.0f
                    && me.b == 0.0f;
            }
        }
        public static bool IsClear(this Color32 me, bool considerAlpha = true)
        {
            if (considerAlpha)
            {
                return me.r == 0
                    && me.g == 0
                    && me.b == 0
                    && me.a == 0;
            }
            else
            {
                return me.r == 0
                    && me.g == 0
                    && me.b == 0;
            }
        }

        public static float GetMagnitude(this Color me)
        {
            return Tools.Sqrt(me.GetSqrMagnitude());
        }
        public static float GetSqrMagnitude(this Color me)
        {
            return me.r * me.r +
                   me.g * me.g +
                   me.b * me.b +
                   me.a * me.a;
        }

        public static float GetMagnitude(this Color32 me)
        {
            return Tools.Sqrt(me.GetSqrMagnitude());
        }
        public static float GetSqrMagnitude(this Color32 me)
        {
            return me.r * me.r +
                   me.g * me.g +
                   me.b * me.b +
                   me.a * me.a;
        }

        public static Color GetWithAlpha01(this Color me, float alpha)
        {
            me.a = Tools.Clamp01(alpha);

            return me;
        }
        public static Color GetWithAlpha32(this Color me, int alpha)
        {
            me.a = Tools.Clamp01(alpha / 255.0f);

            return me;
        }

        public static Color32 GetWithAlpha01(this Color32 me, float alpha)
        {
            me.a = (byte)Tools.Clamp(alpha * 255, 0, 255);

            return me;
        }
        public static Color32 GetWithAlpha32(this Color32 me, int alpha)
        {
            me.a = (byte)Tools.Clamp(alpha, 0, 255);

            return me;
        }

        public static Color GetClamped(this Color me)
        {
            me.r = Tools.Clamp01(me.r);
            me.g = Tools.Clamp01(me.g);
            me.b = Tools.Clamp01(me.b);
            me.a = Tools.Clamp01(me.a);

            return me;
        }

        public static Color Add(this Color me, float value, bool considerAlpha = true)
        {
            me.r += value;
            me.g += value;
            me.b += value;

            if (considerAlpha)
            {
                me.a += value;
            }

            return me;
        }
        public static Color Sub(this Color me, float value, bool considerAlpha = true)
        {
            me.r -= value;
            me.g -= value;
            me.b -= value;

            if (considerAlpha)
            {
                me.a -= value;
            }

            return me;
        }
        public static Color Mul(this Color me, float value, bool considerAlpha = true)
        {
            me.r *= value;
            me.g *= value;
            me.b *= value;

            if (considerAlpha)
            {
                me.a *= value;
            }

            return me;
        }
        public static Color Div(this Color me, float value, bool considerAlpha = true)
        {
            me.r /= value;
            me.g /= value;
            me.b /= value;

            if (considerAlpha)
            {
                me.a /= value;
            }

            return me;
        }

        public static string ToHex(this Color me, bool considerAlpha = true)
        {
            return ((Color32)me).ToHex(considerAlpha);
        }
        public static string ToHex(this Color32 me, bool considerAlpha = true)
        {
            var r = me.r.ToString("X2").ToLowerInvariant();
            var g = me.g.ToString("X2").ToLowerInvariant();
            var b = me.b.ToString("X2").ToLowerInvariant();
            var a = me.a.ToString("X2").ToLowerInvariant();

            return considerAlpha ? $"#{r}{g}{b}{a}" : $"#{r}{g}{b}";
        }
    }
}
