using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools.Editor
{
    using MaxTools.Editor.Extensions;

    using MaxTools.Extensions;

    using Editor = UnityEditor.Editor;

    public class NiceGeneralPack
    {
        public List<NiceMethodPack> niceMethodPacks = new List<NiceMethodPack>();
        public List<NicePropertyPack> nicePropertyPacks = new List<NicePropertyPack>();
        public List<string> skipVariableNames = new List<string>();

        Editor mainEditor = null;
        SerializedProperty mainProperty = null;

        public NiceGeneralPack(Editor editor)
        {
            mainEditor = editor;

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var member in editor.target.GetType().GetMembers(bindingFlags))
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var property = editor.serializedObject.FindProperty(member.Name);

                            if (property != null)
                                nicePropertyPacks.Add(CreatePropertyPack((FieldInfo)member, property));

                            break;
                        }

                    case MemberTypes.Method:
                        {
                            var easyButton = member.GetCustomAttribute<EasyButtonAttribute>();

                            if (easyButton != null)
                                niceMethodPacks.Add(CreateMethodPack((MethodInfo)member, easyButton));

                            break;
                        }
                }
            }

            SortNiceMethodPacks();
            SortNicePropertyPacks();
        }
        public NiceGeneralPack(SerializedProperty property)
        {
            mainProperty = property;

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var member in property.GetPropertyType().GetMembers(bindingFlags))
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        {
                            var propertyRelative = property.FindPropertyRelative(member.Name);

                            if (propertyRelative != null)
                                nicePropertyPacks.Add(CreatePropertyPack((FieldInfo)member, propertyRelative));

                            break;
                        }

                    case MemberTypes.Method:
                        {
                            var easyButton = member.GetCustomAttribute<EasyButtonAttribute>();

                            if (easyButton != null)
                                niceMethodPacks.Add(CreateMethodPack((MethodInfo)member, easyButton));

                            break;
                        }
                }
            }

            SortNiceMethodPacks();
            SortNicePropertyPacks();
        }

        NiceMethodPack CreateMethodPack(MethodInfo methodInfo, EasyButtonAttribute easyButton)
        {
            var methodPack = new NiceMethodPack();

            methodPack.methodInfo = methodInfo;
            methodPack.easyButton = easyButton;

            methodPack.hideInInspector = methodInfo.GetCustomAttribute<HideInInspector>();
            methodPack.showIf = methodInfo.GetCustomAttribute<ShowIfAttribute>();

            var inspectorName = methodInfo.GetCustomAttribute<InspectorNameAttribute>()?.displayName;
            var niceName = ObjectNames.NicifyVariableName(methodInfo.Name);
            methodPack.displayName = inspectorName ?? niceName;

            return methodPack;
        }
        NicePropertyPack CreatePropertyPack(FieldInfo fieldInfo, SerializedProperty property)
        {
            var propertyPack = new NicePropertyPack();

            propertyPack.fieldInfo = fieldInfo;
            propertyPack.property = property;

            propertyPack.hideInInspector = fieldInfo.GetCustomAttribute<HideInInspector>();
            propertyPack.showIf = fieldInfo.GetCustomAttribute<ShowIfAttribute>();

            propertyPack.beginToggleGroup = fieldInfo.GetCustomAttribute<BeginToggleGroupAttribute>();
            propertyPack.endToggleGroup = fieldInfo.GetCustomAttribute<EndToggleGroupAttribute>();

            propertyPack.beginFoldoutGroup = fieldInfo.GetCustomAttribute<BeginFoldoutGroupAttribute>();
            propertyPack.endFoldoutGroup = fieldInfo.GetCustomAttribute<EndFoldoutGroupAttribute>();

            propertyPack.serializedObject = property.serializedObject;
            propertyPack.generalPack = this;

            var inspectorName = fieldInfo.GetCustomAttribute<InspectorNameAttribute>()?.displayName;
            propertyPack.displayName = inspectorName ?? property.displayName;

            if (propertyPack.beginFoldoutGroup != null &&
                propertyPack.beginFoldoutGroup.toggleName.IsNotNullOrWhiteSpace())
            {
                skipVariableNames.Add(propertyPack.beginFoldoutGroup.toggleName);
            }

            return propertyPack;
        }

        void SortNiceMethodPacks()
        {
            if (niceMethodPacks.Count > 1)
            {
                var sorted = new List<NiceMethodPack>();

                foreach (var group in niceMethodPacks.GroupBy((pack) =>
                pack.methodInfo.DeclaringType).Reverse())
                {
                    sorted.AddRange(group);
                }

                niceMethodPacks = sorted;
            }
        }
        void SortNicePropertyPacks()
        {
            if (nicePropertyPacks.Count > 1)
            {
                var sorted = new List<NicePropertyPack>();

                foreach (var group in nicePropertyPacks.GroupBy((pack) =>
                pack.fieldInfo.DeclaringType).Reverse())
                {
                    sorted.AddRange(group);
                }

                nicePropertyPacks = sorted;
            }
        }

        public NicePropertyPack GetPropertyPack(string variableName)
        {
            return nicePropertyPacks.Find((pack) => pack.property.name == variableName);
        }

        public object GetInstance()
        {
            if (mainEditor != null)
            {
                return mainEditor.serializedObject.targetObject;
            }
            else if (mainProperty != null)
            {
                return mainProperty.serializedObject.targetObject.GetNestedInstance(mainProperty.GetPropertyPath());
            }

            return null;
        }
        public IEnumerable<object> GetInstances()
        {
            if (mainEditor != null)
            {
                foreach (var targetObject in mainEditor.serializedObject.targetObjects)
                {
                    yield return targetObject;
                }
            }
            else if (mainProperty != null)
            {
                foreach (var targetObject in mainProperty.serializedObject.targetObjects)
                {
                    yield return targetObject.GetNestedInstance(mainProperty.GetPropertyPath());
                }
            }
        }

        public void DrawProperties(ref Rect position, ref float totalHeight)
        {
            var canVisualize = true;

            int cancelToggleGroupCounter = 0;
            int visualToggleGroupCounter = 0;

            int cancelFoldoutGroupCounter = 0;
            int visualFoldoutGroupCounter = 0;

            int oldIndentLevel = EditorGUI.indentLevel;

            foreach (var niceProperty in nicePropertyPacks)
            {
                if (skipVariableNames.Contains(niceProperty.property.name))
                {
                    continue;
                }

                if (niceProperty.endToggleGroup != null)
                {
                    if ((cancelToggleGroupCounter -= niceProperty.endToggleGroup.iteration) <= 0)
                    {
                        if (cancelToggleGroupCounter < -visualToggleGroupCounter)
                            cancelToggleGroupCounter = -visualToggleGroupCounter;

                        visualToggleGroupCounter += cancelToggleGroupCounter;
                        EditorGUI.indentLevel += cancelToggleGroupCounter;
                        cancelToggleGroupCounter = 0;
                        canVisualize = true;
                    }
                }

                if (niceProperty.endFoldoutGroup != null)
                {
                    if ((cancelFoldoutGroupCounter -= niceProperty.endFoldoutGroup.iteration) <= 0)
                    {
                        if (cancelFoldoutGroupCounter < -visualFoldoutGroupCounter)
                            cancelFoldoutGroupCounter = -visualFoldoutGroupCounter;

                        visualFoldoutGroupCounter += cancelFoldoutGroupCounter;
                        EditorGUI.indentLevel += cancelFoldoutGroupCounter;
                        cancelFoldoutGroupCounter = 0;
                        canVisualize = true;
                    }
                }

                if (canVisualize)
                {
                    if (niceProperty.hideInInspector != null)
                    {
                        if (niceProperty.beginToggleGroup != null)
                        {
                            canVisualize = false;
                            cancelToggleGroupCounter++;
                        }

                        if (niceProperty.beginFoldoutGroup != null)
                        {
                            canVisualize = false;
                            cancelFoldoutGroupCounter++;
                        }

                        continue;
                    }

                    if (niceProperty.showIf != null)
                    {
                        if (!niceProperty.showIf.CheckCondition(GetInstance()))
                        {
                            if (niceProperty.beginToggleGroup != null)
                            {
                                canVisualize = false;
                                cancelToggleGroupCounter++;
                            }

                            if (niceProperty.beginFoldoutGroup != null)
                            {
                                canVisualize = false;
                                cancelFoldoutGroupCounter++;
                            }

                            continue;
                        }
                    }
                }

                if (niceProperty.beginToggleGroup != null)
                {
                    if (canVisualize)
                    {
                        EditorGUI.BeginChangeCheck();

                        position.y += 2.0f;
                        position.height = 16.0f;

                        EditorGUI.BeginProperty(position, null, niceProperty.property);

                        var value = EditorGUI.ToggleLeft(
                            position,
                            niceProperty.displayName,
                            niceProperty.property.boolValue);

                        EditorGUI.EndProperty();

                        position.y += 16.0f;
                        totalHeight += 18.0f;

                        if (EditorGUI.EndChangeCheck())
                            niceProperty.property.boolValue = value;

                        canVisualize = niceProperty.property.boolValue;

                        if (canVisualize)
                        {
                            visualToggleGroupCounter++;
                            EditorGUI.indentLevel++;
                        }
                        else
                            cancelToggleGroupCounter++;
                    }
                    else
                        cancelToggleGroupCounter++;

                    continue;
                }

                if (niceProperty.beginFoldoutGroup != null)
                {
                    if (canVisualize)
                    {
                        position.y += 2.0f;
                        position.height = 16.0f;

                        niceProperty.property.FoldoutRect(
                            position,
                            niceProperty.beginFoldoutGroup.tabName,
                            GetPropertyPack(niceProperty.beginFoldoutGroup.toggleName)?.property);

                        position.y += 16.0f;
                        totalHeight += 18.0f;

                        canVisualize = niceProperty.property.boolValue;

                        if (canVisualize)
                        {
                            visualFoldoutGroupCounter++;
                            EditorGUI.indentLevel++;
                        }
                        else
                            cancelFoldoutGroupCounter++;
                    }
                    else
                        cancelFoldoutGroupCounter++;

                    continue;
                }

                if (canVisualize)
                {
                    position.y += 2.0f;
                    position.height = EditorGUI.GetPropertyHeight(niceProperty.property);

                    niceProperty.property.VisualizeRect(
                        position,
                        niceProperty.displayName,
                        true);

                    position.y += position.height;
                    totalHeight += position.height + 2.0f;
                }
            }

            EditorGUI.indentLevel = oldIndentLevel;
        }
        public void DrawMethods(ref Rect position, ref float totalHeight)
        {
            var oldGUIEnabled = GUI.enabled;

            GUI.enabled = true;

            position.x += EditorGUI.indentLevel * 15.0f;
            position.width -= EditorGUI.indentLevel * 15.0f;

            foreach (var niceMethod in niceMethodPacks)
            {
                if (niceMethod.hideInInspector != null)
                {
                    continue;
                }

                if (niceMethod.showIf != null)
                {
                    if (!niceMethod.showIf.CheckCondition(GetInstance()))
                    {
                        continue;
                    }
                }

                position.y += 2.0f;
                position.height = 18.0f;

                if (GUI.Button(position, niceMethod.displayName))
                {
                    foreach (var instance in GetInstances())
                    {
                        niceMethod.methodInfo.Invoke(instance, null);
                    }
                }

                position.y += 18.0f;
                totalHeight += 20.0f;
            }

            GUI.enabled = oldGUIEnabled;
        }
    }
}
