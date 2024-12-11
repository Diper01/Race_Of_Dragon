using UnityEngine;
using System;

namespace MaxTools.Extensions
{
    public static class EX_Object
    {
        public static GameObject FindNestedChild(this GameObject me, string childName)
        {
            return me.transform.FindNestedChild(childName)?.gameObject;
        }
        public static GameObject FindAncestor(this GameObject me, string ancestorName)
        {
            return me.transform.FindAncestor(ancestorName)?.gameObject;
        }

        #region Components
        public static T GetOrAddComponent<T>(this GameObject me) where T : Component
        {
            T component = me.GetComponent<T>();

            return component != null ? component : me.AddComponent<T>();
        }
        public static T GetOrAddComponent<T>(this Component me) where T : Component
        {
            T component = me.GetComponent<T>();

            return component != null ? component : me.gameObject.AddComponent<T>();
        }
        public static Component GetOrAddComponent(this GameObject me, Type type)
        {
            Component component = me.GetComponent(type);

            return component != null ? component : me.AddComponent(type);
        }
        public static Component GetOrAddComponent(this Component me, Type type)
        {
            Component component = me.GetComponent(type);

            return component != null ? component : me.gameObject.AddComponent(type);
        }

        public static void AddComponentIfAbsent<T>(this GameObject me) where T : Component
        {
            if (me.GetComponent<T>() == null)
            {
                me.AddComponent<T>();
            }
        }
        public static void AddComponentIfAbsent<T>(this Component me) where T : Component
        {
            if (me.GetComponent<T>() == null)
            {
                me.gameObject.AddComponent<T>();
            }
        }
        public static void AddComponentIfAbsent(this GameObject me, Type type)
        {
            if (me.GetComponent(type) == null)
            {
                me.AddComponent(type);
            }
        }
        public static void AddComponentIfAbsent(this Component me, Type type)
        {
            if (me.GetComponent(type) == null)
            {
                me.gameObject.AddComponent(type);
            }
        }

        public static bool TryGetComponent<T>(this GameObject me, out T result)
        {
            result = me.GetComponent<T>();

            return result != null;
        }
        public static bool TryGetComponent<T>(this Component me, out T result)
        {
            result = me.GetComponent<T>();

            return result != null;
        }
        public static bool TryGetComponent(this GameObject me, Type type, out Component result)
        {
            result = me.GetComponent(type);

            return result != null;
        }
        public static bool TryGetComponent(this Component me, Type type, out Component result)
        {
            result = me.GetComponent(type);

            return result != null;
        }

        public static bool HasComponent<T>(this GameObject me) where T : Component
        {
            return me.GetComponent<T>() != null;
        }
        public static bool HasComponent<T>(this Component me) where T : Component
        {
            return me.GetComponent<T>() != null;
        }
        public static bool HasComponent(this GameObject me, Type type)
        {
            return me.GetComponent(type) != null;
        }
        public static bool HasComponent(this Component me, Type type)
        {
            return me.GetComponent(type) != null;
        }
        #endregion

        #region Instantiate
        public static GameObject Instantiate(this GameObject me)
        {
            return UnityEngine.Object.Instantiate(me);
        }
        public static GameObject Instantiate(this GameObject me, Vector3 position)
        {
            return UnityEngine.Object.Instantiate(me, position, me.transform.rotation);
        }
        public static GameObject Instantiate(this GameObject me, Vector3 position, Transform parent)
        {
            return UnityEngine.Object.Instantiate(me, position, me.transform.rotation, parent);
        }

        public static T Instantiate<T>(this T me) where T : Component
        {
            return UnityEngine.Object.Instantiate(me);
        }
        public static T Instantiate<T>(this T me, Vector3 position) where T : Component
        {
            return UnityEngine.Object.Instantiate(me, position, me.transform.rotation);
        }
        public static T Instantiate<T>(this T me, Vector3 position, Transform parent) where T : Component
        {
            return UnityEngine.Object.Instantiate(me, position, me.transform.rotation, parent);
        }
        #endregion
    }
}
