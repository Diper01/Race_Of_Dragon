using UnityEngine;

[DefaultExecutionOrder(-123)]
public class Manager<T> : MonoBehaviour
    where T : Manager<T>
{
    private static T instance = null;

    protected void Awake()
    {
        //.Assert(null == instance);
        instance = (T)this;
    }

    protected void Start()
    { }

    protected void OnDestroy()
    {
        //.Assert(this == instance);
        instance = null;
    }

    public static T Get()
    {
        if (null == instance)
            return FindObjectOfType<T>();
        return instance;
    }
}
