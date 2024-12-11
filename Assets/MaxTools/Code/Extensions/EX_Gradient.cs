using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace MaxTools.Extensions
{
    public static class EX_Gradient
    {
        public static Color[] GetColors(this Gradient me)
        {
            List<Color> colors = new List<Color>();

            for (int i = 0; i < me.colorKeys.Length; ++i)
            {
                colors.Add(me.colorKeys[i].color);
            }

            for (int i = 0; i < me.alphaKeys.Length; ++i)
            {
                colors[i] = colors[i].GetWithAlpha01(me.alphaKeys[i].alpha);
            }

            return colors.ToArray();
        }
        public static void SetColors(this Gradient me, params Color[] colors)
        {
            if (colors.Length == 0)
            {
                return;
            }

            if (colors.Length == 1)
            {
                me.SetColors(colors[0], colors[0]);

                return;
            }

            var colorKeys = new GradientColorKey[colors.Length];
            var alphaKeys = new GradientAlphaKey[colors.Length];

            for (int i = 0; i < colors.Length; ++i)
            {
                colorKeys[i].color = colors[i];
                colorKeys[i].time = 1.0f / (colors.Length - 1) * i;

                alphaKeys[i].alpha = colors[i].a;
                alphaKeys[i].time = 1.0f / (colors.Length - 1) * i;
            }

            me.SetKeys(colorKeys, alphaKeys);
        }
        public static void ReverseColors(this Gradient me)
        {
            var colorKeys = me.colorKeys;
            var alphaKeys = me.alphaKeys;

            for (int i = 0; i < colorKeys.Length; ++i)
                colorKeys[i].time = Tools.Abs(colorKeys[i].time - 1.0f);

            for (int i = 0; i < alphaKeys.Length; ++i)
                alphaKeys[i].time = Tools.Abs(colorKeys[i].time - 1.0f);

            me.SetKeys(colorKeys, alphaKeys);
        }
        public static bool IsClear(this Gradient me)
        {
            return me.GetColors().All((color) => color.IsClear());
        }
    }
}
