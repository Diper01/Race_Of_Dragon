using UnityEngine;
using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    [Serializable]
    public class ArrayWrapper
    {
        [SerializeField] string[] values;
        [SerializeField] int[] lengths;

        [SerializeField]
        string elementTypeName;

        Array m_Array;

        public Array array
        {
            get
            {
                if (m_Array == null)
                {
                    var elementType = Type.GetType(elementTypeName);

                    m_Array = Array.CreateInstance(elementType, lengths);

                    for (int i = 0; i < values.Length; ++i)
                    {
                        var value = Tools.Deserialize(values[i], elementType);

                        m_Array.SetValue(value, m_Array.GetIndices(i));
                    }
                }

                return m_Array;
            }

            private set
            {
                m_Array = value;
            }
        }

        public ArrayWrapper(Array target)
        {
            array = target;
            values = new string[target.Length];
            lengths = target.GetLengths();

            elementTypeName = target.GetType()
                .GetElementType()
                .AssemblyQualifiedName;

            for (int i = 0; i < target.Length; ++i)
            {
                var value = target.GetValue(target.GetIndices(i));

                values[i] = Tools.Serialize(value);
            }
        }
    }

    [Serializable]
    public class ArrayWrapper<T>
    {
        [SerializeField] T[] values;
        [SerializeField] int[] lengths;

        Array m_Array;

        public Array array
        {
            get => m_Array ?? (m_Array = values.ChangeRank(lengths));

            private set => m_Array = value;
        }

        public ArrayWrapper(Array target)
        {
            array = target;
            values = target.To1D<T>();
            lengths = target.GetLengths();
        }
    }
}
