using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace MaxTools
{
    using MaxTools.Extensions;

    [Serializable]
    public class StaticMethod
    {
        class Variety
        {
            public MethodInfo methodInfo;
            public ParameterInfo[] allParameters;
            public object[] extraParameters;
            public object[] defaultValues;

            public bool TryInvoke(object[] args, ref object result)
            {
                if (args.Length + extraParameters.Length > allParameters.Length
                 || args.Length + extraParameters.Length < allParameters.Length - defaultValues.Length)
                {
                    return false;
                }
                else
                {
                    int i = 0;

                    var newArgs = args.Concat(extraParameters).ToArray();

                    for (; i < newArgs.Length; ++i)
                    {
                        var argumentType = newArgs[i].GetType();
                        var parameterType = allParameters[i].ParameterType;

                        if (!argumentType.IsSubclassOrSame(parameterType))
                        {
                            // Пробуем преобразовать...

                            var conversionCacheKey = Tools.MakeUniqueKey(argumentType);

                            if (!Cache<MethodInfo>.TryGetValue(conversionCacheKey, out var conversionOperator))
                            {
                                conversionOperator = argumentType.GetConversionOperator(argumentType, parameterType);

                                if (conversionOperator == null)
                                {
                                    conversionOperator = parameterType.GetConversionOperator(argumentType, parameterType);
                                }

                                Cache<MethodInfo>.Add(conversionCacheKey, conversionOperator);
                            }

                            if (conversionOperator != null)
                            {
                                newArgs[i] = conversionOperator.Invoke(null, new object[] { newArgs[i] });
                            }
                            else
                                return false;
                        }
                    }

                    int skip = allParameters.Length - i;

                    if (skip > 0)
                    {
                        newArgs = newArgs.Concat(defaultValues.Skip(defaultValues.Length - skip)).ToArray();
                    }

                    result = methodInfo.Invoke(null, newArgs);

                    return true;
                }
            }
        }

        [SerializeField] EditorVariable[] extraParams;
        [SerializeField] string methodPath;

        Variety[] varieties = null;
        bool? successfully = null;

        public object Invoke(params object[] args)
        {
            if (this)
            {
                object result = null;

                foreach (var variety in varieties)
                {
                    if (variety.TryInvoke(args, ref result))
                    {
                        return result;
                    }
                }

                throw new Exception("[StaticMethod.Invoke] Invalid arguments!");
            }
            else
                throw new Exception("[StaticMethod.Invoke] Initialization error!");
        }

        public static implicit operator bool(StaticMethod sm)
        {
            if (sm != null)
            {
                if (sm.successfully == null)
                {
                    if (!sm.methodPath.IsNullOrEmpty() && sm.methodPath != "None")
                    {
                        var split = sm.methodPath.Split('/');

                        var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                        sm.varieties = Tools.GetTypeByFullName(split[0]).GetMember(split[1], bindingFlags).Select((memberInfo) =>
                        {
                            var newVariety = new Variety();
                            newVariety.methodInfo = (MethodInfo)memberInfo;
                            newVariety.allParameters = newVariety.methodInfo.GetParameters();
                            newVariety.defaultValues = newVariety.allParameters.Where((parameter) =>
                            parameter.HasDefaultValue).Select((parameter) =>
                            parameter.DefaultValue).ToArray();
                            newVariety.extraParameters = sm.extraParams.Select((editorVariable) =>
                            editorVariable.value).ToArray();
                            return newVariety;
                        }).ToArray();

                        sm.successfully = true;
                    }
                    else
                        sm.successfully = false;
                }
            }
            else
                return false;

            return sm.successfully.Value;
        }
    }
}
