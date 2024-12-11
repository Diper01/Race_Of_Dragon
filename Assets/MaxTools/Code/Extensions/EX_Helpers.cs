using UnityEngine;
using System.Runtime.CompilerServices;

namespace MaxTools.Extensions
{
    public static class EX_Helpers
    {
        public class Dummy { Dummy() { } }

        public static bool CheckTriggered(this object sender, bool condition,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<bool>(sender, fp, ln);

            var oldValue = cacheVar.value;

            cacheVar.value = condition;

            return condition && !oldValue;
        }

        public static bool CheckInterval(this object sender, float interval, float delay, TimeMode timeMode = TimeMode.ScaledTime,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var now = 0.0f;

            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    now = Time.time;
                    break;

                case TimeMode.UnscaledTime:
                    now = Time.realtimeSinceStartup;
                    break;
            }

            var cacheVar = new CacheVar<float>(sender, fp, ln);

            if (!cacheVar.TryGetValue(out var result))
            {
                result = now + delay;

                cacheVar.value = result;
            }

            if (now >= result)
            {
                cacheVar.value = now + interval;

                return true;
            }

            return false;
        }

        public static bool CheckInterval(this object sender, float interval, TimeMode timeMode = TimeMode.ScaledTime,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            return sender.CheckInterval(interval, 0.0f, timeMode, fp, ln);
        }

        public static bool CheckIntervalFrame(this object sender, int interval, int delay,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var now = Time.frameCount;

            var cacheVar = new CacheVar<int>(sender, fp, ln);

            if (!cacheVar.TryGetValue(out var result))
            {
                result = now + delay;

                cacheVar.value = result;
            }

            if (now >= result)
            {
                cacheVar.value = now + interval;

                return true;
            }

            return false;
        }

        public static bool CheckIntervalFrame(this object sender, int interval,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            return sender.CheckIntervalFrame(interval, 0, fp, ln);
        }

        public static bool CheckIntervalNonstop(this object sender, float interval, bool autoReset = true, TimeMode timeMode = TimeMode.ScaledTime,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<(int frameCount, float totalTime)>(sender, fp, ln);

            if (cacheVar.TryGetValue(out var result))
            {
                if (Time.frameCount - result.frameCount == 1)
                {
                    switch (timeMode)
                    {
                        case TimeMode.ScaledTime:
                            result.totalTime += Time.deltaTime;
                            break;

                        case TimeMode.UnscaledTime:
                            result.totalTime += Time.unscaledDeltaTime;
                            break;
                    }
                }
                else
                    result.totalTime = 0.0f;
            }

            result.frameCount = Time.frameCount;

            cacheVar.value = result;

            if (result.totalTime >= interval)
            {
                if (autoReset)
                {
                    result.totalTime = 0.0f;

                    cacheVar.value = result;
                }

                return true;
            }

            return false;
        }

        public static bool CheckInputString(this object sender, string code,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<int>(sender, fp, ln);

            cacheVar.TryGetValue(out var iterator);

            if (Input.inputString.Length > 0)
            {
                var symbol = Input.inputString[0];

                if (symbol == code[iterator])
                {
                    if (++iterator == code.Length)
                    {
                        cacheVar.value = 0;

                        return true;
                    }

                    cacheVar.value = iterator;
                }
                else
                    cacheVar.value = symbol == code[0] ? 1 : 0;
            }

            return false;
        }

        public static bool CheckChange(this object sender, object value, bool firstReturn = false,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<object>(sender, fp, ln);

            if (cacheVar.TryGetValue(out var oldValue))
            {
                cacheVar.value = value;

                return !value.Equals(oldValue);
            }

            cacheVar.value = value;

            return firstReturn;
        }

        public static T GetPreviousValue<T>(this object sender, T value, T firstReturn, Dummy _dummy_ = null,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<T>(sender, fp, ln);

            if (cacheVar.TryGetValue(out var oldValue))
            {
                cacheVar.value = value;

                return oldValue;
            }

            cacheVar.value = value;

            return firstReturn;
        }

        public static T GetPreviousValue<T>(this object sender, T value, Dummy _dummy_ = null,
            [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            var cacheVar = new CacheVar<T>(sender, fp, ln);

            if (cacheVar.TryGetValue(out var oldValue))
            {
                cacheVar.value = value;

                return oldValue;
            }

            cacheVar.value = value;

            return value;
        }
    }
}
