using UnityEngine;
using System.Collections;

public class Logo_Controller : MonoBehaviour
{

    public Sprite ENG, RU;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Localisation.LoadLanguage();
        if (Localisation.CurrentLanguage.ToString() == SystemLanguage.Russian.ToString())
            sr.sprite = RU;
        else
            sr.sprite = ENG;

        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
