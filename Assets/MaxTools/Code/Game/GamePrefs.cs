using UnityEngine;
using System;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static class GamePrefs
    {
        public static Array GetArray(string key, Array defaultValue = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return GetObject<ArrayWrapper>(key).array;
            }
            else
                return defaultValue;
        }
        public static void SetArray(string key, Array value)
        {
            SetObject(key, new ArrayWrapper(value));
        }

        public static List<T> GetList<T>(string key, List<T> defaultValue = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return GetArray(key).MakeList<T>();
            }
            else
                return defaultValue;
        }
        public static void SetList<T>(string key, List<T> value)
        {
            SetArray(key, value.ToArray());
        }

        public static object GetList(string key, object defaultValue = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return GetArray(key).MakeList();
            }
            else
                return defaultValue;
        }
        public static void SetList(string key, object value)
        {
            SetArray(key, (Array)value.InvokeMethod("ToArray"));
        }

        public static T GetObject<T>(string key, object defaultValue = null)
        {
            return (T)GetObject(key, typeof(T), defaultValue);
        }
        public static object GetObject(string key, Type valueType, object defaultValue = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                if (valueType.IsArray)
                {
                    return GetArray(key);
                }

                if (valueType.IsGenericType(typeof(List<>)))
                {
                    return GetList(key);
                }

                var strValue = PlayerPrefs.GetString(key);

                if (valueType.IsCommonType())
                {
                    return Tools.ChangeType(strValue, valueType);
                }
                else
                    return Jsoner.FromJson(strValue, valueType);
            }
            else
                return defaultValue;
        }
        public static void SetObject(string key, object value)
        {
            if (value.GetType().IsArray)
            {
                SetArray(key, (Array)value);

                return;
            }

            if (value.GetType().IsGenericType(typeof(List<>)))
            {
                SetList(key, value);

                return;
            }

            if (value.GetType().IsCommonType())
            {
                PlayerPrefs.SetString(key, Tools.ChangeType<string>(value));
            }
            else
                PlayerPrefs.SetString(key, Jsoner.ToJson(value));
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
