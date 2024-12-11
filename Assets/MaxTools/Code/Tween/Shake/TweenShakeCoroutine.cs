using UnityEngine;
using System;
using System.Collections;

namespace MaxTools
{
    public static partial class TweenShake
    {
        public static class Coroutine
        {
            public static IEnumerator Simple2D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
            {
                var item = TweenShake.Simple2D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator Simple3D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
            {
                var item = TweenShake.Simple3D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator Spring2D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
            {
                var item = TweenShake.Spring2D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator Spring3D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
            {
                var item = TweenShake.Spring3D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }

            public static IEnumerator SimpleReplace2D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
            {
                var item = TweenShake.SimpleReplace2D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, priority, startPosition, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator SimpleReplace3D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
            {
                var item = TweenShake.SimpleReplace3D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, priority, startPosition, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator SpringReplace2D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
            {
                var item = TweenShake.SpringReplace2D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, priority, startPosition, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
            public static IEnumerator SpringReplace3D(Transform me, float radius, float factor01, float factorDelay, float speedMove, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
            {
                var item = TweenShake.SpringReplace3D(me, radius, factor01, factorDelay, speedMove, spreadAngle, space, timeMode, priority, startPosition, breakAction);

                item.Start();

                yield return new WaitWhile(() => !item.isCompleted);
            }
        }
    }
}
