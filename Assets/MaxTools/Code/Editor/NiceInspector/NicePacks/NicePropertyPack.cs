using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace MaxTools.Editor
{
    public class NicePropertyPack
    {
        public FieldInfo fieldInfo;
        public SerializedProperty property;

        public HideInInspector hideInInspector;
        public ShowIfAttribute showIf;

        public BeginToggleGroupAttribute beginToggleGroup;
        public EndToggleGroupAttribute endToggleGroup;

        public BeginFoldoutGroupAttribute beginFoldoutGroup;
        public EndFoldoutGroupAttribute endFoldoutGroup;

        public SerializedObject serializedObject;
        public NiceGeneralPack generalPack;
        public string displayName;
    }
}
