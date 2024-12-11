using UnityEditor;
using UnityEngine;

namespace MaxTools.Editor
{
    public abstract class PropertyDrawerBase : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Cache<float>.TryGetValue(GetCacheKey(property), out float result))
            {
                result = base.GetPropertyHeight(property, label);

                SetPropertyHeight(property, result);
            }

            return result;
        }

        public void SetPropertyHeight(SerializedProperty property, float totalHeight)
        {
            Cache<float>.SetValue(GetCacheKey(property), totalHeight);
        }

        object GetCacheKey(SerializedProperty property)
        {
            return Tools.MakeUniqueKey((property.serializedObject.targetObject, property.propertyPath));
        }
    }
}
