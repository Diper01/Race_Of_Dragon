using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static partial class Tools
    {
        #region Conversion
        public static string StringEncrypt(string value)
        {
            var oldState = Randomize.state;
            Randomize.SetSeed(2048);
            var temp = Encoding.UTF8.GetBytes(value);
            for (int i = 0; i < temp.Length; ++i)
                temp[i] ^= (byte)Randomize.Range(0, byte.MaxValue + 1);
            Randomize.state = oldState;
            return Convert.ToBase64String(temp);
        }
        public static string StringDecrypt(string value)
        {
            var oldState = Randomize.state;
            Randomize.SetSeed(2048);
            var temp = Convert.FromBase64String(value);
            for (int i = 0; i < temp.Length; ++i)
                temp[i] ^= (byte)Randomize.Range(0, byte.MaxValue + 1);
            Randomize.state = oldState;
            return Encoding.UTF8.GetString(temp);
        }

        public static T ChangeType<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
        public static T ChangeType<T>(object value, IFormatProvider provider)
        {
            return (T)Convert.ChangeType(value, typeof(T), provider);
        }

        public static object ChangeType(object value, Type conversionType)
        {
            return Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
        }
        public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(value, conversionType, provider);
        }

        public static string Serialize(object value)
        {
            return Serialize(value, false);
        }
        public static string Serialize(object value, bool prettyPrint)
        {
            if (value.GetType().IsArray)
            {
                var wrapper = new ArrayWrapper(value as Array);

                return Jsoner.ToJson(wrapper, prettyPrint);
            }

            if (value.GetType().IsGenericType(typeof(List<>)))
            {
                var wrapper = new ArrayWrapper(value.InvokeMethod("ToArray") as Array);

                return Jsoner.ToJson(wrapper, prettyPrint);
            }

            if (value.GetType().IsCommonType())
            {
                return ChangeType<string>(value);
            }
            else
                return Jsoner.ToJson(value, prettyPrint);
        }

        public static T Deserialize<T>(string value)
        {
            return (T)Deserialize(value, typeof(T));
        }
        public static object Deserialize(string value, Type type)
        {
            if (type.IsArray)
            {
                var wrapper = Jsoner.FromJson<ArrayWrapper>(value);

                return wrapper.array;
            }

            if (type.IsGenericType(typeof(List<>)))
            {
                var wrapper = Jsoner.FromJson<ArrayWrapper>(value);

                return Activator.CreateInstance(type, wrapper.array);
            }

            if (type.IsCommonType())
            {
                return ChangeType(value, type);
            }
            else
                return Jsoner.FromJson(value, type);
        }
        #endregion

        #region Bind Fields
        public static void BindFields(Type source, Type receiver)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var f1 in receiver.GetFields(bindingFlags | BindingFlags.Static))
            {
                foreach (var f2 in source.GetFields(bindingFlags | BindingFlags.Static))
                {
                    if (f1.Name == f2.Name && f1.FieldType == f2.FieldType)
                    {
                        f1.SetValue(null, f2.GetValue(null));
                    }
                }
            }
        }
        public static void BindFields(Type source, object receiver)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var f1 in receiver.GetType().GetFields(bindingFlags | BindingFlags.Instance))
            {
                foreach (var f2 in source.GetFields(bindingFlags | BindingFlags.Static))
                {
                    if (f1.Name == f2.Name && f1.FieldType == f2.FieldType)
                    {
                        f1.SetValue(receiver, f2.GetValue(null));
                    }
                }
            }
        }
        public static void BindFields(object source, Type receiver)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var f1 in receiver.GetFields(bindingFlags | BindingFlags.Static))
            {
                foreach (var f2 in source.GetType().GetFields(bindingFlags | BindingFlags.Instance))
                {
                    if (f1.Name == f2.Name && f1.FieldType == f2.FieldType)
                    {
                        f1.SetValue(null, f2.GetValue(source));
                    }
                }
            }
        }
        public static void BindFields(object source, object receiver)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var f1 in receiver.GetType().GetFields(bindingFlags | BindingFlags.Instance))
            {
                foreach (var f2 in source.GetType().GetFields(bindingFlags | BindingFlags.Instance))
                {
                    if (f1.Name == f2.Name && f1.FieldType == f2.FieldType)
                    {
                        f1.SetValue(receiver, f2.GetValue(source));
                    }
                }
            }
        }
        #endregion

        #region Enum
        public static T GetEnumByName<T>(string name) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        public static IEnumerable<T> GetEnumTypes<T>() where T : Enum
        {
            foreach (var value in Enum.GetNames(typeof(T)))
            {
                yield return GetEnumByName<T>(value);
            }
        }
        #endregion

        #region Get Types
        public static Type GetTypeByName(string name)
        {
            var cacheVar = new CacheVar<Type>(name);

            if (!cacheVar.TryGetValue(out var result))
            {
                foreach (var type in GetAllTypes())
                {
                    if (type.Name == name)
                    {
                        result = type;

                        cacheVar.value = result;

                        break;
                    }
                }
            }

            return result;
        }
        public static Type GetTypeByFullName(string fullName)
        {
            var cacheVar = new CacheVar<Type>(fullName);

            if (!cacheVar.TryGetValue(out var result))
            {
                foreach (var type in GetAllTypes())
                {
                    if (type.FullName == fullName)
                    {
                        result = type;

                        cacheVar.value = result;

                        break;
                    }
                }
            }

            return result;
        }

        public static Type[] GetAllTypes()
        {
            var cacheVar = new CacheVar<Type[]>();

            if (!cacheVar.TryGetValue(out var types))
            {
                var list = new List<Type>();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        list.Add(type);
                    }
                }

                types = list.ToArray();

                cacheVar.value = types;
            }

            return types;
        }
        public static Type[] GetAllTypes(params string[] assemblyNames)
        {
            var cacheVar = new CacheVar<Type[]>(string.Join("\n", assemblyNames));

            if (!cacheVar.TryGetValue(out var types))
            {
                var list = new List<Type>();

                foreach (var assemblyName in assemblyNames)
                {
                    foreach (var type in Assembly.Load(assemblyName).GetTypes())
                    {
                        list.Add(type);
                    }
                }

                types = list.ToArray();

                cacheVar.value = types;
            }

            return types;
        }
        #endregion

        #region Find Objects
        public static IEnumerable<Component> FindObjectsWithType(Type type)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    foreach (var child in root.GetComponentsInChildren(type, true))
                    {
                        yield return child;
                    }
                }
            }

            if (Application.isPlaying)
            {
                foreach (var root in GetDontDestroyOnLoadScene().GetRootGameObjects())
                {
                    foreach (var child in root.GetComponentsInChildren(type, true))
                    {
                        yield return child;
                    }
                }
            }
        }
        public static IEnumerable<T> FindObjectsWithType<T>() where T : Component
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                foreach (var root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    foreach (var child in root.GetComponentsInChildren<T>(true))
                    {
                        yield return child;
                    }
                }
            }

            if (Application.isPlaying)
            {
                foreach (var root in GetDontDestroyOnLoadScene().GetRootGameObjects())
                {
                    foreach (var child in root.GetComponentsInChildren<T>(true))
                    {
                        yield return child;
                    }
                }
            }
        }

        public static Component FindObjectWithType(Type type)
        {
            foreach (var _object in FindObjectsWithType(type))
            {
                return _object;
            }

            return null;
        }
        public static T FindObjectWithType<T>() where T : Component
        {
            foreach (var _object in FindObjectsWithType<T>())
            {
                return _object;
            }

            return null;
        }

        public static Transform FindObjectWithTag(string tag)
        {
            foreach (var _object in FindObjectsWithType<Transform>())
            {
                if (_object.CompareTag(tag))
                {
                    return _object;
                }
            }

            return null;
        }
        public static Transform FindObjectWithName(string name)
        {
            foreach (var _object in FindObjectsWithType<Transform>())
            {
                if (_object.name == name)
                {
                    return _object;
                }
            }

            return null;
        }

        public static Transform[] FindObjectsWithTag(string tag)
        {
            var list = new List<Transform>();

            foreach (var _object in FindObjectsWithType<Transform>())
            {
                if (_object.CompareTag(tag))
                {
                    list.Add(_object);
                }
            }

            return list.ToArray();
        }
        public static Transform[] FindObjectsWithName(string name)
        {
            var list = new List<Transform>();

            foreach (var _object in FindObjectsWithType<Transform>())
            {
                if (_object.name == name)
                {
                    list.Add(_object);
                }
            }

            return list.ToArray();
        }
        #endregion

        #region Variable
        public static string GetVariableNameWithoutArrayIndices(string variableName)
        {
            int i = variableName.IndexOf('[');

            if (i > 0)
            {
                return variableName.Substring(0, i);
            }
            else
                return variableName;
        }
        public static int[] GetArrayIndicesWithoutVariableName(string variableName)
        {
            var i = variableName.IndexOf('[');
            var j = variableName.IndexOf(']');

            if (i > 0 && j > 0)
            {
                var arrayIndices = variableName.Substring(i + 1, j - i - 1).Split(',');

                return arrayIndices.Select((index) => int.Parse(index)).ToArray();
            }
            else
                return null;
        }
        #endregion

        #region Other
        public static Scene GetDontDestroyOnLoadScene()
        {
            if (Application.isPlaying)
            {
                GameObject gameObject = new GameObject();
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                Scene dontDestroyOnLoadScene = gameObject.scene;
                UnityEngine.Object.Destroy(gameObject);
                return dontDestroyOnLoadScene;
            }

            throw new Exception("Only runtime mode!");
        }

        public static void InjectedLog([CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            Debug.Log($"{nameof(InjectedLog)}:{fp}:{ln}");
        }

        public static object MakeUniqueKey(object key = null, [CallerFilePath] string fp = "", [CallerLineNumber] int ln = 0)
        {
            return (key, fp, ln);
        }
        #endregion
    }
}
