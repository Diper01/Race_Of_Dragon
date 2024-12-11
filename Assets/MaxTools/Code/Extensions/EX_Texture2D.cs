using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_Texture2D
    {
        public static void Fill(this Texture2D me, Color color)
        {
            for (int x = 0; x < me.width; ++x)
            {
                for (int y = 0; y < me.height; ++y)
                {
                    me.SetPixel(x, y, color);
                }
            }
        }
        public static void Fill(this Texture2D me, Color color, RectInt rect)
        {
            for (int x = rect.x; x < rect.x + rect.width; ++x)
            {
                for (int y = rect.y; y < rect.y + rect.height; ++y)
                {
                    me.SetPixel(x, y, color);
                }
            }
        }

        public static void SetGradient(this Texture2D me, AxisType axis, Gradient gradient)
        {
            switch (axis)
            {
                case AxisType.Horizontal:
                    for (int x = 0; x < me.width; ++x)
                    {
                        for (int y = 0; y < me.height; ++y)
                        {
                            me.SetPixel(x, y, gradient.Evaluate(x / (me.width - 1.0f)));
                        }
                    }
                    break;

                case AxisType.Vertical:
                    for (int x = 0; x < me.width; ++x)
                    {
                        for (int y = 0; y < me.height; ++y)
                        {
                            me.SetPixel(x, y, gradient.Evaluate(y / (me.height - 1.0f)));
                        }
                    }
                    break;

                default:
                    throw new Exception("Invalid axis!");
            }
        }
        public static void SetGradient(this Texture2D me, AxisType axis, params Color[] colors)
        {
            switch (colors.Length)
            {
                case 0:
                    return;

                case 1:
                    me.Fill(colors[0]);
                    return;

                default:
                    var gradient = new Gradient();
                    gradient.SetColors(colors);
                    me.SetGradient(axis, gradient);
                    break;
            }
        }

        public static void SetBorder(this Texture2D me, Color color, int thickness, int padding)
        {
            for (int i = 0; i < thickness; ++i)
            {
                for (int x = padding; x < me.width - padding; ++x)
                {
                    me.SetPixel(x, i + padding, color);
                    me.SetPixel(x, me.height - 1 - i - padding, color);
                }

                for (int y = padding + thickness; y < me.height - padding - thickness; ++y)
                {
                    me.SetPixel(i + padding, y, color);
                    me.SetPixel(me.width - 1 - i - padding, y, color);
                }
            }
        }
        public static void SetBorder(this Texture2D me, Color color, int thickness)
        {
            me.SetBorder(color, thickness, 0);
        }

        public static void SetScale(this Texture2D me, int newWidth, int newHeight)
        {
            var temporary = RenderTexture.GetTemporary(newWidth, newHeight, 0, RenderTextureFormat.ARGB32);
            Graphics.Blit(me, temporary);
            var oldActive = RenderTexture.active;
            RenderTexture.active = temporary;
            me.Resize(newWidth, newHeight);
            me.ReadPixels();
            RenderTexture.active = oldActive;
            RenderTexture.ReleaseTemporary(temporary);
        }
        public static void SetScale(this Texture2D me, Vector2Int newSize)
        {
            me.SetScale(newSize.x, newSize.y);
        }

        public static void Expand(this Texture2D me, int addWidth, int addHeight)
        {
            Color[] colors = me.GetPixels();

            int oldWidth = me.width;
            int oldHeight = me.height;

            me.Resize(oldWidth + addWidth, oldHeight + addHeight);
            me.Fill(Color.clear);
            me.SetPixels(addWidth / 2, addHeight / 2, oldWidth, oldHeight, colors);
        }
        public static void Expand(this Texture2D me, Vector2Int addSize)
        {
            me.Expand(addSize.x, addSize.y);
        }

        static Sprite CreateSprite(this Texture2D me, Rect rect, Vector2 pivot, float pixelsPerUnit)
        {
            Sprite sprite = Sprite.Create(me, rect, pivot, pixelsPerUnit);

            sprite.name = me.name;

            return sprite;
        }

        public static Sprite MakeSprite(this Texture2D me, float pixelsPerUnit = 100.0f)
        {
            return CreateSprite(me, me.GetRect(), Vector2.one * 0.5f, pixelsPerUnit);
        }
        public static Sprite MakeSprite(this Texture2D me, Rect rect, float pixelsPerUnit = 100.0f)
        {
            return CreateSprite(me, rect, Vector2.one * 0.5f, pixelsPerUnit);
        }
        public static Sprite MakeSprite(this Texture2D me, Vector2 pivot, float pixelsPerUnit = 100.0f)
        {
            return CreateSprite(me, me.GetRect(), pivot, pixelsPerUnit);
        }
        public static Sprite MakeSprite(this Texture2D me, Rect rect, Vector2 pivot, float pixelsPerUnit = 100.0f)
        {
            return CreateSprite(me, rect, pivot, pixelsPerUnit);
        }

        public static bool IsValidPixelPosition(this Texture2D me, int x, int y)
        {
            return x >= 0 && x < me.width
                && y >= 0 && y < me.height;
        }
        public static bool IsValidPixelPosition(this Texture2D me, Vector2Int position)
        {
            return position.x >= 0 && position.x < me.width
                && position.y >= 0 && position.y < me.height;
        }

        public static void ReadPixels(this Texture2D me)
        {
            me.ReadPixels(me.GetRect(), 0, 0);
        }
        public static void ReadPixels(this Texture2D me, Rect rect)
        {
            me.ReadPixels(rect, 0, 0);
        }

        public static Rect GetRect(this Texture2D me)
        {
            return new Rect(0, 0, me.width, me.height);
        }
        public static RectInt GetRectInt(this Texture2D me)
        {
            return new RectInt(0, 0, me.width, me.height);
        }
    }
}
