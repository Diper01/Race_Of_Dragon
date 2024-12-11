using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static class TweenCoroutine
    {
        #region Color
        public static IEnumerator Color(ColorWrapper me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Color(me, color, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.color = color;

            finalAction?.Invoke();
        }
        public static IEnumerator Color(Graphic me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Color(me, color, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.color = color;

            finalAction?.Invoke();
        }
        public static IEnumerator Color(SpriteRenderer me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Color(me, color, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.color = color;

            finalAction?.Invoke();
        }
        public static IEnumerator Color(Material me, Color color, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Color(me, color, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.color = color;

            finalAction?.Invoke();
        }
        #endregion

        #region Alpha
        public static IEnumerator Alpha(ColorWrapper me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetAlpha01(alpha);

            finalAction?.Invoke();
        }
        public static IEnumerator Alpha(Graphic me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetAlpha01(alpha);

            finalAction?.Invoke();
        }
        public static IEnumerator Alpha(SpriteRenderer me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetAlpha01(alpha);

            finalAction?.Invoke();
        }
        public static IEnumerator Alpha(Material me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetAlpha01(alpha);

            finalAction?.Invoke();
        }
        public static IEnumerator Alpha(CanvasGroup me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.alpha = alpha;

            finalAction?.Invoke();
        }
        public static IEnumerator Alpha(AlphaGroup me, float alpha, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Alpha(me, alpha, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.alpha = alpha;

            finalAction?.Invoke();
        }
        #endregion

        #region Scale
        public static IEnumerator Scale2D(Transform me, Vector3 scale, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Scale2D(me, scale, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetScale2D(scale);

            finalAction?.Invoke();
        }
        public static IEnumerator Scale2D(Transform me, float size, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Scale2D(me, size, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetUniformSize2D(size);

            finalAction?.Invoke();
        }

        public static IEnumerator Scale3D(Transform me, Vector3 scale, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Scale3D(me, scale, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.localScale = scale;

            finalAction?.Invoke();
        }
        public static IEnumerator Scale3D(Transform me, float size, float intensity, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Scale3D(me, size, intensity, lerpType, timeMode))
            {
                yield return null;
            }

            me.SetUniformSize3D(size);

            finalAction?.Invoke();
        }
        #endregion

        #region Position
        public static IEnumerator Position2D(Transform me, Vector3 position, float intensity, Action finalAction = null)
        {
            return Position2D(me, position, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Position2D(Transform me, Vector3 position, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Position2D(me, position, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Position2D(Transform me, Vector3 position, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Position2D(me, position, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.SetPosition2D(position, space);

            finalAction?.Invoke();
        }

        public static IEnumerator Position2D(Transform me, Transform target, float intensity, Action finalAction = null)
        {
            return Position2D(me, target, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Position2D(Transform me, Transform target, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Position2D(me, target, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Position2D(Transform me, Transform target, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Position2D(me, target.position, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.SetPosition2D(target.position, space);

            finalAction?.Invoke();
        }

        public static IEnumerator Position3D(Transform me, Vector3 position, float intensity, Action finalAction = null)
        {
            return Position3D(me, position, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Position3D(Transform me, Vector3 position, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Position3D(me, position, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Position3D(Transform me, Vector3 position, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Position3D(me, position, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.SetPosition3D(position, space);

            finalAction?.Invoke();
        }

        public static IEnumerator Position3D(Transform me, Transform target, float intensity, Action finalAction = null)
        {
            return Position3D(me, target, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Position3D(Transform me, Transform target, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Position3D(me, target, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Position3D(Transform me, Transform target, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Position3D(me, target.position, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.SetPosition3D(target.position, space);

            finalAction?.Invoke();
        }
        #endregion

        #region Rotation
        public static IEnumerator Rotation2D(Transform me, float angle, float intensity, Action finalAction = null)
        {
            return Rotation2D(me, angle, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Rotation2D(Transform me, float angle, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Rotation2D(me, angle, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Rotation2D(Transform me, float angle, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Rotation2D(me, angle, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.SetRotation2D(angle, space);

            finalAction?.Invoke();
        }

        public static IEnumerator Rotation3D(Transform me, Quaternion rotation, float intensity, Action finalAction = null)
        {
            return Rotation3D(me, rotation, intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Rotation3D(Transform me, Quaternion rotation, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Rotation3D(me, rotation, intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Rotation3D(Transform me, Quaternion rotation, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            while (!Tween.Rotation3D(me, rotation, intensity, lerpType, space, timeMode))
            {
                yield return null;
            }

            me.rotation = rotation;

            finalAction?.Invoke();
        }

        public static IEnumerator Rotation3D(Transform me, Vector3 angles, float intensity, Action finalAction = null)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, LerpType.Simple, Space.World, TimeMode.ScaledTime, finalAction);
        }
        public static IEnumerator Rotation3D(Transform me, Vector3 angles, float intensity, Space space = Space.World, LerpType lerpType = LerpType.Simple, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, lerpType, space, timeMode, finalAction);
        }
        public static IEnumerator Rotation3D(Transform me, Vector3 angles, float intensity, LerpType lerpType = LerpType.Simple, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action finalAction = null)
        {
            return Rotation3D(me, Quaternion.Euler(angles), intensity, lerpType, space, timeMode, finalAction);
        }
        #endregion

        #region c_LookAt2DSmooth
        public static IEnumerator LookAt2DSmooth(Transform me, Vector3 target, float intensity, LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            while (!me.LookAt2DSmooth(target, intensity, lerpType, localLookDirection, timeMode))
            {
                yield return null;
            }
        }
        public static IEnumerator LookAt2DSmooth(Transform me, Transform target, float intensity, LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            while (!me.LookAt2DSmooth(target, intensity, lerpType, localLookDirection, timeMode))
            {
                yield return null;
            }
        }
        public static IEnumerator LookAt2DSmooth(Transform me, GameObject target, float intensity, LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            while (!me.LookAt2DSmooth(target, intensity, lerpType, localLookDirection, timeMode))
            {
                yield return null;
            }
        }
        public static IEnumerator LookAt2DSmooth(Transform me, Component target, float intensity, LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            while (!me.LookAt2DSmooth(target, intensity, lerpType, localLookDirection, timeMode))
            {
                yield return null;
            }
        }
        #endregion
    }
}
