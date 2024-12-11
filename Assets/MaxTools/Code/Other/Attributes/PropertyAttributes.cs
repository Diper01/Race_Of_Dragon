using UnityEngine;
using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    [AttributeUsage(AttributeTargets.Field)]
    public class MaxAttribute : PropertyAttribute
    {
        public float max
        {
            get;
            private set;
        }

        public MaxAttribute(float max)
        {
            if (float.IsInfinity(max) || float.IsNaN(max))
            {
                throw new ArgumentException("Invalid value!");
            }

            this.max = max;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public float min { get; private set; } = 0.0f;
        public float max { get; private set; } = 1.0f;

        public MinMaxSliderAttribute(float min, float max)
        {
            if (float.IsInfinity(min) || float.IsInfinity(max))
            {
                throw new ArgumentException("Invalid values!");
            }

            if (float.IsNaN(min) || float.IsNaN(max))
            {
                throw new ArgumentException("Invalid values!");
            }

            if (min >= max)
            {
                throw new ArgumentException("Invalid values!");
            }

            this.min = min;
            this.max = max;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxUnlimitedAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class Range01Attribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class RangeNamedAttribute : PropertyAttribute
    {
        public float min { get; private set; } = 0.0f;
        public float max { get; private set; } = 1.0f;

        public string minName { get; private set; } = "Min";
        public string maxName { get; private set; } = "Max";

        public RangeNamedAttribute(float min, float max, string minName, string maxName)
        {
            if (float.IsInfinity(min) || float.IsInfinity(max))
            {
                throw new ArgumentException("Invalid values!");
            }

            if (float.IsNaN(min) || float.IsNaN(max))
            {
                throw new ArgumentException("Invalid values!");
            }

            if (min >= max)
            {
                throw new ArgumentException("Invalid values!");
            }

            if (minName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Invalid minName!");
            }

            if (maxName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Invalid maxName!");
            }

            this.min = min;
            this.max = max;
            this.minName = minName;
            this.maxName = maxName;
        }
    }
}
