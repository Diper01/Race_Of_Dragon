using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MaxTools
{
    [NiceInspector]
    [DisallowMultipleComponent]
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected bool dontDestroyOnLoad = true;

        static T m_Instance = null;

        protected void Awake()
        {
            if (m_Instance == null)
            {
                var trigger = instance;
            }
            else if (m_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        [EasyButton]
        public void SetSingletonName()
        {
#if UNITY_EDITOR
            Undo.RecordObject(gameObject, "Rename");
#endif
            gameObject.name = singletonName;
        }

        public static T instance
        {
            get
            {
#if MAXTOOLS_SINGLETON_DEBUG
                var objects = FindObjectsOfType<T>();

                for (int i = 1; i < objects.Length; ++i)
                {
                    Debug.LogError($"{singletonName}: More than one!", objects[i]);
                }
#endif
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<T>();

                    if (m_Instance == null)
                    {
                        m_Instance = new GameObject(singletonName).AddComponent<T>();
                    }

                    var singleton = m_Instance as Singleton<T>;

                    if (singleton.dontDestroyOnLoad)
                    {
                        singleton.transform.SetParent(null);

                        DontDestroyOnLoad(singleton.gameObject);
                    }
                }

                return m_Instance;
            }
        }

        static string singletonName => $"[Singleton] {typeof(T).Name}";
    }
}
