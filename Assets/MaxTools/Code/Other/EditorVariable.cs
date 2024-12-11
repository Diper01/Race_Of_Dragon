using UnityEngine;
using System;

namespace MaxTools
{
    [Serializable]
    public class EditorVariable
    {
        public enum VariableType
        {
            Int,
            Bool,
            Float,
            String,
            Color,
            Vector2,
            Vector3,
            Vector2Int,
            Vector3Int,
            Object,
            Static
        }

        [SerializeField] VariableType variableType;
        [SerializeField] int intVariable;
        [SerializeField] bool boolVariable;
        [SerializeField] float floatVariable;
        [SerializeField] string stringVariable;
        [SerializeField] Color colorVariable;
        [SerializeField] Vector2 vector2Variable;
        [SerializeField] Vector3 vector3Variable;
        [SerializeField] Vector2Int vector2IntVariable;
        [SerializeField] Vector3Int vector3IntVariable;
        [SerializeField] UnityEngine.Object objectVariable;
        [SerializeField] StaticVariable staticVariable;

        public object value
        {
            get
            {
                switch (variableType)
                {
                    case VariableType.Int: return intVariable;
                    case VariableType.Bool: return boolVariable;
                    case VariableType.Float: return floatVariable;
                    case VariableType.String: return stringVariable;
                    case VariableType.Color: return colorVariable;
                    case VariableType.Vector2: return vector2Variable;
                    case VariableType.Vector3: return vector3Variable;
                    case VariableType.Vector2Int: return vector2IntVariable;
                    case VariableType.Vector3Int: return vector3IntVariable;
                    case VariableType.Object: return objectVariable;
                    case VariableType.Static: return staticVariable.value;
                }

                throw new Exception("[EditorVariable.GetValue] Invalid variableType!");
            }
        }
    }
}
