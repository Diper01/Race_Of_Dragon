using UnityEngine;
using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static partial class TweenShake
    {
        public static class ShakeTools
        {
            public static Vector3 GetShakePositionByDimension(Vector3 startPosition, float radius, int dimension)
            {
                switch (dimension)
                {
                    case 2:
                        return GetShakePosition2D(startPosition, radius);

                    case 3:
                        return GetShakePosition3D(startPosition, radius);
                }

                throw new Exception("Invalid dimension!");
            }
            public static Vector3 GetNextShakePositionByDimension(Vector3 startPosition, float radius, float spreadAngle, Vector3 direction, int dimension)
            {
                switch (dimension)
                {
                    case 2:
                        return GetNextShakePosition2D(startPosition, radius, spreadAngle, direction);

                    case 3:
                        return GetNextShakePosition3D(startPosition, radius, spreadAngle, direction);
                }

                throw new Exception("Invalid dimension!");
            }

            static Vector3 GetShakePosition2D(Vector3 startPosition, float radius)
            {
                return startPosition.Add(Randomize.vectorOnCircle * radius);
            }
            static Vector3 GetShakePosition3D(Vector3 startPosition, float radius)
            {
                return startPosition + Randomize.vectorOnSphere * radius;
            }

            static Vector3 GetNextShakePosition2D(Vector3 startPosition, float radius, float spreadAngle, Vector2 direction)
            {
                return startPosition.Add(direction.Spread(spreadAngle) * radius);
            }
            static Vector3 GetNextShakePosition3D(Vector3 startPosition, float radius, float spreadAngle, Vector3 direction)
            {
                return startPosition + direction.Spread(spreadAngle) * radius;
            }
        }
    }
}
