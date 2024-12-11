using System;
using System.Collections;
using System.Collections.Generic;

namespace MaxTools.Extensions
{
    public static class EX_Array
    {
        public static T GetRandomItem<T>(this T[] me)
        {
            return me[Randomize.Range(0, me.Length)];
        }
        public static T GetRandomItem<T>(this List<T> me)
        {
            return me[Randomize.Range(0, me.Count)];
        }
        public static object GetRandomItem(this Array me)
        {
            int[] indices = new int[me.Rank];

            for (int dimension = 0; dimension < me.Rank; ++dimension)
            {
                indices[dimension] = Randomize.Range(0, me.GetLength(dimension));
            }

            return me.GetValue(indices);
        }
        public static object GetRandomItem(this ArrayList me)
        {
            return me[Randomize.Range(0, me.Count)];
        }

        public static T GetLastItem<T>(this T[] me)
        {
            return me[me.Length - 1];
        }
        public static T GetLastItem<T>(this List<T> me)
        {
            return me[me.Count - 1];
        }
        public static object GetLastItem(this Array me)
        {
            return me.GetValue(me.GetUpperBounds());
        }
        public static object GetLastItem(this ArrayList me)
        {
            return me[me.Count - 1];
        }

        public static void Swap<T>(this T[] me, int i, int j)
        {
            var k = me[i];
            me[i] = me[j];
            me[j] = k;
        }
        public static void Swap<T>(this List<T> me, int i, int j)
        {
            var k = me[i];
            me[i] = me[j];
            me[j] = k;
        }
        public static void Swap(this Array me, int[] indices_i, int[] indices_j)
        {
            var value_i = me.GetValue(indices_i);
            var value_j = me.GetValue(indices_j);

            me.SetValue(value_i, indices_j);
            me.SetValue(value_j, indices_i);
        }
        public static void Swap(this Array me, params int[] indices)
        {
            var l = indices.Length / 2;
            var indices_i = new ArraySegment<int>(indices, 0, l);
            var indices_j = new ArraySegment<int>(indices, l, l);

            me.Swap(indices_i.Array, indices_j.Array);
        }
        public static void Swap(this ArrayList me, int i, int j)
        {
            var k = me[i];
            me[i] = me[j];
            me[j] = k;
        }

        public static void Shuffle<T>(this T[] me)
        {
            for (int i = 0; i < me.Length - 1; ++i)
            {
                me.Swap(i, Randomize.Range(i + 1, me.Length));
            }
        }
        public static void Shuffle<T>(this List<T> me)
        {
            for (int i = 0; i < me.Count - 1; ++i)
            {
                me.Swap(i, Randomize.Range(i + 1, me.Count));
            }
        }
        public static void Shuffle(this Array me)
        {
            for (int i = 0; i < me.Length - 1; ++i)
            {
                var indices_i = me.GetIndices(i);
                var indices_j = me.GetIndices(Randomize.Range(i + 1, me.Length));

                me.Swap(indices_i, indices_j);
            }
        }
        public static void Shuffle(this ArrayList me)
        {
            for (int i = 0; i < me.Count - 1; ++i)
            {
                me.Swap(i, Randomize.Range(i + 1, me.Count));
            }
        }

        public static Array ChangeType(this Array me, Type newType)
        {
            Array newArray = Array.CreateInstance(newType, me.GetLengths());

            me.CopyTo(newArray);

            return newArray;
        }
        public static Array ChangeType<T>(this Array me)
        {
            return me.ChangeType(typeof(T));
        }
        public static Array ChangeRank(this Array me, params int[] lengths)
        {
            if (lengths == null || lengths.Length == 0)
            {
                throw new ArgumentException("Invalid lengths!");
            }

            if (Tools.Mul(lengths) != me.Length)
            {
                throw new ArgumentException("Invalid lengths!");
            }

            var elementType = me.GetType().GetElementType();

            var newArray = Array.CreateInstance(elementType, lengths);

            me.CopyTo(newArray);

            return newArray;
        }

        public static void Initialize(this Array me, object value)
        {
            for (int i = 0; i < me.Length; ++i)
            {
                me.SetValueUniversal(value, i);
            }
        }
        public static bool SequenceEqual(this Array me, Array other)
        {
            if (me.Length != other.Length)
            {
                return false;
            }

            if (me.Length == 0 && other.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < me.Length; ++i)
            {
                if (!me.GetValueUniversal(i).Equals(other.GetValueUniversal(i)))
                {
                    return false;
                }
            }

            return true;
        }
        public static void CopyTo(this Array me, Array receiver)
        {
            if (me.Length != receiver.Length)
            {
                throw new ArgumentException("Invalid lengths!");
            }

            if (me.Length == 0 && receiver.Length == 0)
            {
                return;
            }

            for (int i = 0; i < me.Length; ++i)
            {
                receiver.SetValueUniversal(me.GetValueUniversal(i), i);
            }
        }

        public static int[] GetIndices(this Array me, int index)
        {
            if (index < 0 || index >= me.Length)
            {
                throw new IndexOutOfRangeException("Invalid index!");
            }

            int[] indices = new int[me.Rank];

            if (index == 0)
            {
                return indices;
            }

            if (index == me.Length - 1)
            {
                return me.GetUpperBounds();
            }

            if (me.Rank == 1)
            {
                indices[0] = index;

                return indices;
            }

            for (int i = 0; i < indices.Length; ++i)
            {
                int length1 = 1;
                int length2 = 1;

                for (int d = me.Rank - 1; d >= 0; --d)
                {
                    length2 = length1;
                    length1 = length1 * me.GetLength(d);

                    if (index < length1)
                    {
                        int j = index / length2;
                        indices[d] = j;
                        index -= j * length2;

                        if (index == 0)
                        {
                            return indices;
                        }
                        else
                            break;
                    }
                }
            }

            throw new Exception("Bad operation!");
        }
        public static int[] GetUpperBounds(this Array me)
        {
            int[] bounds = new int[me.Rank];

            for (int dimension = 0; dimension < me.Rank; ++dimension)
            {
                bounds[dimension] = me.GetUpperBound(dimension);
            }

            return bounds;
        }
        public static int[] GetLengths(this Array me)
        {
            int[] lengths = new int[me.Rank];

            for (int dimension = 0; dimension < me.Rank; ++dimension)
            {
                lengths[dimension] = me.GetLength(dimension);
            }

            return lengths;
        }

        public static object GetValueUniversal(this Array me, int i)
        {
            return me.GetValue(me.GetIndices(i));
        }
        public static void SetValueUniversal(this Array me, object value, int i)
        {
            me.SetValue(value, me.GetIndices(i));
        }

        public static object MakeList(this Array me)
        {
            var elementType = me.GetType().GetElementType();

            var listType = typeof(List<>).MakeGenericType(elementType);

            if (me.Rank == 1)
            {
                return Activator.CreateInstance(listType, me);
            }
            else
                return Activator.CreateInstance(listType, me.ChangeRank(me.Length));
        }
        public static List<T> MakeList<T>(this Array me)
        {
            if (me.Rank == 1)
            {
                return new List<T>((T[])me);
            }
            else
                return new List<T>(me.To1D<T>());
        }

        public static T[] To1D<T>(this Array me)
        {
            return (T[])me.ChangeRank(me.Length);
        }
        public static T[,] To2D<T>(this Array me, int xCount, int yCount)
        {
            return (T[,])me.ChangeRank(xCount, yCount);
        }
        public static T[,,] To3D<T>(this Array me, int xCount, int yCount, int zCount)
        {
            return (T[,,])me.ChangeRank(xCount, yCount, zCount);
        }
    }
}
