using System.Collections.Generic;

namespace MaxTools
{
    public static class Cache<T>
    {
        static Dictionary<object, T> cache = new Dictionary<object, T>();

        public static bool TryGetValue(object key, out T value)
        {
            return cache.TryGetValue(key, out value);
        }
        public static bool HasKey(object key)
        {
            return cache.ContainsKey(key);
        }

        public static T GetValue(object key)
        {
            return cache[key];
        }
        public static T GetValueOrDefault(object key)
        {
            TryGetValue(key, out var result);

            return result;
        }

        public static void Add(object key, T value)
        {
            cache.Add(key, value);
        }
        public static void SetValue(object key, T value)
        {
            cache[key] = value;
        }
    }
}
