using UnityEngine;
using System;
using System.Reflection;

namespace MaxTools
{
    using MaxTools.Extensions;

    [AttributeUsage(AttributeTargets.Method)]
    public class ExternalMethodAttribute : Attribute
    {
        public static object Invoke(string methodFullName, params object[] args)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            var cacheVar = new CacheVar<MethodInfo>(methodFullName);

            if (!cacheVar.TryGetValue(out var result))
            {
                foreach (var type in Tools.GetAllTypes())
                {
                    foreach (var method in type.GetMethods(bindingFlags))
                    {
                        if (method.GetCustomAttribute<ExternalMethodAttribute>() != null)
                        {
                            if ($"{method.DeclaringType.FullName}.{method.Name}" == methodFullName)
                            {
                                cacheVar.value = result = method;

                                return result.Invoke(null, args);
                            }
                        }
                    }
                }
            }

            return result.Invoke(null, args);
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class GameResourceAttribute : Attribute
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            foreach (var type in Tools.GetAllTypes())
            {
                foreach (var field in type.GetFields(bindingFlags))
                {
                    string resourcePath = field.GetCustomAttribute<GameResourceAttribute>()?.resourcePath;

                    if (resourcePath != null)
                    {
                        if (field.FieldType.IsArray)
                        {
                            Type elementType = field.FieldType.GetElementType();

                            UnityEngine.Object[] resources = Resources.LoadAll(resourcePath, elementType);

                            if (resources.Length > 0)
                            {
                                Array.Sort(resources, (a, b) => a.name.CompareNumber(b.name));

                                field.SetValue(null, resources.ChangeType(elementType));
                            }
                            else
                                Debug.LogWarning($"Resources not found!\nPath:{resourcePath}");
                        }
                        else
                        {
                            UnityEngine.Object resource = Resources.Load(resourcePath, field.FieldType);

                            if (resource != null)
                            {
                                field.SetValue(null, resource);
                            }
                            else
                                Debug.LogWarning($"Resources not found!\nPath:{resourcePath}");
                        }
                    }
                }
            }
        }

        readonly string resourcePath = "";

        public GameResourceAttribute(string resourcePath)
        {
            if (resourcePath.IsNotNullOrWhiteSpace())
            {
                this.resourcePath = resourcePath;
            }
            else
                throw new Exception($"Invalid resource path!");
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class JsonIgnoreAttribute : Attribute { }
}
