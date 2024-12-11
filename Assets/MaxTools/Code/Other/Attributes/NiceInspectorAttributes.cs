using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    [AttributeUsage(AttributeTargets.Class)]
    public class NiceInspectorAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class BeginToggleGroupAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class EndToggleGroupAttribute : Attribute
    {
        public int iteration { get; private set; } = 1;

        public EndToggleGroupAttribute() { }
        public EndToggleGroupAttribute(int iteration)
        {
            if (iteration > 1)
            {
                this.iteration = iteration;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class BeginFoldoutGroupAttribute : Attribute
    {
        public string tabName { get; private set; } = "";
        public string toggleName { get; private set; } = "";

        public BeginFoldoutGroupAttribute() { }
        public BeginFoldoutGroupAttribute(string tabName)
        {
            if (tabName.IsNotNullOrWhiteSpace())
            {
                this.tabName = tabName;
            }
        }
        public BeginFoldoutGroupAttribute(string tabName, string toggleName)
        {
            if (tabName.IsNotNullOrWhiteSpace())
            {
                this.tabName = tabName;
            }

            if (toggleName.IsNotNullOrWhiteSpace())
            {
                this.toggleName = toggleName;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class EndFoldoutGroupAttribute : Attribute
    {
        public int iteration { get; private set; } = 1;

        public EndFoldoutGroupAttribute() { }
        public EndFoldoutGroupAttribute(int iteration)
        {
            if (iteration > 1)
            {
                this.iteration = iteration;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class EasyButtonAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class ShowIfAttribute : Attribute
    {
        readonly string condition = null;
        readonly string[] variableNames = null;

        public ShowIfAttribute(string condition)
        {
            char[] charArray = condition.ToCharArray();

            condition = "";

            for (int i = 0; i < charArray.Length; ++i)
            {
                if (!char.IsWhiteSpace(charArray[i]))
                {
                    condition += charArray[i];
                }
            }

            condition = condition.Replace("&&", "&");
            condition = condition.Replace("||", "|");

            this.condition = condition;

            condition = condition.Replace("(", "");
            condition = condition.Replace(")", "");
            condition = condition.Replace("!", "");

            variableNames = condition.Split('&', '|');

            Array.Sort(variableNames, (a, b) => b.Length - a.Length);
        }

        public bool CheckCondition(object container)
        {
            object[] values = new object[variableNames.Length];

            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = container.GetNestedInstance(variableNames[i]);
            }

            string result = condition;

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] is bool boolValue)
                {
                    result = result.Replace(variableNames[i], boolValue ? "+" : "-");
                }
                else
                    result = result.Replace(variableNames[i], values[i].Equals(null) ? "-" : "+");
            }

            while (result.Length > 1)
            {
                int _l = result.Length;

                result = result.Replace("!+", "-");
                result = result.Replace("!-", "+");

                result = result.Replace("+&+", "+");
                result = result.Replace("-&+", "-");
                result = result.Replace("+&-", "-");
                result = result.Replace("-&-", "-");

                result = result.Replace("+|+", "+");
                result = result.Replace("-|+", "+");
                result = result.Replace("+|-", "+");
                result = result.Replace("-|-", "-");

                result = result.Replace("(+)", "+");
                result = result.Replace("(-)", "-");

                if (result.Length == _l)
                {
                    throw new Exception("Invalid condition!");
                }
            }

            return result[0] == '+';
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class InspectorNameAttribute : Attribute
    {
        public string displayName { get; private set; } = "";

        public InspectorNameAttribute(string displayName)
        {
            if (displayName.IsNotNullOrWhiteSpace())
            {
                this.displayName = displayName;
            }
        }
    }
}
