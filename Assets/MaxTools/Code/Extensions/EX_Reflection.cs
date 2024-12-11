using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MaxTools.Extensions
{
    public static class EX_Reflection
    {
        public static bool IsFractionalType(this Type me)
        {
            switch (Type.GetTypeCode(me))
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
            }

            return false;
        }
        public static bool IsIntegerType(this Type me)
        {
            switch (Type.GetTypeCode(me))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }

            return false;
        }
        public static bool IsNumericType(this Type me)
        {
            switch (Type.GetTypeCode(me))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
            }

            return false;
        }
        public static bool IsCommonType(this Type me)
        {
            switch (Type.GetTypeCode(me))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Char:
                case TypeCode.String:
                case TypeCode.DateTime:
                    return true;
            }

            return false;
        }
        public static bool IsGenericType(this Type me, Type definitionType)
        {
            return me.IsGenericType && me.GetGenericTypeDefinition() == definitionType;
        }
        public static bool IsSubclassOrSame(this Type me, Type type)
        {
            return me == type || me.IsSubclassOf(type);
        }
        public static bool IsAutoProperty(this PropertyInfo me)
        {
            return me.GetBackingField() != null;
        }
        public static bool HasDefaultConstructor(this Type me)
        {
            return me.IsValueType || me.GetConstructor(Type.EmptyTypes) != null;
        }

        public static object GetValue(this MemberInfo me, object _object)
        {
            switch (me)
            {
                case FieldInfo fieldInfo:
                    return fieldInfo.GetValue(_object);

                case PropertyInfo propertyInfo:
                    return propertyInfo.GetValue(_object);
            }

            throw new Exception("Member is not a variable!");
        }
        public static object GetValue(this MemberInfo me, object _object, int[] arrayIndices)
        {
            var currentValue = me.GetValue(_object);

            if (arrayIndices != null && arrayIndices.Length > 0)
            {
                if (currentValue is Array array)
                {
                    return array.GetValue(arrayIndices);
                }
                else
                {
                    foreach (var indexer in currentValue.GetType().GetIndexers(IndexerType.get_Item))
                    {
                        var parameters = indexer.GetParameters();

                        if (parameters.Length == arrayIndices.Length)
                        {
                            if (parameters.All(p => p.ParameterType.IsIntegerType()))
                            {
                                return indexer.Invoke(currentValue, (object[])arrayIndices.ChangeType<object>());
                            }
                        }
                    }

                    throw new Exception("Indexer not found!");
                }
            }
            else
                return currentValue;
        }
        public static object GetValue(this MemberInfo me, object _object, int? arrayIndex)
        {
            if (arrayIndex == null)
            {
                return me.GetValue(_object);
            }
            else
                return me.GetValue(_object, new int[] { arrayIndex.Value });
        }
        public static object GetValue(this MemberInfo me, object _object, int arrayIndex)
        {
            return me.GetValue(_object, new int[] { arrayIndex });
        }
        public static object GetValue(this MemberInfo me, object _object, object i)
        {
            if (i == null)
            {
                return me.GetValue(_object);
            }
            else if (i.GetType() == typeof(int[]))
            {
                return me.GetValue(_object, (int[])i);
            }
            else if (i.GetType() == typeof(int?))
            {
                return me.GetValue(_object, (int?)i);
            }
            else if (i.GetType() == typeof(int))
            {
                return me.GetValue(_object, (int)i);
            }

            throw new Exception("Unsupported type!");
        }

        public static void SetValue(this MemberInfo me, object _object, object value)
        {
            switch (me)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(_object, value);
                    return;

                case PropertyInfo propertyInfo:
                    propertyInfo.SetValue(_object, value);
                    return;
            }

            throw new Exception("Member is not a variable!");
        }
        public static void SetValue(this MemberInfo me, object _object, object value, int[] arrayIndices)
        {
            if (arrayIndices != null && arrayIndices.Length > 0)
            {
                var currentValue = me.GetValue(_object);

                if (currentValue is Array array)
                {
                    array.SetValue(value, arrayIndices);
                }
                else
                {
                    foreach (var indexer in currentValue.GetType().GetIndexers(IndexerType.set_Item))
                    {
                        var parameters = indexer.GetParameters();

                        if (parameters.Length - 1 == arrayIndices.Length)
                        {
                            if (parameters.Take(parameters.Length - 1).All(p => p.ParameterType.IsIntegerType()))
                            {
                                List<object> args = new List<object>((object[])arrayIndices.ChangeType<object>());

                                args.Add(value);

                                indexer.Invoke(currentValue, args.ToArray());

                                return;
                            }
                        }
                    }

                    throw new Exception("Indexer not found!");
                }
            }
            else
                me.SetValue(_object, value);
        }
        public static void SetValue(this MemberInfo me, object _object, object value, int? arrayIndex)
        {
            if (arrayIndex == null)
            {
                me.SetValue(_object, value);
            }
            else
                me.SetValue(_object, value, new int[] { arrayIndex.Value });
        }
        public static void SetValue(this MemberInfo me, object _object, object value, int arrayIndex)
        {
            me.SetValue(_object, value, new int[] { arrayIndex });
        }
        public static void SetValue(this MemberInfo me, object _object, object value, object i)
        {
            if (i == null)
            {
                me.SetValue(_object, value);
                return;
            }
            else if (i.GetType() == typeof(int[]))
            {
                me.SetValue(_object, value, (int[])i);
                return;
            }
            else if (i.GetType() == typeof(int?))
            {
                me.SetValue(_object, value, (int?)i);
                return;
            }
            else if (i.GetType() == typeof(int))
            {
                me.SetValue(_object, value, (int)i);
                return;
            }

            throw new Exception("Unsupported type!");
        }

        public static object GetNestedInstance(this object me, string variablePath)
        {
            var pathElements = variablePath.Split('.', '/', '\\').Select((element) => (
            variableName: Tools.GetVariableNameWithoutArrayIndices(element),
            arrayIndices: Tools.GetArrayIndicesWithoutVariableName(element))).ToArray();

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            for (int i = 0; i < pathElements.Length; ++i)
            {
                var variableName = pathElements[i].variableName;
                var arrayIndices = pathElements[i].arrayIndices;

                me = me.GetType().GetMember(variableName, bindingFlags)[0].GetValue(me, arrayIndices);
            }

            return me;
        }
        public static void SetNestedInstance(this object me, string variablePath, object value)
        {
            var pathElements = variablePath.Split('.', '/', '\\').Select((element) => (
            variableName: Tools.GetVariableNameWithoutArrayIndices(element),
            arrayIndices: Tools.GetArrayIndicesWithoutVariableName(element))).ToArray();

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var list = new List<(MemberInfo memberInfo, object memberValue)>();

            var main = me;

            for (int i = 0; i < pathElements.Length; ++i)
            {
                var memberInfo = me.GetType().GetMember(pathElements[i].variableName, bindingFlags)[0];

                me = memberInfo.GetValue(me, pathElements[i].arrayIndices);

                list.Add((memberInfo, me));
            }

            list[list.Count - 1] = (list[list.Count - 1].memberInfo, value);

            for (int i = list.Count - 1; i > 0; --i)
            {
                list[i].memberInfo.SetValue(
                    list[i - 1].memberValue, list[i].memberValue, pathElements[i].arrayIndices);
            }

            list[0].memberInfo.SetValue(
                main, list[0].memberValue, pathElements[0].arrayIndices);
        }

        public static object InvokeMethod(this object me, string methodName, params object[] args)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var types = args?.Select((arg) => arg.GetType()).ToArray() ?? Type.EmptyTypes;

            return me.GetType().GetMethod(methodName, bindingFlags, null, types, null).Invoke(me, args);
        }
        public static object InvokeMethod(this Type me, string methodName, params object[] args)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            var types = args?.Select((arg) => arg.GetType()).ToArray() ?? Type.EmptyTypes;

            return me.GetMethod(methodName, bindingFlags, null, types, null).Invoke(null, args);
        }

        public static object GetDeepClone(this object me)
        {
            if (me == null) return null;

            if (me is Array array)
            {
                var clonedArray = Array.CreateInstance(array.GetType().GetElementType(), array.GetLengths());

                for (int i = 0; i < clonedArray.Length; ++i)
                {
                    var indices = array.GetIndices(i);

                    var clonedValue = array.GetValue(indices).GetDeepClone();

                    clonedArray.SetValue(clonedValue, indices);
                }

                return clonedArray;
            }
            else if (me.GetType().IsGenericType(typeof(List<>)))
            {
                var clonedArray = me.InvokeMethod("ToArray").GetDeepClone();

                return Activator.CreateInstance(me.GetType(), new object[] { clonedArray });
            }
            else if (me is ICloneable cloneable)
            {
                return cloneable.Clone();
            }
            else
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

                var clone = me.GetMemberwiseClone();

                foreach (var field in clone.GetType().GetFields(bindingFlags))
                {
                    if (field.FieldType.IsCommonType())
                    {
                        field.SetValue(clone, field.GetValue(me).GetMemberwiseClone());
                    }
                    else
                        field.SetValue(clone, field.GetValue(me).GetDeepClone());
                }

                return clone;
            }
        }
        public static object GetMemberwiseClone(this object me)
        {
            if (me == null) return null;

            return me.InvokeMethod("MemberwiseClone");
        }

        public static Type GetVariableType(this MemberInfo me)
        {
            switch (me)
            {
                case FieldInfo fieldInfo:
                    return fieldInfo.FieldType;

                case PropertyInfo propertyInfo:
                    return propertyInfo.PropertyType;
            }

            throw new Exception("Member is not a variable!");
        }

        public static FieldInfo GetBackingField(this PropertyInfo me)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

            return me.DeclaringType.GetField($"<{me.Name}>k__BackingField", bindingFlags);
        }

        public static Type FindAncestorType(this Type me, string ancestorName)
        {
            var next = me.BaseType;

            while (next != null)
            {
                if (next.Name == ancestorName)
                {
                    return next;
                }

                next = next.BaseType;
            }

            return null;
        }

        public static object CreateInstance(this Type me, bool? viaActivator = null)
        {
            if (viaActivator == null)
            {
                if (me.HasDefaultConstructor())
                {
                    return Activator.CreateInstance(me);
                }
                else
                    return FormatterServices.GetUninitializedObject(me);
            }
            else if (viaActivator.Value)
            {
                return Activator.CreateInstance(me);
            }
            else
                return FormatterServices.GetUninitializedObject(me);
        }

        public static MethodInfo[] GetOperators(this Type me, OperatorType operatorType,
            Type returnType = null, Type otherParameterType = null, OperatorParameterPosition otherParameterPosition = default)
        {
            List<MethodInfo> operators = new List<MethodInfo>();

            foreach (var method in me.GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                if (method.Name != operatorType.ToString())
                {
                    continue;
                }

                if (returnType != null)
                {
                    if (method.ReturnType != returnType)
                    {
                        continue;
                    }
                }

                if (otherParameterType != null)
                {
                    ParameterInfo[] parameters = method.GetParameters();

                    var p1 = parameters[1].ParameterType == me &&
                        parameters[0].ParameterType == otherParameterType;

                    var p2 = parameters[0].ParameterType == me &&
                        parameters[1].ParameterType == otherParameterType;

                    switch (otherParameterPosition)
                    {
                        case OperatorParameterPosition.Undefined:
                            if (!p1 && !p2)
                                continue;
                            break;

                        case OperatorParameterPosition.First:
                            if (!p1)
                                continue;
                            break;

                        case OperatorParameterPosition.Second:
                            if (!p2)
                                continue;
                            break;
                    }
                }

                operators.Add(method);
            }

            return operators.ToArray();
        }

        public static MethodInfo GetOperator(this Type me, OperatorType operatorType,
            Type returnType = null, Type otherParameterType = null, OperatorParameterPosition otherParameterPosition = default)
        {
            var operators = me.GetOperators(operatorType, returnType, otherParameterType, otherParameterPosition);

            if (operators != null && operators.Length > 0)
            {
                return operators[0];
            }
            else
                return null;
        }

        public static MethodInfo GetConversionOperator(this Type me, Type inputType, Type returnType)
        {
            return me.GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault((m) =>
            {
                return (m.Name == OperatorType.op_Implicit.ToString()
                || m.Name == OperatorType.op_Explicit.ToString())
                && m.GetParameters()[0].ParameterType == inputType
                && m.ReturnType == returnType;
            });
        }

        public static MethodInfo[] GetIndexers(this Type me, IndexerType indexerType,
            BindingFlags? bindingFlags = null, Type returnType = null, bool strictParameterTypes = true, params Type[] parameterTypes)
        {
            if (bindingFlags == null)
            {
                bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            }

            List<MethodInfo> indexers = new List<MethodInfo>();

            foreach (var method in me.GetMethods(bindingFlags.Value))
            {
                if (method.Name != indexerType.ToString())
                {
                    continue;
                }

                if (returnType != null)
                {
                    if (method.ReturnType != returnType)
                    {
                        continue;
                    }
                }

                if (parameterTypes != null && parameterTypes.Length > 0)
                {
                    ParameterInfo[] parameters = method.GetParameters();

                    if (strictParameterTypes && parameters.Length != parameterTypes.Length)
                    {
                        continue;
                    }

                    if (parameters.Length < parameterTypes.Length)
                    {
                        continue;
                    }

                    int min = Tools.Min(parameters.Length, parameterTypes.Length);

                    bool skip = false;

                    for (int i = 0; i < min; ++i)
                    {
                        if (parameters[i].ParameterType != parameterTypes[i])
                        {
                            skip = true;

                            break;
                        }
                    }

                    if (skip)
                    {
                        continue;
                    }
                }

                indexers.Add(method);
            }

            return indexers.ToArray();
        }

        public static MethodInfo GetIndexer(this Type me, IndexerType indexerType,
            BindingFlags? bindingFlags = null, Type returnType = null, bool strictParameterTypes = true, params Type[] parameterTypes)
        {
            var indexers = me.GetIndexers(indexerType, bindingFlags, returnType, strictParameterTypes, parameterTypes);

            if (indexers != null && indexers.Length > 0)
            {
                return indexers[0];
            }
            else
                return null;
        }
    }
}
