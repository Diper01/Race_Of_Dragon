using UnityEngine;
using System;

namespace MaxTools
{
    [Serializable]
    public class EditorEnum
    {
        [SerializeField] int _;
        [SerializeField] string enumItemName;
        [SerializeField] string enumTypeFullName;

        public Enum value
        {
            get
            {
                return (Enum)Enum.Parse(enumType, enumItemName);
            }
        }

        Type m_EnumType;

        public Type enumType
        {
            get
            {
                if (m_EnumType == null)
                    m_EnumType = Tools.GetTypeByFullName(enumTypeFullName);

                return m_EnumType;
            }

            private set
            {
                m_EnumType = value;
            }
        }

        EditorEnum(Enum value)
        {
            enumItemName = value.ToString();
            enumTypeFullName = value.GetType().FullName;
            enumType = value.GetType();
        }

        public static implicit operator EditorEnum(Enum value)
        {
            return new EditorEnum(value);
        }

        public static bool operator ==(EditorEnum a, EditorEnum b)
        {
            return a.enumType == b.enumType && a.enumItemName == b.enumItemName;
        }
        public static bool operator !=(EditorEnum a, EditorEnum b)
        {
            return a.enumType != b.enumType || a.enumItemName != b.enumItemName;
        }

        public override bool Equals(object _object)
        {
            if (_object is EditorEnum editorEnum)
            {
                return editorEnum == this;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return enumItemName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
