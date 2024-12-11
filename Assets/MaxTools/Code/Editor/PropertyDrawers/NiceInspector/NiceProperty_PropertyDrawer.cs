using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    [CustomPropertyDrawer(typeof(NiceProperty), true)]
    public class NiceProperty_PropertyDrawer : PropertyDrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Cache<NiceGeneralPack>.TryGetValue(GetCacheKey(property), out var generalPack))
            {
                generalPack = new NiceGeneralPack(property);

                Cache<NiceGeneralPack>.Add(GetCacheKey(property), generalPack);
            }

            SetPropertyHeight(property, NiceInspector.DrawNiceInspectorRect(position, property, label, generalPack));
        }

        object GetCacheKey(SerializedProperty property)
        {
            return Tools.MakeUniqueKey((property.serializedObject.targetObject, property.propertyPath));
        }
    }
}
