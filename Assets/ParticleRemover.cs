using UnityEngine;
using System.Collections;

public class ParticleRemover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject item = GameObject.Find("Toon Bubble 02 PS");
        if (item != null) item.SetActive(false);
    }
}
