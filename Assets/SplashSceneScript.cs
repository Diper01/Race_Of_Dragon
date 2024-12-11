using UnityEngine;
using System.Collections;

public class SplashSceneScript : MonoBehaviour {


    void Awake()
    {
       // ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
    }

	void Start ()
    {
        StartCoroutine(Load());
	}
	
	// Update is called once per frame
	void Update () {


    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(1);
    }
}

