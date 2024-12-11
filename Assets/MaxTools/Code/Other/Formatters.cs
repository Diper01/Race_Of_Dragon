namespace MaxTools
{
    public static class Formatters
    {
        [StaticMethod]
        public static string IntegerFormatter(int value)
        {
            return value.ToString("N0");
        }

        [StaticMethod]
        public static string FractionalFormatter(float value, int precision = 2)
        {
            return value.ToString($"N{precision}");
        }

        [StaticMethod]
        public static string TimeFormatter(float seconds)
        {
            float s = seconds;

            float m = (int)(s / 60.0f);
            s -= m * 60.0f;

            float h = (int)(m / 60.0f);
            m -= h * 60.0f;

            if (h > 0.0f)
            {
                return $"{h:F0}h {m:F0}m {s:F0}s";
            }
            else if (m > 0.0f)
            {
                return $"{m:F0}m {s:F0}s";
            }
            else
                return $"{s:F0}s";
        }
    }
}
