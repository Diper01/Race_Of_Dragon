using UnityEngine;
using SBS.Core;

[AddComponentMenu("OnTheRun/UI/UIMark")]
public class UIMark : MonoBehaviour
{
    GameObject activeObj = null;
    GameObject inactiveObj = null;

    void Awake()
    {
        activeObj = transform.Find("active").gameObject;
        inactiveObj = transform.Find("inactive").gameObject;
    }

    public void SetMarkActive(bool active)
    {
        activeObj.SetActive(active);
        inactiveObj.SetActive(active);
    }
}