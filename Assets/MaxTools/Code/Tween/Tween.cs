using UnityEngine;
using UnityEngine.UI;
using System;

namespace MaxTools
{
    using MaxTools.Extensions;

    [Flags]
    public enum TweenType
    {
        Color = 1,
        Alpha = 2,
        Scale2D = 4,
        Scale3D = 8,
        Position2D = 16,
        Position3D = 32,
        Rotation2D = 64,
        Rotation3D = 128,
        Shake = 256
    }

    public static class Tween
    {
        #region Similar
        static float GetSimilarColor(LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return 0.01f;

                case LerpType.Towards:
                    return 0.0f;
            }

            throw new Exception($"Invalid {lerpType}!");
        }
        static float GetSimilarAlpha(LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return 0.01f;

                case LerpType.Towards:
                    return 0.0f;
            }

            throw new Exception($"Invalid {lerpType}!");
        }
        static float GetSimilarScale(LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return 0.01f;

                case LerpType.Towards:
                    return 0.0f;
            }

            throw new Exception($"Invalid {lerpType}!");
        }
        static float GetSimilarPosition(LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return 0.01f;

                case LerpType.Towards:
                    return 0.0f;
            }

            throw new Exception($"Invalid {lerpType}!");
        }
        static float GetSimilarRotation(LerpType lerpType)
        {
            switch (lerpType)
            {
                case LerpType.Simple:
                    return 1.0f;

                case LerpType.Towards:
                    return 0.0f;
            }

            throw new Exception($"Invalid {lerpType}!");
        }
        #endregion

        #region Color
        public static bool Color(ColorWrapper me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, color, intensity, lerpType);

            return (me.color - color).GetSqrMagnitude() <= GetSimilarColor(lerpType);
        }
        public static bool Color(Graphic me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, color, intensity, lerpType);

            return (me.color - color).GetSqrMagnitude() <= GetSimilarColor(lerpType);
        }
        public static bool Color(SpriteRenderer me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, color, intensity, lerpType);

            return (me.color - color).GetSqrMagnitude() <= GetSimilarColor(lerpType);
        }
        public static bool Color(Material me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, color, intensity, lerpType);

            return (me.color - color).GetSqrMagnitude() <= GetSimilarColor(lerpType);
        }
        #endregion

        #region Alpha
        public static bool Alpha(ColorWrapper me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, me.color.GetWithAlpha01(alpha), intensity, lerpType);

            return Tools.Abs(me.color.a - alpha) <= GetSimilarAlpha(lerpType);
        }
        public static bool Alpha(Graphic me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, me.color.GetWithAlpha01(alpha), intensity, lerpType);

            return Tools.Abs(me.color.a - alpha) <= GetSimilarAlpha(lerpType);
        }
        public static bool Alpha(SpriteRenderer me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, me.color.GetWithAlpha01(alpha), intensity, lerpType);

            return Tools.Abs(me.color.a - alpha) <= GetSimilarAlpha(lerpType);
        }
        public static bool Alpha(Material me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.color = Tools.LerpChoose(me.color, me.color.GetWithAlpha01(alpha), intensity, lerpType);

            return Tools.Abs(me.color.a - alpha) <= GetSimilarAlpha(lerpType);
        }
        public static bool Alpha(CanvasGroup me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.alpha = Tools.LerpChoose(me.alpha, alpha, intensity, lerpType);

            return Tools.Abs(me.alpha - alpha) <= GetSimilarAlpha(lerpType);
        }
        public static bool Alpha(AlphaGroup me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.alpha = Tools.LerpChoose(me.alpha, alpha, intensity, lerpType);

            return Tools.Abs(me.alpha - alpha) <= GetSimilarAlpha(lerpType);
        }
        #endregion

        #region Scale
        public static bool Scale2D(Transform me, Vector3 scale, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            scale.z = me.localScale.z;

            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.localScale = Tools.LerpChoose(me.localScale, scale, intensity, lerpType);

            return Vector2.SqrMagnitude(me.localScale - scale) <= GetSimilarScale(lerpType);
        }
        public static bool Scale2D(Transform me, float size, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            var scale = new Vector3(size, size, me.localScale.z);

            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.localScale = Tools.LerpChoose(me.localScale, scale, intensity, lerpType);

            return Vector2.SqrMagnitude(me.localScale - scale) <= GetSimilarScale(lerpType);
        }

        public static bool Scale3D(Transform me, Vector3 scale, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.localScale = Tools.LerpChoose(me.localScale, scale, intensity, lerpType);

            return Vector3.SqrMagnitude(me.localScale - scale) <= GetSimilarScale(lerpType);
        }
        public static bool Scale3D(Transform me, float size, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            var scale = new Vector3(size, size, size);

            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            me.localScale = Tools.LerpChoose(me.localScale, scale, intensity, lerpType);

            return Vector3.SqrMagnitude(me.localScale - scale) <= GetSimilarScale(lerpType);
        }
        #endregion

        #region Position
        public static bool Position2D(Transform me, Vector3 position, float intensity)
        {
            return Position2D(me, position, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime);
        }
        public static bool Position2D(Transform me, Vector3 position, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Position2D(me, position, intensity, lerpType, space, TimeMode.ScaledTime);
        }
        public static bool Position2D(Transform me, Vector3 position, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            var newPosition = Tools.LerpChoose(me.GetPosition3D(space), position, intensity, lerpType);

            me.SetPosition2D(newPosition, space);

            return Vector2.SqrMagnitude(newPosition - position) <= GetSimilarPosition(lerpType);
        }

        public static bool Position3D(Transform me, Vector3 position, float intensity)
        {
            return Position3D(me, position, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime);
        }
        public static bool Position3D(Transform me, Vector3 position, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Position3D(me, position, intensity, lerpType, space, TimeMode.ScaledTime);
        }
        public static bool Position3D(Transform me, Vector3 position, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            var newPosition = Tools.LerpChoose(me.GetPosition3D(space), position, intensity, lerpType);

            me.SetPosition3D(newPosition, space);

            return Vector3.SqrMagnitude(newPosition - position) <= GetSimilarPosition(lerpType);
        }
        #endregion

        #region Rotation
        public static bool Rotation2D(Transform me, float angle, float intensity)
        {
            return Rotation2D(me, angle, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime);
        }
        public static bool Rotation2D(Transform me, float angle, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Rotation2D(me, angle, intensity, lerpType, space, TimeMode.ScaledTime);
        }
        public static bool Rotation2D(Transform me, float angle, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            var newAngle = Tools.LerpChooseAngle(me.GetAngles(space).z, angle, intensity, lerpType);

            me.SetRotation2D(newAngle, space);

            return Tools.Abs(Tools.DeltaAngle(newAngle, angle)) <= GetSimilarRotation(lerpType);
        }

        public static bool Rotation3D(Transform me, Quaternion rotation, float intensity)
        {
            return Rotation3D(me, rotation, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime);
        }
        public static bool Rotation3D(Transform me, Quaternion rotation, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Rotation3D(me, rotation, intensity, lerpType, space, TimeMode.ScaledTime);
        }
        public static bool Rotation3D(Transform me, Quaternion rotation, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime)
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    intensity *= Time.deltaTime;
                    break;

                case TimeMode.UnscaledTime:
                    intensity *= Time.unscaledDeltaTime;
                    break;
            }

            var newRotation = Tools.LerpChoose(me.GetRotation3D(space), rotation, intensity, lerpType);

            me.SetRotation3D(newRotation, space);

            return Quaternion.Angle(newRotation, rotation) <= GetSimilarRotation(lerpType);
        }

        public static bool Rotation3D(Transform me, Vector3 angles, float intensity)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime);
        }
        public static bool Rotation3D(Transform me, Vector3 angles, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, lerpType, space, TimeMode.ScaledTime);
        }
        public static bool Rotation3D(Transform me, Vector3 angles, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, lerpType, space, TimeMode.ScaledTime);
        }
        #endregion
    }
}
