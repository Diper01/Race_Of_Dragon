using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    [AttributeUsage(AttributeTargets.Field)]
    public class StaticVariableAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class StaticMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class StaticSaveAttribute : Attribute
    {
        static List<(FieldInfo field, StaticSaveAttribute staticSave)> variables =
           new List<(FieldInfo field, StaticSaveAttribute staticSave)>();

        string saveKey = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            foreach (var type in Tools.GetAllTypes())
            {
                foreach (var field in type.GetFields(bindingFlags))
                {
                    var staticSave = field.GetCustomAttribute<StaticSaveAttribute>();

                    if (staticSave != null)
                    {
                        staticSave.saveKey = $"{type.AssemblyQualifiedName}.{field.Name}";

                        variables.Add((field, staticSave));
                    }
                }
            }

            Load();

            Application.quitting += Save;
        }

        public static void Save()
        {
            foreach (var variable in variables)
            {
                GamePrefs.SetObject(variable.staticSave.saveKey, variable.field.GetValue(null));
            }

            GamePrefs.Save();
        }
        public static void Load()
        {
            foreach (var variable in variables)
            {
                if (!GamePrefs.HasKey(variable.staticSave.saveKey))
                {
                    continue;
                }

                var value = GamePrefs.GetObject(variable.staticSave.saveKey, variable.field.FieldType);

                variable.field.SetValue(null, value);
            }
        }
    }
}
