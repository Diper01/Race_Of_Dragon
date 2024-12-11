using UnityEngine;
using System.Collections;

public class FreezeTextController : MonoBehaviour {

    UITextField text;
	// Use this for initialization
	void Start () {
        text = GetComponent<UITextField>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Localisation.CurrentLanguage == Languages.Polish)
        {
            text.text = " Mrożone przepływy przez:  ";
        }
	
	}
}
