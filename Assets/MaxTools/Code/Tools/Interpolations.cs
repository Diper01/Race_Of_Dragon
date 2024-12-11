using UnityEngine;
using System;

namespace MaxTools
{
    public enum LerpType
    {
        Simple,
        Towards
    }

    public static partial class Tools
    {
        #region Angle
        public static float LerpAngle(float a, float b, float k)
        {
            float result = Repeat(b - a, 360.0f);

            if (result > 180.0f)
            {
                result -= 360.0f;
            }

            return a + result * Clamp01(k);
        }
        public static float TowardsAngle(float a, float b, float d)
        {
            float value = DeltaAngle(a, b);

            return d < Abs(value) ? Towards(a, a + value, d) : b;
        }
        public static float LerpChooseAngle(float a, float b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return LerpAngle(a, b, q);

                case LerpType.Towards:
                    return TowardsAngle(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion

        #region Float
        public static float Lerp(float a, float b, float k)
        {
            return a + (b - a) * Clamp01(k);
        }
        public static float Towards(float a, float b, float d)
        {
            float c = b - a;

            return d < Abs(c) ? a + Sign(c) * d : b;
        }
        public static float LerpChoose(float a, float b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return Lerp(a, b, q);

                case LerpType.Towards:
                    return Towards(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion

        #region Color
        public static Color Lerp(Color a, Color b, float k)
        {
            k = Clamp01(k);
            a.r += (b.r - a.r) * k;
            a.g += (b.g - a.g) * k;
            a.b += (b.b - a.b) * k;
            a.a += (b.a - a.a) * k;
            return a;
        }
        public static Color Towards(Color a, Color b, float d)
        {
            if (d > 0.0f)
            {
                float cR = b.r - a.r;
                float cG = b.g - a.g;
                float cB = b.b - a.b;
                float cA = b.a - a.a;

                float sqrMagnitude = cR * cR + cG * cG + cB * cB + cA * cA;

                if (d * d < sqrMagnitude)
                {
                    float magnitude = Sqrt(sqrMagnitude);

                    a.r += cR / magnitude * d;
                    a.g += cG / magnitude * d;
                    a.b += cB / magnitude * d;
                    a.a += cA / magnitude * d;
                }
                else
                    return b;
            }

            return a;
        }
        public static Color LerpChoose(Color a, Color b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return Lerp(a, b, q);

                case LerpType.Towards:
                    return Towards(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion

        #region Vector2
        public static Vector2 Lerp(Vector2 a, Vector2 b, float k)
        {
            k = Clamp01(k);
            a.x += (b.x - a.x) * k;
            a.y += (b.y - a.y) * k;
            return a;
        }
        public static Vector2 Towards(Vector2 a, Vector2 b, float d)
        {
            if (d > 0.0f)
            {
                float cX = b.x - a.x;
                float cY = b.y - a.y;

                float sqrMagnitude = cX * cX + cY * cY;

                if (d * d < sqrMagnitude)
                {
                    float magnitude = Sqrt(sqrMagnitude);

                    a.x += cX / magnitude * d;
                    a.y += cY / magnitude * d;
                }
                else
                    return b;
            }

            return a;
        }
        public static Vector2 LerpChoose(Vector2 a, Vector2 b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return Lerp(a, b, q);

                case LerpType.Towards:
                    return Towards(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion

        #region Vector3
        public static Vector3 Lerp(Vector3 a, Vector3 b, float k)
        {
            k = Clamp01(k);
            a.x += (b.x - a.x) * k;
            a.y += (b.y - a.y) * k;
            a.z += (b.z - a.z) * k;
            return a;
        }
        public static Vector3 Towards(Vector3 a, Vector3 b, float d)
        {
            if (d > 0.0f)
            {
                float cX = b.x - a.x;
                float cY = b.y - a.y;
                float cZ = b.z - a.z;

                float sqrMagnitude = cX * cX + cY * cY + cZ * cZ;

                if (d * d < sqrMagnitude)
                {
                    float magnitude = Sqrt(sqrMagnitude);

                    a.x += cX / magnitude * d;
                    a.y += cY / magnitude * d;
                    a.z += cZ / magnitude * d;
                }
                else
                    return b;
            }

            return a;
        }
        public static Vector3 LerpChoose(Vector3 a, Vector3 b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return Lerp(a, b, q);

                case LerpType.Towards:
                    return Towards(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion

        #region Quaternion
        public static Quaternion Lerp(Quaternion a, Quaternion b, float k)
        {
            return Quaternion.Lerp(a, b, k);
        }
        public static Quaternion Towards(Quaternion a, Quaternion b, float d)
        {
            return Quaternion.RotateTowards(a, b, d);
        }
        public static Quaternion LerpChoose(Quaternion a, Quaternion b, float q, LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return Lerp(a, b, q);

                case LerpType.Towards:
                    return Towards(a, b, q);
            }

            throw new Exception($"Invalid {nameof(lerpType)}!");
        }
        #endregion
    }
}
