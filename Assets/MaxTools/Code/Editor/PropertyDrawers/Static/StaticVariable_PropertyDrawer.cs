using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using MaxTools.Extensions;

    [CustomPropertyDrawer(typeof(StaticVariable), true)]
    public class StaticVariable_PropertyDrawer : PropertyDrawerBase
    {
        enum ButtonType
        {
            Nothing,
            AddVariable,
            AddArrayIndex
        }

        class VariablePack
        {
            public string[] variableNames;
            public FieldInfo[] members;
            public bool hasVariables
            {
                get => members.Length > 0;
            }

            public VariablePack(Type variableType)
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

                if (variableType.IsArray)
                {
                    members = variableType.GetElementType().GetFields(bindingFlags);
                }
                else
                    members = variableType.GetFields(bindingFlags);

                variableNames = members.Select((m) => m.Name).ToArray();
            }

            public Type GetVariableType(string variableName)
            {
                return members.First((m) => m.Name == variableName).GetVariableType();
            }
        }
        class VariablePath
        {
            public SerializedProperty subPath = null;
            public SerializedProperty staticPath = null;
            public SerializedProperty fullPath = null;

            public VariablePath(SerializedProperty property)
            {
                subPath = property.FindPropertyRelative("subPath");
                staticPath = property.FindPropertyRelative("staticPath");
                fullPath = property.FindPropertyRelative("fullPath");
            }

            public string[] subPathElements
            {
                get
                {
                    if (subPath.stringValue.IsNullOrEmpty())
                    {
                        return new string[0];
                    }
                    else
                        return subPath.stringValue.Split('\n');
                }
            }

            public void EraseSubPath(int n) // n никогда не будет 0 при вызове метода.
            {
                for (int i = subPath.stringValue.Length - 1; i > 0; --i)
                {
                    if (subPath.stringValue[i] == '\n' && --n == 0)
                    {
                        subPath.stringValue = subPath.stringValue.Substring(0, i);

                        return;
                    }
                }

                // Если subPath содержит 1 элемент, то в нём нет '\n'.

                subPath.stringValue = string.Empty;
            }
            public void AddSubPathElement(string value)
            {
                if (subPath.stringValue.IsNullOrEmpty())
                {
                    subPath.stringValue += value;
                }
                else
                    subPath.stringValue += $"\n{value}";
            }
            public void ReplaceSubPathElement(string newValue, int index)
            {
                var elements = subPathElements;

                elements[index] = newValue;

                subPath.stringValue = string.Empty;

                foreach (var element in elements)
                {
                    AddSubPathElement(element);
                }
            }
            public void UpdateFullPath()
            {
                var elements = subPathElements;

                fullPath.stringValue = staticPath.stringValue;

                for (int i = 0; i < elements.Length; ++i)
                {
                    if (int.TryParse(elements[i], out int arrayIndex))
                    {
                        fullPath.stringValue += $"[{arrayIndex}]";
                    }
                    else
                        fullPath.stringValue += $".{elements[i]}";
                }
            }
        }

        static List<string> options = null;
        static Dictionary<string, Type> staticTypes = new Dictionary<string, Type>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var totalHeight = 16.0f;

            if (options == null)
            {
                options = new List<string>() { "None", "" };

                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;

                foreach (var type in Tools.GetAllTypes())
                {
                    foreach (var variable in type.GetFields(bindingFlags | BindingFlags.Static))
                    {
                        if (variable.IsDefined(typeof(StaticVariableAttribute)))
                        {
                            var staticPath = $"{type.FullName}/{variable.Name}";

                            options.Add(staticPath);

                            staticTypes.Add(staticPath, variable.GetVariableType());
                        }
                    }
                }
            }

            if (options.Count > 2)
            {
                var variablePath = new VariablePath(property);

                int staticPopupIndex = 0;

                if (!variablePath.staticPath.stringValue.IsNullOrEmpty())
                {
                    int i = options.IndexOf(variablePath.staticPath.stringValue);

                    if (i > 0)
                    {
                        staticPopupIndex = i;
                    }
                }

                var oldWidth = position.width;

                position.height = 16.0f;
                position.x = EditorGUIUtility.labelWidth + 14.0f - EditorGUI.indentLevel * 15.0f;
                position.width = position.width - EditorGUIUtility.labelWidth + EditorGUI.indentLevel * 15.0f;

                EditorGUI.BeginProperty(position, null, variablePath.staticPath);

                EditorGUI.BeginChangeCheck();
                staticPopupIndex = EditorGUI.Popup(position, staticPopupIndex, options.ToArray());
                if (EditorGUI.EndChangeCheck())
                {
                    variablePath.subPath.stringValue = string.Empty; // Если произошли изменения, то стираем весь subPath.
                    variablePath.staticPath.stringValue = options[staticPopupIndex];
                    variablePath.UpdateFullPath();
                }

                EditorGUI.EndProperty();

                position.x = 14.0f;
                position.width = EditorGUIUtility.labelWidth;

                if (property.VisualizeRect(position, label) && !variablePath.staticPath.hasMultipleDifferentValues)
                {
                    if (variablePath.staticPath.stringValue != "None" &&
                       !variablePath.staticPath.stringValue.IsNullOrEmpty())
                    {
                        EditorGUI.indentLevel++;
                        position.width = oldWidth;

                        Type nextVariableType = staticTypes[variablePath.staticPath.stringValue];

                        if (!Cache<VariablePack>.TryGetValue(nextVariableType, out var nextPack))
                        {
                            nextPack = new VariablePack(nextVariableType);

                            Cache<VariablePack>.Add(nextVariableType, nextPack);
                        }

                        var addButtonType = ButtonType.Nothing;

                        if (nextPack.hasVariables)
                            addButtonType = ButtonType.AddVariable;

                        if (nextVariableType.IsArray)
                            addButtonType = ButtonType.AddArrayIndex;

                        var subPathElements = variablePath.subPathElements;

                        for (int i = 0; i < subPathElements.Length; ++i)
                        {
                            // Попытка получить индекс массива.

                            if (int.TryParse(subPathElements[i], out int index))
                            {
                                position.y += 18.0f;
                                totalHeight += 18.0f;

                                EditorGUI.BeginChangeCheck();
                                index = EditorGUI.DelayedIntField(position, "Array Index", index);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    variablePath.ReplaceSubPathElement($"{Tools.Abs(index)}", i);
                                    variablePath.UpdateFullPath();
                                }

                                if (nextPack.hasVariables)
                                    addButtonType = ButtonType.AddVariable;
                                else
                                    addButtonType = ButtonType.Nothing;
                            }
                            else // Если это имя переменной, а не индекс массива.
                            {
                                int nextPopupIndex = Array.IndexOf(nextPack.variableNames, subPathElements[i]);

                                if (nextPopupIndex < 0) // Если имя переменной исчезло из списка.
                                {
                                    variablePath.EraseSubPath(subPathElements.Length - i);
                                    variablePath.UpdateFullPath();
                                    break;
                                }

                                position.y += 18.0f;
                                totalHeight += 18.0f;

                                EditorGUI.BeginChangeCheck();
                                nextPopupIndex = EditorGUI.Popup(position, "Next Variable", nextPopupIndex, nextPack.variableNames);
                                string nextVariableName = nextPack.variableNames[nextPopupIndex];
                                if (EditorGUI.EndChangeCheck())
                                {
                                    variablePath.ReplaceSubPathElement(nextVariableName, i);
                                    variablePath.UpdateFullPath();
                                }

                                nextVariableType = nextPack.GetVariableType(nextVariableName);

                                if (!Cache<VariablePack>.TryGetValue(nextVariableType, out nextPack))
                                {
                                    nextPack = new VariablePack(nextVariableType);

                                    Cache<VariablePack>.Add(nextVariableType, nextPack);
                                }

                                addButtonType = ButtonType.Nothing;

                                if (nextPack.hasVariables)
                                    addButtonType = ButtonType.AddVariable;

                                if (nextVariableType.IsArray)
                                    addButtonType = ButtonType.AddArrayIndex;
                            }
                        }

                        EditorGUI.indentLevel--;
                        position.x = EditorGUIUtility.labelWidth + 14.0f;
                        position.width = position.width - EditorGUIUtility.labelWidth;

                        if (addButtonType != ButtonType.Nothing)
                        {
                            position.y += 18.0f;
                            totalHeight += 18.0f;

                            switch (addButtonType)
                            {
                                case ButtonType.AddVariable:
                                    if (GUI.Button(position, "Add Variable"))
                                    {
                                        variablePath.AddSubPathElement(nextPack.variableNames[0]);
                                        variablePath.UpdateFullPath();
                                    }
                                    break;

                                case ButtonType.AddArrayIndex:
                                    if (GUI.Button(position, "Add Array Index"))
                                    {
                                        variablePath.AddSubPathElement("0");
                                        variablePath.UpdateFullPath();
                                    }
                                    break;
                            }
                        }

                        if (subPathElements.Length > 0)
                        {
                            position.y += 18.0f;

                            if (GUI.Button(position, "Remove"))
                            {
                                variablePath.EraseSubPath(1);
                                variablePath.UpdateFullPath();
                            }

                            position.y += 18.0f;

                            if (GUI.Button(position, "Clear"))
                            {
                                variablePath.subPath.stringValue = string.Empty;
                                variablePath.UpdateFullPath();
                            }

                            position.y += 18.0f;
                            position.x -= EditorGUI.indentLevel * 15.0f;
                            position.width += EditorGUI.indentLevel * 15.0f;

                            EditorGUI.LabelField(position, variablePath.fullPath.stringValue);

                            totalHeight += 54.0f;
                        }
                    }
                }
            }
            else
                EditorGUI.LabelField(position, label.text, "No static variables found!");

            SetPropertyHeight(property, totalHeight);
        }
    }
}
