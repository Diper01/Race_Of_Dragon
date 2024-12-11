using UnityEngine;
using System.Collections;

public class LanguageController : MonoBehaviour {
    [SerializeField]
    public string[] languages = {"  ", "Arabic", "Chinese", "English", "French", "German", "Italian", "Polish", "Portuguese", "Spanish", "Turkish", "Ukrainian", "Russian", "Urdu" };

    public UITextField text;
	void Start ()
    {

	}
    void OnEnable()
    {
        SetTitleText();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ChangeLanguage()
    {
        
        int currLang = PlayerPrefs.GetInt("Language");
        
        if (currLang + 1 > 13||currLang==0) currLang = 1;
        else { currLang++; }
        PlayerPrefs.SetInt("Language", currLang);
        SetTitleText();
    }

    void SetTitleText()
    {
        int currLang = PlayerPrefs.GetInt("Language");
        currLang -= 2;
        if (currLang < 0) currLang = 13 - 1;
        if (PlayerPrefs.GetInt("Language") == 0) text.text = "Language";
        else
        {
            text.text = languages[currLang];
        }
    }
}
