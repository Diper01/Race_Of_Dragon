using UnityEngine;

namespace MaxTools.Extensions
{
    public static class EX_Vector
    {
        public static bool IsOne(this Vector2 me)
        {
            return me.x == 1.0f
                && me.y == 1.0f;
        }
        public static bool IsOne(this Vector3 me)
        {
            return me.x == 1.0f
                && me.y == 1.0f
                && me.z == 1.0f;
        }

        public static bool IsZero(this Vector2 me)
        {
            return me.x == 0.0f
                && me.y == 0.0f;
        }
        public static bool IsZero(this Vector3 me)
        {
            return me.x == 0.0f
                && me.y == 0.0f
                && me.z == 0.0f;
        }

        public static Vector2 Add(this Vector2 me, Vector3 v)
        {
            me.x += v.x;
            me.y += v.y;
            return me;
        }
        public static Vector3 Add(this Vector3 me, Vector2 v)
        {
            me.x += v.x;
            me.y += v.y;
            return me;
        }

        public static Vector2 Sub(this Vector2 me, Vector3 v)
        {
            me.x -= v.x;
            me.y -= v.y;
            return me;
        }
        public static Vector3 Sub(this Vector3 me, Vector2 v)
        {
            me.x -= v.x;
            me.y -= v.y;
            return me;
        }

        public static Vector3 Mul(this Vector3 me, Vector3 v)
        {
            me.x *= v.x;
            me.y *= v.y;
            me.z *= v.z;
            return me;
        }
        public static Vector3 Div(this Vector3 me, Vector3 v)
        {
            me.x /= v.x;
            me.y /= v.y;
            me.z /= v.z;
            return me;
        }

        public static Vector2 Rotate(this Vector2 me, float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * me;
        }
        public static Vector3 Rotate(this Vector3 me, float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis) * me;
        }

        public static Vector2 Spread(this Vector2 me, float spreadAngle)
        {
            return me.Rotate(Randomize.Diapason(spreadAngle));
        }
        public static Vector3 Spread(this Vector3 me, float spreadAngle)
        {
            return me.Rotate(Randomize.Diapason(spreadAngle), Vector3.Cross(me, Randomize.vectorOnSphere));
        }

        public static Vector3[] To3D(this Vector2[] me)
        {
            var vectors = new Vector3[me.Length];

            for (int i = 0; i < vectors.Length; ++i)
            {
                vectors[i].x = me[i].x;
                vectors[i].y = me[i].y;
            }

            return vectors;
        }
        public static Vector2[] To2D(this Vector3[] me)
        {
            var vectors = new Vector2[me.Length];

            for (int i = 0; i < vectors.Length; ++i)
            {
                vectors[i].x = me[i].x;
                vectors[i].y = me[i].y;
            }

            return vectors;
        }

        public static Vector2Int ToVector2Int(this Vector2 me)
        {
            return new Vector2Int((int)me.x, (int)me.y);
        }
        public static Vector3Int ToVector3Int(this Vector2 me)
        {
            return new Vector3Int((int)me.x, (int)me.y, 0);
        }

        public static Vector2Int ToVector2Int(this Vector3 me)
        {
            return new Vector2Int((int)me.x, (int)me.y);
        }
        public static Vector3Int ToVector3Int(this Vector3 me)
        {
            return new Vector3Int((int)me.x, (int)me.y, (int)me.z);
        }

        public static Vector2Int ToVector2Int(this Vector3Int me)
        {
            return new Vector2Int(me.x, me.y);
        }
        public static Vector3Int ToVector3Int(this Vector2Int me)
        {
            return new Vector3Int(me.x, me.y, 0);
        }

        public static Vector2 Clamp(this Vector2 me, float min, float max)
        {
            me.x = Tools.Clamp(me.x, min, max);
            me.y = Tools.Clamp(me.y, min, max);
            return me;
        }
        public static Vector2 Clamp01(this Vector2 me)
        {
            me.x = Tools.Clamp01(me.x);
            me.y = Tools.Clamp01(me.y);
            return me;
        }
        public static Vector2 ClampMin(this Vector2 me, float min)
        {
            me.x = Tools.ClampMin(me.x, min);
            me.y = Tools.ClampMin(me.y, min);
            return me;
        }
        public static Vector2 ClampMax(this Vector2 me, float max)
        {
            me.x = Tools.ClampMax(me.x, max);
            me.y = Tools.ClampMax(me.y, max);
            return me;
        }

        public static Vector3 Clamp(this Vector3 me, float min, float max)
        {
            me.x = Tools.Clamp(me.x, min, max);
            me.y = Tools.Clamp(me.y, min, max);
            me.z = Tools.Clamp(me.z, min, max);
            return me;
        }
        public static Vector3 Clamp01(this Vector3 me)
        {
            me.x = Tools.Clamp01(me.x);
            me.y = Tools.Clamp01(me.y);
            me.z = Tools.Clamp01(me.z);
            return me;
        }
        public static Vector3 ClampMin(this Vector3 me, float min)
        {
            me.x = Tools.ClampMin(me.x, min);
            me.y = Tools.ClampMin(me.y, min);
            me.z = Tools.ClampMin(me.z, min);
            return me;
        }
        public static Vector3 ClampMax(this Vector3 me, float max)
        {
            me.x = Tools.ClampMax(me.x, max);
            me.y = Tools.ClampMax(me.y, max);
            me.z = Tools.ClampMax(me.z, max);
            return me;
        }

        public static Vector2Int Clamp(this Vector2Int me, int min, int max)
        {
            me.x = Tools.Clamp(me.x, min, max);
            me.y = Tools.Clamp(me.y, min, max);
            return me;
        }
        public static Vector2Int ClampMin(this Vector2Int me, int min)
        {
            me.x = Tools.ClampMin(me.x, min);
            me.y = Tools.ClampMin(me.y, min);
            return me;
        }
        public static Vector2Int ClampMax(this Vector2Int me, int max)
        {
            me.x = Tools.ClampMax(me.x, max);
            me.y = Tools.ClampMax(me.y, max);
            return me;
        }

        public static Vector3Int Clamp(this Vector3Int me, int min, int max)
        {
            me.x = Tools.Clamp(me.x, min, max);
            me.y = Tools.Clamp(me.y, min, max);
            me.z = Tools.Clamp(me.z, min, max);
            return me;
        }
        public static Vector3Int ClampMin(this Vector3Int me, int min)
        {
            me.x = Tools.ClampMin(me.x, min);
            me.y = Tools.ClampMin(me.y, min);
            me.z = Tools.ClampMin(me.z, min);
            return me;
        }
        public static Vector3Int ClampMax(this Vector3Int me, int max)
        {
            me.x = Tools.ClampMax(me.x, max);
            me.y = Tools.ClampMax(me.y, max);
            me.z = Tools.ClampMax(me.z, max);
            return me;
        }

        public static float Project(this Vector2 me, Vector2 v)
        {
            return (me.x * v.x + me.y * v.y) / v.magnitude;
        }
        public static float Project(this Vector3 me, Vector3 v)
        {
            return (me.x * v.x + me.y * v.y + me.z * v.z) / v.magnitude;
        }

        public static bool Greater(this Vector2 a, Vector2 b)
        {
            return a.x > b.x
                && a.y > b.y;
        }
        public static bool Less(this Vector2 a, Vector2 b)
        {
            return a.x < b.x
                && a.y < b.y;
        }
        public static bool GreaterOrEqual(this Vector2 a, Vector2 b)
        {
            return a.x >= b.x
                && a.y >= b.y;
        }
        public static bool LessOrEqual(this Vector2 a, Vector2 b)
        {
            return a.x <= b.x
                && a.y <= b.y;
        }

        public static bool GreaterAny(this Vector2 a, Vector2 b)
        {
            return a.x > b.x
                || a.y > b.y;
        }
        public static bool LessAny(this Vector2 a, Vector2 b)
        {
            return a.x < b.x
                || a.y < b.y;
        }
        public static bool GreaterOrEqualAny(this Vector2 a, Vector2 b)
        {
            return a.x >= b.x
                || a.y >= b.y;
        }
        public static bool LessOrEqualAny(this Vector2 a, Vector2 b)
        {
            return a.x <= b.x
                || a.y <= b.y;
        }

        public static bool Greater(this Vector3 a, Vector3 b)
        {
            return a.x > b.x
                && a.y > b.y
                && a.z > b.z;
        }
        public static bool Less(this Vector3 a, Vector3 b)
        {
            return a.x < b.x
                && a.y < b.y
                && a.z < b.z;
        }
        public static bool GreaterOrEqual(this Vector3 a, Vector3 b)
        {
            return a.x >= b.x
                && a.y >= b.y
                && a.z >= b.z;
        }
        public static bool LessOrEqual(this Vector3 a, Vector3 b)
        {
            return a.x <= b.x
                && a.y <= b.y
                && a.z <= b.z;
        }

        public static bool GreaterAny(this Vector3 a, Vector3 b)
        {
            return a.x > b.x
                || a.y > b.y
                || a.z > b.z;
        }
        public static bool LessAny(this Vector3 a, Vector3 b)
        {
            return a.x < b.x
                || a.y < b.y
                || a.z < b.z;
        }
        public static bool GreaterOrEqualAny(this Vector3 a, Vector3 b)
        {
            return a.x >= b.x
                || a.y >= b.y
                || a.z >= b.z;
        }
        public static bool LessOrEqualAny(this Vector3 a, Vector3 b)
        {
            return a.x <= b.x
                || a.y <= b.y
                || a.z <= b.z;
        }

        public static bool Greater(this Vector2Int a, Vector2Int b)
        {
            return a.x > b.x
                && a.y > b.y;
        }
        public static bool Less(this Vector2Int a, Vector2Int b)
        {
            return a.x < b.x
                && a.y < b.y;
        }
        public static bool GreaterOrEqual(this Vector2Int a, Vector2Int b)
        {
            return a.x >= b.x
                && a.y >= b.y;
        }
        public static bool LessOrEqual(this Vector2Int a, Vector2Int b)
        {
            return a.x <= b.x
                && a.y <= b.y;
        }

        public static bool GreaterAny(this Vector2Int a, Vector2Int b)
        {
            return a.x > b.x
                || a.y > b.y;
        }
        public static bool LessAny(this Vector2Int a, Vector2Int b)
        {
            return a.x < b.x
                || a.y < b.y;
        }
        public static bool GreaterOrEqualAny(this Vector2Int a, Vector2Int b)
        {
            return a.x >= b.x
                || a.y >= b.y;
        }
        public static bool LessOrEqualAny(this Vector2Int a, Vector2Int b)
        {
            return a.x <= b.x
                || a.y <= b.y;
        }

        public static bool Greater(this Vector3Int a, Vector3Int b)
        {
            return a.x > b.x
                && a.y > b.y
                && a.z > b.z;
        }
        public static bool Less(this Vector3Int a, Vector3Int b)
        {
            return a.x < b.x
                && a.y < b.y
                && a.z < b.z;
        }
        public static bool GreaterOrEqual(this Vector3Int a, Vector3Int b)
        {
            return a.x >= b.x
                && a.y >= b.y
                && a.z >= b.z;
        }
        public static bool LessOrEqual(this Vector3Int a, Vector3Int b)
        {
            return a.x <= b.x
                && a.y <= b.y
                && a.z <= b.z;
        }

        public static bool GreaterAny(this Vector3Int a, Vector3Int b)
        {
            return a.x > b.x
                || a.y > b.y
                || a.z > b.z;
        }
        public static bool LessAny(this Vector3Int a, Vector3Int b)
        {
            return a.x < b.x
                || a.y < b.y
                || a.z < b.z;
        }
        public static bool GreaterOrEqualAny(this Vector3Int a, Vector3Int b)
        {
            return a.x >= b.x
                || a.y >= b.y
                || a.z >= b.z;
        }
        public static bool LessOrEqualAny(this Vector3Int a, Vector3Int b)
        {
            return a.x <= b.x
                || a.y <= b.y
                || a.z <= b.z;
        }
    }
}
