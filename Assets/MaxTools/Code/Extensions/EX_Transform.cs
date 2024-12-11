using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_Transform
    {
        #region Child + Parent
        public static void DestroyChildren(this Transform me)
        {
            if (Application.isPlaying)
            {
                foreach (Transform child in me)
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }
            else
            {
                while (me.childCount > 0)
                {
                    foreach (Transform child in me)
                    {
                        UnityEngine.Object.DestroyImmediate(child.gameObject);
                    }
                }
            }
        }

        public static Transform FindNestedChild(this Transform me, string childName)
        {
            for (int i = 0; i < me.childCount; ++i)
            {
                var child = me.GetChild(i);

                if (child.name == childName)
                    return child;

                var nested = child.FindNestedChild(childName);

                if (nested != null)
                    return nested;
            }

            return null;
        }
        public static Transform FindAncestor(this Transform me, string ancestorName)
        {
            var parent = me.parent;

            while (parent != null)
            {
                if (parent.name == ancestorName)
                {
                    return parent;
                }

                parent = parent.parent;
            }

            return null;
        }

        public static void MoveChildren(this Transform me, Transform receiver)
        {
            while (me.childCount > 0)
            {
                me.GetChild(0).SetParent(receiver);
            }
        }
        public static void MoveChildren(this Transform me, Transform receiver, bool worldPositionStays)
        {
            while (me.childCount > 0)
            {
                me.GetChild(0).SetParent(receiver, worldPositionStays);
            }
        }

        public static T GetChild<T>(this Transform me, int i) where T : Component
        {
            return me.GetChild(i).GetComponent<T>();
        }
        public static Transform GetLastChild(this Transform me)
        {
            if (me.childCount > 0)
            {
                return me.GetChild(me.childCount - 1);
            }

            return null;
        }
        public static T GetLastChild<T>(this Transform me) where T : Component
        {
            if (me.childCount > 0)
            {
                return me.GetChild<T>(me.childCount - 1);
            }

            return null;
        }
        #endregion

        #region LookAt2D
        public static void LookAt2D(this Transform me, Vector3 target, Vector3? localLookDirection = null)
        {
            if (localLookDirection == null)
                localLookDirection = Vector3.up;

            var worldLookDirection = me.TransformDirection(localLookDirection.Value);
            var signedAngle = Vector2.SignedAngle(worldLookDirection, target - me.position);

            if (Tools.Abs(signedAngle) > Tools.Epsilon6)
            {
                var eulerAngles = me.eulerAngles;
                eulerAngles.z += signedAngle;
                me.eulerAngles = eulerAngles;
            }
        }
        public static void LookAt2D(this Transform me, Transform target, Vector3? localLookDirection = null)
        {
            me.LookAt2D(target.position, localLookDirection);
        }
        public static void LookAt2D(this Transform me, GameObject target, Vector3? localLookDirection = null)
        {
            me.LookAt2D(target.transform.position, localLookDirection);
        }
        public static void LookAt2D(this Transform me, Component target, Vector3? localLookDirection = null)
        {
            me.LookAt2D(target.transform.position, localLookDirection);
        }
        #endregion

        #region LookAt2DSmooth
        public static bool LookAt2DSmooth(this Transform me, Vector3 target, float intensity,
            LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            if (localLookDirection == null)
                localLookDirection = Vector3.up;

            var worldLookDirection = me.TransformDirection(localLookDirection.Value);
            var signedAngle = Vector2.SignedAngle(worldLookDirection, target - me.position);

            return Tween.Rotation2D(me, me.eulerAngles.z + signedAngle, intensity, lerpType, Space.World, timeMode);
        }

        public static bool LookAt2DSmooth(this Transform me, Transform target, float intensity,
            LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return me.LookAt2DSmooth(target.position, intensity, lerpType, localLookDirection, timeMode);
        }

        public static bool LookAt2DSmooth(this Transform me, GameObject target, float intensity,
            LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return me.LookAt2DSmooth(target.transform.position, intensity, lerpType, localLookDirection, timeMode);
        }

        public static bool LookAt2DSmooth(this Transform me, Component target, float intensity,
            LerpType lerpType = LerpType.Simple, Vector3? localLookDirection = null, TimeMode timeMode = TimeMode.ScaledTime)
        {
            return me.LookAt2DSmooth(target.transform.position, intensity, lerpType, localLookDirection, timeMode);
        }
        #endregion

        #region Scale
        public static void SetUniformSize2D(this Transform me, float value)
        {
            me.localScale = new Vector3(value, value, me.localScale.z);
        }
        public static void SetUniformSize3D(this Transform me, float value)
        {
            me.localScale = new Vector3(value, value, value);
        }
        public static void SetScale2D(this Transform me, Vector3 value)
        {
            me.localScale = new Vector3(value.x, value.y, me.localScale.z);
        }
        #endregion

        #region Position
        public static Vector3 GetPosition3D(this Transform me, Space space)
        {
            switch (space)
            {
                case Space.World:
                    return me.position;

                case Space.Self:
                    return me.localPosition;
            }

            throw new Exception("Invalid space!");
        }
        public static void SetPosition3D(this Transform me, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.World:
                    me.position = value;
                    return;

                case Space.Self:
                    me.localPosition = value;
                    return;
            }

            throw new Exception("Invalid space!");
        }
        public static void SetPosition2D(this Transform me, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.World:
                    value.z = me.position.z;
                    me.position = value;
                    return;

                case Space.Self:
                    value.z = me.localPosition.z;
                    me.localPosition = value;
                    return;
            }

            throw new Exception("Invalid space!");
        }
        #endregion

        #region Rotation
        public static Quaternion GetRotation3D(this Transform me, Space space)
        {
            switch (space)
            {
                case Space.World:
                    return me.rotation;

                case Space.Self:
                    return me.localRotation;
            }

            throw new Exception("Invalid space!");
        }
        public static void SetRotation3D(this Transform me, Quaternion value, Space space)
        {
            switch (space)
            {
                case Space.World:
                    me.rotation = value;
                    return;

                case Space.Self:
                    me.localRotation = value;
                    return;
            }

            throw new Exception("Invalid space!");
        }
        public static void SetRotation2D(this Transform me, float value, Space space)
        {
            switch (space)
            {
                case Space.World:
                    var eulerAngles = me.eulerAngles;
                    eulerAngles.z = value;
                    me.eulerAngles = eulerAngles;
                    return;

                case Space.Self:
                    var localEulerAngles = me.localEulerAngles;
                    localEulerAngles.z = value;
                    me.localEulerAngles = localEulerAngles;
                    return;
            }

            throw new Exception("Invalid space!");
        }
        #endregion

        #region Angles
        public static Vector3 GetAngles(this Transform me, Space space)
        {
            switch (space)
            {
                case Space.World:
                    return me.eulerAngles;

                case Space.Self:
                    return me.localEulerAngles;
            }

            throw new Exception("Invalid space!");
        }
        public static void SetAngles(this Transform me, Vector3 value, Space space)
        {
            switch (space)
            {
                case Space.World:
                    me.eulerAngles = value;
                    return;

                case Space.Self:
                    me.localEulerAngles = value;
                    return;
            }

            throw new Exception("Invalid space!");
        }
        #endregion

        #region RectTransform
        public static void SetRectSize(this RectTransform me, Vector2 size)
        {
            me.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            me.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
        public static void SetRectSize(this RectTransform me, float width, float height)
        {
            me.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            me.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public static Vector3 GetPositionWithPivot(this RectTransform me, Vector2 pivot)
        {
            return me.position.Add(me.rotation * ((pivot - me.pivot) * me.rect.size * me.lossyScale));
        }
        public static void SetPositionWithPivot(this RectTransform me, Vector2 pivot, Vector3 position)
        {
            me.position = position.Sub(me.rotation * ((pivot - me.pivot) * me.rect.size * me.lossyScale));
        }
        public static Vector2 GetPivotWithPosition(this RectTransform me, Vector3 position)
        {
            Vector2 delta = position - me.GetPositionWithPivot(Vector2.zero);

            return new Vector2(delta.Project(me.right), delta.Project(me.up)) / (me.rect.size * me.lossyScale);
        }
        #endregion
    }
}
