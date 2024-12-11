using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MaxTools
{
    public class CacheVar<T>
    {
        static Dictionary<object, T> cacheDictionary = new Dictionary<object, T>();

        public object key { get; }

        T m_Value;

        public T value
        {
            get
            {
                return m_Value;
            }

            set
            {
                cacheDictionary[key] = m_Value = value;

                hasValue = true;
            }
        }

        public bool hasValue { get; private set; }

        public CacheVar(object sender = null, [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            key = (sender, fp, ln);

            hasValue = cacheDictionary.TryGetValue(key, out m_Value);
        }

        public bool TryGetValue(out T result)
        {
            if (hasValue)
            {
                result = m_Value;

                return true;
            }
            else
            {
                result = default;

                return false;
            }
        }

        public static implicit operator T(CacheVar<T> cacheVar)
        {
            return cacheVar.m_Value;
        }
    }
}
