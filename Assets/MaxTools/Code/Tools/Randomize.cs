using UnityEngine;

namespace MaxTools
{
    public static class Randomize
    {
        public static void SetSeed(int seed)
        {
            Random.InitState(seed);
        }

        public static Random.State state
        {
            get => Random.state;
            set => Random.state = value;
        }

        public static int Range(int min, int max)
        {
            return Random.Range(min, max);
        }
        public static char Range(char min, char max)
        {
            return (char)Random.Range(min, max + 1);
        }
        public static float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
        public static Color Range(Color min, Color max)
        {
            min.r = Random.Range(min.r, max.r);
            min.g = Random.Range(min.g, max.g);
            min.b = Random.Range(min.b, max.b);
            min.a = Random.Range(min.a, max.a);
            return min;
        }
        public static Vector2 Range(Vector2 min, Vector2 max)
        {
            min.x = Random.Range(min.x, max.x);
            min.y = Random.Range(min.y, max.y);
            return min;
        }
        public static Vector3 Range(Vector3 min, Vector3 max)
        {
            min.x = Random.Range(min.x, max.x);
            min.y = Random.Range(min.y, max.y);
            min.z = Random.Range(min.z, max.z);
            return min;
        }
        public static Vector2Int Range(Vector2Int min, Vector2Int max)
        {
            min.x = Random.Range(min.x, max.x);
            min.y = Random.Range(min.y, max.y);
            return min;
        }
        public static Vector3Int Range(Vector3Int min, Vector3Int max)
        {
            min.x = Random.Range(min.x, max.x);
            min.y = Random.Range(min.y, max.y);
            min.z = Random.Range(min.z, max.z);
            return min;
        }
        public static Quaternion Range(Quaternion min, Quaternion max)
        {
            return Quaternion.Lerp(min, max, value01);
        }

        public static int Diapason(int diapason)
        {
            return Random.Range(-diapason, diapason);
        }
        public static float Diapason(float diapason)
        {
            return Random.Range(-diapason, diapason);
        }
        public static Vector2 Diapason(Vector2 diapason)
        {
            return Range(-diapason, diapason);
        }
        public static Vector3 Diapason(Vector3 diapason)
        {
            return Range(-diapason, diapason);
        }
        public static Vector2Int Diapason(Vector2Int diapason)
        {
            var negative = new Vector2Int(-diapason.x, -diapason.y);

            return Range(negative, diapason);
        }
        public static Vector3Int Diapason(Vector3Int diapason)
        {
            var negative = new Vector3Int(-diapason.x, -diapason.y, -diapason.z);

            return Range(negative, diapason);
        }

        public static bool Chance(int chance)
        {
            return chance / 100.0f >= value01;
        }
        public static bool Chance(float chance)
        {
            return chance >= value01;
        }

        public static int signed
        {
            get
            {
                return boolean ? -1 : 1;
            }
        }
        public static bool boolean
        {
            get
            {
                return Random.value > 0.5f;
            }
        }
        public static float value01
        {
            get
            {
                return Random.value;
            }
        }
        public static float diapason1
        {
            get
            {
                return Diapason(1.0f);
            }
        }
        public static Color color
        {
            get
            {
                return new Color(Random.value, Random.value, Random.value, 1.0f);
            }
        }
        public static Vector2 vectorOnCircle
        {
            get
            {
                var angle = Random.Range(0.0f, Tools.PI2);

                return new Vector2(Tools.Cos(angle), Tools.Sin(angle));
            }
        }
        public static Vector3 vectorOnSphere
        {
            get
            {
                return Random.onUnitSphere;
            }
        }
        public static Vector2 vectorInsideCircle
        {
            get
            {
                return Random.insideUnitCircle;
            }
        }
        public static Vector3 vectorInsideSphere
        {
            get
            {
                return Random.insideUnitSphere;
            }
        }
        public static Quaternion quaternionX
        {
            get
            {
                var angle = Random.Range(0.0f, Tools.PI);

                return new Quaternion(Tools.Sin(angle), 0.0f, 0.0f, Tools.Cos(angle));
            }
        }
        public static Quaternion quaternionY
        {
            get
            {
                var angle = Random.Range(0.0f, Tools.PI);

                return new Quaternion(0.0f, Tools.Sin(angle), 0.0f, Tools.Cos(angle));
            }
        }
        public static Quaternion quaternionZ
        {
            get
            {
                var angle = Random.Range(0.0f, Tools.PI);

                return new Quaternion(0.0f, 0.0f, Tools.Sin(angle), Tools.Cos(angle));
            }
        }
        public static Quaternion quaternionXYZ
        {
            get
            {
                return Random.rotation;
            }
        }
        public static Quaternion quaternionXYZ_Uniform
        {
            get
            {
                return Random.rotationUniform;
            }
        }
    }
}
