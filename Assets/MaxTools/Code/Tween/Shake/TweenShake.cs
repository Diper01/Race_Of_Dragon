using UnityEngine;
using System;

namespace MaxTools
{
    public static partial class TweenShake
    {
        public enum Type
        {
            Simple,
            Spring
        }

        public static Item GetShakeItem(Transform key)
        {
            if (shakeItems.TryGetValue(key, out var item))
            {
                return item;
            }
            else
                return null;
        }

        public static Item Simple2D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 2;
            item.priority = 0;
            item.breakAction = breakAction;
            item.startPosition = null;
            item.shakeType = Type.Simple;
            item.replaceItem = false;
            return item;
        }
        public static Item Simple3D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 3;
            item.priority = 0;
            item.breakAction = breakAction;
            item.startPosition = null;
            item.shakeType = Type.Simple;
            item.replaceItem = false;
            return item;
        }
        public static Item Spring2D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 2;
            item.priority = 0;
            item.breakAction = breakAction;
            item.startPosition = null;
            item.shakeType = Type.Spring;
            item.replaceItem = false;
            return item;
        }
        public static Item Spring3D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 3;
            item.priority = 0;
            item.breakAction = breakAction;
            item.startPosition = null;
            item.shakeType = Type.Spring;
            item.replaceItem = false;
            return item;
        }

        public static Item SimpleReplace2D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 2;
            item.priority = priority;
            item.breakAction = breakAction;
            item.startPosition = startPosition;
            item.shakeType = Type.Simple;
            item.replaceItem = true;
            return item;
        }
        public static Item SimpleReplace3D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 3;
            item.priority = priority;
            item.breakAction = breakAction;
            item.startPosition = startPosition;
            item.shakeType = Type.Simple;
            item.replaceItem = true;
            return item;
        }
        public static Item SpringReplace2D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 2;
            item.priority = priority;
            item.breakAction = breakAction;
            item.startPosition = startPosition;
            item.shakeType = Type.Spring;
            item.replaceItem = true;
            return item;
        }
        public static Item SpringReplace3D(Transform me, float radius, float factor01, float delay, float intensity, float spreadAngle, Space space = Space.World, TimeMode timeMode = TimeMode.ScaledTime, int priority = 0, Vector3? startPosition = null, Action breakAction = null)
        {
            var item = new Item();
            item.transform = me;
            item.radius = radius;
            item.factor01 = factor01;
            item.delay = delay;
            item.intensity = intensity;
            item.spreadAngle = spreadAngle;
            item.space = space;
            item.timeMode = timeMode;
            item.dimension = 3;
            item.priority = priority;
            item.breakAction = breakAction;
            item.startPosition = startPosition;
            item.shakeType = Type.Spring;
            item.replaceItem = true;
            return item;
        }
    }
}
