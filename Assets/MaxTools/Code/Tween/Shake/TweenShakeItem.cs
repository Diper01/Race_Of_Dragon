using UnityEngine;
using System;
using System.Collections.Generic;

namespace MaxTools
{
    using MaxTools.Extensions;

    public static partial class TweenShake
    {
        static Dictionary<Transform, Item> shakeItems = new Dictionary<Transform, Item>();

        public static float breakRadius = 0.01f;

        public class Item
        {
            Executor.Item executorItem;
            public Transform transform;
            public float radius;
            public float factor01;
            public float delay;
            public float intensity;
            public float spreadAngle;
            public Space space;
            public TimeMode timeMode;
            public int dimension;
            public int priority;
            public Action breakAction;
            public Vector3? startPosition;
            public Type shakeType;
            public bool replaceItem;
            public bool isCompleted
            {
                get => executorItem.isBroken;
            }

            public void Start()
            {
                var startPosition = this.startPosition ?? transform.GetPosition3D(space);
                var shakePosition = ShakeTools.GetShakePositionByDimension(startPosition, radius, dimension);
                var moveDirection = (shakePosition - startPosition).normalized;

                Action runAction = () =>
                {
                    if (Tween.Position3D(transform, shakePosition, intensity, LerpType.Towards, space, timeMode))
                    {
                        if (transform.CheckIntervalNonstop(delay, false, timeMode))
                        {
                            if (shakeType == Type.Spring)
                            {
                                intensity *= factor01;
                            }

                            radius *= factor01;
                        }

                        if (radius >= breakRadius)
                        {
                            moveDirection = -moveDirection;

                            shakePosition = ShakeTools.GetNextShakePositionByDimension(
                                startPosition, radius, spreadAngle, moveDirection, dimension);
                        }
                        else
                            transform.SetPosition3D(startPosition, space);
                    }
                };

                Action finalWork = () =>
                {
                    breakAction?.Invoke();

                    shakeItems.Remove(transform);
                };

                Func<bool> condition = () => transform != null && radius >= breakRadius;

                if (shakeItems.TryGetValue(transform, out var oldItem))
                {
                    if (replaceItem && priority >= oldItem.priority)
                    {
                        executorItem = Executor.NewItem((transform, TweenType.Shake), condition, runAction, finalWork).RunReplace();

                        shakeItems[transform] = this;
                    }
                }
                else
                {
                    executorItem = Executor.NewItem((transform, TweenType.Shake), condition, runAction, finalWork).Run();

                    shakeItems[transform] = this;
                }
            }
            public void Break()
            {
                executorItem.Break();
            }
        }
    }
}
