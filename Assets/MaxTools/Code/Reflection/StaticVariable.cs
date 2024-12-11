using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    [Serializable]
    public class StaticVariable
    {
        public struct BaseInfo
        {
            public string[] variableNames;
            public Type baseType;
            public MemberInfo baseMemberInfo;
        }

        [SerializeField] int _;
        [SerializeField] string subPath;
        [SerializeField] string staticPath;
        [SerializeField] string fullPath;

        public object value
        {
            get
            {
                if (this)
                    return variable.value;
                else
                    throw new Exception("[StaticVariable.value] Initialization failed!");
            }

            set
            {
                if (this)
                    variable.value = value;
                else
                    throw new Exception("[StaticVariable.value] Initialization failed!");
            }
        }
        bool? successfully = null;
        StaticVariable<object> variable;

        public static object[] GetValues(params StaticVariable[] variables)
        {
            return variables.Select((variable) => variable.value).ToArray();
        }

        public static BaseInfo GetBaseInfo(string variableFullPath)
        {
            var splittedPath = variableFullPath.Split('.', '/', '\\');

            var baseName = splittedPath[0];
            var baseType = Tools.GetTypeByFullName(baseName);

            var i = 0;

            while (baseType == null && ++i < splittedPath.Length)
            {
                baseName += "." + splittedPath[i];
                baseType = Tools.GetTypeByFullName(baseName);
            }

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

            while (++i < splittedPath.Length)
            {
                var nestedType = baseType.GetNestedType(splittedPath[i], bindingFlags);

                if (nestedType != null)
                {
                    baseType = nestedType;
                }
                else
                    break;
            }

            var variableNames = splittedPath.Skip(i).ToArray();

            var baseMemberInfo = baseType.GetMember(
                Tools.GetVariableNameWithoutArrayIndices(variableNames[0]), bindingFlags | BindingFlags.Static)[0];

            var baseInfo = new BaseInfo();
            baseInfo.variableNames = variableNames;
            baseInfo.baseType = baseType;
            baseInfo.baseMemberInfo = baseMemberInfo;
            return baseInfo;
        }

        public static implicit operator bool(StaticVariable sv)
        {
            if (sv != null)
            {
                if (sv.successfully == null)
                {
                    if (!sv.fullPath.IsNullOrEmpty() && sv.fullPath != "None")
                    {
                        sv.variable = new StaticVariable<object>(sv.fullPath);

                        sv.successfully = true;
                    }
                    else
                        sv.successfully = false;
                }
            }
            else
                return false;

            return sv.successfully.Value;
        }
    }

    public class StaticVariable<T>
    {
        class Variable
        {
            MemberInfo member;
            int[] arrayIndices;

            public Variable(MemberInfo member, int[] arrayIndices)
            {
                this.member = member;
                this.arrayIndices = arrayIndices;
            }

            public object GetValue(object _object)
            {
                return member.GetValue(_object, arrayIndices);
            }

            public void SetValue(object _object, object value)
            {
                member.SetValue(_object, value, arrayIndices);
            }
        }

        List<Variable> variables = new List<Variable>();

        public T value
        {
            get
            {
                object result = null;

                for (int i = 0; i < variables.Count; ++i)
                {
                    result = variables[i].GetValue(result);
                }

                return (T)result;
            }

            set
            {
                object nextObject = null;

                List<object> objects = new List<object>();

                for (int i = 0; i < variables.Count; ++i)
                {
                    nextObject = variables[i].GetValue(nextObject);

                    objects.Add(nextObject);
                }

                objects[objects.Count - 1] = value;

                for (int i = variables.Count - 1; i > 0; --i)
                {
                    variables[i].SetValue(objects[i - 1], objects[i]);
                }

                variables[0].SetValue(null, objects[0]);
            }
        }

        public StaticVariable(string fullPath)
        {
            var baseInfo = StaticVariable.GetBaseInfo(fullPath);
            var nextMemberInfo = baseInfo.baseMemberInfo;
            var baseArrayIndices = Tools.GetArrayIndicesWithoutVariableName(baseInfo.variableNames[0]);
            var nextObject = baseInfo.baseMemberInfo.GetValue(null, baseArrayIndices);

            variables.Add(new Variable(baseInfo.baseMemberInfo, baseArrayIndices));

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            for (int i = 1; i < baseInfo.variableNames.Length; ++i)
            {
                var variableName = Tools.GetVariableNameWithoutArrayIndices(baseInfo.variableNames[i]);
                var arrayIndices = Tools.GetArrayIndicesWithoutVariableName(baseInfo.variableNames[i]);

                nextMemberInfo = nextObject.GetType().GetMember(variableName, bindingFlags)[0];
                nextObject = nextMemberInfo.GetValue(nextObject, arrayIndices);

                variables.Add(new Variable(nextMemberInfo, arrayIndices));
            }
        }

        public static T[] GetValues(params StaticVariable<T>[] variables)
        {
            return variables.Select((variable) => variable.value).ToArray();
        }
    }
}
