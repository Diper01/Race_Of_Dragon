using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour {

    // Use this for initialization
   public GameObject areUSure;
    void Start()
    {
        areUSure.SetActive(false);
        DontDestroyOnLoad(this);
    }

    public void Stay()
    {

        areUSure.SetActive(false);
    }

    void Update()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
