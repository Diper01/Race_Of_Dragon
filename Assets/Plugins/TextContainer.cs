using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TextContainer : MonoBehaviour {

    public Text txtContainer;
    string[] Words;
    public bool showWords=false;
    void Start ()
    {

        DontDestroyOnLoad(this);
        txtContainer = GetComponent<Text>();
	}



    void Update ()
    {
        
        if(showWords)
        {
            txtContainer.text = "";
            for(int i=0;i<Words.Length;i++)
            {
                txtContainer.text += Words[i] + "\n";
            }
            showWords = false;
        }
        //UITextField[] array = FindObjectsOfType<UITextField>();  /// Поиск и добавление слов, закоментить если не нужно
        //foreach(UITextField item in array)
        //{

        //     TryWord(item.text); 
        //}
	}

    public void TryWord(string word)
    {
        double variable;

        if (double.TryParse(word, out variable)) return;
        if (Words == null)
        {
            Words = new string[1];
            Words[0] = word;
            return;
        }
        else
        {
            for(int i=0;i<Words.Length;i++)
            {
                if (Words[i] == word) return;
            }

            Array.Resize(ref Words, Words.Length + 1);
            Words[Words.Length - 1] = word;
        }
    }

}
