using System;

namespace MaxTools
{
    public static partial class Tools
    {
        #region Constants
        public const float PI = 3.141592f;
        public const float PI2 = PI * 2.0f;
        public const float Epsilon3 = 1e-3f;
        public const float Epsilon6 = 1e-6f;
        public const float Deg2Rad = PI / 180.0f;
        public const float Rad2Deg = 180.0f / PI;
        #endregion

        #region Methods
        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }
        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }
        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }
        public static float Asin(float value)
        {
            return (float)Math.Asin(value);
        }
        public static float Acos(float value)
        {
            return (float)Math.Acos(value);
        }
        public static float Atan(float value)
        {
            return (float)Math.Atan(value);
        }
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        public static float Cbrt(float value)
        {
            return Pow(value, 1.0f / 3.0f);
        }
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }

        public static float Tanh(float value)
        {
            return (float)Math.Tanh(value);
        }
        public static float Sigmoid(float value)
        {
            float k = (float)Math.Exp(value);
            return k / (1.0f + k);
        }
        public static float ReLU(float value)
        {
            return value < 0.0f ? 0.0f : value;
        }
        public static float LeakyReLU(float value)
        {
            return value < 0.0f ? value * 0.01f : value;
        }

        public static int Sign(int value)
        {
            return Math.Sign(value);
        }
        public static float Sign(float value)
        {
            return Math.Sign(value);
        }

        public static int Abs(int value)
        {
            return value < 0 ? -value : value;
        }
        public static float Abs(float value)
        {
            return value < 0.0f ? -value : value;
        }

        public static float AngleXY(float x, float y)
        {
            return (float)Math.Atan2(y, x) * Rad2Deg;
        }

        public static float Log(float value, float newBase)
        {
            return (float)Math.Log(value, newBase);
        }
        public static float LogE(float value)
        {
            return (float)Math.Log(value);
        }
        public static float Log10(float value)
        {
            return (float)Math.Log10(value);
        }

        public static float Pow(float value, float power)
        {
            return (float)Math.Pow(value, power);
        }

        public static float Round(float value)
        {
            return (float)Math.Round(value);
        }
        public static float Floor(float value)
        {
            return (float)Math.Floor(value);
        }
        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }

        public static int RoundToInt(float value)
        {
            return (int)Math.Round(value);
        }
        public static int FloorToInt(float value)
        {
            return (int)Floor(value);
        }
        public static int CeilingToInt(float value)
        {
            return (int)Ceiling(value);
        }

        // [0, length)
        public static int Repeat(int value, int length)
        {
            int mod = value % length;

            if (mod < 0)
            {
                return mod + length;
            }
            else
                return mod;
        }
        public static float Repeat(float value, float length)
        {
            return value - Floor(value / length) * length;
        }

        // [0, length]
        public static int PingPong(int value, int length)
        {
            value = Repeat(value, length * 2);

            return length - Abs(value - length);
        }
        public static float PingPong(float value, float length)
        {
            value = Repeat(value, length * 2.0f);

            return length - Abs(value - length);
        }

        public static float DeltaAngle(float a, float b)
        {
            float result = Repeat(b - a, 360.0f);

            if (result > 180.0f)
            {
                result -= 360.0f;
            }

            return result;
        }

        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static int Min(params int[] values)
        {
            int result = values[0];
            for (int i = 1; i < values.Length; ++i)
                if (values[i] < result)
                    result = values[i];
            return result;
        }
        public static float Min(params float[] values)
        {
            float result = values[0];
            for (int i = 1; i < values.Length; ++i)
                if (values[i] < result)
                    result = values[i];
            return result;
        }

        public static int Max(params int[] values)
        {
            int result = values[0];
            for (int i = 1; i < values.Length; ++i)
                if (values[i] > result)
                    result = values[i];
            return result;
        }
        public static float Max(params float[] values)
        {
            float result = values[0];
            for (int i = 1; i < values.Length; ++i)
                if (values[i] > result)
                    result = values[i];
            return result;
        }

        public static int Sum(params int[] values)
        {
            int result = values[0];
            for (int i = 1; i < values.Length; ++i)
                result += values[i];
            return result;
        }
        public static float Sum(params float[] values)
        {
            float result = values[0];
            for (int i = 1; i < values.Length; ++i)
                result += values[i];
            return result;
        }

        public static int Mul(params int[] values)
        {
            int result = values[0];
            for (int i = 1; i < values.Length; ++i)
                result *= values[i];
            return result;
        }
        public static float Mul(params float[] values)
        {
            float result = values[0];
            for (int i = 1; i < values.Length; ++i)
                result *= values[i];
            return result;
        }

        public static float Average(params int[] values)
        {
            return (float)Sum(values) / values.Length;
        }
        public static float Average(params float[] values)
        {
            return Sum(values) / values.Length;
        }

        public static float Clamp01(float value)
        {
            if (value < 0.0f) return 0.0f;
            if (value > 1.0f) return 1.0f;
            return value;
        }

        public static float ClampAngle(float value, float min, float max)
        {
            float delta1 = DeltaAngle(value, min);
            float delta2 = DeltaAngle(value, max);

            if (delta1 < 0.0f && delta2 > 0.0f)
            {
                return value;
            }

            if (Abs(delta1) < Abs(delta2))
                return min;
            else
                return max;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static int ClampMin(int value, int min)
        {
            if (value < min) return min;
            return value;
        }
        public static float ClampMin(float value, float min)
        {
            if (value < min) return min;
            return value;
        }

        public static int ClampMax(int value, int max)
        {
            if (value > max) return max;
            return value;
        }
        public static float ClampMax(float value, float max)
        {
            if (value > max) return max;
            return value;
        }

        public static int ClampInterval(int value, int a, int b)
        {
            if (a < b)
                return Clamp(value, a, b);
            else
                return Clamp(value, b, a);
        }
        public static float ClampInterval(float value, float a, float b)
        {
            if (a < b)
                return Clamp(value, a, b);
            else
                return Clamp(value, b, a);
        }

        public static bool EnterInterval(int value, int a, int b)
        {
            if (a < b)
                return value >= a && value <= b;
            else
                return value <= a && value >= b;
        }
        public static bool EnterInterval(float value, float a, float b)
        {
            if (a < b)
                return value >= a && value <= b;
            else
                return value <= a && value >= b;
        }
        #endregion
    }
}
