using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_String
    {
        public static int CompareNumber(this string strA, string strB)
        {
            if (strA.IsNullOrEmpty() ||
                strB.IsNullOrEmpty() ||
                strA.IsNullOrWhiteSpace() ||
                strB.IsNullOrWhiteSpace())
            {
                return strA.CompareTo(strB);
            }

            int min_l = strA.Length < strB.Length ? strA.Length : strB.Length;

            for (int i = 0; i < min_l; ++i)
            {
                if (char.IsDigit(strA[i]) && char.IsDigit(strB[i]))
                {
                    string num_a = "";
                    string num_b = "";

                    for (int j = i; j < strA.Length; ++j)
                    {
                        if (char.IsDigit(strA[j]))
                        {
                            num_a += strA[j];
                        }
                        else
                            break;
                    }

                    for (int j = i; j < strB.Length; ++j)
                    {
                        if (char.IsDigit(strB[j]))
                        {
                            num_b += strB[j];
                        }
                        else
                            break;
                    }

                    int compare = int.Parse(num_a).CompareTo(int.Parse(num_b));

                    if (compare != 0)
                    {
                        return compare;
                    }
                    else
                        i += num_a.Length - 1;
                }
                else
                {
                    int compare = ((int)strA[i]).CompareTo(strB[i]);

                    if (compare != 0)
                    {
                        return compare;
                    }
                }
            }

            if (strA.Length < strB.Length) return -1;
            else if (strA.Length > strB.Length) return 1;
            else return 0;
        }

        public static bool IsNullOrEmpty(this string me)
        {
            return string.IsNullOrEmpty(me);
        }
        public static bool IsNullOrWhiteSpace(this string me)
        {
            return string.IsNullOrWhiteSpace(me);
        }

        public static bool IsNotNullOrEmpty(this string me)
        {
            return !string.IsNullOrEmpty(me);
        }
        public static bool IsNotNullOrWhiteSpace(this string me)
        {
            return !string.IsNullOrWhiteSpace(me);
        }

        public static string Colored(this string me, Color32 color)
        {
            return $"<color={color.ToHex()}>{me}</color>";
        }

        public static string ReplaceWhile(this string me, string oldValue, string newValue)
        {
            if (newValue.Contains(oldValue))
            {
                throw new ArgumentException("Values are similar!");
            }

            while (me.Contains(oldValue))
            {
                me = me.Replace(oldValue, newValue);
            }

            return me;
        }

        public static string Reverse(this string me)
        {
            char[] charArray = me.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }
    }
}
